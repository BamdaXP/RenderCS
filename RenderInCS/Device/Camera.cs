using System;
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

        public float fovy = 20f / 180f * (float)Math.PI;
        public float aspect = 1f;
        public float farZ = 100f;
        public float nearZ = 1f;


        public Camera()
        {
            position = new Vector3(0, 0, 0);
            towards = new Vector3(0, 0, -1);
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
