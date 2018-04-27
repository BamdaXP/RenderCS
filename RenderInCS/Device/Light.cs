using System.Numerics;
using System.Drawing;
namespace RenderCS
{
    class Light
    {
        public float intensity = 1f;
        public Vector3 position;
        public Color ambient;
        public Color fusion;
        public Color perspect;


        public Light(Vector3 _pos)
        {
            position = _pos;
        }
    }
}
