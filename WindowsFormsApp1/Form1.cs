using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            geckoWebBrowser1.Navigate("http://www.crime.ee");
            //test

        }
        public void make ()
        {
            
            Gecko.GeckoHtmlElement Btn = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("nupuke420_aerator");
            Btn.Click();
        }
        private void button1_Click(object sender, EventArgs e)
        {   
             if (captchaCheck())
             {
                 click();
             }
             else
                 System.Media.SystemSounds.Asterisk.Play();
            
        }
        void click()
        {
            Gecko.GeckoElement btn = (Gecko.GeckoElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("nupuke420")[0];
            Gecko.GeckoHtmlElement button = (Gecko.GeckoHtmlElement)btn;
            button.Click();
            return;
        }
        void wait(int x)
        {
            DateTime t = DateTime.Now;
            DateTime tf = DateTime.Now.AddMilliseconds(x);

            while (t < tf)
            {
                t = DateTime.Now;
            }
        }

        public bool captchaCheck()
        {
            Gecko.GeckoHtmlElement captcha = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("captcha_container");
            if (captcha == null)
            {
                Console.WriteLine("caphcha not found");
                return false;
            }
            else
            {
                if (captcha.GetAttribute("style").Contains("display: none"))
                {
                    Console.WriteLine("captcha peidus");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < 1; i++)
            {
                button1_Click(sender, e);
                wait(300);
            }
        }
    }
}
