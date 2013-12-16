using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



using System.Diagnostics;

namespace Bouncing_Bravery
{
    class Sounds
    {
        private ContentManager _content;
        private static SoundEffect bounce;
        private static SoundEffect jump;
        private static SoundEffect death;
        private static SoundEffect coin;
        private static Song theme;
        private static Song menu;
        private static Song sonic;
        private static bool mute;

        public Sounds(ContentManager content)
        {
            _content = content;
            bounce = _content.Load<SoundEffect>("Sounds/bounce");
            jump = _content.Load<SoundEffect>("Sounds/jump");
            coin = _content.Load<SoundEffect>("Sounds/coin");
            death = _content.Load<SoundEffect>("Sounds/death");
            theme = _content.Load<Song>("Sounds/songofstorms");
            menu = _content.Load<Song>("Sounds/prelude");
            sonic = _content.Load<Song>("Sounds/sonic");
            
            mute = false;
        }

        public static void Play(string name)
        {
            if (mute == false)
            {
                if (name == "bounce")
                    bounce.Play();
                else if (name == "jump")
                    jump.Play();
                else if (name == "theme")
                    MediaPlayer.Play(theme);
                else if (name == "menu")
                    MediaPlayer.Play(menu);
                else if (name == "death")
                {
                    death.Play();
                }
                else if (name == "coin")
                    coin.Play();
            }
        }


        public static void goSonic()
        {
            MediaPlayer.Play(sonic);
        }

        public static void stopSonic()
        {
            MediaPlayer.Play(theme);
        }

        public static bool isMute()
        {
            return mute;
        }
        private static void Mute()
        {
            mute = true;
            MediaPlayer.Volume = 0.0f;
        }

        private static void Unmute()
        {
            mute = false;
            MediaPlayer.Volume = 1.0f;
        }

        public static void ToggleSound()
        {
            if (mute == true)
            {
                mute = false;
                Unmute();
            }
            else
            {
                mute = true;
                Mute();
            }
        }

        


    }
}
