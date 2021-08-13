using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Emailcontato.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult EnviaEmail()
        {
            string emailDestinatario = Request.Form["txtEmail"];
            SendMail(emailDestinatario);
            return View("Index");
        }

        //método para enviar email

        public bool SendMail(string email)
        {
            try
            {

                string nomeForm = Request.Form["txtNome"];
                string emailForm = Request.Form["txtEmail"];

                //Instância de classe de mensagem
                MailMessage _mailMessage = new MailMessage();

                //remetente ( email de quem envia )
                _mailMessage.From = new MailAddress("igorandradeferreira@gmail.com");

                //Destinatário seta no método abaixo
                //Constrói o MailMessage
                _mailMessage.CC.Add("igor_andrade_2@hotmail.com"); //email para o qual queremos enviar a mensagem
                _mailMessage.Subject = "Relatório Contato TESTE"; //Título do Email
                _mailMessage.IsBodyHtml = true; //Tipo de arquivo que será enviado o email ( HTML )
                _mailMessage.Body = nomeForm+ "<br/>"+ emailForm; //conteúdo HTML do email
               
                
                //configuração com porta
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));// se for o hotmail, tem que procurar qual a porta e o smtp padrao do hotmail por exemplo

                //configuração sem porta
                //SmtpClient _smtpClient = new SmtpClient(UtilResource.ConfigSmtp);
                //Credencial para envio por SMTP seguro ( Quando o servidor exige autenticação )
                _smtpClient.UseDefaultCredentials = true;
                _smtpClient.Credentials = new NetworkCredential("igorandradeferreira@gmail.com", "Eujafui33");
                _smtpClient.EnableSsl = true;
                _smtpClient.Send(_mailMessage);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}