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
    class Screen
    {
        private ContentManager _content;
        private SpriteFont font;
        private SpriteBatch _spriteBatch;
        private Vector2 font_pos;
        private Texture2D UIHolder;
        private Vector2 UIHolder_pos;
        private Texture2D UIHeart;
        private Vector2 UIHeart_pos;
        private Texture2D UIStar;
        private Vector2 UIStar_pos;
        private Texture2D UIClock;
        private Vector2 UIClock_pos;
        private Texture2D UISoundOn;
        private Vector2 UISoundOn_pos;
        private Texture2D UISoundOff;
        private Vector2 UISoundOff_pos;
        private Texture2D won;

        private Texture2D menu1;
        private Texture2D menu2;
        private Texture2D menu3;
        private Texture2D cursor;
        public Vector2 cursor_pos;


        private Texture2D Bar;
        private Vector2 Bar_pos;

        private Texture2D filter;


        public Screen(SpriteBatch sb, ContentManager ct)
        {
            _spriteBatch = sb;
            _content = ct;
            font = _content.Load<SpriteFont>("Fonts/simple");
            UIHolder = _content.Load<Texture2D>("Images/UI/wood");
            UIHeart = _content.Load<Texture2D>("Images/UI/heart");
            UIStar = _content.Load<Texture2D>("Images/UI/star");
            UIClock = _content.Load<Texture2D>("Images/UI/clock");
            UISoundOn = _content.Load<Texture2D>("Images/UI/Sound - On");
            UISoundOff = _content.Load<Texture2D>("Images/UI/Sound - Off");
            Bar = _content.Load<Texture2D>("Images/UI/Bar");
            filter = _content.Load<Texture2D>("Images/UI/filter");
            won = _content.Load<Texture2D>("Images/won");
            menu1 = _content.Load<Texture2D>("Images/menu1");
            menu2 = _content.Load<Texture2D>("Images/menu2");
            menu3 = _content.Load<Texture2D>("Images/menu3");
            cursor = _content.Load<Texture2D>("Images/cursor");
            font_pos = new Vector2(690, 35);
            UIHolder_pos = new Vector2(640, 10);
            UIHeart_pos = new Vector2(660, 30);
            UIStar_pos = new Vector2(660, 58);
            UIClock_pos = new Vector2(660, 84);
            cursor_pos = new Vector2(375, 275);
            UISoundOn_pos = new Vector2(GameConstants.ScreenX - (UISoundOn.Width * 0.1f) - 10, GameConstants.ScreenY - (UISoundOn.Height * 0.1f) - 10);
            UISoundOff_pos = new Vector2(GameConstants.ScreenX - (UISoundOff.Width * 0.1f) - 10, GameConstants.ScreenY - (UISoundOff.Height * 0.1f) - 10);
            Bar_pos = new Vector2((Bar.Width * 0.02f), GameConstants.ScreenY - (Bar.Height) - 10);        }

        public void DrawText(DataManager data)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(UIHolder, UIHolder_pos, Color.White);
            _spriteBatch.Draw(UIHeart, UIHeart_pos, Color.White);
            _spriteBatch.Draw(UIStar, UIStar_pos, Color.White);
            _spriteBatch.Draw(UIClock, UIClock_pos, Color.White);

            //_spriteBatch.Draw(Bar, Bar_pos,Color.White);
            

            if (Sounds.isMute())
            {
                _spriteBatch.Draw(UISoundOff, UISoundOff_pos, null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f); //scaled at 10%
            }
            else
            {
                _spriteBatch.Draw(UISoundOn, UISoundOn_pos, null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            }
            _spriteBatch.DrawString(font, " x " + data.heroLives, new Vector2(UIHeart_pos.X + 31, UIHeart_pos.Y), Color.Black);
            _spriteBatch.DrawString(font, " x " + Math.Round(data.starsCaptured,0), new Vector2(UIStar_pos.X + 31, UIStar_pos.Y), Color.Black);
            _spriteBatch.DrawString(font, "   " + Math.Round(data.timeElapsed, 1) + "s", new Vector2(UIClock_pos.X + 31, UIClock_pos.Y), Color.Black);


            if (data.isPaused)
            {
                if (data.gameWon == false)
                {
                    _spriteBatch.Draw(filter, Vector2.Zero, Color.White);
                    _spriteBatch.DrawString(font, "Press any key to continue", new Vector2(GameConstants.ScreenX / 2 - 80, GameConstants.ScreenY / 2), Color.White);
                    if (data.HeroDead == true)
                    {
                        _spriteBatch.DrawString(font, "You have died! Avoid falling!\n         Avoid spikes!", new Vector2(GameConstants.ScreenX / 2 - 80, GameConstants.ScreenY / 2 - 80), Color.White);
                    }
                }
                else
                {
                    _spriteBatch.Draw(won, Vector2.Zero, Color.White);
                }
                if (data.isMenu == true)
                {
                    if (data.currentMenu == 1)
                    {
                        _spriteBatch.Draw(menu1, Vector2.Zero, Color.White);
                        _spriteBatch.Draw(cursor, cursor_pos, Color.White);
                    }
                    else if (data.currentMenu == 2)
                    {
                        _spriteBatch.Draw(menu2, Vector2.Zero, Color.White);
                    }
                    else
                        _spriteBatch.Draw(menu3, Vector2.Zero, Color.White);
                }
                
            }
            _spriteBatch.End();
        }

    }
}
