using Mvc.Mailer;

namespace BlueZero.Air.Mailers
{ 
    public interface IUserMailer
    {
        MvcMailMessage Welcome(string to, string childName, string carerName);
		MvcMailMessage PasswordReset(string to, string resetLink);
	}
}