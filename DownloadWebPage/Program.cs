using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Timers;
using System.Net.Mail;

namespace DownloadWebPage
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Timer t = new Timer(10000);
            t.Start();
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            while (true);   //messo per non far terminare il programam prima che sia scaduto il timer
            

            //SendMail();
            

            /*int start = myDataBuffer.IndexOf(Constant.UP_ARROW);
            int stop = myDataBuffer.IndexOf(Constant.END);
            var delta = stop - start;
            
            string usefullData = myDataBuffer.Substring(start, delta);

            start = usefullData.IndexOf(Constant.DATO);
            stop = usefullData.IndexOf(Constant.DATO_END);
            var dato1 = usefullData.Substring(start + Constant.DATO.Length, stop - start - Constant.DATO.Length);
                        
            start = usefullData.IndexOf(Constant.DATO2);
            usefullData = usefullData.Substring(start);

            start = usefullData.IndexOf(Constant.DATO2);
            stop = usefullData.IndexOf(Constant.DATO2_END);
            usefullData = usefullData.Substring(start);
            var dato2 = usefullData.Substring(start + Constant.DATO2.Length, stop - start - Constant.DATO2.Length);

            string path = System.IO.Directory.GetCurrentDirectory();
            StreamWriter sw = new StreamWriter(path + "/data.txt");
            sw.Write(myDataBuffer);
            sw.Close();  */           
        }

        static void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            PerformEverything();
            Console.WriteLine(DateTime.Now.ToString());
        }

        public static void PerformEverything()
        {
            var myDataBuffer = downloadWebPage();
            Data d = GetData(myDataBuffer);
            AppendToFile(d);
        }


        public static string downloadWebPage()
        {
            string addr = Constant.URL;
            
            WebClient client = new WebClient();
            //client.Headers.Add("user-agent", "Only a test!");
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Ssl3 |
                                                   (SecurityProtocolType)3072 |
                                                   (SecurityProtocolType)768;


            var myDataBuffer = client.DownloadString(addr);

            return myDataBuffer;
        }

        public static Data GetData(string data)
        {
            int start = data.IndexOf(Constant.UP_ARROW);
            int stop = data.IndexOf(Constant.END);
            var delta = stop - start;

            string usefullData = data.Substring(start, delta);

            start = usefullData.IndexOf(Constant.DATO);
            stop = usefullData.IndexOf(Constant.DATO_END);
            var dato1 = usefullData.Substring(start + Constant.DATO.Length, stop - start - Constant.DATO.Length);
            double d1 = Convert.ToDouble(dato1);

            start = usefullData.IndexOf(Constant.DATO2);
            usefullData = usefullData.Substring(start);

            start = usefullData.IndexOf(Constant.DATO2);
            stop = usefullData.IndexOf(Constant.DATO2_END);
            usefullData = usefullData.Substring(start);
            var dato2 = usefullData.Substring(start + Constant.DATO2.Length, stop - start - Constant.DATO2.Length);
            double d2 = Convert.ToDouble(dato2);

            start = usefullData.IndexOf(Constant.DATO3);
            usefullData = usefullData.Substring(start);

            start = usefullData.IndexOf(Constant.DATO3);
            stop = usefullData.IndexOf(Constant.DATO3_END);
            usefullData = usefullData.Substring(start);
            var dato3 = usefullData.Substring(start + Constant.DATO3.Length, stop - start - Constant.DATO3.Length);
            dato3 = dato3.Replace('%', ' ');
            dato3 = dato3.TrimEnd();
            double d3 = Convert.ToDouble(dato3);

            Data toReturn = new Data(d1, d2, d3);
            return toReturn;
        }
        
        public static void AppendToFile(Data d)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            StreamWriter sw = File.AppendText(path + "/output.txt");
            DateTime now = DateTime.Now;
            sw.WriteLine(now.ToString() + ", " +  
                         d.value.ToString().Replace(',', '.') + ", " + 
                         d.value2.ToString().Replace(',', '.') + ", " +
                         d.value3.ToString().Replace(',', '.') + '\n');
            sw.Close();
        }


        public static void SendMail()
        {
            // vedi anche "https://www.iprogrammatori.it/forum-programmazione/csharp/mail-con-t44491-15.html"
            // esempio invio mail + allegato con un account google,
            // ultimamente google non considera sicuro l'invio di mail senza certificazione,
            // per permettere l'invio, abilitare: Accesso app meno sicure
            // https://www.google.com/settings/security/lesssecureapps

            string to = "marco.truccolo@astrelgroup.com";
            string from = "test4noks@gmail.com";
            string smtp = "smtp.gmail.com";

            MailMessage message = new MailMessage(from, to);
            SmtpClient SmtpServer = new SmtpClient(smtp);

            

            string username = "test4noks@gmail.com";
            string password = "test4nok$";

            
            message.Subject = "subject";
            message.Body = "everything is ok";

            
            SmtpServer.UseDefaultCredentials = false;
            NetworkCredential credential = new NetworkCredential(username, password);
            SmtpServer.Port = 587;
            SmtpServer.Credentials = credential;
            SmtpServer.EnableSsl = true;


            try
            {
                SmtpServer.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            /*
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            string MailAccount = "test4noks@gmail.com"; // aggiungi il tuo indirizzo mail google
            string Password = "test4nok$"; // la tua password
            string Destinatario = "marco.truccolo@astrelgroup.com"; // a chi va
            mail.Subject = "Mail da C#"; // oggetto
            mail.Body = "In allegato file .txt, cordiali saluti"; // testo della mail

            mail.From = new MailAddress(MailAccount);
            mail.To.Add(Destinatario);
            // omettere se non serve allegato
            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("c:/allegato.txt"); // deve esistere
            //mail.Attachments.Add(attachment);
            // -------------------------------

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MailAccount, Password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
             * */
        }

    }
}
