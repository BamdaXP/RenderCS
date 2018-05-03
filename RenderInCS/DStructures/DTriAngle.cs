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

        public Vector3 normal
        { get
            {
                Vector3 normal = Vector3.Cross(p2.vector - p1.vector, p3.vector - p2.vector);
                return normal;
            }

            private set { normal = value; }
        }

        public bool visible = false;

        public DTriAngle(DVertex _p1, DVertex _p2, DVertex _P3)
        {
            p1 = _p1;
            p2 = _p2;
            p3 = _P3;
        }

        public void SetVisibility(Camera camera)
        {
            Vector3 viewLine1 = Vector3.Subtract(p1.vector, camera.position);
            Vector3 viewLine2 = Vector3.Subtract(p2.vector, camera.position);
            Vector3 viewLine3 = Vector3.Subtract(p3.vector, camera.position);

            float i = Vector3.Dot(viewLine1, p1.normal);
            float j = Vector3.Dot(viewLine2, p2.normal);
            float k = Vector3.Dot(viewLine3, p3.normal);
            if (i>0&&j>0&&k>0)
            {
                visible = false;
            }else
            {
                visible = true;
            }
        }

        
    }
   
    
}
