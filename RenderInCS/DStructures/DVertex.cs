using System;
using System.Drawing;
using System.Numerics;

namespace RenderCS
{
    public class DVertex
    {
        public float x;
        public float y;
        public float z;
        public float w = 1f;

        public Color color;
        public Vector3 vector
        {
            get
            {
                Vector3 v = new Vector3(x, y, z);
                return v;
            }

        }
        public Vector3 normal;
       
        #region four kinds of constructor
        public DVertex(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;

            //Matrix matrix = new Matrix();
            //vertexes.Add(this);
        }
        public DVertex()
        {
            x = 0f;
            y = 0f;
            z = 0f;

        }
        public DVertex(Vector3 v)
        {
            x = v.X;
            y = v.Y;
            z = v.Z;
        }
        public DVertex(DVertex v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            color = v.color;
        }
        #endregion

        //transfer real vertex into the point of the viewport

        public void Assign(DVertex v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            color = v.color;
        }

        


        public Point ToScreenPoint(ViewPort view)
        {
            Point point = new Point();

            point.X = (int)(x * view.scale) - view.position.X + view.width / 2;
            point.Y = (int)(view.height - y * view.scale + view.position.Y) - view.height / 2;

            return point;
        }

        public void Translate(Vector3 translate)
        {
            x = x + translate.X;
            y = y + translate.Y;
            z = z + translate.Z;
        }


        public void Rotate(float angle, Vector3 axis)
        {

            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            float u = axis.X;
            float v = axis.Y;
            float w = axis.Z;
            float x0 = x;
            float y0 = y;
            float z0 = z;
            x = (u * u + (1f - u * u) * cos) * x0 + (u * v * (1f - cos) - w * sin) * y0 + (u * w * (1f - cos) + v * sin) * z0;
            y = (u * v * (1f - cos) + w * sin) * x0 + (v * v + (1f - v * v) * cos) * y0 + (v * w * (1f - cos) - u * sin) * z0;
            z = (u * w * (1f - cos) - v * sin) * x0 + (v * w * (1f - cos) + u * sin) * y0 + (w * w + (1f - w * w) * cos) * z0;
        }
        #region Matix Transfer
        public void PosRelatedTransfer(Vector3 pos)
        {
            x += pos.X;
            y += pos.Y;
            z += pos.Z;
        }

        public void CameraRelatedTransfer(Camera camera)
        {
            x -= camera.position.X;
            y -= camera.position.Y;
            z -= camera.position.Z;
        }

        public void PerspectiveTransfer(Camera camera)
        {
            if (z == 0)
            {
                return;
            }
            x = -x*(float)Math.Cos(camera.fovy/2)/ (float)Math.Sin(camera.fovy / 2)/camera.aspect/z;
            y = -y * (float)Math.Cos(camera.fovy / 2) / (float)Math.Sin(camera.fovy / 2)/z;
            z = (camera.farZ + camera.nearZ) / (camera.farZ - camera.nearZ) + 2 *w* camera.farZ * camera.nearZ / (camera.farZ - camera.nearZ)/z;
            //w = -z;
        }

        #endregion


    }



}
