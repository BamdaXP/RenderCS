using System.Drawing;
using System.Numerics;

namespace RenderCS.Device
{
    public class Viewport
    {
        //The position on the screen
        public Point position = new Point(0, 0);

        //The size of yhe viewport
        public int width;
        public int height;

        //The mutilpier for the real vertex transfering into a pixel point
        public float scale = 100;

        public Viewport(int _w, int _h)
        {
            width = _w;
            height = _h;
        }

    }
}
