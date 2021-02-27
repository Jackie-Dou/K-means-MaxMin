using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Windows.Forms;


namespace MaxMin
{
    class UseKMeans
    {

        const int sz = 130;


        public List<Form1.Pixel> prevcentres = new List<Form1.Pixel>();

        public Form1.Pixel[] RedefineCentres(List<Form1.Pixel> centres, Form1.Pixel[,] dots)
        {
            int K = centres.Count;
            Form1.Pixel[] wcentres = new Form1.Pixel[K];
            wcentres = centres.ToArray();
            int[] amnt = new int[K];
            int[,] means = new int[K, K];
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    int a = dots[i, j].numOfClass;
                    amnt[a]++;
                    means[a, 0] += dots[i, j].x;
                    means[a, 1] += dots[i, j].y;
                }

            }
            for (int k = 0; k < K; k++)
            {
                means[k, 0] = means[k, 0] / amnt[k];
                means[k, 1] = means[k, 1] / amnt[k];
                //prevcentres.Add = centres[k];
                wcentres[k].x = means[k, 0];
                wcentres[k].y = means[k, 1];
            }
            return wcentres;
        }
    
        
        
        public bool DefineClasses(Form1.Pixel[] centres, Form1.Pixel[,] dots)
        {
            int K = centres.Count();
            for (int i = 0; i < sz; i++)
            {
                for (int j = 0; j < sz; j++)
                {
                    double mindist = 10000;
                    for (int k = 0; k < K; k++)
                    {

                        double a = Math.Pow((centres[k].x - dots[i, j].x), 2);
                        double b = Math.Pow((centres[k].y - dots[i, j].y), 2);
                        double res = Math.Sqrt(a + b);
                        if (res < mindist)
                        {
                            mindist = res;
                            dots[i, j].numOfClass = k;
                            dots[i, j].clr = Form1.colors[k];
                        }
                    }
                }
            }
            return false;
        }

    }


}

