using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
//Thiết kế chương trình để nhập vào tọa độ của 2 điểm (x1, y1) và (x2, y2) và tính:
//a, Hệ số góc của đường thẳng đi qua 2 điểm đó theo công thức: 61,1 925 70->1170
//     Hệ số góc=(y2-y1)/( x2-x1)
//b, Khoảng cách giữa 2 điểm


namespace Coordinates
{
    public partial class Form1 : Form
    {
        private float zoomFactor = 1.0f;
        private const float zoomStep = 0.1f;
        public Form1()
        {
            InitializeComponent();         
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonresult_Click(object sender, EventArgs e)
        {
            try
            {
                double x1 = double.Parse(x_1.Text);
                if (x1 is double.NaN)
                {
                    throw new Exception("Nhập sai kiểu dữ liệu x1");
                }
                double y1 = double.Parse(y_1.Text);
                if (y1 is double.NaN)
                {
                    throw new Exception("Nhập sai kiểu dữ liệu y1");
                }
                double x2 = double.Parse(x_2.Text);
                if (x2 is double.NaN)
                {
                    throw new Exception("Nhập sai kiểu dữ liệu x2");
                }
                double y2 = double.Parse(y_2.Text);
                if (y2 is double.NaN)
                {
                    throw new Exception("Nhập sai kiểu dữ liệu y2");
                }
                double a = (y2 - y1) / (x2 - x1);
                double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                resulta.Text = a.ToString();
                resultdistance.Text = distance.ToString();
                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            x_1.Text = "";
            y_1.Text = "";
            x_2.Text = "";
            y_2.Text = "";
            resulta.Text = "";
            resultdistance.Text = "";
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red, 2);
            Font font = new Font("Arial", 8);
            g.DrawLine(pen, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
            g.DrawLine(pen, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);

            for (int i = 1; i < pictureBox1.Width / (20 * zoomFactor); i++)
            {
                int x = pictureBox1.Width / 2 + (int)(i * 20 * zoomFactor);
                int y = pictureBox1.Height / 2;
                g.DrawLine(pen, x, y - 5, x, y + 5);
                g.DrawString((i).ToString(), font, Brushes.Black, x + 2, y + 5);
                g.DrawLine(pen, pictureBox1.Width / 2 - (int)(i * 20 * zoomFactor), y - 5, pictureBox1.Width / 2 - (int)(i * 20 * zoomFactor), y + 5);
                g.DrawString((-i).ToString(), font, Brushes.Black, pictureBox1.Width / 2 - (int)(i * 20 * zoomFactor) + 2, y + 5);
            }

            for (int i = 1; i < pictureBox1.Height / (20 * zoomFactor); i++)
            {
                int x = pictureBox1.Width / 2;
                int y = pictureBox1.Height / 2 + (int)(i * 20 * zoomFactor);
                g.DrawLine(pen, x - 5, y, x + 5, y);
                g.DrawString((i).ToString(), font, Brushes.Black, x - 20, y + 2);
                g.DrawLine(pen, x - 5, pictureBox1.Height / 2 - (int)(i * 20 * zoomFactor), x + 5, pictureBox1.Height / 2 - (int)(i * 20 * zoomFactor));
                g.DrawString((-i).ToString(), font, Brushes.Black, x - 20, pictureBox1.Height / 2 - (int)(i * 20 * zoomFactor) + 2);
            }

            if (double.TryParse(x_1.Text, out double x1) &&
                double.TryParse(y_1.Text, out double y1) &&
                double.TryParse(x_2.Text, out double x2) &&
                double.TryParse(y_2.Text, out double y2))
            {
                int startX = pictureBox1.Width / 2 + (int)(x1 * 20 * zoomFactor);
                int startY = pictureBox1.Height / 2 - (int)(y1 * 20 * zoomFactor);
                int endX = pictureBox1.Width / 2 + (int)(x2 * 20 * zoomFactor);
                int endY = pictureBox1.Height / 2 - (int)(y2 * 20 * zoomFactor);            

                if (x1 == x2)
                {
                    MessageBox.Show("Hai điểm có cùng giá trị x. Không thể vẽ đường thẳng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    g.FillEllipse(Brushes.Blue, startX - 2, startY - 2, 4, 4);
                    g.FillEllipse(Brushes.Blue, endX - 2, endY - 2, 4, 4);
                    g.DrawLine(pen, startX, startY, endX, endY);
                }
            }

            pen.Dispose();
            font.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zoomFactor += zoomStep;
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (zoomFactor > zoomStep)
            {
                zoomFactor -= zoomStep;
                pictureBox1.Invalidate(); 
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://github.com/hoangmanhkhiem";
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở liên kết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
