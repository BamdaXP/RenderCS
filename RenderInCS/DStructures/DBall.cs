using System;
using System.Numerics;
using System.Drawing;

namespace RenderCS
{
    class DMeshBall : DMesh
    {
        public float radius { get; }
        public int precition { get; }

        public Color color;

        public DMeshBall(float radius,int precition,Color color)
        {
            this.radius = radius;
            this.precition = precition;
            this.color = color;

            
            float fi = 0f;
            float sitar = 0f;

            for (int i = 0; i <= precition; i++)
            {
                if (i==0)
                {
                    AddVertex(0, radius, 0, color);

                    continue;
                }
                if (i==precition)
                {
                    AddVertex(0, -radius, 0, color);

                    break;
                }
                fi = (float)Math.PI * ((float)i / precition);
                for (int j = 0; j < precition; j++)
                {
                    sitar = (float)(2 * Math.PI) * ((float)j / precition);
                    float _x = radius*(float)Math.Cos(sitar);
                    float _z = radius*(float)Math.Sin(sitar);
                    float _y = radius*(float)Math.Cos(fi);

                    AddVertex(_x, _y, _z, color);
             
                    if (i==1)
                    {
                        if (j>0)
                        {
                            if (j==precition-1)
                            {
                                AddFace(0, FindIndex(i, j), FindIndex(i, 0));
                            }
                            else
                            {
                                AddFace(0, FindIndex(i, j), FindIndex(i, j-1));
                            }
                        }
                    }
                    else
                    {
                        if (j>0)
                        {
                            AddFace(FindIndex(i - 1, j - 1), FindIndex(i, j - 1), FindIndex(i, j));
                            AddFace(FindIndex(i - 1, j - 1), FindIndex(i, j), FindIndex(i - 1, j));
                        }
                    }

                }

            }

            for (int i = 0; i < precition; i++)
            {
                if (i==precition-1)
                {
                    AddFace(FindIndex(precition - 1, i), FindIndex(precition, 0), FindIndex(precition - 1, 0));
                }
                AddFace(FindIndex(precition - 1, i), FindIndex(precition, 0), FindIndex(precition - 1, i + 1));
            }
            
        }
        
        
        
        private int FindIndex(int i,int j)
        {
            if (i<=0||j<0)
            {
                return 0;
            }
            if (i>=precition||j>=precition)
            {
                return vertexCount-1;
            }

            int index = (i - 1) * precition + j + 1;
            return index;

        }
    }
}
