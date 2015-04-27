using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01Example
{
    public partial class Form1 : Form
    {
        private ComplexCalculater _complexCalculater;

        public Form1()
        {
            InitializeComponent();

            _complexCalculater = new ComplexCalculater();
            _complexCalculater.OnCountChanged += _complexCalculater_OnCountChanged;
        }

        void _complexCalculater_OnCountChanged(long currentNumber)
        {
            this.Invoke(new Action(() => { 
                this.lblCurrent.Text = currentNumber.ToString(); 
            }));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!_complexCalculater.IsRunning) 
            {
                _complexCalculater.Begin();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _complexCalculater.Pause();
        }

        private void txtStart_TextChanged(object sender, EventArgs e)
        {
            long start;
            if (!long.TryParse(txtStart.Text.Trim(), out start))
            {
                start = 0;
                this.txtStart.Text = start.ToString();
            }

            _complexCalculater.SetInitNumber(start);
        }
    }
}
