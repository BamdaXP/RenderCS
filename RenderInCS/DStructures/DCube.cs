using System.Numerics;
using System.Drawing;
namespace RenderCS
{
    class DCubeMesh:DMesh
    {
        public float len, width, heigt;

        public DCubeMesh(float _len,float _height,float _width)
        {
            
            len = _len;
            width = _width;
            heigt = _height;

            //front
            
            AddVertex(new DVertex(len / 2, heigt / 2, width / 2) {normal = new Vector3(1,1,1), color = Color.White});//0
            AddVertex(new DVertex(-len / 2, heigt / 2, width / 2) { normal = new Vector3(-1, 1, 1), color = Color.White });//1
            AddVertex(new DVertex(len / 2, -heigt / 2, width / 2) { normal = new Vector3(1, -1, 1), color = Color.White });//2
            AddVertex(new DVertex(-len / 2, -heigt / 2, width / 2) { normal = new Vector3(-1, -1, 1), color = Color.White });//3

            AddVertex(new DVertex(len / 2, heigt / 2, -width / 2) { normal = new Vector3(1, 1, -1), color = Color.White });//4
            AddVertex(new DVertex(-len / 2, heigt / 2, -width / 2) { normal = new Vector3(-1, 1, -1), color = Color.White });//5
            AddVertex(new DVertex(len / 2, -heigt / 2, -width / 2) { normal = new Vector3(1, -1, -1), color = Color.White });//6
            AddVertex(new DVertex(-len / 2, -heigt / 2, -width / 2) { normal = new Vector3(-1, -1, -1), color = Color.White });//7



            //front
            AddFace(0, 1, 2);
            AddFace(2, 1, 3);
            //behind
            AddFace(5, 4, 7);
            AddFace(7, 4, 6);         

            //top
            AddFace(4, 5, 0);
            AddFace(0, 5, 1);
            //bottom
            AddFace(2, 3, 6);
            AddFace(6, 3, 7);
            //left
            AddFace(1, 5, 3);
            AddFace(3, 5, 7);
            //right
            AddFace(4, 0, 6);
            AddFace(6, 0, 2);

        }
    }
}
