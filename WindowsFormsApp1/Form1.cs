
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
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

            //test
        }
        public static class Globals
        {
            public static int rekordKlikke = 0;
            public static int kasClickida = 0;
            public static string rekordAeg = "0";
            public static int ostetud = 0;
            public static int klikkideCount = 0;
            public static string otsas = "";
        }

        public async void make(bool jarjest)
        {
            do {
                if (jarjest== false)
                {
                    label9.Text = "Edukaid klikke järjest: 0";
                    Globals.klikkideCount = 0;
                }
                do
                {
                    Gecko.GeckoHtmlElement captcha = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("captcha_container");
                    Gecko.GeckoNodeCollection veaSisud = (Gecko.GeckoNodeCollection)geckoWebBrowser1.DomDocument.GetElementsByClassName("message info");
                    int veaCount = 0;
                    Gecko.GeckoHtmlElement vigaInfo = null;
                    Gecko.GeckoHtmlElement vigaError = null;
                    foreach (Gecko.GeckoNode veaSisu in veaSisud)
                    {
                        veaCount++;
                        if (veaCount > 1)
                        {
                            vigaError = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("message error")[0];
                            vigaInfo = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("message info")[1];
                            break;
                        }
                    }

                    if (captcha == null)
                    {

                        if (vigaInfo == null)
                        {
                            Console.WriteLine("caphcha not found and viga is null");
                            continue;
                        }
                        else
                        {
                            if (vigaError.InnerHtml.Contains("otsas"))
                            {
                                Console.WriteLine(vigaError.InnerHtml.Substring(47, (vigaError.InnerHtml.Length - 63)));
                                Globals.kasClickida = 0;
                                System.Media.SystemSounds.Asterisk.Play();
                                watch.Stop();
                                Globals.otsas = vigaError.InnerHtml.Substring(47, (vigaError.InnerHtml.Length - 63));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("caphcha hidden and viga is OK");
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (captcha.GetAttribute("style").Contains("display: none"))
                        {
                            if (vigaInfo == null)
                            {
                                Console.WriteLine("caphcha hidden and viga is null");
                            }
                            else
                            if (vigaError.InnerHtml.Contains("otsas"))
                            {
                                Console.WriteLine(vigaError.InnerHtml.Substring(47, (vigaError.InnerHtml.Length - 63)));
                                Globals.kasClickida = 0;
                                System.Media.SystemSounds.Asterisk.Play();
                                watch.Stop();
                                Globals.otsas = vigaError.InnerHtml.Substring(47, (vigaError.InnerHtml.Length - 63));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("caphcha hidden and viga is OK");
                            }
                        }
                        else //(!captcha.GetAttribute("style").Contains("display: none"))
                        {
                            Console.WriteLine("captcha detected");
                            Globals.kasClickida = 0;
                            System.Media.SystemSounds.Asterisk.Play();
                            watch.Stop();
                            break;
                        }
                    }
                    label11.Text = watch.Elapsed.TotalSeconds.ToString() + "s";
                    if (double.Parse(watch.Elapsed.TotalSeconds.ToString()) > double.Parse(Globals.rekordAeg))
                    {
                        Globals.rekordAeg = watch.Elapsed.TotalSeconds.ToString();
                        label13.Text = Globals.rekordAeg + "s";
                    }
                    Gecko.GeckoElement btn = (Gecko.GeckoElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("nupuke420")[0];
                    Gecko.GeckoHtmlElement button = (Gecko.GeckoHtmlElement)btn;
                    button.Click();
                    Globals.klikkideCount++;
                    if (Globals.rekordKlikke < Globals.klikkideCount)
                    {
                        Globals.rekordKlikke = Globals.klikkideCount;
                        label10.Text = "Rekord: " + Globals.klikkideCount;
                    }
                    label9.Text = "Edukaid klikke järjest: " + Globals.klikkideCount;
                    await PutTaskDelay();
                    getStats();
                }
                while (Globals.kasClickida == 1);

                if (Globals.otsas != "")
                {
                    buyMaterials(Globals.otsas);
                }
                jarjest = true;
                Console.WriteLine(Globals.kasClickida + " " + jarjest.ToString());
            }
            
            while (Globals.kasClickida == 1) ;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            //Stopwatch stopper = new Stopwatch();
            watch.Reset();
            watch.Start();
            Globals.kasClickida = 1;
            make(false);
        }
        private async void continuation(string Material)
        {
            
            if (Material == "Plastikutükid" || Material == "Rauatükid" || Material == "Vasetükid" || Material == "Tinatükid" || Material == "Värvitopsid" || Material == "Titaanitükid" || Material == "Savitükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=house&tegevus=ovens");
                await Task.Delay(1500);
                Globals.kasClickida = 1;
            }
            else if(Material == "Puidutükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=house&tegevus=desk");
                await Task.Delay(1500);
                Globals.kasClickida = 1;
            }
            else if (Material == "Riiderullid" || Material == "Niidirullid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=house&tegevus=chair");
                await Task.Delay(1500);
                Globals.kasClickida = 1;
            }
            else
            {
                Globals.kasClickida = 0;
            }
        }

        private async void buyMaterials(string Material)
        {
            do
            {     
            await Task.Delay(1500);
            if(Material == "Nahatükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=1#x");
                await Task.Delay(1500);
            }
            else if (Material == "Niidirullid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=2#x");
                await Task.Delay(1500);
            }
            else if(Material == "Puidutükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=3#x");
                await Task.Delay(1500);
            }
            else if(Material == "Tinatükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=4#x");
                await Task.Delay(1500);
            }
            else if(Material == "Rauatükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=5#x");
                await Task.Delay(1500);
            }
            else if (Material == "Riiderullid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=6#x");
                await Task.Delay(1500);
            }
            else if(Material == "Vasetükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=7#x");
                await Task.Delay(1500);
            }
            else if(Material == "Värvitopsid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=8#x");
                await Task.Delay(1500);
            }
            else if(Material == "Savitükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=9#x");
                await Task.Delay(1500);
            }
            else if(Material == "Plastikutükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=10#x");
                await Task.Delay(1500);
            }
            else if(Material == "Titaanitükid")
            {
                geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=11#x");
                await Task.Delay(1500);
            }
            Gecko.GeckoHtmlElement osta = (Gecko.GeckoHtmlElement)geckoWebBrowser1.Document.GetElementsByName("purchcrafitem")[0];
            osta.Click();
            await Task.Delay(1500);
            geckoWebBrowser1.Navigate("http://valge.crime.ee/index.php?asukoht=house&tegevus=materialsstorage");
            await Task.Delay(3800);
            Gecko.GeckoHtmlElement tosta = geckoWebBrowser1.Document.GetElementsByName("ktookappi")[0];
            tosta.Click();
            await Task.Delay(500);
            Globals.ostetud++;
            }
            while (Globals.ostetud < 6);
            Globals.ostetud = 0;
            Globals.otsas = "";
            continuation(Material);
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
