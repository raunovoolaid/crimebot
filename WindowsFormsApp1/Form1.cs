using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        }
        public void make ()
        {
            Gecko.GeckoHtmlElement Btn = (Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("nupuke420_aerator");
            Btn.Click();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            
            Gecko.GeckoHtmlElement Moves = ((Gecko.GeckoHtmlElement)geckoWebBrowser1.DomDocument.GetElementById("moves_left"));
            String Move = Moves.InnerHtml;
            Console.WriteLine(Moves.ToString());
            Console.WriteLine(Move);
            
            
            for (int i = 0; i < int.Parse(Move); i++)
            {
                make();
                await PutTaskDelay();
                Console.WriteLine(Move);
            }

        }
        

        private void geckoWebBrowser1_Click(object sender, EventArgs e)
        {

        }
        async Task PutTaskDelay()
        {
            await Task.Delay(600);
        }
    }
}
