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
    public partial class frmHoang : Form
    {
        private string currentFilePath = "";
        public frmHoang()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rtbDoc.SelectionFont != null)
            {
                Font currentFont = rtbDoc.SelectionFont;
                FontStyle newStyle = currentFont.Style;
                if (currentFont.Bold)
                    newStyle &= ~FontStyle.Bold;
                else
                    newStyle |= FontStyle.Bold;

                rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.Clear();
            currentFilePath = "";
            this.Text = "Mini Word - Untitled";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Rich Text Format (*.rtf)|*.rtf|Text File (*.txt)|*.txt";
                ofd.Title = "Chọn file để mở";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = ofd.FileName;
                    if (currentFilePath.EndsWith(".rtf"))
                        rtbDoc.LoadFile(currentFilePath, RichTextBoxStreamType.RichText);
                    else
                        rtbDoc.LoadFile(currentFilePath, RichTextBoxStreamType.PlainText);

                    this.Text = "Mini Word - " + System.IO.Path.GetFileName(currentFilePath);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Không tìm thấy tệp tin này. Vui lòng kiểm tra lại đường dẫn!", "Lỗi file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xử lý file:\n" + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Rich Text Format (*.rtf)|*.rtf|Text File (*.txt)|*.txt";
                sfd.Title = "Lưu tệp với tên mới";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = sfd.FileName;
                    if (sfd.FilterIndex == 1) // Chọn rtf
                        rtbDoc.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    else 
                        rtbDoc.SaveFile(currentFilePath, RichTextBoxStreamType.PlainText);

                    this.Text = "Mini Word - " + System.IO.Path.GetFileName(currentFilePath);
                    MessageBox.Show("Lưu file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Không tìm thấy tệp tin này. Vui lòng kiểm tra lại đường dẫn!", "Lỗi file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xử lý file:\n" + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentFilePath))
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    if (currentFilePath.EndsWith(".rtf"))
                        rtbDoc.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    else
                        rtbDoc.SaveFile(currentFilePath, RichTextBoxStreamType.PlainText);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Không tìm thấy tệp tin này. Vui lòng kiểm tra lại đường dẫn!", "Lỗi file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xử lý file:\n" + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDoc.Clear();
            currentFilePath = "";
            this.Text = "Mini Word - Untitled";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTextColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();    
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                rtbDoc.SelectionColor = colorDialog.Color;
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                rtbDoc.SelectionBackColor = colorDialog.Color;
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.ZoomFactor < 60f)
                {
                    rtbDoc.ZoomFactor += 0.5f;
                }
            }
            catch { }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.ZoomFactor > 1f)
                {
                    rtbDoc.ZoomFactor -= 0.5f;
                }
            }
            catch { }
        }
        private void rtbDoc_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int index = rtbDoc.SelectionStart;
                int line = rtbDoc.GetLineFromCharIndex(index);
                int col = index - rtbDoc.GetFirstCharIndexFromLine(line);
                lblStatus.Text = $"Dòng: {line + 1}, Cột: {col + 1}";
            }
            catch
            {
            }
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbDoc.CanUndo)
            {
                rtbDoc.Undo();
            }
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (rtbDoc.CanRedo)
            {
                rtbDoc.Redo();
            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Cut();
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show("Bộ nhớ tạm (Clipboard) đang bị chiếm dụng bởi ứng dụng khác. Hãy thử lại sau giây lát!", "Lỗi Clipboard", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thao tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Copy();
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show("Bộ nhớ tạm (Clipboard) đang bị chiếm dụng bởi ứng dụng khác. Hãy thử lại sau giây lát!", "Lỗi Clipboard", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thao tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.Paste();
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show("Bộ nhớ tạm (Clipboard) đang bị chiếm dụng bởi ứng dụng khác. Hãy thử lại sau giây lát!", "Lỗi Clipboard", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thao tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rtbDoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmHoang_Load(object sender, EventArgs e)
        {
            foreach (FontFamily font in FontFamily.Families)
            {
                cbFontFamily.Items.Add(font.Name);
            }
            cbFontFamily.SelectedItem = "Times New Roman";
            string[] sizes = { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
            cbFontSize.Items.AddRange(sizes);
            cbFontSize.SelectedItem = "14";
        }

        private void cbFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.SelectionFont != null && cbFontFamily.SelectedItem != null)
                {
                    rtbDoc.SelectionFont = new Font(cbFontFamily.SelectedItem.ToString(), rtbDoc.SelectionFont.Size, rtbDoc.SelectionFont.Style);
                }
            }
            catch { }
        }

        private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rtbDoc.SelectionFont != null && cbFontSize.SelectedItem != null)
                {
                    float newSize;
                    if (float.TryParse(cbFontSize.Text, out newSize))
                    {
                        if (newSize > 0 && newSize <= 1638)
                        {
                            rtbDoc.SelectionFont = new Font(rtbDoc.SelectionFont.FontFamily, newSize, rtbDoc.SelectionFont.Style);
                        }
                        else
                        {
                            MessageBox.Show("Kích thước chữ phải nằm trong khoảng từ 1 đến 1638.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi định dạng cỡ chữ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rtbDoc.SelectionFont != null)
            {
                Font currentFont = rtbDoc.SelectionFont;
                FontStyle newStyle = currentFont.Style;

                if (currentFont.Italic)
                    newStyle &= ~FontStyle.Italic;
                else
                    newStyle |= FontStyle.Italic;

                rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rtbDoc.SelectionFont != null)
            {
                Font currentFont = rtbDoc.SelectionFont;
                FontStyle newStyle = currentFont.Style;

                if (currentFont.Underline)
                    newStyle &= ~FontStyle.Underline;
                else
                    newStyle |= FontStyle.Underline;

                rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem1_Click(sender, e);
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem1_Click(sender, e);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem1_Click(sender, e);
        }

        private void undoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            undoToolStripMenuItem_Click(sender, e);
        }

        private void redoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            redoToolStripMenuItem1_Click(sender, e);
        }

        private void cutToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem1_Click(sender, e);
        }

        private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem1_Click(sender, e);
        }

        private void pasteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem1_Click(sender, e);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFindReplace findForm = new frmFindReplace(rtbDoc);
            findForm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            findToolStripMenuItem_Click(sender, e);
        }

        private void btnShowEmoji_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = !flowLayoutPanel1.Visible;
            flowLayoutPanel1.BringToFront();
        }

        private void InsertEmoji_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedBtn = sender as Button;
                if (clickedBtn != null)
                {
                    rtbDoc.SelectedText = clickedBtn.Text;
                    flowLayoutPanel1.Visible = false;
                }
            }
            catch { }
        }

        private void btnInsertImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openImage = new OpenFileDialog();
                openImage.Filter = "Hình ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openImage.Title = "Chọn hình ảnh để chèn";

                if (openImage.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(openImage.FileName);
                    Clipboard.SetImage(img);
                    rtbDoc.Paste();
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Không tìm thấy tệp tin này. Vui lòng kiểm tra lại đường dẫn!", "Lỗi file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xử lý file:\n" + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnInsertImage_Click(sender, e);
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi căn lề: " + ex.Message);
            }
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi căn lề: " + ex.Message);
            }
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi căn lề: " + ex.Message);
            }
        }
    }
}
