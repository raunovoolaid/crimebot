using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.Mime;


namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        /*
        UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                 new ClientSecrets
                                 {
                                     ClientId = "74366732451-1l3qnjv73hd2k4u5tk2snfifimpn3jnn.apps.googleusercontent.com",
                                     ClientSecret = "RFj3UskvgarxmNsQzFci_w3i"
                                 },
                                 new[] { "https://mail.google.com/ email" },
                                 "Crime",
                                 CancellationToken.None,
                                 new FileDataStore("Analytics.Auth.Store")).Result;
        ImapClient ic = new ImapClient("imap.gmail.com", "crimecaptcha@gmail.com", credential.Token.AccessToken,
                                ImapClient.AuthMethods.SaslOAuth, 993, true);
        ic.SelectMailbox("INBOX");
        Console.WriteLine(ic.GetMessageCount());
        // Get the first *11* messages. 0 is the first message; 
        // and it also includes the 10th message, which is really the eleventh ;) 
        // MailMessage represents, well, a message in your mailbox 
        MailMessage[] mm = ic.GetMessages(0, 10);
        foreach (MailMessage m in mm) 
        { 
        Console.WriteLine(m.Subject + " " + m.Date.ToString());
        }
        // Probably wiser to use a using statement 
        ic.Dispose();
        */

        Stopwatch watch = new Stopwatch();
        async Task PutTaskDelay()
        {
            Random rnd = new Random();
            int delay = rnd.Next(204, 230);
            await Task.Delay(delay);
        }

        public Form1()
        {
            //createTimer();
            

            InitializeComponent();
            label1.Text = "Joogimeistri level:";
            label2.Text = "Käsitöö level:";
            label3.Text = "Keemiku level:";
            geckoWebBrowser1.Navigate("http://www.crime.ee");
            SendIt();
            //test
        }
        public static class Globals
        {
            public static int rekordKlikke = 0;
            public static int kasClickida = 0;
            public static string rekordAeg = "0";
        }

        public void SendIt()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { "https://www.googleapis.com/auth/gmail.modify" },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Crimecaptcha",
            });

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            // List labels.
            IList<Google.Apis.Gmail.v1.Data.Label> labels = request.Execute().Labels;
            Console.WriteLine("Labels:");
            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    Console.WriteLine("{0}", labelItem.Name);
                }
            }
            else
            {
                Console.WriteLine("No labels found.");
            }
            Console.Read();

            /*
            var msg = new MailMessage
            {
                Subject = "Your Subject",
                Body = "Hello, World, from Gmail API!",
                From = new MailAddress("crimecaptcha@gmail.com")
            };
            msg.To.Add(new MailAddress("crimecaptcha@gmail.com"));
            var bytes = File.ReadAllBytes(path);
            
            string mimeType = System.Net.Mime.GetMimeMapping(path);
            var path = "C:\Users\heiko.parmas\Source\Repos\crimebot2\WindowsFormsApp1\test.txt";
            
            Attachment attachment = new Attachment(path);bytes, mimeType, Path.GetFileName(path), true);
            msg.Attachments.Add(attachment);

            var gmail = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Crimecaptcha",
            });

            var result = gmail.Users.Messages.Send(new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = Base64UrlEncode(msg.ToString())
            }, "me").Execute();
            Console.WriteLine("Message ID {0} sent.", result.Id);
            */
        }

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
        public void make ()
        {
            label11.Text = watch.Elapsed.TotalSeconds.ToString() + "s";
            if(double.Parse(watch.Elapsed.TotalSeconds.ToString()) > double.Parse(Globals.rekordAeg))
            {
                Globals.rekordAeg = watch.Elapsed.TotalSeconds.ToString();
                label13.Text = Globals.rekordAeg + "s";
            }
            Gecko.GeckoElement btn = (Gecko.GeckoElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("nupuke420")[0];
            Gecko.GeckoHtmlElement button = (Gecko.GeckoHtmlElement)btn;
            button.Click();
            return;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            
            
        label9.Text = "Edukaid klikke järjest: 0";
            //Stopwatch stopper = new Stopwatch();
            watch.Reset();
            watch.Start();
            Globals.kasClickida = 1;
            int counter = 0;
            do
            {
                make();
                counter++;
                if (Globals.rekordKlikke < counter)
                {
                    Globals.rekordKlikke = counter;
                    label10.Text = "Rekord: "+counter;
                }
                label9.Text = "Edukaid klikke järjest: " + counter;
                await PutTaskDelay();
                getStats();

            }
            while (captchaCheck() && Globals.kasClickida == 1);
        }
        private void geckoWebBrowser1_DocumentCompleted(object sender, EventArgs e)
        {
            // Here you can add the coding to perform after document loaded
            Console.WriteLine("finishedloading");
        }
        public bool captchaCheck()
        {
            Gecko.GeckoHtmlElement captcha = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("captcha_container");
            Gecko.GeckoNodeCollection veaSisud = (Gecko.GeckoNodeCollection)geckoWebBrowser1.DomDocument.GetElementsByClassName("message info");
            int veaCount = 0;
            Gecko.GeckoHtmlElement vigaSisu = null;
            foreach (Gecko.GeckoNode veaSisu in veaSisud)
            {
                veaCount++;
                if(veaCount > 1)
                {
                   vigaSisu = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("message info")[1];
                    break;
                }
            }

            if (captcha == null)
            {
                
                if (vigaSisu == null)
                {
                    Console.WriteLine("caphcha not found and viga is null");
                    return true;
                }
                else
                {
                    if (vigaSisu.InnerHtml.Contains("tellida"))
                    {
                        Console.WriteLine("tellida aineid");
                        Globals.kasClickida = 0;
                        System.Media.SystemSounds.Asterisk.Play();
                        watch.Stop();
                        return false;
                    }
                    else
                    {
                        if(vigaSisu.InnerHtml.Contains("valmistada")){
                            Console.WriteLine("valmistada mahla");
                            Globals.kasClickida = 0;
                            System.Media.SystemSounds.Asterisk.Play();
                            watch.Stop();
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("caphcha hidden and viga is OK");
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (captcha.GetAttribute("style").Contains("display: none"))
                {
                        if (vigaSisu == null)
                        {
                            Console.WriteLine("caphcha hidden and viga is null");
                            return true;
                        }
                        else
                        {
                            if (vigaSisu.InnerHtml.Contains("tellida"))
                            {
                                Console.WriteLine("caphcha hidden and tellida aineid");
                                Globals.kasClickida = 0;
                                System.Media.SystemSounds.Asterisk.Play();
                                watch.Stop();
                                return false;
                            }
                            else
                            {
                                if (vigaSisu.InnerHtml.Contains("valmistada"))
                                {
                                    Console.WriteLine("caphcha hidden and valmistada mahla");
                                    Globals.kasClickida = 0;
                                    System.Media.SystemSounds.Asterisk.Play();
                                    watch.Stop();
                                    return false;
                                }
                                else
                                {
                                    Console.WriteLine("caphcha hidden and viga is OK");
                                    return true;
                                }
                            }
                        }
                }
                else //(!captcha.GetAttribute("style").Contains("display: none"))
                {
                    Console.WriteLine("captcha detected");
                    Globals.kasClickida = 0;
                    System.Media.SystemSounds.Asterisk.Play();
                    watch.Stop();
                    return false;
                }
            }
            /* igaks juhuks saved
                }
                Console.WriteLine("captcha detected");
                Globals.kasClickida = 0;
                System.Media.SystemSounds.Asterisk.Play();
                MessageBox.Show(vigaSisu. + "FIX IT");
                return false;*/
        }
        public void getStats()
        {
            Gecko.GeckoHtmlElement kasitoolvl = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("s_crafting");
            Gecko.GeckoHtmlElement keemiklvl = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("s_chemistry");
            Gecko.GeckoHtmlElement joogilvl = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("s_barkeeping");
            label5.Text = joogilvl.InnerHtml;
            label6.Text = kasitoolvl.InnerHtml;
            label7.Text = keemiklvl.InnerHtml;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Globals.kasClickida = 0;
            watch.Stop();
        }


    }
}
