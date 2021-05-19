using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Timers;
using System.Net.Mail;
using System.Net.Http;
using Newtonsoft.Json;


namespace DownloadWebPage
{
    class Program
    {
        static Entry[] values;

        //uso questo per convertire da stringa a int della enum "tassi"
        //si può fare anche senza, ma così è più veloce
        //vedi anche https://stackoverflow.com/questions/16100/convert-a-string-to-an-enum-in-c-sharp/38711#38711
        public static Dictionary<String, int> d = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            Test.TestGraph();
            return;
            
            InitDictionary();
            InitValues();
            //PrintValues();
            FastChange fc = new FastChange();
            InitFastChange(fc);
            //fc.print();

            //Entry e = new Entry("EUR_AUD");
            //e.PrintEntry();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            string ID = GetID();
            //GetQuote(ID, "EUR_AUD", e);
            //Stream(ID);
            TestDelayMultiple(ID);

            System.Threading.Thread th = new System.Threading.Thread(() => Stream(ID));
            th.Start();

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            /*while(true)
            {
                //System.Threading.Thread.Sleep(10000);
                System.Threading.Thread.Sleep(500);
                Console.Clear();
                PrintValues();
            }*/

            //e.PrintEntry();
            Console.Read();
        }

        static void InitFastChange(FastChange fc)
        {
            for(int i = 0; i < (int)Constant.tassi.LAST_VALUE; i++)
            {
                string[] entry = ((Constant.tassi)i).ToString().Split('_');
                fc.addString(entry[0]);
                fc.addString(entry[1]);
            }
        }

        static void InitDictionary()
        {
            Array array = Enum.GetValues(typeof(Constant.tassi));
            string[] names = Enum.GetNames(typeof(Constant.tassi));

            int[] values = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
                values[i] = (int)array.GetValue(i);

            for (int i = 0; i < (int)Constant.tassi.LAST_VALUE; i++)
                d.Add(names[i], values[i]);                
        }

        static void InitValues()
        {
            /*values = new Entry[(int)Constant.tassi.LAST_VALUE];
            for (int i = 0; i < values.Length; i++)
                values[i] = new Entry(((Constant.tassi)i).ToString());*/
            values = new Entry[d.Count];

            var keys = d.Keys;
            int i = 0;
            foreach (string s in keys)
            {
                values[i] = new Entry(s);
                values[i].ID = i;
                i++;
            }
        }

        static void PrintValues()
        {
            for (int i = 0; i < values.Length; i++)
                values[i].PrintEntry();
        }

        static string GetID()
        {
            string addr = "https://api-fxpractice.oanda.com/v3/accounts";
            
            //copiato in parte da qui
            //https://github.com/oanda/csharp-exchange-rates/blob/master/ExchangeRatesAPI/ExchangeRatesAPI.cs

            var request = (HttpWebRequest)WebRequest.Create(addr);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + Constant.TOKEN);

            try
            {
                //https://docs.microsoft.com/it-it/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class

                HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse();
                var stream = webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string responseFromServer = reader.ReadToEnd();
                //Console.WriteLine(responseFromServer);

                dynamic obj = JsonConvert.DeserializeObject(responseFromServer);
                //Console.WriteLine("-----------");
                //Console.WriteLine(obj.accounts[0].id);
                webresponse.Close();

                string ID = obj.accounts[0].id;
                return ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "";
        }

        static void GetQuote(string ID, string item, Entry e)
        {
            /*info
             * prezzo vendita -> prices.bid.price
             * prezzo corrente -> prices.ask.price
             * prezzo a cui ho acquistato -> non trovato
             */
            //(double, double) toReturn = (-1, -1);
            string addr = "https://api-fxpractice.oanda.com/v3/accounts/" + ID + "/pricing?instruments=" + item;

            //copiato in parte da qui
            //https://github.com/oanda/csharp-exchange-rates/blob/master/ExchangeRatesAPI/ExchangeRatesAPI.cs

            var request = (HttpWebRequest)WebRequest.Create(addr);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + Constant.TOKEN);

            try
            {
                //https://docs.microsoft.com/it-it/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class

                HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse();
                var stream = webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);

                dynamic obj = JsonConvert.DeserializeObject(responseFromServer);
                //Console.WriteLine("-----------");
                //Console.WriteLine(obj.prices[0].bids[0].price);
                //Console.WriteLine(obj.prices[0].asks[0].price);
                webresponse.Close();

