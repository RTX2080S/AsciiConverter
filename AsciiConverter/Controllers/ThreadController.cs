using AsciiConverter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsciiConverter.Providers
{
    public class ThreadController : IThreadController
    {
        private Thread _thread;

        public Thread AccessThread(ThreadStart start)
        {
            _thread = _thread ?? new Thread(start);
            _thread.Priority = ThreadPriority.BelowNormal;
            return _thread;
        }

        public void StartThread()
        {
            if (_thread != null)
                _thread.Start();
        }

        public void AbortThread()
        {
            if (_thread != null)
                _thread.Abort();
        }
    }
}
