using AsciiConverter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsciiConverter.Controllers
{
    delegate void invokeTextBox(string txt, TextBox printBox, Form owner);

    public class PrinterController : IPrinterController
    {
        public void PrintText(string txt, TextBox printBox, Form owner)
        {
            if (printBox.InvokeRequired)
            {
                invokeTextBox d = new invokeTextBox(PrintText);
                owner.Invoke(d, txt, printBox, owner);
            }
            else
            {
                printBox.Text += txt;
                printBox.Select(printBox.Text.Length, 0);
                printBox.ScrollToCaret();
            }
        }
    }
}
