using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    class Mission : IDrawable, IInteractable
    {
        public Vector2 m_playerMissionPosition;

        public Mission(int x, int y)
        {
            m_playerMissionPosition = new Vector2(x, y);
        }

        public void Draw()
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(m_playerMissionPosition);

            if (screenVector.X + WarlockGame.m_textures["mission"].Width > 0
                && screenVector.X < WarlockGame.m_graphics.PreferredBackBufferWidth
                && screenVector.Y + WarlockGame.m_textures["mission"].Height > 0
                && screenVector.Y < WarlockGame.m_graphics.PreferredBackBufferHeight)
            {
                WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures["mission"], screenVector, Color.White);
                WarlockGame.m_spriteBatch.End();
            }
        }

        public void InteractGesture(GestureSample gesture)
        {
            
        }

        public void InteractLocation(TouchLocation touchLocation)
        {

        }
    }
}
