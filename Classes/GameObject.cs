using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bouncing_Bravery
{
    class GameObject
    {
        public Model Model;
        public Vector3 Position;
        public bool isActive;
        public BoundingSphere BoundingSphere;

        public GameObject()
        {
            Model = null;
            Position = Vector3.Zero;
            isActive = false;
            BoundingSphere = new BoundingSphere();
        }
    }
}
