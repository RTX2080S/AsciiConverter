using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsciiConverter.Interfaces
{
    interface IAsciiController
    {
        char GetCharFromInt(int source);
        int GetIntFromChar(char source);
        char GetCharFromDecimal(decimal source);
    }
}
