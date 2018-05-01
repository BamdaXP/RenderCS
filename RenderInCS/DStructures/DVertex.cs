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
            normal = v.normal;
            color = v.color;
        }
        #endregion

        //transfer real vertex into the point of the viewport

        public void Assign(DVertex v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            normal = v.normal;
            color = v.color;
        }

        public Vector3 ToVector()
        {
            Vector3 v = new Vector3(x, y, z);
            return v;
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
            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;
            this.x = ((1f - cos) * x * x + cos) * this.x + ((1f - cos) * x * y + sin * z) * this.y + ((1f - cos) * x * z - sin * y) * this.z;
            this.y = ((1f - cos) * x * y - sin * z) * this.x + ((1f - cos) * y * y + cos) * this.y + ((1f - cos) * y * z + sin * x) * this.z;
            this.z = ((1f - cos) * x * z + sin * y) * this.x + ((1f - cos) * y * z - sin * x) * this.y + ((1f - cos) * z * z + cos) * this.z;
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
