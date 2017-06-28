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
            Random rnd = new Random();
            int delay = rnd.Next(204, 230);
            await Task.Delay(delay);
        }

        public Form1()
        {
            InitializeComponent();
            geckoWebBrowser1.Navigate("http://www.crime.ee");
            //test
        }
        public void make ()
        {
            
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
                    Console.WriteLine("captcha detected");
                    checkBox1.Checked = false;
                    System.Media.SystemSounds.Asterisk.Play();
                    return false;
                }
            }
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            do
            {
                make();
                await PutTaskDelay();

            }
            while (checkBox1.Checked && captchaCheck());
        }
    }
}
