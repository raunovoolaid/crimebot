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
