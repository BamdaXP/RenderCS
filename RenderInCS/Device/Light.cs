using System;
using System.Numerics;
using System.Drawing;
namespace RenderCS
{
    public class Light
    {
        public float intensity = 1f;
        public Vector3 position;
        public Color ambient = Color.Red;
        public Color fusion = Color.White;
        public Color perspect = Color.Yellow;

        public float ambientK = 0.1f;
        public float fusionK = 0.1f;
        public float perspectK = 1f;
        public float perspectN = 2f;

        public Light(Vector3 _pos)
        {
            position = _pos;
        }

        public void LightenVertex(DVertex dv,Camera camera)
        {
            Vector3 ori = new Vector3(dv.color.R, dv.color.G, dv.color.B);
            Vector3 amb = new Vector3(ambient.R, ambient.G, ambient.B);
            Vector3 fus = new Vector3(fusion.R, fusion.G, fusion.B);
            Vector3 per = new Vector3(perspect.R, perspect.G, perspect.B);


            Vector3 vi = Vector3.Subtract(position, dv.ToVector());
            Vector3 vo = Vector3.Subtract(camera.position, dv.ToVector());
            float cosi = Vector3.Dot(vi, dv.normal) / (vi.Length() * dv.normal.Length());
            float coso = Vector3.Dot(vo, dv.normal) / (vo.Length() * dv.normal.Length());

            Vector3 colorV = amb * ambientK + fus * cosi * fusionK + per * (float)Math.Pow(coso, perspectN) * perspectK;

            int r = (int)(ori.X * colorV.X / 255);
            if (r >255)
            {
                r = 255;
            }
            if (r<0)
            {
                r = 0;
            }


            int g = (int)(ori.Y * colorV.Y / 255);
            if (g > 255)
            {
                g = 255;
            }
            if (g < 0)
            {
                g = 0;
            }
            int b = (int)(ori.Z * colorV.Z / 255);
            if (b > 255)
            {
                b = 255;
            }
            if (b < 0)
            {
                b = 0;
            }


            Color color = Color.FromArgb(255,r,g,b);

            dv.color = color;

        }
    }
}
