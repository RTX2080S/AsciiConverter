using System;
using System.Windows.Forms;
using AsciiConverter.Controllers;
using AsciiConverter.Interfaces;

namespace AsciiConverter
{
    public partial class MainWindow : Form
    {
        private IPrinterController printer;
        private IThreadController thread;
        private IAsciiController core;

        public MainWindow()
        {
            InitializeComponent();
            printer = new PrinterController();
            thread = new ThreadController();
            core = new AsciiController();
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
            char c = core.GetCharFromDecimal(numericUpDown1.Value);
            printer.SetText(c, ansBox1);      
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
            printer.SetText(core.GetIntFromChar(c), ansBox2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartPrinting();
        }

        private void StartPrinting()
        {
            printBox.Clear();
            thread.AccessThread(fillPrintBox);
            thread.StartThread();
        }

        private void fillPrintBox()
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
                    char c = core.GetCharFromInt(i);
                    cacheStr += string.Format("{0}: {1}; ", i, c);
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
            thread.AbortThread();
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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.AbortThread();
        }
    }
}
