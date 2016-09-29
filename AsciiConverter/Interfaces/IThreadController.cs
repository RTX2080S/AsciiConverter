using System.Threading;

namespace AsciiConverter.Interfaces
{
    interface IThreadController
    {
        Thread AccessThread(ThreadStart start);
        void StartThread();
        void AbortThread();
    }
}
