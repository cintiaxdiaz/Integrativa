using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabClinico_9418202
{
    public partial class Form_UsuarioNivel_2 : Form
    {
        public Form_UsuarioNivel_2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file|*.pdf", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.LETTER);
                    {
                        try
                        {
                            PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                            doc.Open();

                            //doc.Add(new iTextSharp.text.Paragraph(label2.Text + "\n" + richTextBox1.Text));
                            doc.Add(new iTextSharp.text.Paragraph(label2.Text + "\n" + label3.Text + "\n" + "\n" + "\n" + label4.Text + "\n" + label5.Text + "\n" + "\n" + "\n" + label6.Text + "\n" + label7.Text + "\n" + "\n" + "\n" + label8.Text + "\n" + label9.Text + "\n" + "\n" + "\n" + label10.Text + "\n" + label11.Text + "\n" + "\n" + "\n" + label12.Text + "\n" + richTextBox1.Text));

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            doc.Close();
                        }
                    }
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(pictureBox2.Image, 600, 100, pictureBox2.Width, pictureBox2.Height);
            e.Graphics.DrawString(label2.Text + "\n" + label3.Text + "\n" + "\n" + "\n" + label4.Text + "\n" + label5.Text + "\n" + "\n" + "\n" + label6.Text + "\n" + label7.Text + "\n" + "\n" + "\n" + label8.Text + "\n" + label9.Text + "\n" + "\n" + "\n" + label10.Text + "\n" + label11.Text + "\n" + "\n" + "\n" + label12.Text + "\n" + richTextBox1.Text, richTextBox1.Font, Brushes.Black, 10,150);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                printDocument1.Print();
            }
        }
    }
}
