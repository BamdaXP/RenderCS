using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace RenderCS
{
    class DBall : DMesh
    {
        public float radius { get; private set; }
        public int precition { get; private set; }

        public DBall(Vector3 _pos,float radius,int precition,Color color)
        {
            worldPosition = _pos;
            float stepAngle = (float)Math.PI / precition;

            int countSitar = 0;
            int countFi = 0;
            float fi = 0f;
            float sitar = 0f;

            vertexes.Add(new DVertex(0, radius, 0));
            for (fi = stepAngle; countFi < precition-2; fi += stepAngle,countFi++)
            {
                for (sitar = 0;  countSitar<precition-1;sitar+= stepAngle,countSitar++)
                {
                    vertexes.Add(new DVertex(radius * (float)Math.Cos(sitar), radius * (float)Math.Cos(fi), radius * (float)Math.Sin(sitar)));
                }
            }

            for (int i = 0; i < countFi; i++)
            {
                for (int j = 0; j < countSitar; j++)
                {

                }
            }
        }
        

    }
}
