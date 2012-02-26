using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    public class CityBase : IDrawable, IInteractable
    {
        public string CityStr { get; set; }
        private Rectangle rect;
        public CityBase()
        {
            rect = new Rectangle(0, 0, WarlockGame.m_graphics.PreferredBackBufferWidth, WarlockGame.m_graphics.PreferredBackBufferHeight);
        }

        public void Draw()
        {
            // Main Draw for every city
            WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures[CityStr], rect, Color.White);
            WarlockGame.m_spriteBatch.End();
        }

        public void InteractLocation(TouchLocation touchLocation)
        {

        }

        public void InteractGesture(GestureSample gestureSample)
        {

        }

        public void InitCityString(City city)
        {
            switch (city)
            {
                case City.Albador:
                    CityStr = "albador";
                    break;
                case City.Hibador:
                    CityStr = "hibador";
                    break;
                case City.Midador:
                    CityStr = "midador";
                    break;
                default:
                    throw new Exception("Could not initialize city name.");
            }
        }
    }
}
