using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaxMin
{
    public partial class Form1 : Form
    {

        const int delta = 150;

        Bitmap bitmap;
        public struct Pixel
        {
            public Color clr;
            public int x;
            public int y;
            public int numOfClass;
        }

        public static Color[] colors = new Color[32] 
        { Color.Red, Color.Green, Color.Pink, Color.Blue, Color.Gray, Color.Beige, Color.Aquamarine, Color.Yellow, 
            Color.DarkCyan, Color.DeepPink, Color.Azure, Color.DarkBlue, Color.DarkViolet, Color.OrangeRed, Color.Gold, Color.Chocolate, 
            Color.Bisque, Color.Coral, Color.LightPink, Color.LavenderBlush, Color.Khaki, Color.PapayaWhip, Color.PaleVioletRed, Color.Plum, 
            Color.PaleGreen, Color.YellowGreen, Color.Purple, Color.MediumSeaGreen, Color.Olive, Color.LightCyan, Color.LemonChiffon, Color.FloralWhite };

        const int sz = 130;
        public Pixel[,] dots = new Pixel[sz, sz];
        public List<Pixel> centres = new List<Pixel>();

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(picbxMain.Width, picbxMain.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            MaxMin();
        }
        public void MaxMin()
        {
            SetCoordinates(Color.White);
            SetFirstCentre(); 
            DrawCentres();
            DefineClasses();
            RedrawField();
            bool isEnd = false;
            while (!isEnd)
            {
                DefineCentres(); 
                DefineClasses();
                RedrawField();
                isEnd = CheckEnd();
                if (centres.Count == 32 && !isEnd)
                {
                    MessageBox.Show(
                        "Подсчёт прекращён, т.к. закончились цвета для обозначения классов",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    isEnd = true;
                }
            }

            /*UseKMeans newK = new UseKMeans();

            for (int a = 0; a<5; a++)
            {
                Pixel[] wcentres = newK.RedefineCentres(centres, dots);
                RedrawKField(wcentres);
                newK.DefineClasses(wcentres, dots);
                RedrawKField(wcentres);
            }*/
            MessageBox.Show(
                       "Алгоритм завершил работу",
                       "Сообщение",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
        }

        public void RedrawKCentres(Pixel[] wcentres)
        {
            for (int i = 0; i < centres.Count; i++)
            {
                Pen myPen = new Pen(wcentres[i].clr, 2);
                using (Graphics myGraphics = Graphics.FromImage(bitmap))
                    myGraphics.DrawRectangle(myPen, wcentres[i].x * 2, wcentres[i].y * 2, 2, 2);
                picbxMain.Image = bitmap;
            }
            picbxMain.Refresh();
        }

        public bool CheckEnd()
        {
            bool flag = true;
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    int numOfCurrClass = 0;
                    foreach (Pixel cent in centres)
                    {                        
                        if (dots[i,j].numOfClass == numOfCurrClass)
                        {
                            double res = Distance(cent, dots[i, j]);
                            if (res > delta)
                            {
                                flag = false;
                                return flag;
                            }
                        }                      
                        numOfCurrClass++;
                    }
                }
            }
            return flag;
        }

        public void DefineCentres()
        {

            double maxdist;
            List<Pixel> newPs = new List<Pixel>();
            int numOfCurrClass = 0;
            int k = centres.Count;
            foreach (Pixel cent in centres)
            {
                Pixel newP = new Pixel();
                maxdist = 0;
                for (int i = 0; i < sz; i++)
                {
                    for (int j = 0; j < sz; j++)
                    {
                        if (dots[i,j].numOfClass == numOfCurrClass)
                        {
                            double res = Distance(cent, dots[i, j]);
                            if (res > maxdist)
                            {
                                
                                maxdist = res;
                                newP.x = dots[i, j].x;
                                newP.y = dots[i, j].y;
                                k += numOfCurrClass;
                                newP.numOfClass = k;
                                newP.clr = Color.Black;
                            }
                        }          
                    }
                }
                
                newPs.Add(newP);
                numOfCurrClass++;
            }
            foreach (Pixel p in newPs)
            {
                centres.Add(p);
            }
        }

        public void DefineClasses()
        {
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    double mindist = 10000;
                    int num = 0;
                    foreach (Pixel cent in centres)
                    {
                        double res = Distance(cent, dots[i, j]);
                        if (res < mindist)
                        {
                            mindist = res;
                            dots[i, j].numOfClass = num;
                            dots[i, j].clr = colors[num];
                        }
                        num++;
                    }
                }
            }
        }

        public double Distance(Pixel dot1, Pixel dot2)
        {
            double a = Math.Pow((dot1.x - dot2.x), 2);
            double b = Math.Pow((dot1.y - dot2.y), 2);
            double res = Math.Sqrt(a + b);
            return res;
        }

        public void RedrawKField(Pixel[] wcentres)
        {
            if (picbxMain.Image != null)
            {
                picbxMain.Image.Dispose();
                picbxMain.Image = null;
                bitmap = new Bitmap(picbxMain.Width, picbxMain.Height);
            }
            DrawDots();
            RedrawKCentres(wcentres);
            System.Threading.Thread.Sleep(1000);

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

        public void SetCoordinates(Color newclr)
        {
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    dots[i, j].x = i * 2;
                    dots[i, j].y = j * 2;
                    dots[i, j].clr = newclr;
                }
            }
        }

        public void SetFirstCentre()
        {
            Random rnd = new Random();
            Pixel newP = new Pixel();
            newP.clr = Color.Black;
            int a = rnd.Next(1, sz);
            int b = rnd.Next(1, sz);
            newP.x = a;
            newP.y = b;
            newP.numOfClass = 0;
            centres.Add(newP);
        }

        public void DrawCentres()
        {
            foreach (Pixel cent in centres)
            {
                Pen myPen = new Pen(cent.clr, 2);
                using (Graphics myGraphics = Graphics.FromImage(bitmap))
                    myGraphics.DrawRectangle(myPen,( cent.x * 2) + 10, (cent.y * 2) + 10, 2, 2);
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
                        myGraphics.DrawRectangle(myPen, (dots[i, j].x * 2) + 10, (dots[i, j].y * 2) + 10, 1, 1);
                    picbxMain.Image = bitmap;
                }
            }
            picbxMain.Refresh();
        }

    }
}
