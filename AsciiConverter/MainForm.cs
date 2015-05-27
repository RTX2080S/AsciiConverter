using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AsciiConverter
{
    delegate void invokeTextBox(string txt);
     
    public partial class mainForm : Form
    {

        private int counter = 0;

        public void printText(string txt)
        {
            if (printBox.InvokeRequired)
            {
                invokeTextBox d = new invokeTextBox(printText);
                this.Invoke(d, txt);
            }
            else
            {
                printBox.Text += txt;
                counter += 1;
                printBox.Select(printBox.Text.Length, 0);
                printBox.ScrollToCaret();
            }
        }

        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            char c = (char)numericUpDown1.Value;
            ansBox1.Text = c.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            char c = (char)textBox1.Text[0];
            ansBox2.Text = ((int)c).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printBox.Clear();
            t = new Thread(fillPrintBox);
            t.Priority = ThreadPriority.BelowNormal;
            t.Start();                         
        }

        public Thread t;

        public void fillPrintBox()
        {
            int a = (int)printerStart.Value;
            int b = (int)printerEnd.Value;
            if (a <= b)
            {
                string cacheStr = "";
                const int cacheSize = 127;
                int flushCounter = 0;
                for (int i = a; i <= b; i++)
                {
                    char c = (char)i;
                    cacheStr += i.ToString() + ": " + c.ToString() + "; ";
                    flushCounter += 1;
                    if (flushCounter > cacheSize)
                    {
                        printText(cacheStr);
                        flushCounter = 0;
                        cacheStr = null;
                    }
                }
                printText(cacheStr);
            }            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (t != null) t.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.MainIcon;
        }
    }
}
