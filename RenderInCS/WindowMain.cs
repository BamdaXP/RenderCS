using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenderInCS
{
    public partial class WindowMain : Form
    {
        private Graphics graphics;
        private Bitmap bitmap;



        public WindowMain()
        {
            InitializeComponent();
            InitializeGraphics();

            Render();
            
            
        }

        public void InitializeGraphics()
        {
            graphics = CreateGraphics();
            bitmap = new Bitmap(Width, Height);
        }


        private void Render()
        {
            Point a = new Point(70, 60);
            Point b = new Point(10, 40);
            Point c = new Point(0, 0);
            SetLine(a, b, Color.Black);
            SetTri(a,b,c,Color.Black);
        }














        #region Basic 2D Painters
        public void SetPixel(Point point, Color color)
        {
            //Set the single pixel of the Bitmap

            bitmap.SetPixel(point.X, point.Y, color);
        }


        //Based on Bresenham Algorithm
        public void SetLine(Point from, Point to, Color color)
        {
            //Initialize the vars
            int dx = to.X - from.X;
            int dy = to.Y - from.Y;
            int sx = dx > 0 ? 1 : -1;
            int sy = dx > 0 ? 1 : -1;
            int dx2 = 2 * dx;
            int dy2 = 2 * dy;

            //If the k is smaller than 1,use X to increase
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                int e = -dx; //e = -0.5*2*dx

                int x = from.X;
                int y = from.Y;
                for (x = from.X; x < to.X; x += sx)
                {
                    SetPixel(new Point(x, y), color);

                    e = e + dy2;//2*e*dx=2*e*dx+2dy
                    if (e > 0)//If the e > 0,draw the upper point   
                    {
                        y += sy;
                        e = e - dx2;//2*e*dx=2*e*dx-2*dx
                    }
                }
            }
            else//If the k is bigger than 1,use Y to increase
            {
                int e = -dy; //e = -0.5 * 2 * dy
                int x = from.X;
                int y = from.Y;
                for (y = from.Y; y < to.Y; y += sy)
                {
                    SetPixel(new Point(x, y), color);

                    e = e + dx2;//2*e*dy=2*e*dy+2dy 
                    if (e > 0)//If the e > 0,draw the upper point
                    {
                        x += sx;
                        e = e - dy2;//2*e*dy = 2*e*dy-2*dy 
                    }
                }
            }
        }
        public void SetTri(Point a,Point b,Point c,Color color)
        {
            ////If itself is a kind of special triangle
            if (a.Y == b.Y)
            {
                if (c.Y <= a.Y) //Flat Bottom 
                {
                    SetTriFlatBottom(a, b, c, color);
                }
                else //Flat Top  
                {
                    SetTriFlatTop(a, b, c, color);
                }
                return;
            }
            else if (a.Y == c.Y)
            {
                if (b.Y <= a.Y) //Flat Bottom   
                {
                    SetTriFlatBottom(a, c, b, color);
                }
                else //Flat Top  
                {
                    SetTriFlatTop(a, c, b, color);
                }
                return;
            }
            else if (b.Y == c.Y)
            {
                if (a.Y <= b.Y) //Flat Bottom  
                {
                    SetTriFlatBottom(b, c, a, color);
                }
                else // Flat Top
                {
                    SetTriFlatTop(b, c, a, color);
                }
                return;
            }


            Point up = new Point();
            Point middle = new Point();
            Point bottom = new Point();

            //Find the related position of the three points
            if (a.Y < b.Y && b.Y < c.Y)
            {
                up = a;
                middle = b;
                bottom = c;
            }
            else if (a.Y < c.Y && c.Y < b.Y)
            {
                up = a;
                middle = c;
                bottom = b;
            }
            else if (b.Y < a.Y && a.Y < c.Y)
            {
                up = b;
                middle = a;
                bottom = c;
            }
            else if (b.Y < c.Y && c.Y < a.Y)
            {
                up = b;
                middle = c;
                bottom = a;
            }
            else if (c.Y < a.Y && a.Y < b.Y)
            {
                up = c;
                middle = a;
                bottom = b;
            }
            else if (c.Y < b.Y && b.Y < a.Y)
            {
                up = c;
                middle = b;
                bottom = a;
            }


            //The x of the long edge when it is on the y of the middle
            int xMiddle = (middle.Y - up.Y) * (bottom.X - up.X) / (bottom.Y - up.Y) + up.X;
            Point anotherMiddle = new Point(xMiddle, middle.Y);

            
            // 画平底  
            SetTriFlatBottom(anotherMiddle, middle, up, color);

            // 画平顶  
            SetTriFlatTop(anotherMiddle, middle, bottom, color);
               
            
        }
        public void SetTriFlatBottom(Point bottom1,Point bottom2,Point top, Color color)
        {
            if (bottom1.X>bottom2.X)
            {
                Point tmp = new Point();
                tmp = bottom1;
                bottom1 = bottom2;
                bottom2 = tmp;
            }
            for (int y = bottom1.Y; y >= top.Y; y--)
            {
                int xs, xe;
                xs = (y - bottom1.Y) * (top.X - bottom1.X) / (top.Y - bottom1.Y) + bottom1.X;
                xe = (y - bottom2.Y) * (top.X - bottom2.X) / (top.Y - bottom2.Y) + bottom2.X;
                Point sp = new Point(xs, y);
                Point ep = new Point(xe, y);
                SetLine(sp, ep, color);
            }
        }
        public void SetTriFlatTop(Point top1,Point top2,Point bottom, Color color)
        {
            if (top1.X > top2.X)
            {
                Point tmp = new Point();
                tmp = top1;
                top1 = top2;
                top2 = tmp;
            }
            for (int y = bottom.Y; y >= top1.Y; y--)
            {
                int xs, xe;
                xs = (y - bottom.Y) * (top1.X - bottom.X) / (top1.Y - bottom.Y) + bottom.X;
                xe = (y - bottom.Y) * (top2.X - bottom.X) / (top2.Y - bottom.Y) + bottom.X;
                Point sp = new Point(xs, y);
                Point ep = new Point(xe, y);
                SetLine(sp, ep, color);
               
            }
        }
        #endregion


        #region CallBack Functions
        //Paint CallBack Function
        private void OnPaint(object sender, PaintEventArgs e)
        {
            graphics.DrawImage(bitmap, 0, 0);
        }
        #endregion
    }
}