                e.acquisto = obj.prices[0].bids[0].price;
                e.vendita = obj.prices[0].asks[0].price;

                //toReturn.Item1 = obj.prices[0].bids[0].price;
                //toReturn.Item2 = obj.prices[0].asks[0].price;
                //string value = "";// obj.accounts[0].id;
                //return toReturn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //return toReturn;
        }

        public static void Stream(string ID)
        {
            //string addr = "https://stream-fxpractice.oanda.com//v3/accounts/" + ID + "/pricing/stream?instruments=EUR_AUD";

            var keys = d.Keys;

            string end = "";
            foreach(string s in keys)
                end += s + "%2C";
                     
            end = end.Remove(end.Length - 3);  //tolgo ultimi 3 caratteri

            string addr = "https://stream-fxpractice.oanda.com//v3/accounts/" + ID + "/pricing/stream?instruments=" + end;
            
            //copiato in parte da qui
            //https://github.com/oanda/csharp-exchange-rates/blob/master/ExchangeRatesAPI/ExchangeRatesAPI.cs

            var request = (HttpWebRequest)WebRequest.Create(addr);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + Constant.TOKEN);
            request.Headers.Add("Accept-Datetime-Format", "RFC3339");

            try
            {
                //https://docs.microsoft.com/it-it/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class

                HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse();
                var stream = webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                //string responseFromServer = reader.ReadToEnd();
                //Console.WriteLine(responseFromServer);

                while(true)
                {
                    string message = reader.ReadLine();
                    //Console.WriteLine(reader.ReadLine());
                    Console.WriteLine(message);

                    //TODO: mi sa che sta roba è molto lenta, vedi se si riesce a velocizzare
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    //https://www.html.it/pag/71674/gestire-il-formato-json/
                    dynamic obj = JsonConvert.DeserializeObject(message);
                    sw.Stop();
                    //Console.WriteLine(sw.ElapsedMilliseconds);

                    if(obj.type == "PRICE")
                    {
                        string index = obj.instrument;
                        int i = d[index];
                        values[i].name = index;
                        values[i].acquisto = obj.bids[0].price;
                        values[i].vendita = obj.asks[0].price;
                        values[i].timeStamp = obj.time;

                        System.DateTime now = DateTime.Now;
                        values[i].timeStampLocal = now;
                        
                        values[i].NumberOfUpdate++;
                        values[i].updateDelta();
                    }
                    /*catch
                    {
                        Console.WriteLine("IMPOSSIBILE CONVERITE!!!!!!!!!!");
                    }*/
                    


                    //e.acquisto = obj.prices[0].bids[0].price;
                    //e.vendita = obj.prices[0].asks[0].price;
                }

                //dynamic obj = JsonConvert.DeserializeObject(responseFromServer);
                //Console.WriteLine("-----------");
                //Console.WriteLine(obj.accounts[0].id);
                webresponse.Close();

                //string ID = obj.accounts[0].id;
                //return ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //return "";
        }

        public static StreamReader OpenStream(string ID)
        {
            var keys = d.Keys;

            string end = "";
            foreach (string s in keys)
                end += s + "%2C";

            end = end.Remove(end.Length - 3);  //tolgo ultimi 3 caratteri

            string addr = "https://stream-fxpractice.oanda.com//v3/accounts/" + ID + "/pricing/stream?instruments=" + end;

            //copiato in parte da qui
            //https://github.com/oanda/csharp-exchange-rates/blob/master/ExchangeRatesAPI/ExchangeRatesAPI.cs

            var request = (HttpWebRequest)WebRequest.Create(addr);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + Constant.TOKEN);
            request.Headers.Add("Accept-Datetime-Format", "RFC3339");

            
            //https://docs.microsoft.com/it-it/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class

            HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse();
            var stream = webresponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader;
        }

        public static void TestDelaySingle(string ID)
        {
            /*
             * controlla ogni quanti secondi viene aggiornato il pacchetto di una certa informazione richiesta
             */

            try
            {
                //https://docs.microsoft.com/it-it/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class

                StreamReader reader = OpenStream(ID);

                DateTime currentTime = new DateTime(1900, 1, 1);
                DateTime previousTime = new DateTime(1900, 1, 1);

                /*
                 * per scelta personale e arbitraria
                 * delayArray[0] -> contatore dei delay compresi tra 0ms e 100ms
                 * delayArray[1] -> contatore dei delay compresi tra 100ms e 200ms
                 * ...
                 */
                int[] delayArray = new int[100];
                for (int i = 0; i < delayArray.Length; i++)
                    delayArray[i] = 0;
                
                while (true)
                {
                    string message = reader.ReadLine();
                    //Console.WriteLine(message);

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    dynamic obj = JsonConvert.DeserializeObject(message);
                    
                    if (obj.type == "PRICE")
                    {
                        string index = obj.instrument;
                        int i = d[index];
                        //values[i].timeStamp = obj.time;

                        previousTime = currentTime;
                        currentTime = obj.time;

                        if(currentTime.Year != 1900 & previousTime.Year != 1900)
                        {
                            TimeSpan span = currentTime - previousTime;
                            double delta = span.TotalSeconds;
                            int indice = (int)(delta);
                            delayArray[indice]++;
                        }

                        Console.Clear();
                        for (int j = 0; j < 10; j++)
                            Console.WriteLine(delayArray[j]);                       
                    }                    
                }  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

         public static void TestDelayMultiple(string ID)
        {
            /*
             * controlla ogni quanti secondi viene aggiornato il pacchetto di una certa informazione richiesta
             */

            try
            {
                //https://docs.microsoft.com/it-it/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class

                StreamReader reader = OpenStream(ID);

                DateTime[] currentTime = new DateTime[100];
                DateTime[] previousTime = new DateTime[100];
                
                for(int i = 0; i < 100; i++)
                {
                   currentTime[i] = new DateTime(1900, 1, 1);
                   previousTime[i] = new DateTime(1900, 1, 1);
                }
                
                /*
                 * per scelta personale e arbitraria
                 * delayArray[0] -> contatore dei delay compresi tra 0ms e 100ms
                 * delayArray[1] -> contatore dei delay compresi tra 100ms e 200ms
                 * ...
                 */
                
                //nota: delayArray[x][y] con x = identificativo della valuta e y il contatore che voglio aggiornare
                int[][] delayArray = new int[100][];

                for (int i = 0; i < delayArray.Length; i++)
                    delayArray[i] = new int[100];

                for (int i = 0; i < delayArray.Length; i++)
                    for(int j = 0; j < delayArray.Length; j++)
                        delayArray[i][j] = 0;

                while (true)
                {
                    string message = reader.ReadLine();
                    //Console.WriteLine(message);

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    dynamic obj = JsonConvert.DeserializeObject(message);
                    
                    if (obj.type == "PRICE")
                    {
                        string index = obj.instrument;
                        int i = d[index];
                        
                        previousTime[i] = currentTime[i];
                        currentTime[i] = obj.time;

                        if(currentTime[i].Year != 1900 & previousTime[i].Year != 1900)
                        {
                            TimeSpan span = currentTime[i] - previousTime[i];
                            double delta = span.TotalSeconds;
                            int indice = (int)(delta);
                            //delayArray[indice]++;

                            if(indice < 100 && i < 5)
                                delayArray[i][indice]++;
                        }

                        Console.Clear();
                        for (int k = 0; k < 20; k++)
                        {
                            for (int h = 0; h < 10; h++)
                                Console.Write("{0} ", delayArray[k][h]);
                            Console.WriteLine();
                        }
                    }                    
                }  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        
        
        
        

        static void HttpRequestGet()
        {
            string addr = "https://api-fxpractice.oanda.com/v3/accounts";
            string token = "54e3bee1a8c10b14f85d9fd1348d5902-e80cd3a388c2d5c5d39d2609f3804654";

            //copiato in parte da qui
            //https://github.com/oanda/csharp-exchange-rates/blob/master/ExchangeRatesAPI/ExchangeRatesAPI.cs

            var request = (HttpWebRequest)WebRequest.Create(addr);
            //string credentialHeader = String.Format("Bearer {0}", token);
            request.Method = "GET";
            //request.ContentType = "application/json";
            //request.UserAgent = "OANDAExchangeRates.C#/0.01";
            request.Headers.Add("Authorization", "Bearer " + token);

            try
            {
                //https://docs.microsoft.com/it-it/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class

                HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse();
                var stream = webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);

                dynamic obj = JsonConvert.DeserializeObject(responseFromServer);
                Console.WriteLine("-----------");
                Console.WriteLine(obj.accounts[0].id);

                webresponse.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
