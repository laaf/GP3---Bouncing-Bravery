using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bouncing_Bravery
{
    class DangerFactory
    {
        // Danger creation functions


        public static Danger PassableDanger(Vector3 pos, DataManager data)
        {
            Danger danger = new Danger();
            danger.position = pos;
            danger.model = data.danger1;
            danger.Transforms = data.danger1Transforms;
            danger.scale = 1.0f;
            danger.dangerBox = new BoundingBox(pos, new Vector3(pos.X + 5, 10, pos.Z + 5));
            return danger;
        }

        public static Danger UnpassableDanger(Vector3 pos, DataManager data)
        {
            Danger danger = new Danger();
            danger.position = pos;
            danger.model = data.danger2;
            danger.Transforms = data.danger2Transforms;
            danger.scale = 0.4f;
            danger.dangerBox = new BoundingBox(pos, new Vector3(pos.X + 5, 30, pos.Z + 5));
            return danger;
        }
    }
}
