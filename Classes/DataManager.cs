using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Bouncing_Bravery
{
    class DataManager
    {
        public int heroLives;
        public float starsCaptured;
        public float timeElapsed;
        public int cameraThirdPerson;
        public bool isPaused;
        public bool HeroDead;
        public bool gameWon;
        public int NumStars;
        public bool isSuper;
        public Model danger1;
        public Matrix[] danger1Transforms;
        public Model danger2;
        public Matrix[] danger2Transforms;
        public bool isMenu;
        public int currentMenu;
        public Star[] starList = new Star[GameConstants.MaxNumStars];
        public List<Danger> dangerList;

        public Vector3 positionPortal1;
        public Vector3 positionPortal2;



        public DataManager()
        {
            isSuper = false;
            heroLives = 100;
            starsCaptured = 10;
            timeElapsed = 0.0f;
            cameraThirdPerson = 0; //fixed
            NumStars = 0;
            dangerList = new List<Danger>();
            isPaused = true;
            HeroDead = false;
            gameWon = false;
            isMenu = true;
            currentMenu = 1;
        }

        public void SetDangerModels(Model d1, Model d2)
        {
            danger1 = d1;
            danger2 = d2;
        }

    }
}
