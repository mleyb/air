using System.Net.Mail;
using Mvc.Mailer;

namespace BlueZero.Air.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage Welcome(string to, string childName, string carerName)
		{
            ViewBag.Username = to;
            ViewBag.ChildName = childName;
            ViewBag.CarerName = carerName;

            MvcMailMessage message = Populate(x =>
			{
                x.Subject = "Kiddycare Diary - Welcome";
				x.ViewName = "Welcome";
                x.To.Add(to);
                x.From = new MailAddress(Constants.ContactEmailAddress, Constants.ApplicationName);
			});

            return message;
		}
 
		public virtual MvcMailMessage PasswordReset(string to, string resetLink)
		{
            ViewBag.Username = to;
            ViewBag.ResetLink = resetLink;

            MvcMailMessage message = Populate(x =>
			{
				x.Subject = "Kiddycare Diary - Password Reset";
				x.ViewName = "PasswordReset";
                x.To.Add(to);
                x.From = new MailAddress(Constants.ContactEmailAddress, Constants.ApplicationName);
			});

            return message;
		}
 	}
}