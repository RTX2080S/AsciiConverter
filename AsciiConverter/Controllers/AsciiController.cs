using AsciiConverter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsciiConverter.Controllers
{
    public class AsciiController : IAsciiController
    {
        public char GetCharFromDecimal(decimal source)
        {
            return (char)source;
        }

        public char GetCharFromInt(int source)
        {
            return (char)source;
        }

        public int GetIntFromChar(char source)
        {
            return source;
        }
    }
}
