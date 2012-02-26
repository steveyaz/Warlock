using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    class CenterButton : IDrawable, IInteractable
    {
        private Vector2 m_buttonPosition;
        const int m_xOffset = 48;
        const int m_yOffset = 10;

        public CenterButton()
        {
            m_buttonPosition = new Vector2(WarlockGame.m_graphics.GraphicsDevice.Viewport.Width - m_xOffset, m_yOffset);
        }

        public void Draw()
        {
            WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures["center"], m_buttonPosition, Color.White);
            WarlockGame.m_spriteBatch.End();
        }

        public void InteractGesture(GestureSample gesture)
        {
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < m_buttonPosition.X + WarlockGame.m_textures["center"].Width && gesture.Position.X > m_buttonPosition.X
                && gesture.Position.Y < m_buttonPosition.Y + WarlockGame.m_textures["center"].Height && gesture.Position.Y > m_buttonPosition.Y)
            {
                WorldGameMode.m_Instance.CenterOnPlayer();
            }
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }
    }
}
