using System.Drawing;
using System.Numerics;
//using Microsoft.DirectX;
//using Microsoft.DirectX.Direct3D;

namespace RenderCS
{
    public class Camera
    {
        public Vector3 position;
        public Vector3 towards;

        public float nearWidth = 4;
        public float nearHeight = 8;
        public float zNearPlane = 2;
        public float zFarPlane = 200;

        public Camera()
        {
            position = new Vector3(0, 0, 0);
            towards = new Vector3(0, 0, 1);
        }

    }

    public class ViewPort
    {
        //The position on the screen
        public Point position = new Point(0,0);

        //The size of yhe viewport
        public int width;
        public int height;

        //public int minZ;//min depth
        //public int maxZ;//max depth

        public float scale = 100;//The mutilpier for the real vertex transfering into a pixel point

        public ViewPort(int _w,int _h)
        {
            width = _w;
            height = _h;
        }

    }



}
