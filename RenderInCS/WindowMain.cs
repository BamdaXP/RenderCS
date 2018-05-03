using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Numerics;

namespace RenderCS
{
    public partial class WindowMain : Form
    {
        private Graphics graphics;
        private Bitmap bitmap;

        public Camera camera;
        public ViewPort view;

        public System.Windows.Forms.Timer timer;
        public int deltaTime = 20;//in ms
        //public delegate void Update();

        public List<DObject> objs;
        public List<Light> lights;
        

        public float duration=0f;
        public WindowMain()
        {
            
            ThreadPool.SetMaxThreads(10, 10);

            InitializeComponent();

            InitializeGraphics();

            InitializeTimer();

            InitializeObjects();

            InitializeLights();
            
            



        }

        public void InitializeGraphics()
        {

            graphics = CreateGraphics();
            bitmap = new Bitmap(Width, Height);
            camera = new Camera();
        
            view = new ViewPort(800, 600);
        }

        public void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = deltaTime;
            timer.Tick += GraphicsUpdate;
            timer.Start();
        }


        public void InitializeObjects()
        {
            objs = new List<DObject>();
            AddCube();
        }

        public void InitializeLights()
        {
            lights = new List<Light>();
            Light l = new Light(new Vector3(4,4,4));
            lights.Add(l);

        }




        public void AddCube()
        {
            DMesh cubeMesh = new DCubeMesh(3, 3, 3);
            DObject cube = new DObject(new Vector3(0,0,-15),cubeMesh);

            objs.Add(cube);
        }

        private void Clear(Color color)
        {
            bool flag = Monitor.TryEnter(bitmap, 1000);
            try
            {
                if (flag)
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            bitmap.SetPixel(i, j, color);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (flag)
                    Monitor.Exit(bitmap);
            }
            
        }




