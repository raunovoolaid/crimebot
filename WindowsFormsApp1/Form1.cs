
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
            int delay = rnd.Next(220,230);
            await Task.Delay(delay);
        }
        Dictionary<int, string> jook = new Dictionary<int, string>()
        {
            {1, "http://valge.crime.ee/index.php?asukoht=tavern#kitchen" },
            {2,"http://valge.crime.ee/index.php?asukoht=tavern#cellar" },
            {3,"http://valge.crime.ee/index.php?asukoht=tavern#aerator" },
            {4,"http://valge.crime.ee/index.php?asukoht=tavern#distiller" },
            {5,"http://valge.crime.ee/index.php?asukoht=tavern#cider" },
            {6,"http://valge.crime.ee/index.php?asukoht=tavern#blender" }
        };
        Dictionary<int, int> kusjook = new Dictionary<int, int>()
        {
{1,1},
{2,2},
{3,3},
{4,4},
{5,2},
{6,4},
{7,1},
{8,5},
{9,1},
{10,2},
{11,1},
{12,4},
{13,1},
{14,2},
{15,6},
{16,5},
{17,1},
{18,4},
{19,6},
{20,2},
{21,6},
{22,5},
{23,1},
{24,2},
{25,3},
{26,4},
{27,1},
{28,2},
{29,6},
{30,2},
{31,6},
{32,2},
{33,1},
{34,2},
{35,6},
{36,4},
{37,6},
{38,5},
{39,3},
{40,2},
{41,1},
{42,4},
{43,6},
{44,4},
{45,3},
{46,5},
{47,6},
{48,4},
{49,1},
{50,4},
{51,6},
{52,5},
{53,1},
{54,2},
{55,6},
{56,4},
{57,6},
{58,4},
{59,3},
{60,4},
{61,6},
{62,4},
{63,3},
{64,4},
{65,6},
{66,4},
{67,3},
{68,4},
{69,3},
{70,2},
{71,3},
{72,2},
{73,6},
{74,4},
{75,6},
{76,2},
{77,3},
{78,2},
{79,6},
{80,4},
{81,6},
{82,2},
{83,3},
{84,2},
{85,3},
{86,4},
{87,6},
{88,2},
{89,3},
{90,4},
{91,6},
{92,2},
{93,3},
{94,2},
{95,6},
{96,2},
{97,3},
{98,2},
{99,6},
{100,3},
{101,3},
{102,3},
{105,2},
{110,2}
            };
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
            {"Tume viinamarjamahl","Tumedad viinamarjad" }

        };
        Dictionary<string, int> mahlapress = new Dictionary<string, int>()
        {
            {"Apelsinimahl",1 },
            {"Greibimahl",2 },
            {"Hele viinamarjamahl",3 },
             {"Jõhvikamahl",4 },
             {"Kirsimahl",5 },
             {"Maasikamahl",6 },
            {"Mustsõstramahl",7 },
             {"Pirnimahl",8 },
             {"Ploomimahl",9 },
            {"Pohlamahl",10 },
            {"Punasõstramahl",11 },
            {"Sidrunimahl",12 },
            {"Tikrimahl",13 },
            {"Tume viinamarjamahl",14 },
            {"Vaarikamahl",15 },
            {"Õunamahl",16 }

        };
        Dictionary<string, int> tellimine = new Dictionary<string, int>() {
            {"Apelsinid", 1},
            {"Greibid", 2},
            {"Heledad viinamarjad", 3},
            {"Jõhvikad", 4},
            {"Kadakamarjad", 5},
            {"Karamell", 6},
            {"Kirsid", 7},
            {"Kohvioad", 8},
            {"Kokalehed", 9},
            {"Kummel", 10},
            {"Maasikad",11},
            {"Mais", 12},
            {"Mesi", 13},
            {"Mustsõstrad", 14},
            {"Nisu", 15},
            {"Nõges", 16},
            {"Oder", 17},
            {"Pipar", 18},
            {"Pirnid", 19},
            {"Ploomid", 20},
            {"Pohlad", 21},
            {"Punasõstrad", 22},
            {"Pärm", 23},
            {"Rukis", 24},
            {"Sidrunid", 25},
            {"Sinine agaav", 26},
            {"Suhkur", 27},
            {"Teepuulehed", 28},
            {"Teepuuõied", 29},
            {"Tikrid", 30},
            {"Toorpiim", 31},
            {"Tumedad viinamarjad", 32},
            {"Vaarikad", 33},
            {"Vanilje", 34},
            {"Vesi", 35},
            {"Õunad", 36}
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
        public int getDict3(string vaartus)
        {
            int tulemus = 0;
            if (mahlapress.TryGetValue(vaartus, out tulemus))
            {
                return tulemus;
            }
            else
            {
                return 0;
            }
        }
        public int getDict4(int vaartus)
        {
            int tulemus = 0;
            if (kusjook.TryGetValue(vaartus, out tulemus))
            {
                return tulemus;
            }
            else
            {
                return 0;
            }
        }
        public string getDict5(int vaartus)
        {
            string tulemus = "";
            if (jook.TryGetValue(vaartus, out tulemus))
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
            public static int kaptcha = 0;


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
            Globals.kaptcha = 0;
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
                        if(listBox1.GetItemText(listBox1.SelectedItem) == "JOOGIMEISTER")
                        {
                            int hetkelevel = int.Parse(label5.Text);
                            if ( getDict4(hetkelevel) == 4)
                            {
                                Console.WriteLine("tried to get more stuff");
                                
                                await buyMaterials(Globals.otsas);
                                if(Globals.kaptcha !=0)
                                {
                                    break;
                                }
                            }
                            else if(getDict5(getDict4(hetkelevel)) == geckoWebBrowser1.Url.ToString().Replace("world2","valge")){
                                Console.WriteLine("tried to get more stuff & olen õiges kohas");
                                if (action)
                                {
                                    continue;
                                }
                                else
                                {
                                    if(Globals.kaptcha != 0)
                                    {
                                        return;
                                    
                                    }
                                    else
                                    {
                                        await buyMaterials(Globals.otsas);
                                    }
                                    
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine(getDict5(getDict4(hetkelevel)));
                                Console.WriteLine(geckoWebBrowser1.Url.ToString());
                                var kusjook = getDict4(hetkelevel);
                                await navwaitLoad(getDict5(kusjook));
                                Console.WriteLine(kusjook);
                                Console.WriteLine(hetkelevel);
                                Globals.kasClickida = 1;
                                int indexfinder = 0;
                                Gecko.GeckoNodeCollection drinks_list = geckoWebBrowser1.DomDocument.GetElementById("drinks_list").ChildNodes;
                                foreach (Gecko.GeckoNode drink in drinks_list)
                                {
                                    indexfinder++;
                                    if (drink.TextContent.Contains(hetkelevel.ToString()))
                                    {
                                        break;
                                    }

                                }
                                Console.WriteLine(indexfinder);
                                do
                                {
                                    await Task.Delay(20);
                                }
                                while ((Gecko.DOM.GeckoSelectElement)geckoWebBrowser1.Document.GetElementById("drinks_list")==null);
                                var selectElement2 = (Gecko.DOM.GeckoSelectElement)geckoWebBrowser1.Document.GetElementById("drinks_list");
                                selectElement2.SelectedIndex = indexfinder / 2 - 1;
                            }
                        }
                        else
                        {
                            Console.WriteLine("tried to get more stuff");
                            await buyMaterials(Globals.otsas);
                        }
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

            do
            {
                await Task.Delay(10);
            }
            while ((Gecko.GeckoElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("nupuke420")[0] == null);
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
                    Globals.kaptcha = 1;
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
            await Task.Delay(20);
            if (geckoWebBrowser1.Text.Contains("OOTA 1 SEKUND ENNE LEHE REFRESHIMIST!!!"))
            {
                Console.WriteLine("Found the fucking error");
                geckoWebBrowser1.Reload();
                await Task.Delay(50);
                do
                {
                    await Task.Delay(10);
                }
                while (geckoWebBrowser1.IsBusy || geckoWebBrowser1.IsAjaxBusy);

            }
        }
        private async Task navwaitLoad(string URL)
        {
            await errorPage();
            do
            {
                await Task.Delay(100);
                await errorPage();
            }
            while (geckoWebBrowser1.IsBusy || geckoWebBrowser1.IsAjaxBusy);
            if (geckoWebBrowser1.Url.ToString().Contains("world2"))
            {
                URL = URL.Replace("valge", "world2");
            }
            geckoWebBrowser1.Navigate(URL);
            await errorPage();
            do
            {
                await Task.Delay(100);
            }
            while (geckoWebBrowser1.IsBusy || geckoWebBrowser1.IsAjaxBusy);
            await errorPage();
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
                var whereURL = geckoWebBrowser1.Url;
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=tavern#phone");
                do
                {
                    await Task.Delay(20);
                }
                while (geckoWebBrowser1.Document.GetElementById("mat")==null);
                Gecko.GeckoElement chooser = geckoWebBrowser1.Document.GetElementById("mat");
                Gecko.GeckoElement quantity = geckoWebBrowser1.Document.GetElementById("quant");
                Gecko.GeckoHtmlElement ostaBtn = geckoWebBrowser1.Document.GetElementsByName("order_mat")[0];
                var document = geckoWebBrowser1.Document;
                var selectElement = (Gecko.DOM.GeckoSelectElement)document.GetElementById("mat");
                selectElement.SelectedIndex = getDict(Material);
                Console.WriteLine(getDict(Material));
                quantity.SetAttribute("value", "100");
                ostaBtn.Click();
                await navwaitLoad(whereURL.ToString());
                await Task.Delay(50);
                Globals.kasClickida = 1;
                Console.WriteLine("going to " + whereURL.ToString());
                return;
            }
            else if (Material.ToString().Contains("mahl"))
            {
                var whereURL = geckoWebBrowser1.Url;
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=tavern#phone");
                do
                {
                    await Task.Delay(20);
                }
                while (geckoWebBrowser1.Document.GetElementById("quant") == null);
                Gecko.GeckoElement quantity = geckoWebBrowser1.Document.GetElementById("quant");
                Gecko.GeckoHtmlElement ostaBtn = geckoWebBrowser1.Document.GetElementsByName("order_mat")[0];
                var document = geckoWebBrowser1.Document;
                var selectElement = (Gecko.DOM.GeckoSelectElement)document.GetElementById("mat");
                selectElement.SelectedIndex = getDict(getDict2(Material));
                quantity.SetAttribute("value", "100");
                ostaBtn.Click();
                await navwaitLoad("http://valge.crime.ee/index.php?asukoht=tavern#juicer");
                do
                {
                    await Task.Delay(20);
                }
                while (geckoWebBrowser1.Document.GetElementById("mpress_a") == null);
                Gecko.GeckoElement quantity2 = geckoWebBrowser1.Document.GetElementById("mpress_a");
                Gecko.GeckoHtmlElement makeBtn = geckoWebBrowser1.Document.GetElementsByName("make_juice")[0];
                var selectElement2 = (Gecko.DOM.GeckoSelectElement)document.GetElementById("mpress_n");
                selectElement2.SelectedIndex = getDict3(Material);
                quantity2.SetAttribute("value", "100");
                makeBtn.Click();
                await navwaitLoad(whereURL.ToString());
                await Task.Delay(50);
                Globals.kasClickida = 1;
                Console.WriteLine("going to " + whereURL.ToString());
                return;
            }
            else
            {
                Console.WriteLine("sum ting wong");
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
