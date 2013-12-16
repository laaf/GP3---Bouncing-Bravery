using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


using System.Diagnostics;

namespace Bouncing_Bravery
{
    class EventHandler
    {
        Map map;
        DataManager data;
        Hero hero;
        Screen screen;
        Camera camera;

        List<string> events;


        public EventHandler(Map m, DataManager d, Hero h, Screen sc, Camera c)
        {
            map = m;
            data = d;
            hero = h;
            screen = sc;
            camera = c;
            events = new List<string>();
        }

        public void AddEvent(string e)
        {
            events.Add(e);
        }

        private void HandleEvent(string e)
        {
            if (String.Equals(e, "restart", StringComparison.OrdinalIgnoreCase))
            { 
                #region restart hero
                hero.Position = map.defaultPlayerPos;
                hero.Velocity = Vector3.Zero;
                hero.bouncing = false;
                hero.upSpeed = 0.0f;
                hero.avatarYaw = 0;
                hero.isActive = true;
                #endregion
                data.heroLives--;
                data.HeroDead = true;
                data.isPaused = true;
                data.isSuper = false;
                Sounds.Play("death");
                MediaPlayer.Stop();
            }

        }

        public void Update(float elapsed)
        {
            while(events.Count > 0) //while there is an event to be handled
            {
                HandleEvent(events.ElementAt(0)); //handle the event
                events.RemoveAt(0); //remove the event from the list
            }
        }
    }
}
