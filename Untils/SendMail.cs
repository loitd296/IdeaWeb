using System.Net;
using System.Net.Mail;
namespace IdeaWeb.Untils{
    
    public class Send{
        public void SendEmail(string email, string subject, string body)
        {  
            var SendMail = new MailMessage();
            SendMail.From = new MailAddress("jackscuong@gmail.com");
            SendMail.To.Add(new MailAddress(email));
            Console.WriteLine(email);
            SendMail.Subject =subject ;
            SendMail.Body = body;
            SmtpClient stmp = new SmtpClient("smtp.gmail.com");
            stmp.EnableSsl = true;
            stmp.Port = 587;
            stmp.DeliveryMethod = SmtpDeliveryMethod.Network;
            stmp.Credentials= new NetworkCredential("jackscuong@gmail.com","adffpqmecimzympc");
            stmp.Send(SendMail);
        }
    }
}