        #region VirtualWorld Painters
        private void DrawDObject(DObject obj)
        {
            foreach (DTriAngle tri in obj.matureFaces)
            {
                //skip the tri behind
                if (tri.visible == false)
                {
                    continue;
                }
                Object o = tri as Object;

                //Start thread pools

                DrawTri(o);
                
                //DrawTri(tri,mesh);
            }
        }

       
        private void DrawTri(Object o)
        {
            bool flag = Monitor.TryEnter(bitmap, 1000);

            DTriAngle tri = o as DTriAngle;

            try
            {
                if (flag)
                {

                    Point a = tri.p1.ToScreenPoint(view);
                    Point b = tri.p2.ToScreenPoint(view);
                    Point c = tri.p3.ToScreenPoint(view);
                    Color ca = tri.p1.color;
                    Color cb = tri.p2.color;
                    Color cc = tri.p3.color;


                SetTri(a, b, c, ca, cb, cc);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (flag)
                    Monitor.Exit(bitmap);
            }


        }
        #endregion
        //==========================================================================
        #region Basic 2D Painters
        public void SetPixel(Point point, Color color)
        {
            //Set the single pixel of the Bitmap
            if (point.X>=bitmap.Width||point.X<0)
            {
                return;
            }
            if (point.Y>=bitmap.Height||point.Y<0)
            {
                return;
            }

            bitmap.SetPixel(point.X, point.Y, color);
        }


        //Based on Bresenham Algorithm
        public void SetLine(Point from, Point to, Color ca,Color cb)
        {
            if (from == to)
            {
                SetPixel(from, ca);
                return;
            }
            //Initialize the vars
            int dx = to.X - from.X;
            int dy = to.Y - from.Y;
            int sx = dx > 0 ? 1 : -1;
            int sy = dx > 0 ? 1 : -1;
            int dx2 = 2 * dx;
            int dy2 = 2 * dy;

            Vector4 va = new Vector4(ca.R, ca.G, ca.B, ca.A);
            Vector4 vb = new Vector4(cb.R, cb.G, cb.B, cb.A);
            //If the k is smaller than 1,use X to increase
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                int e = -dx; //e = -0.5*2*dx

                int x = from.X;
                int y = from.Y;

                

                for (x = from.X; x <= to.X; x += sx)
                {
                    Vector4 vcc = Vector4.Lerp(va, vb, (x - from.X) / (float)Math.Abs(dx));
                    Color color = Color.FromArgb((int)(vcc.W), (int)(vcc.X), (int)(vcc.Y), (int)(vcc.Z));

                    if (x >= bitmap.Width || x < 0)
                    {
                        break;
                    }
                    if (y >= bitmap.Height || x < 0)
                    {
                        break;
                    }

                    SetPixel(new Point(x, y),color);

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
                for (y = from.Y; y <= to.Y; y += sy)
                {

                    if (x >= bitmap.Width || x < 0)
                    {
                        break;
                    }
                    if (y >= bitmap.Height || x < 0)
                    {
                        break;
                    }

                    Vector4 vcc = Vector4.Lerp(va, vb, (y - from.Y) / (float)Math.Abs(dy));
                    Color color = Color.FromArgb((int)(vcc.W), (int)(vcc.X), (int)(vcc.Y), (int)(vcc.Z));


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
        public void SetTri(Point a,Point b,Point c,Color colora,Color colorb,Color colorc)
        {
            if (a.X==b.X&&a.X==c.X)
            {
                return;
            }
            if (a.Y==b.Y&&a.Y==c.Y)
            {
                return;
            }
            ////If itself is a kind of special triangle
            if (a.Y == b.Y)
            {
                if (c.Y <= a.Y) //Flat Bottom 
                {
                    SetTriFlatBottom(a, b, c, colora,colorb,colorc);
                }
                else //Flat Top  
                {
                    SetTriFlatTop(a, b, c, colora, colorb, colorc);
                }
                return;
            }
            else if (a.Y == c.Y)
            {
                if (b.Y <= a.Y) //Flat Bottom   
                {
                    SetTriFlatBottom(a, c, b, colora,colorc,colorb);
                }
                else //Flat Top  
                {
                    SetTriFlatTop(a, c, b, colora, colorc, colorb);
                }
                return;
            }
            else if (b.Y == c.Y)
            {
                if (a.Y <= b.Y) //Flat Bottom  
                {
                    SetTriFlatBottom(b, c, a, colorb,colorc,colora);
                }
                else // Flat Top
                {
                    SetTriFlatTop(b, c, a, colorb, colorc, colora);
                }
                return;
            }


            Point up = new Point();
            Point middle = new Point();
            Point bottom = new Point();

            Color upc = new Color();
            Color middlec = new Color();
            Color bottomc = new Color();
            //Find the related position of the three points
            if (a.Y < b.Y && b.Y < c.Y)
            {
                up = a;
                middle = b;
                bottom = c;

                upc = colora;
                middlec = colorb;
                bottomc = colorc;
            }
            else if (a.Y < c.Y && c.Y < b.Y)
            {
                up = a;
                middle = c;
                bottom = b;

                upc = colora;
                middlec = colorc;
                bottomc = colorb;
            }
            else if (b.Y < a.Y && a.Y < c.Y)
            {
                up = b;
                middle = a;
                bottom = c;

                upc = colorb;
                middlec = colora;
                bottomc = colorc;
            }
            else if (b.Y < c.Y && c.Y < a.Y)
            {
                up = b;
                middle = c;
                bottom = a;

                upc = colorb;
                middlec = colorc;
                bottomc = colora;
            }
            else if (c.Y < a.Y && a.Y < b.Y)
            {
                up = c;
                middle = a;
                bottom = b;

                upc = colorc;
                middlec = colora;
                bottomc = colorb;
            }
            else if (c.Y < b.Y && b.Y < a.Y)
            {
                up = c;
                middle = b;
                bottom = a;

                upc = colorc;
                middlec = colorb;
                bottomc = colora;
            }


            //The x of the long edge when it is on the y of the middle
            int xMiddle = (middle.Y - up.Y) * (bottom.X - up.X) / (bottom.Y - up.Y) + up.X;

            float kMiddle = Math.Abs((float)(bottom.Y - middle.Y) / (bottom.Y - up.Y));

            Vector4 tc = new Vector4(upc.R, upc.G, upc.B, upc.A);
            Vector4 bc = new Vector4(bottomc.R, bottomc.G, bottomc.B, bottomc.A);

            Vector4 mcv = Vector4.Lerp(bc, tc, kMiddle);

            Color anotherColor = Color.FromArgb((int)(mcv.W), (int)(mcv.X), (int)(mcv.Y), (int)(mcv.Z));

            Point anotherMiddle = new Point(xMiddle, middle.Y);

            //Divide the triangle into two pieces
            //Draw the upper one with flat bottom
            SetTriFlatBottom(anotherMiddle, middle, up, anotherColor,middlec,upc);
            //Draw the lower one with flat top 
            SetTriFlatTop(anotherMiddle, middle, bottom, anotherColor,middlec,bottomc);
               
            
        }
        public void SetTriFlatBottom(Point bottom1,Point bottom2,Point top, Color color1,Color color2,Color color)
        {
            if (bottom1.X>bottom2.X)
            {
                Point tmp = new Point();
                tmp = bottom1;
                bottom1 = bottom2;
                bottom2 = tmp;
                
                Color tp = new Color();
                tp =color1;
                color1 = color2;
                color2 = tp;
                
            }

            Vector4 vc1 = new Vector4(color1.R, color1.G, color1.B, color1.A);
            Vector4 vc2 = new Vector4(color2.R, color2.G, color2.B, color2.A);
            Vector4 vc = new Vector4(color.R, color.G, color.B, color.A);

            for (int y = bottom1.Y; y >= top.Y; y--)
            {
                float k = (float)(bottom1.Y - y) / (bottom1.Y - top.Y);

                Vector4 vcc1 = Vector4.Lerp(vc1, vc, k);
                Vector4 vcc2 = Vector4.Lerp(vc2, vc, k);

                Color c1 = Color.FromArgb((int)(vcc1.W), (int)(vcc1.X), (int)(vcc1.Y), (int)(vcc1.Z));
                Color c2 = Color.FromArgb((int)(vcc2.W), (int)(vcc2.X), (int)(vcc2.Y), (int)(vcc2.Z));

                int xs, xe;
                
                xs = (y - bottom1.Y) * (top.X - bottom1.X) / (top.Y - bottom1.Y) + bottom1.X;
                xe = (y - bottom2.Y) * (top.X - bottom2.X) / (top.Y - bottom2.Y) + bottom2.X;

                Point sp = new Point(xs, y);
                Point ep = new Point(xe, y);
                SetLine(sp, ep, c1, c2);
            }
        }
        public void SetTriFlatTop(Point top1,Point top2,Point bottom, Color color1,Color color2,Color color)
        {
            Vector4 vc1 = new Vector4(color1.R, color1.G, color1.B, color1.A);
            Vector4 vc2 = new Vector4(color2.R, color2.G, color2.B, color2.A);
            Vector4 vc = new Vector4(color.R, color.G, color.B, color.A);

            if (top1.X > top2.X)
            {
                Point tmp = new Point();
                tmp = top1;
                top1 = top2;
                top2 = tmp;
                
                Color tp = new Color();
                tp = color1;
                color1 = color2;
                color2 = tp;
                
            }
            for (int y = top1.Y; y <= bottom.Y; y++)
            {

                float k = (float)(y-top1.Y) / (bottom.Y - top1.Y);

                Vector4 vcc1 = Vector4.Lerp(vc1, vc, k);
                Vector4 vcc2 = Vector4.Lerp(vc2, vc, k);

                Color c1 = Color.FromArgb((int)(vcc1.W), (int)(vcc1.X), (int)(vcc1.Y), (int)(vcc1.Z));
                Color c2 = Color.FromArgb((int)(vcc2.W), (int)(vcc2.X), (int)(vcc2.Y), (int)(vcc2.Z));



                int xs, xe;
                xs = (y - bottom.Y) * (top1.X - bottom.X) / (top1.Y - bottom.Y) + bottom.X;
                xe = (y - bottom.Y) * (top2.X - bottom.X) / (top2.Y - bottom.Y) + bottom.X;
                Point sp = new Point(xs, y);
                Point ep = new Point(xe, y);
                SetLine(sp, ep, c1,c2);

               
            }
        }
        #endregion
        //==========================================================================

        #region CallBack Functions
        //Paint CallBack Function
        private void OnPaint(object sender, PaintEventArgs e)
        {
                
            
        }

        private void GraphicsUpdate(object sender,EventArgs e)
        {
            Clear(Color.Black);

            objs[0].ClearRotation();
            objs[0].AddRotation(new Vector3(0, 1, 0), duration*5 / 180f * (float)Math.PI);

            foreach (var obj in objs)
            {
                
                obj.UpdateDObject(camera);
                obj.Lighten(lights,camera);
                  DrawDObject(obj);
            }



            Point a = new Point(10, 10);
            Point b = new Point(40, 50);
            Point c = new Point(40, 10);
            Point d = new Point(10, 50);

            SetTri(a, b, c, objs[0].matureFaces[0].p1.color, objs[0].matureFaces[0].p2.color, objs[0].matureFaces[0].p3.color);
            SetTri(a, d, b, objs[0].matureFaces[1].p1.color, objs[0].matureFaces[1].p2.color, objs[0].matureFaces[1].p3.color);





            duration += 1f;



            camera.position.Y = 5 * (float)Math.Sin(duration/5);
            camera.position.X = 5 * (float)Math.Cos(duration/5);

            //lights[0].position.Y = 4 * (float)Math.Sin(duration / 5);
            //lights[0].position.X = 5 * (float)Math.Cos(duration / 5);




            bool flag = Monitor.TryEnter(bitmap, 1000);

            try
            {
                if (flag)
                {
                    graphics.DrawImage(bitmap, 0, 0);
                    Console.WriteLine("A frame has been drawn");
                }
                

            }
            catch (Exception)
            {

            }
            finally
            {
                if (flag)
                {
                    Monitor.Exit(bitmap);

                }
                
            }
            
            
        }
        #endregion
    }
}
