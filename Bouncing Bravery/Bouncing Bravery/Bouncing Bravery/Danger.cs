using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bouncing_Bravery
{
    class Danger
    {
        public Vector3 position;
        public bool isActive;
        public Matrix RotationMatrix;
        public Model model;
        public Matrix[] Transforms;
        public float scale;
        public BoundingBox dangerBox;

        public Danger()
        {
            isActive = true;
            RotationMatrix = Matrix.CreateRotationY(MathHelper.PiOver2);
        }


    }
}
