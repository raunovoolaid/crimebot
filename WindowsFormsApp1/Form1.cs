
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
        public async Task PutTaskDelay()
        {
            Random rnd = new Random();
            int delay = rnd.Next(204, 230);
            await Task.Delay(delay);
        }
        Dictionary<string, string> mahlad = new Dictionary<string, string>()
        {
            {"Apelsinimahl","Apelsinid" },
            {"Greibimahl","Greibid" },
            {"Õunamahl","Õunad" },
            {"Jõhvikamahl","Jõhvikad" },
            {"Maasikamahl","Maasikad" },
            {"Pohlamahl","Pohlad" },
            {"Mustsõstramahl","Mustsõstrad" },
            {"Kirsimahl","Kirsid" },
            {"Ploomimahl","Ploomid" },
            {"Hele viinamarjamahl","Heledad viinamarjad" },
            {"Punasõstramahl","Punasõstrad" },
            {"Pirnimahl","Pirnid" },
            {"Sidrunimahl","Sidrunid" },
            {"Vaarikamahl","Vaarikad" },
            {"Tikrimahl","Tikrid" },
            {"Tume viinamarjamahl","Tumedad viinamarjad" },

        };
        Dictionary<string, int> tellimine = new Dictionary<string, int>() {
            {"Apelsinid", 9},
            {"Greibid", 11},
            {"Heledad viinamarjad", 15},
            {"Jõhvikad", 12},
            {"Kadakamarjad", 17},
            {"Karamell", 32},
            {"Kirsid", 4},
            {"Kohvioad", 31},
            {"Kokalehed", 18},
            {"Kummel", 22},
            {"Maasikad", 6},
            {"Mais", 27},
            {"Mesi", 33},
            {"Mustsõstrad", 14},
            {"Nisu", 24},
            {"Nõges", 23},
            {"Oder", 25},
            {"Pipar", 28},
            {"Pirnid", 2},
            {"Ploomid", 3},
            {"Pohlad", 8},
            {"Punasõstrad", 13},
            {"Pärm", 34},
            {"Rukis", 26},
            {"Sidrunid", 10},
            {"Sinine agaav", 21},
            {"Suhkur", 35},
            {"Teepuulehed", 19},
            {"Teepuuõied", 20},
            {"Tikrid", 7},
            {"Toorpiim", 30},
            {"Tumedad viinamarjad", 16},
            {"Vaarikad", 5},
            {"Vanilje", 36},
            {"Vesi", 29},
            {"Õunad", 1}
        };
        public int getDict(string vaartus)
        {
            int tulemus = 0;
            if (tellimine.TryGetValue(vaartus, out tulemus))
            {
                return tulemus;
            }
            else
            {
                return 0;
            }
        }
        public string getDict2(string vaartus)
        {
            string tulemus = "";
            if (mahlad.TryGetValue(vaartus, out tulemus))
            {
                return tulemus;
            }
            else
            {
                return "";
            }
        }
        public Form1()
        {
            //createTimer();
            InitializeComponent();
            label1.Text = "Joogimeistri level:";
            label2.Text = "Käsitöö level:";
            label3.Text = "Keemiku level:";
            //Gecko.GeckoPreferences.[geckoWebBrowser1.Default]("extensions.blocklist.enabled") = False;
            geckoWebBrowser1.Navigate("http://www.crime.ee");
            do
            {
                Task.Delay(100);
                Console.WriteLine("test");
            }
            while (geckoWebBrowser1.IsBusy || geckoWebBrowser1.IsAjaxBusy);

            //test
        }


        public static class Globals
        {
            public static int rekordKlikke = 0;
            public static int kasClickida = 0;
            public static string rekordAeg = "0";
            public static int klikkideCount = 0;
            public static string otsas = "";


        }
        public async Task<bool> makeD()
        {
            await navwaitLoad("http://valge.crime.ee/?asukoht=tanav#drugmachine");
            await make();
            await navwaitLoad("http://valge.crime.ee/?asukoht=tanav#streets");
            if ((Gecko.GeckoElementCollection)geckoWebBrowser1.Document.GetElementsByTagName("input") != null)
            {
                Gecko.GeckoElementCollection buttons = (Gecko.GeckoElementCollection)geckoWebBrowser1.Document.GetElementsByTagName("input");
                Gecko.GeckoHtmlElement sellButton = null;
                Gecko.GeckoHtmlElement buyButton = null;
                foreach (Gecko.GeckoHtmlElement button in buttons)
                {
                    if (button.GetAttribute("name") == "selldrugs")
                    {
                        sellButton = button;
                        await Task.Delay(500);
                        sellButton.Click();
                        await Task.Delay(500);
                        if (buyButton != null)
                        {
                            buyButton.Click();
                            break;
                        }
                        Console.WriteLine("FOUND sell button ");
                        Console.WriteLine(sellButton);
                    }
                    else if (button.GetAttribute("name") == "purcdrugs")
                    {
                        buyButton = button;
                        if (sellButton != null)
                        {
                            buyButton.Click();
                            break;
                        }
                        else
                        {
                            continue;
                        }

                    }
                    if (sellButton != null && buyButton != null)
                    {
                        break;
                    }

                }
                if (sellButton == null || buyButton == null)
                {
                    Console.WriteLine("didnt find buy or sell button");
                    Globals.kasClickida = 0;
                    return false;
                }
                else if (sellButton != null && buyButton != null)
                {
                    return true;
                }
                else
                {
                    Globals.kasClickida = 0;
                    return false;
                }
            }
            else
            {
                Globals.kasClickida = 0;
                return false;
            }
        }
        public async Task makeController()
        {
            label9.Text = "Edukaid klikke järjest: 0";
            Globals.klikkideCount = 0;
            do
            {

                await PutTaskDelay();
                watch.Start();
                if (listBox1.GetItemText(listBox1.SelectedItem) == "KEEMIK")
                {
                    bool actionD = await (makeD());
                    if (actionD)
                    {
                        Console.WriteLine("maed soem");
                        getStats();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("sum tin wong");
                    }
                }
                else
                {
                    bool action = await (make());
                    if (!action && Globals.otsas != "")
                    {
                        Console.WriteLine("tried to get more stuff");
                        await buyMaterials(Globals.otsas);

                    }
                    else
                    {
                        Console.WriteLine("i am making");
                        getStats();
                        continue;
                    }
                }

            }
            while (Globals.kasClickida == 1);
            Console.WriteLine("i stopped making ");
        }
        private async Task<bool> make()
        {


            if ((Gecko.GeckoElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("nupuke420")[0] != null)
            {
                Gecko.GeckoElement btn = (Gecko.GeckoElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("nupuke420")[0];
                Gecko.GeckoHtmlElement button = (Gecko.GeckoHtmlElement)btn;
                button.Click();
                do
                {
                    await Task.Delay(10);
                }
                while (geckoWebBrowser1.IsBusy || geckoWebBrowser1.IsAjaxBusy);
                Globals.klikkideCount++;
                Gecko.GeckoHtmlElement captcha = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("captcha_container");
                if ((Gecko.GeckoNodeCollection)geckoWebBrowser1.DomDocument.GetElementsByClassName("message notice") != null)
                {
                    Gecko.GeckoNodeCollection noticed = (Gecko.GeckoNodeCollection)geckoWebBrowser1.DomDocument.GetElementsByClassName("message notice");
                    Gecko.GeckoHtmlElement possibleLevelup = null;
                    int noticeCount = 0;
                    Console.WriteLine((Gecko.GeckoNodeCollection)geckoWebBrowser1.DomDocument.GetElementsByClassName("message notice"));
                    foreach (Gecko.GeckoNode notice in noticed)
                    {
                        noticeCount++;
                    }
                    if (noticeCount >= 1)
                    {
                        possibleLevelup = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("message notice")[0];
                        if (possibleLevelup.InnerHtml.Contains("nüüd"))
                        {
                            Console.WriteLine("Levelup detected");
                        }
                    }
                    else
                    {
                        Console.WriteLine(possibleLevelup);
                    }
                }

                if (captcha != null && !captcha.GetAttribute("style").Contains("display: none"))

                {

                    Console.WriteLine("captcha detected");
                    Globals.kasClickida = 0;
                    System.Media.SystemSounds.Asterisk.Play();
                    watch.Stop();
                    return false;
                }

                else
                {

                    if ((Gecko.GeckoNodeCollection)geckoWebBrowser1.DomDocument.GetElementsByClassName("message info") != null)
                    {

                        int veaCount = 0;
                        int errorCount = 0;
                        Gecko.GeckoNodeCollection veaSisud = (Gecko.GeckoNodeCollection)geckoWebBrowser1.DomDocument.GetElementsByClassName("message info");
                        Gecko.GeckoHtmlElement vigaInfo = null;
                        Gecko.GeckoHtmlElement vigaError = null;
                        foreach (Gecko.GeckoNode veaSisu in veaSisud)
                        {
                            veaCount++;
                        }
                        if (veaCount >= 1)
                        {
                            vigaInfo = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("message info")[0];
                            if ((Gecko.GeckoNodeCollection)geckoWebBrowser1.Document.GetElementsByClassName("message error") != null)
                            {
                                Gecko.GeckoNodeCollection errorid = (Gecko.GeckoNodeCollection)geckoWebBrowser1.Document.GetElementsByClassName("message error");
                                foreach (Gecko.GeckoNode errorSisu in errorid)
                                {
                                    errorCount++;
                                }
                                if (errorCount > 0)
                                {
                                    vigaError = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("message error")[0];
                                }

                            }
                        }
                        if (vigaInfo == null && vigaError != null || vigaError == null)

                        {
                            Console.WriteLine("viga is null");
                            return true;
                        }
                        else
                        {
                            if (vigaError.InnerHtml.Contains("otsas"))
                            {
                                Console.WriteLine(vigaError.InnerHtml.Substring(47, (vigaError.InnerHtml.Length - 63)));
                                Globals.kasClickida = 0;
                                System.Media.SystemSounds.Asterisk.Play();
                                watch.Stop();
                                Console.WriteLine("material otsas");
                                Globals.otsas = vigaError.InnerHtml.Substring(47, (vigaError.InnerHtml.Length - 63));
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("viga is OK");
                                return true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("viga is null");
                        return true;
                    }

                }


            }
            else
            {
                Globals.kasClickida = 0;
                return false;
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            watch.Reset();
            watch.Start();
            Globals.kasClickida = 1;
            await makeController();
        }
        private async Task errorPage()
        {
            await Task.Delay(50);
            if (geckoWebBrowser1.Text.Contains("OOTA 1 SEKUND ENNE LEHE REFRESHIMIST!!!"))
            {
                Console.WriteLine("Found the fucking error");
                geckoWebBrowser1.Reload();
                await Task.Delay(50);
                do
                {
                    await Task.Delay(100);
                }
                while (geckoWebBrowser1.IsBusy || geckoWebBrowser1.IsAjaxBusy);

            }
        }
        private async Task navwaitLoad(string URL)
        {
            do
            {
                await Task.Delay(100);
            }
            while (geckoWebBrowser1.IsBusy || geckoWebBrowser1.IsAjaxBusy);
            Console.WriteLine(geckoWebBrowser1.Url);
            if (geckoWebBrowser1.Url.ToString().Contains("world2"))
            {
                Console.WriteLine(geckoWebBrowser1.Url);
                URL = URL.Replace("valge", "world2");
                Console.WriteLine(URL);
            }
            geckoWebBrowser1.Navigate(URL);
            await errorPage();
            do
            {
                await Task.Delay(200);
            }
            while (geckoWebBrowser1.IsBusy || geckoWebBrowser1.IsAjaxBusy);
        }
        private async Task continuation(string Material)
        {
            Globals.otsas = "";
            if (Material == "Plastikutükid" || Material == "Rauatükid" || Material == "Vasetükid" || Material == "Tinatükid" || Material == "Värvitopsid" || Material == "Titaanitükid" || Material == "Savitükid")
            {

                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=house&tegevus=ovens");
                Console.WriteLine("goto ovens");
                Globals.kasClickida = 1;
            }
            else if (Material == "Puidutükid")
            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=house&tegevus=desk");

                Globals.kasClickida = 1;
                Console.WriteLine("goto desk");
            }
            else if (Material == "Riiderullid" || Material == "Niidirullid" || Material == "Nahatükid")
            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=house&tegevus=chair");

                Console.WriteLine("goto chair");
                Globals.kasClickida = 1;
            }
            else
            {
                Globals.kasClickida = 0;
            }
        }

        private async Task buyMaterials(string Material)
        {
            await Task.Delay(100);
            if (String.Equals("Nahatükid", Material))
            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=1#x");
            }
            else if (String.Equals("Niidirullid", Material))
            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=2#x");
            }
            else if (String.Equals("Puidutükid", Material))
            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=3#x");
            }
            else if (String.Equals("Tinatükid", Material))

            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=4#x");
            }
            else if (String.Equals("Rauatükid", Material))
            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=5#x");
            }
            else if (String.Equals("Riiderullid", Material))

            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=6#x");
            }
            else if (String.Equals("Vasetükid", Material))
            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=7#x");
            }
            else if (String.Equals("Värvitopsid", Material))

            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=8#x");
            }
            else if (String.Equals("Savitükid", Material))

            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=9#x");
            }
            else if (String.Equals("Plastikutükid", Material))

            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=10#x");
            }
            else if (String.Equals("Titaanitükid", Material))

            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=slumm&paik=4&lett=2&ese=11#x");
            }
            else if (getDict(Material) != 0)
            {
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=tavern#phone");
                return;
            }
            else
            {
                Console.WriteLine("õiges kohas raisk + " + Material);
                return;
            }
            Console.WriteLine(Globals.kasClickida);

            Gecko.GeckoHtmlElement osta = (Gecko.GeckoHtmlElement)geckoWebBrowser1.Document.GetElementsByName("purchcrafitem")[0];
            osta.Click();

            await navwaitLoad("http://valge.crime.ee/index.php?asukoht=house&tegevus=materialsstorage");
            Gecko.GeckoHtmlElement tosta = geckoWebBrowser1.Document.GetElementsByName("ktookappi")[0];
            tosta.Click();
            await continuation(Material);
            return;
        }
        public void getStats()
        {
            label11.Text = watch.Elapsed.TotalSeconds.ToString() + "s";
            if (double.Parse(watch.Elapsed.TotalSeconds.ToString()) > double.Parse(Globals.rekordAeg))
            {
                Globals.rekordAeg = watch.Elapsed.TotalSeconds.ToString();
                label13.Text = Globals.rekordAeg + "s";
            }
            if (Globals.rekordKlikke < Globals.klikkideCount)
            {
                Globals.rekordKlikke = Globals.klikkideCount;
                label10.Text = "Rekord: " + Globals.klikkideCount;
            }
            label9.Text = "Edukaid klikke järjest: " + Globals.klikkideCount;
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
