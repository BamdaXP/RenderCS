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
        public float w;//The depth

        public Color color;

        #region three kinds of constructor
        public DVertex(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
            w = 1f;
            //Matrix matrix = new Matrix();
            //vertexes.Add(this);
        }
        public DVertex()
        {
            x = 0f;
            y = 0f;
            z = 0f;
            w = 1f;
        }
        public DVertex(Vector3 v)
        {
            x = v.X;
            y = v.Y;
            z = v.Z;
            w = 1f;
        }
        #endregion

        public bool visible = true;

        //transfer real vertex into the point of the viewport


        public Vector3 ToVector()
        {
            Vector3 v = new Vector3(x, y, z);
            return v;
        }


        public Point ToViewPort(ViewPort view)
        {
            Point point = new Point();

            point.X = (int)(x * view.scale) - view.position.X + view.width / 2;
            point.Y = (int)(view.height - y * view.scale + view.position.Y) - view.height / 2;

            return point;
        }

        public DVertex Translate(Vector3 translate)
        {
            DVertex vertex = new DVertex();
            vertex.x = x + translate.X;
            vertex.y = y + translate.Y;
            vertex.z = z + translate.Z;
            return vertex;
        }


        public DVertex Rotate(float angle, Vector3 axis)
        {
            DVertex vertex = new DVertex();
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;
            vertex.x = ((1f - cos) * x * x + cos) * this.x + ((1f - cos) * x * y + sin * z) * this.y + ((1f - cos) * x * z - sin * y) * this.z;
            vertex.y = ((1f - cos) * x * y - sin * z) * this.x + ((1f - cos) * y * y + cos) * this.y + ((1f - cos) * y * z + sin * x) * this.z;
            vertex.z = ((1f - cos) * x * z + sin * y) * this.x + ((1f - cos) * y * z - sin * x) * this.y + ((1f - cos) * z * z + cos) * this.z;
            return vertex;
        }


        public Point ToScreenPoint(DMesh mesh,Camera camera, ViewPort view)
        {
            DVertex vertex = new DVertex(x,y,z);
            

            foreach (var rotation in mesh.rotations)
            {
                vertex = vertex.Rotate(rotation.angle, rotation.axis);
            }

            return vertex.PosRelatedTransfer(mesh.worldPosition).CameraRelatedTransfer(camera).PerspectiveTransfer(camera).ToViewPort(view);
        }




        #region Matix Transfer
        public DVertex PosRelatedTransfer(Vector3 pos)
        {
            DVertex vertex = new DVertex(x + pos.X, y + pos.Y, z + pos.Z);
            return vertex;
        }

        public DVertex CameraRelatedTransfer(Camera camera)
        {
            DVertex vertex = new DVertex(x - camera.position.X, y - camera.position.Y, z - camera.position.Z);
            return vertex;
        }

        public DVertex PerspectiveTransfer(Camera camera)
        {
            if (z == 0)
            {
                return this;
            }
            DVertex vertex = new DVertex();

            vertex.x = 2 * camera.zNearPlane * x / (camera.nearWidth * z);
            vertex.y = 2 * camera.zNearPlane * y / (camera.nearWidth * z);
            vertex.z = camera.zFarPlane * (z - camera.zNearPlane) / (z * (camera.zFarPlane - camera.zNearPlane));


            return vertex;
        }

        #endregion


    }



}
