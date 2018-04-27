using System;
using System.Drawing;
using System.Collections.Generic;
using System.Numerics;



namespace RenderCS
{
   
    /// <summary>
    /// TriAngle
    /// </summary>
    public class DTriAngle
    {
        //Clockwise rotation is positive
        public DVertex p1;
        public DVertex p2;
        public DVertex p3;

        public bool visible = true;

        public DTriAngle(DVertex _p1, DVertex _p2, DVertex _P3)
        {
            p1 = _p1;
            p2 = _p2;
            p3 = _P3;
        }

        public void SetVisibility(Camera camera)
        {
            if (Vector3.Dot(camera.towards,NormalVector())<0)
            {
                visible = false;
            }
        }

        public Vector3 NormalVector()
        {
            Vector3 normal = Vector3.Cross(p2.ToVector() - p1.ToVector(), p3.ToVector() - p1.ToVector());
            return normal / normal.Length();
        }
    }
   
    
}
