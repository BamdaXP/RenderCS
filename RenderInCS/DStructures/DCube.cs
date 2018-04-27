using System.Numerics;
using System.Drawing;
namespace RenderCS
{
    class DCube:DMesh
    {
        public float len, width, heigt;

        public DCube(Vector3 _pos,float _len,float _height,float _width)
        {
            worldPosition = _pos;
            len = _len;
            width = _width;
            heigt = _height;

            //front

            vertexes.Add(new DVertex(len / 2, heigt / 2, width / 2) { color = Color.AliceBlue});//0
            vertexes.Add(new DVertex(-len / 2, heigt / 2, width / 2) { color = Color.Red });//1
            vertexes.Add(new DVertex(len / 2, -heigt / 2, width / 2) { color = Color.Yellow });//2
            vertexes.Add(new DVertex(-len / 2, -heigt / 2, width / 2) { color = Color.Green });//3

            vertexes.Add(new DVertex(len / 2, heigt / 2, -width / 2) { color = Color.Gray });//4
            vertexes.Add(new DVertex(-len / 2, heigt / 2, -width / 2) { color = Color.HotPink });//5
            vertexes.Add(new DVertex(len / 2, -heigt / 2, -width / 2) { color = Color.Ivory });//6
            vertexes.Add(new DVertex(-len / 2, -heigt / 2, -width / 2) { color = Color.Moccasin });//7



            //front
            faces.Enqueue(new DTriAngle(vertexes[0], vertexes[1], vertexes[2]));
            faces.Enqueue(new DTriAngle(vertexes[1], vertexes[3], vertexes[2]));
            //behind
            faces.Enqueue(new DTriAngle(vertexes[4], vertexes[5], vertexes[6]));
            faces.Enqueue(new DTriAngle(vertexes[5], vertexes[7], vertexes[6]));

            //top
            faces.Enqueue(new DTriAngle(vertexes[4], vertexes[5], vertexes[0]));
            faces.Enqueue(new DTriAngle(vertexes[5], vertexes[1], vertexes[0]));
            //bottom
            faces.Enqueue(new DTriAngle(vertexes[6], vertexes[7], vertexes[2]));
            faces.Enqueue(new DTriAngle(vertexes[7], vertexes[3], vertexes[2]));
            //left
            faces.Enqueue(new DTriAngle(vertexes[1], vertexes[5], vertexes[3]));
            faces.Enqueue(new DTriAngle(vertexes[5], vertexes[7], vertexes[3]));
            //right
            faces.Enqueue(new DTriAngle(vertexes[4], vertexes[0], vertexes[2]));
            faces.Enqueue(new DTriAngle(vertexes[0], vertexes[2], vertexes[6]));

        }
    }
}
