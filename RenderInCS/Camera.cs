using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace RenderInCS
{
    public class Camera
    {
        public Vector3 position;
        public Vector3 towards;

        public float wilderness;
        public float nearWidth;
        public float nearHeight;
        public float zNearPlane;
        public float zFarPlane;

        

    }


    public class ViewPort
    {
        public int x;
        public int y;
        //The size of yhe viewport
        public int width;
        public int height;
        public int minZ;//min depth
        public int maxZ;//max depth

    }



}
