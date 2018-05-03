using System.Collections.Generic;
using System.Numerics;
namespace RenderCS
{
    public class DObject
    {
        public class Rotation
        {
            public Vector3 axis;
            public float angle;

            public Rotation(Vector3 _axis, float _angle = 0)
            {
                axis = _axis;
                angle = _angle;
            }
        }

        public DMesh mesh;
        public Vector3 pos;
        public List<DVertex> matureVertexes;
        public List<DTriAngle> matureFaces;
        public List<Rotation> rotations;

        public DObject(Vector3 pos,DMesh mesh)
        {
            matureVertexes = new List<DVertex>();
            matureFaces = new List<DTriAngle>();
            rotations = new List<Rotation>();

            this.mesh = mesh;
            this.pos = pos;

            foreach (var v in mesh.vertexes)
            {
                DVertex mv = new DVertex(v);
                matureVertexes.Add(mv);
            }

            LinkFaces();
        }

        public void AddRotation(Vector3 axis, float angle)
        {
            rotations.Add(new Rotation(axis, angle));
        }
        public void ClearRotation()
        {
            rotations.Clear();
        }
        

        private void MaturateVertexes(Camera camera)
        {

            for (int i = 0; i < mesh.vertexCount; i++)
            {
                DVertex v = matureVertexes[i];
                v.Assign(mesh.vertexes[i]);
                v.normal = mesh.vertexes[i].vector;

                foreach (var rotation in rotations)
                {
                    v.Rotate(rotation.angle, rotation.axis);
                }

                v.Translate(pos);
                v.Translate(-camera.position);
                v.PerspectiveTransfer(camera);
            }
               
            
        }

        private void LinkFaces()
        {
            foreach (var array in mesh.faces)
            {
                DTriAngle tri = new DTriAngle(matureVertexes[array[0]], matureVertexes[array[1]], matureVertexes[array[2]]);
                matureFaces.Add(tri);
            }
        }

        private void FadeBehind(Camera camera)
        {
            foreach (var tri in matureFaces)
            {
                tri.SetVisibility(camera);
            }
        }

        public  void Lighten(List<Light> lights,Camera camera)
        {
            foreach (var light in lights)
            {
                foreach (var v in matureVertexes)
                {
                    light.LightenVertex(v,camera);
                }
            }
        }

        public void UpdateDObject(Camera camera)
        {
            MaturateVertexes(camera);
            FadeBehind(camera);
        }
    }
}
