
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace RenderCS
{
    //The Object to Render
    public class DMesh
    {
        public int vertexCount { get; private set; }
        public int faceCount { get; private set; }



        public List<DVertex> vertexes;
        //public List<DVertex> matureVertexes;
        public List<int[]> faces;
        //public List<DTriAngle> matureFaces;

        public DMesh()
        {
            vertexes = new List<DVertex>();
            faces = new List<int[]>();
        }

        public void AddVertex(DVertex v)
        {
            vertexes.Add(v);
            vertexCount++;
        }

        public void AddFace(int index1, int index2, int index3)
        {
            //Check for the args
            if (index1==index2||index1==index3||index2==index3)
            {
                return;
            }
            if (index1>=vertexes.LongCount()||index2>= vertexes.LongCount()||index3>= vertexes.LongCount())
            {
                return;
            }

            int[] array = { index1, index2, index3 };
            faces.Add(array);
            faceCount++;
        }


        //Clear all the faces
        public void ClearFaces()
        {
            faces.Clear();
        }

 
    }
}
