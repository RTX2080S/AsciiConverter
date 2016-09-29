using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AsciiConverter.Controllers;
using AsciiConverter.Providers;

namespace AsciiConverter
{
    public partial class MainWindow : Form
    {
        private PrinterController printer;
        private ThreadController myThread;

        public MainWindow()
        {
            InitializeComponent();
            printer = new PrinterController();
            myThread = new ThreadController();
        }

        private void InitUI()
        {
            Icon = Properties.Resources.MainIcon;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            convertToChar();
        }

        private void convertToChar()
        {
            char c = (char)numericUpDown1.Value;
            ansBox1.Text = c.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            converToAscii();
        }

        private void converToAscii()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                return;
            char c = textBox1.Text[0];
            ansBox2.Text = ((int)c).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartPrinting();
        }

        private void StartPrinting()
        {
            printBox.Clear();
            myThread.AccessThread(fillPrintBox);
            myThread.StartThread();
        }

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
                        printer.PrintText(cacheStr, printBox, this);
                        flushCounter = 0;
                        cacheStr = null;
                    }
                }
                printer.PrintText(cacheStr, printBox, this);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myThread.AbortThread();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitUI();
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                convertToChar();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                converToAscii();
        }

        private void printerStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                StartPrinting();
        }

        private void printerEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                StartPrinting();
        }
    }
}
