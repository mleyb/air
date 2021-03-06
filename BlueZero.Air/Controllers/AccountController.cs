﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BlueZero.Air.Data;
using BlueZero.Air.Data.Models;
using BlueZero.Air.Filters;
using BlueZero.Air.Mailers;
using BlueZero.Air.Models;
using BlueZero.Air.Support;
using DotNetOpenAuth.AspNet;
using log4net;
using Microsoft.Web.WebPages.OAuth;
using NMALib;
using WebMatrix.WebData;

namespace BlueZero.Air.Controllers
{
    [Authorize]    
    public class AccountController : Controller
    {
        private ILog _log;
        private IDataContext _db;
        private IUserMailer _mailer;

        public AccountController(ILog log, IDataContext db, IUserMailer mailer)
        {
            _log = log;
            _db = db;
            _mailer = mailer;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.EmailAddress, model.Password, persistCookie: model.RememberMe))
            {
                _log.InfoFormat("Login for user '{0}' accepted.", model.EmailAddress);

                return RedirectToLocal(returnUrl);
            }
            else
            {
                _log.WarnFormat("Login for user '{0}' with password '{1}' rejected. Unknown username or incorrect password.", model.EmailAddress, model.Password);

                // notify
                NMANotifier.SendNotification("Login Failure", String.Format("Failed login attempt by user '{0}'.", model.EmailAddress), NMANotificationPriority.Normal);

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }
        }

        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.UserExists(model.EmailAddress))
                {
                    string token = WebSecurity.GeneratePasswordResetToken(model.EmailAddress);
                    string resetLink = "<a href='" + Url.Action("ResetPassword", "Account", new { username = model.EmailAddress, token = token }, "http") + "'>here</a>";                    

                    // send password reset email
                    _mailer.PasswordReset(model.EmailAddress, resetLink).Send();

                    ViewBag.Message = "Password reset email has been sent. Please follow the instructions in the message.";
                    return View("PasswordResetResult");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(ForgotPasswordLinkModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.UserExists(model.Username))
                {
                    return View(new ResetPasswordModel { Username = model.Username, Token = model.Token });
                }
                else
                {
                    _log.WarnFormat("Attempt to reset password but unrecognised username '{0}' was supplied.", model.Username);
                }
            }

            // If we got this far, something failed
            return new HttpNotFoundResult();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.UserExists(model.Username) && WebSecurity.ResetPassword(model.Token, model.ConfirmPassword))
                {
                    ViewBag.Message = "Your password has been changed successfully.";
                    return View("PasswordResetResult");
                }
                else
                {
                    return new HttpNotFoundResult();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                _log.Warn("Attempt to register but no registration code was supplied.");

                return RedirectToAction("Login", "Account");
            }           

            Child child = _db.Children.SingleOrDefault(c => c.RegistrationCode == id);
            if (child != null)
            {
                Carer carer = _db.Carers.Include(c => c.Children).Where(c => c.Children.Any(c2 => c2.Id == child.Id)).SingleOrDefault();
                if (carer != null)
                {
                    string childName = String.Format("{0} {1}", child.Forename, child.Surname);                    

                    return View(new RegisterModel { ChildName = childName, CarerName = carer.Name, RegistrationCode = id });
                }
                else
                {
                    _log.ErrorFormat("Failed to find Carer entity for Child with registration code '{0}'.", id);

                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                _log.ErrorFormat("Failed to find Child entity with registration code '{0}'.", id);

                return RedirectToAction("Login", "Account");
            }                               
        }            

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [CaptchaValidator]
        public ActionResult Register(RegisterModel model, bool isCaptchaValid, string captchaErrorMessage)
        {
            if (!isCaptchaValid)
            {
                ModelState.AddModelError("", captchaErrorMessage);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    Child child = _db.Children.SingleOrDefault(c => c.RegistrationCode == model.RegistrationCode);
                    if (child != null)
                    {
                        Parent parent = null;

                        // create local account
                        WebSecurity.CreateUserAndAccount(model.EmailAddress, model.Password);

                        // create Parent record and link it to the local account
                        parent = new Parent();
                        parent.UserProfile = _db.UserProfiles.Find(WebSecurity.GetUserId(model.EmailAddress));
                        parent.Children = new List<Child> { child };

                        _db.Parents.Add(parent);

                        // registration code used up
                        child.RegistrationCode = null;

                        _db.SaveChanges();

                        Roles.AddUserToRole(model.EmailAddress, RoleNames.Parent);

                        WebSecurity.Login(model.EmailAddress, model.Password);
                    }

                    // notify
                    NMANotifier.SendNotification("New Registration", String.Format("User '{0}' registered.", model.EmailAddress), NMANotificationPriority.Normal);

                    // send welcome email
                    _mailer.Welcome(model.EmailAddress, model.ChildName, model.CarerName).Send();

                    return RedirectToAction("Index", "Parent");
                }
                catch (MembershipCreateUserException ex)
                {
                    ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
                }
                catch (Exception ex)
                {
                    _log.ErrorFormat("Exception during registration. {0}", ex.ToString());
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterExisting(RegisterExistingModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                //try
                //{
                //    Child child = _db.Children.SingleOrDefault(c => c.RegistrationCode == model.RegistrationCode);
                //    if (child != null)
                //    {
                //        Parent parent = null;

                //        // create local account
                //        WebSecurity.CreateUserAndAccount(model.EmailAddress, model.Password);

                //        // create Parent record and link it to the local account
                //        parent = new Parent();
                //        parent.UserProfile = _db.UserProfiles.Find(WebSecurity.GetUserId(model.EmailAddress));
                //        parent.Children = new List<Child> { child };

                //        _db.Parents.Add(parent);

                //        // registration code used up
                //        child.RegistrationCode = null;

                //        _db.SaveChanges();

                //        Roles.AddUserToRole(model.EmailAddress, RoleNames.Parent);

                //        WebSecurity.Login(model.EmailAddress, model.Password);
                //    }

                //    // notify
                //    NMANotifier.SendNotification("New Registration", String.Format("User '{0}' registered.", model.EmailAddress), NMANotificationPriority.Normal);

                //    return RedirectToAction("Index", "Parent");
                //}
                //catch (MembershipCreateUserException ex)
                //{
                //    ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
                //}
                //catch (Exception ex)
                //{
                //    _log.ErrorFormat("Exception during registration. {0}", ex.ToString());
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));

                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageAccountModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));

            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);

                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }
            
            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is already logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Check if user already exists
                UserProfile user = _db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                if (user == null)
                {
                    // Insert name into the profile table
                    _db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                    _db.SaveChanges();

                    OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);

                    Roles.AddUserToRole(model.UserName, RoleNames.Parent);

                    OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);

            List<ExternalLogin> externalLogins = new List<ExternalLogin>();

            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
