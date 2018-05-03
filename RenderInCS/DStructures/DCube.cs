using System.Numerics;
using System.Drawing;
namespace RenderCS
{
    class DCubeMesh:DMesh
    {
        public float len { get; }
        public float height { get; }
        public float width { get; }

        public DCubeMesh(float _len,float _height,float _width)
        {
            
            len = _len;
            width = _width;
            height = _height;

            //front
            
            AddVertex(len / 2, height / 2, width / 2,Color.White);//0
            AddVertex(-len / 2, height / 2, width / 2,Color.White);//1
            AddVertex(len / 2, -height / 2, width / 2,Color.White);//2
            AddVertex(-len / 2, -height / 2, width / 2,Color.White);//3

            AddVertex(len / 2, height / 2, -width / 2,Color.White);//4
            AddVertex(-len / 2, height / 2, -width / 2,Color.White);//5
            AddVertex(len / 2, -height / 2, -width / 2,Color.White);//6
            AddVertex(-len / 2, -height / 2, -width / 2,Color.White);//7



            //front
            AddFace(1, 2, 0);
            AddFace(1, 3, 2);
            //behind
            AddFace(4, 7, 5);
            AddFace(4, 6, 7);         

            //top
            AddFace(5, 0, 4);
            AddFace(5, 1, 0);
            //bottom
            AddFace(3, 6, 2);
            AddFace(3, 7, 6);
            //left
            AddFace(5, 3, 1);
            AddFace(5, 7, 3);
            //right
            AddFace(0, 6, 4);
            AddFace(0, 2, 6);

        }
    }
}
