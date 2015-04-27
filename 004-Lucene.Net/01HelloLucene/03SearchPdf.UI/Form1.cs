using _03SearchPdf.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03SearchPdf.UI
{
    public partial class Form1 : Form
    {
        private BackgroundWorker _bgWorker;
        private IndexHelper _indexHelper;
        private SearchHelper _searchHelper;

        public Form1()
        {
            InitializeComponent();

            _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerReportsProgress = true;
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.ProgressChanged += _bgWorker_ProgressChanged;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;

            _indexHelper = new IndexHelper(Config.IndexFolder, Config.TextFilesFolder);
            _indexHelper.HasLog += ShowLog;
            _indexHelper.OnProgressChanged += (percent) => {
                _bgWorker.ReportProgress(percent);
            };

            _searchHelper = new SearchHelper(Config.IndexFolder);
            _searchHelper.HasLog += ShowLog;
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblCreateIndexCompleted.Visible = true;
        }

        private void _bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarCreateIndex.Value = e.ProgressPercentage;
        }

        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _indexHelper.CreateIndex();
        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            if (lblCreateIndexCompleted.Visible == true) 
            {
                lblCreateIndexCompleted.Visible = false;
            }

            _bgWorker.RunWorkerAsync();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (keyword != string.Empty)
            {
                DataTable dtResult = _searchHelper.Search(keyword);
                this.dataGridView1.DataSource = dtResult;
            }
            else 
            {
                MessageBox.Show("Keyword should not be empty.");
            }
        }

        private void ShowLog(string log) 
        {
            var writeLog = new Action(() => { this.txtLog.AppendText(log + "\r\n"); });
            if (this.txtLog.InvokeRequired)
            {
                this.Invoke(writeLog);
            }
            else 
            {
                writeLog();
            }
        }
    }
}
