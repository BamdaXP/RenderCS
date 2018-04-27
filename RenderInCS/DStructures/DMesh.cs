using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;


namespace RenderCS
{
    //The Object to Render
    public class DMesh
    {
        public class Rotation
        {
            public Vector3 axis;
            public float angle;
            

            public Rotation(Vector3 _axis,float _angle = 0)
            {
                axis = _axis;
                angle = _angle;
            }
        }



        public List<DVertex> vertexes;
        //public List<DVertex> matureVertexes;
        public Queue<DTriAngle> faces;
        //public List<DTriAngle> matureFaces;

        public Vector3 worldPosition;

        public List<Rotation> rotations;

        public DMesh()
        {
            vertexes = new List<DVertex>();
            faces = new Queue<DTriAngle>();
            rotations = new List<Rotation>();
        }

        public void AddTri(int index1, int index2, int index3)
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

            //It's very good because they are all references!!
            DVertex a = vertexes[index1];
            DVertex b = vertexes[index2];
            DVertex c = vertexes[index3];
            DTriAngle tri = new DTriAngle(a,b,c);
            faces.Enqueue(tri);
        }


        //Clear all the faces
        public void ClearTri()
        {
            faces.Clear();
        }

        public void AddRotation(Vector3 axis,float angle)
        {
            rotations.Add(new Rotation(axis, angle));
        }
        public void ClearRotation()
        {
            rotations.Clear();
        }


        public void FadeBehind(Camera camera)
        {
            foreach (var tri in faces)
            {
                tri.SetVisibility(camera);
            }
        }
 
    }
}
