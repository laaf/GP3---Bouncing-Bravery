using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bouncing_Bravery
{
    class GameConstants
    {
        //screen constants
        public const int ScreenX = 800;
        public const int ScreenY = 600;

        //camera constants
        public const float nearClip = 1.0f;
        public const float farClip = 1000.0f;
        public const float viewAngle = MathHelper.PiOver4; //45 degrees
        public const float thirdPersonRef = 30f;

        //gravity constants
        public const float floorHeight = 9.0f;
        public const float jumpSpeed = 10.0f;
        public const float gravity = -9.81f * 2.05f;
            
        //physics constants
        public const float coefficientRestitution = 0.81f;
        public const float airResistance = 0.1f;

        //time constants
        public const float timeScale = 2.0f;

        //movement constants
        public const float movementRate = 30.0f;
        public const float speedThreshold_AllowJump = 5.0f;

        //stars constants
        public const int MaxNumStars = 100;

        //danger constants
        public const float scaleDanger = 0.4f;

        //playfield constants
        public const float PlayfieldSizeX = 173;
        public const float PlayfieldSizeZ = 170;

        //collision constants
        public const float scaleHero = 1f;
        public const float scaleStar = 0.1f;

        //tile constants
        public const float tileSize = 7.0f;

        //map constants
        public const int amountOfRows= 5;
        public const int amountOfBlocksPerRow = 5;
        public const int amountOfLinesPerBlock = 11;

    }
}
