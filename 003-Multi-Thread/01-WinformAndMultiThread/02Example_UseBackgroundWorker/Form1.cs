using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _02Example_UseBackgroundWorker
{
    public partial class Form1 : Form
    {
        private BackgroundWorker _bgWorker;

        public Form1()
        {
            InitializeComponent();

            _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerReportsProgress = true;
            _bgWorker.WorkerSupportsCancellation = true;
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.ProgressChanged += _bgWorker_ProgressChanged;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                MessageBox.Show("All cancelled.");
            }
            else if (e.Error == null)
            {
                MessageBox.Show("All completed. Result=" + e.Result);
            }
            else 
            {
                MessageBox.Show(e.Result.ToString());
            }
        }

        void _bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.label1.Text = e.ProgressPercentage + "%";
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int sum = 0;
            for (int i = 1; i <= 100; i++) 
            {
                sum += i;
                Thread.Sleep((int)e.Argument);
                _bgWorker.ReportProgress(i);

                if (_bgWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
            }
            e.Result = sum;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _bgWorker.RunWorkerAsync(100);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _bgWorker.CancelAsync();
        }
    }
}
