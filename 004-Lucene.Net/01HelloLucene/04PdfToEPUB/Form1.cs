using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _04PdfToEPUB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF文件(*.pdf)|*.pdf|所有文件(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK) 
            {
                this.textBox1.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = this.textBox1.Text.Trim();

            if (path != string.Empty)
            {
                label1.Visible = false;

                ThreadPool.QueueUserWorkItem((userState) => {
                    // load PDF document
                    Document pdfDocument = new Document(path);

                    // instantiate Epub Save options
                    EpubSaveOptions options = new EpubSaveOptions();

                    // specify the layout for contents
                    options.ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow;

                    // save the ePUB document
                    pdfDocument.Save(path + ".epub", options);

                    if (label1.InvokeRequired)
                    {
                        this.Invoke(new Action(() => { label1.Visible = true; }));
                    }
                    else 
                    {
                        label1.Visible = true;
                    }

                });
            }
            else 
            {
                MessageBox.Show("Please choose a pdf file firstly.");
            }
        }
    }
}
