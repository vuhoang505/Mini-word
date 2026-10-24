using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniWord_VuHuyHoang
{
    public partial class frmFindReplace : Form
    {
        private RichTextBox mainRtb;
        public frmFindReplace(RichTextBox rtb)
        {
            InitializeComponent();
            mainRtb = rtb;
        }

        private void frmFindReplace_Load(object sender, EventArgs e)
        {

        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            try
            {
                int startIndex = mainRtb.SelectionStart + mainRtb.SelectionLength;
                int index = mainRtb.Find(txtFind.Text, startIndex, RichTextBoxFinds.None);

                if (index == -1)
                {
                    index = mainRtb.Find(txtFind.Text, 0, RichTextBoxFinds.None);
                    if (index == -1)
                    {
                        MessageBox.Show("Không tìm thấy từ khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch { }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainRtb.SelectedText.Length > 0 && mainRtb.SelectedText == txtFind.Text)
                {
                    DialogResult result = MessageBox.Show(
                        "Bạn có muốn thay thế từ này không?",
                        "Xác nhận thay thế",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        mainRtb.SelectedText = txtReplace.Text; 
                        btnFindNext_Click(sender, e); 
                    }
                    else if (result == DialogResult.No)
                    {
                        btnFindNext_Click(sender, e); 
                    }
                }
                else
                {
                    btnFindNext_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thay thế: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFind.Text)) return;

                int currentIndex = mainRtb.SelectionStart;

                mainRtb.Text = mainRtb.Text.Replace(txtFind.Text, txtReplace.Text);

                mainRtb.SelectionStart = currentIndex;

                MessageBox.Show("Đã thay thế tất cả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
