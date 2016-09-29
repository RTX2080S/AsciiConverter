using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsciiConverter.Interfaces
{
    interface IPrinterController
    {
        void PrintText(string txt, TextBox printBox, Form owner);
    }
}
