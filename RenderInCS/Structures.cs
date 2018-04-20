using System;
using System.Drawing;
using System.Collections.Generic;
//Mainly use the Mathmatical Library of the DX
//Matrix calculation and Vector calculation for example
using Microsoft.DirectX;



namespace RenderInCS
{
    /// <summary>
    /// Point
    /// </summary>
    public class DVertex
    {
        public float x;
        public float y;
        public float z;
        public float w;//The depth

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

        public Vector3 ToVector()
        {
            Vector3 v = new Vector3(x, y, z);
            return v;
        }

        #region Matix Transfer
        public DVertex PerspectiveTransfer(Camera camera)
        {
            DVertex vertex = new DVertex();

            vertex.x = 2 * camera.zNearPlane * x / (w * z);
            vertex.y = 2 * camera.zNearPlane * y / (w * z);
            vertex.z = camera.zFarPlane * (z - camera.zNearPlane) / (z * (camera.zFarPlane - camera.zNearPlane));


            return vertex;
        }

        public DVertex CameraRelatedTransfer(Camera camera)
        {
            Vector3 thisVector = ToVector();

            DVertex vertex = new DVertex(x - camera.position.X, y - camera.position.Y, z - camera.position.Z);
            
            //float in radian 
            float angle = Vector3.Dot(camera.towards, thisVector) / (camera.towards.Length() * thisVector.Length());

            //calculate the sign the the angle using the cross product
            if (Vector3.Cross(camera.towards,thisVector).Y<0)
            {
                angle = -angle;
            }

            return vertex.RotateByY(angle);
        }

        //The translative displacement
        public DVertex Translate(Vector3 translate)
        {
            DVertex vertex = new DVertex();
            vertex.x = x + translate.X;
            vertex.y = y + translate.Y;
            vertex.z = z + translate.Z;
            return vertex;
        }



        //The angle is in degree NOT in radian
        //Rotate by X axis
        public DVertex RotateByX(float angle)
        {
            DVertex vertex = new DVertex();
            float sitar = angle / 180 * (float)Math.PI;

            vertex.y = y;
            vertex.z = z * (float)Math.Cos(sitar) - x * (float)Math.Sin(sitar);
            vertex.x = z * (float)Math.Sin(sitar) + x * (float)Math.Cos(sitar);

            return vertex;
        }
        //Rotate by Y axis
        public DVertex RotateByY(float angle)
        {
            DVertex vertex = new DVertex();
            float sitar = angle / 180 * (float)Math.PI;

            vertex.x = x;
            vertex.y = y * (float)Math.Cos(sitar) - z * (float)Math.Sin(sitar);
            vertex.z = y * (float)Math.Sin(sitar) + z * (float)Math.Cos(sitar);

            return vertex;
        }
        //Rotate by Z axis
        public DVertex RotateByZ(float angle)
        {
            DVertex vertex = new DVertex();
            float sitar = angle / 180 * (float)Math.PI;

            vertex.z = z;
            vertex.x = y * (float)Math.Cos(sitar) - x * (float)Math.Sin(sitar);
            vertex.y = y * (float)Math.Sin(sitar) + x * (float)Math.Cos(sitar);

            return vertex;
        }

        #endregion
    }

    /// <summary>
    /// Line
    /// </summary>
    public class DLine
    {
        public DVertex from;
        public DVertex to;

        public DLine(DVertex _f, DVertex _t)
        {
            from = _f;
            to = _t;
            //vertexes.Add(from);
            //vertexes.Add(to);
        }
    }


    /// <summary>
    /// TriAngle
    /// </summary>
    public class DTriAngle
    {
        public DVertex p1;
        public DVertex p2;
        public DVertex p3;

        public DLine l1 { get; private set; }
        public DLine l2 { get; private set; }
        public DLine l3 { get; private set; }

        public DTriAngle(DVertex _p1, DVertex _p2, DVertex _P3)
        {
            p1 = _p1;
            p2 = _p2;
            p3 = _P3;

            UpdateLine();

            //vertexes.Add(p1);
            //vertexes.Add(p2);
            //vertexes.Add(p3);
        }

        public void UpdateLine()
        {
            l1.from = p1;
            l1.to = p2;
            l2.from = p2;
            l2.to = p3;
            l3.from = p3;
            l3.to = p1;
        }
    }
   
    
}
