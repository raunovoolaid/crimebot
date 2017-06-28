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
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        async Task PutTaskDelay()
        {
            await Task.Delay(600);
        }

        public Form1()
        {
            InitializeComponent();
            geckoWebBrowser1.Navigate("http://www.crime.ee");
            //test
        }
        public void make ()
        {
            
            Console.WriteLine("test");
            Gecko.GeckoElement btn = (Gecko.GeckoElement)geckoWebBrowser1.DomDocument.GetElementsByClassName("nupuke420")[0];
            Gecko.GeckoHtmlElement button = (Gecko.GeckoHtmlElement)btn;
            button.Click();
            return;
        }
        private void button1_Click(object sender, EventArgs e)
        {   
             if (captchaCheck())
             {
                 make();
             }
             else
                 System.Media.SystemSounds.Asterisk.Play();
            
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

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < 3; i++)
            {
                make();
                await PutTaskDelay();
                
            }
        }
    }
}
