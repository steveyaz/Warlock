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
        public string CityBG { get; set; }
        private Rectangle rect;
        public CityBase()
        {
            rect = new Rectangle(0, 0, WarlockGame.Graphics.PreferredBackBufferWidth, WarlockGame.Graphics.PreferredBackBufferHeight);
        }

        public void Draw()
        {
            // Main Draw for every city
            
            WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.Batch.Draw(WarlockGame.TextureDictionary[CityBG], rect, Color.White);
            WarlockGame.Batch.End();
        }

        public void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture(CityBG);
        }

        public void InteractLocation(TouchLocation touchLocation)
        {

        }

        public void InteractGesture(GestureSample gestureSample)
        {

        }
    }
}
