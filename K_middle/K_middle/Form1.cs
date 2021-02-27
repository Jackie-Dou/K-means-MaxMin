using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_middle
{
    public partial class Form1 : Form
    {
        const int K = 5;
        const int max = 25;
        const int delta = 5;
        const int sz = 130; 

        public Pixel[,] dots = new Pixel[sz, sz];
        public Pixel[] centres = new Pixel[K];
        public Pixel[] prevcentres = new Pixel[K];
        public Color[] colors = new Color[K] {Color.Red, Color.Green, Color.Pink, Color.Blue, Color.Gray};

        Bitmap bitmap;
        public struct Pixel
        {
            public Color clr;
            public int x;
            public int y;
            public int numOfClass;
        }

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(picbxMain.Width, picbxMain.Height);           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Kmeans();            
        }


        public void Kmeans()
        {
            SetCoordinates(Color.White);
            SetCentres();
            DefineClasses();
            RedrawField();
            int i = 0;
            bool isEnd = false;
            while (i < max && !isEnd)
            {          
                RedefineCentres();
                RedrawField();
                DefineClasses();
                RedrawField();
                isEnd = CheckEnd();
                i++;
            }
            MessageBox.Show(
                       "Алгоритм завершил работу",
                       "Сообщение",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
        }

        public bool CheckEnd()
        {
            bool flag = true;
            for (int i = 0; i < K; i++)
            {
                int delta1 = Math.Abs(prevcentres[i].x - centres[i].x);
                int delta2 = Math.Abs(prevcentres[i].y - centres[i].y);
                if (delta1>=delta || delta2>=delta)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        public void RedrawField()
        {
            if (picbxMain.Image != null)
            {
                picbxMain.Image.Dispose();
                picbxMain.Image = null;
                bitmap = new Bitmap(picbxMain.Width, picbxMain.Height);
            }
            DrawDots();
            DrawCentres();
            System.Threading.Thread.Sleep(1000);

        }

        public void RedefineCentres()
        {
            int[] amnt = new int[K];
            int[,] means = new int[K, K];
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    switch (dots[i, j].numOfClass)
                    {
                        case 0:
                            amnt[0]++;
                            means[0, 0] += dots[i, j].x;
                            means[0, 1] += dots[i, j].y;
                            break;
                        case 1:
                            amnt[1]++;
                            means[1, 0] += dots[i, j].x;
                            means[1, 1] += dots[i, j].y;
                            break;
                        case 2:
                            amnt[2]++;
                            means[2, 0] += dots[i, j].x;
                            means[2, 1] += dots[i, j].y;
                            break;
                        case 3:
                            amnt[3]++;
                            means[3, 0] += dots[i, j].x;
                            means[3, 1] += dots[i, j].y;
                            break;
                        case 4:
                            amnt[4]++;
                            means[4, 0] += dots[i, j].x;
                            means[4, 1] += dots[i, j].y;
                            break;
                        default:
                            break;
                    }
                    //переписать алгоритм

                }
            }
            for (int k = 0; k < K; k++)
            {
                means[k, 0] = means[k, 0] / amnt[k];
                means[k, 1] = means[k, 1] / amnt[k];
                prevcentres[k] = centres[k];
                centres[k].x = means[k, 0];
                centres[k].y = means[k, 1];
            }           
        }

        public bool DefineClasses()
        {
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    double mindist = 10000;
                    for (int k = 0; k < K; k++)
                    {
                        
                        double a = Math.Pow((centres[k].x - dots[i, j].x),2);
                        double b = Math.Pow((centres[k].y - dots[i, j].y), 2);
                        double res = Math.Sqrt(a+b);
                        if (res<mindist)
                        {
                            mindist = res;
                            dots[i, j].numOfClass = k;
                            dots[i, j].clr = colors[k];
                        }
                    }      
                }
            }
            return false;
        }

        public void SetCoordinates(Color newclr)
        {
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    dots[i,j].x = i * 2;
                    dots[i,j].y = j * 2;
                    dots[i, j].clr = newclr;
                }
            }
        }

        public void SetCentres()
        {
            Random rnd = new Random();
            for (int i = 0; i < K; i++)
            {
                centres[i].clr = Color.Black;
                int a = rnd.Next(1, sz);
                int b = rnd.Next(1, sz);
                centres[i].x = a;
                centres[i].y = b;                
                centres[i].numOfClass = i;
            }
        }

        public void DrawCentres()
        {
            for (int i = 0; i < K; i++)
            {
                Pen myPen = new Pen(centres[i].clr, 2);
                using (Graphics myGraphics = Graphics.FromImage(bitmap))
                    myGraphics.DrawRectangle(myPen, centres[i].x*2, centres[i].y*2, 2, 2);
                picbxMain.Image = bitmap;
            }
            picbxMain.Refresh();
        }

        public void DrawDots()
        {
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                        Pen myPen = new Pen(dots[i, j].clr, 1);
                        using (Graphics myGraphics = Graphics.FromImage(bitmap))
                            myGraphics.DrawRectangle(myPen, dots[i, j].x * 2, dots[i, j].y * 2, 1, 1);
                    picbxMain.Image = bitmap;
                }
            }
            picbxMain.Refresh();
        }
    }
}
