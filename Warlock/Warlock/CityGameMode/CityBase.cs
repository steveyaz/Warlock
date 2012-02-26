using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    public class CityModeBase : IDrawable, IInteractable
    {
        public string CityStr { get; set; }
        private Rectangle rect;
        public CityModeBase()
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

        public void InitCityString(CityEnum city)
        {
            switch (city)
            {
                case CityEnum.Albador:
                    CityStr = "albador";
                    break;
                case CityEnum.Hibador:
                    CityStr = "hibador";
                    break;
                case CityEnum.Midador:
                    CityStr = "midador";
                    break;
                default:
                    throw new Exception("Could not initialize city name.");
            }
        }
    }
}
