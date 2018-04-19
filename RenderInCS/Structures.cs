using System;
using System.Drawing;
using System.Collections.Generic;

//Mainly use the Mathmatical Library of the DX
//Matrix calculation and Vector calculation for example
using Microsoft.DirectX;


namespace CSDirectX
{
    public class DObject
    {
        //The related camera
        public static Camera camera;
        //The real vertexes of this object
        public List<DPoint> vertexes;
        //The Points that has transfered into the ViewPort
        public List<Point> viewPoints;


        static DObject()
        {

        }
        public void UpdatePoints()
        {
            
        }

    }
    /// <summary>
    /// Vector
    /// </summary>
    public class Camera
    {
        public DPoint position;
        public Vector3 towards;
        public float wilderness;//The angle of the Perspective viewport
    }


    /// <summary>
    /// Point
    /// </summary>
    public class DPoint:DObject
    {
        public float x;
        public float y;
        public float z;
        public float w;//The depth

        public DPoint(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
            w = 1f;
            //Matrix matrix = new Matrix();
            vertexes.Add(this);
        }

        public DPoint(Vector3 v)
        {
            x = v.X;
        }

        public Vector3 ToVector()
        {
            Vector3 v = new Vector3(x, y, z);
            return v;
        }
    }

    /// <summary>
    /// Line
    /// </summary>
    public class DLine : DObject
    {
        public DPoint from;
        public DPoint to;

        public DLine(DPoint _f, DPoint _t)
        {
            from = _f;
            to = _t;
            vertexes.Add(from);
            vertexes.Add(to);
        }
    }


    /// <summary>
    /// TriAngle
    /// </summary>
    public class DTriAngle : DObject
    {
        public DPoint p1;
        public DPoint p2;
        public DPoint p3;

        public DLine l1 { get; private set; }
        public DLine l2 { get; private set; }
        public DLine l3 { get; private set; }

        public DTriAngle(DPoint _p1, DPoint _p2, DPoint _P3)
        {
            p1 = _p1;
            p2 = _p2;
            p3 = _P3;

            UpdateLine();

            vertexes.Add(p1);
            vertexes.Add(p2);
            vertexes.Add(p3);
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
