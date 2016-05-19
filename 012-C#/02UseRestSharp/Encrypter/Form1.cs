using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encrypter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = EncryptUtil.EncryptByAES(this.txtSrc.Text, this.txtPwd.Text);
        }

        private void btnDescrypt_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = EncryptUtil.DecryptByAES(this.txtSrc.Text, this.txtPwd.Text);
        }
    }
}
