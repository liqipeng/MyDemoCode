using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01Example
{
    class ComplexCalculater
    {
        private long _count = 0;
        private bool _isRunning = false;

        public ComplexCalculater()
        {
        }

        public void SetInitNumber(long initNum) 
        {
            this._count = initNum;
        }

        public event Action<long> OnCountChanged;

        public void Begin() 
        {
            _isRunning = true;
            ThreadPool.QueueUserWorkItem(DoCalculating, null);
        }

        private void DoCalculating(object state) 
        {
            while (_isRunning)
            {
                ++_count;

                OnCountChanged(_count);

                Thread.Sleep(300);
            }
        }

        public void Pause() 
        {
            _isRunning = false;
        }

        public bool IsRunning
        {
            get 
            {
                return _isRunning;
            }
        }
    }
}
