using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;


namespace RenderInCS
{
    //The Object to Render
    public class DMesh
    {
        
        public List<DVertex> oriVertexes;
        public List<DVertex> matureVertexes;
        public List<DTriAngle> faces;
        public Vector3 worldPosition;


        public void AddTri(int index1, int index2, int index3)
        {
            //Check for the args
            if (index1==index2||index1==index3||index2==index3)
            {
                return;
            }
            if (index1>=oriVertexes.LongCount()||index2>= oriVertexes.LongCount()||index3>= oriVertexes.LongCount())
            {
                return;
            }

            //It's very good because they are all references!!
            DVertex a = oriVertexes[index1];
            DVertex b = oriVertexes[index2];
            DVertex c = oriVertexes[index3];
            DTriAngle tri = new DTriAngle(a,b,c);
            //faces.Add(tri);
        }

        public void MaturateVertexes()
        {

        }

        public void ClearTri()
        {
            faces.Clear();
        }

        public void Render()
        {

        }
    }
}
