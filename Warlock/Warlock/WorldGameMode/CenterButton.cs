using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    public class CenterButton : IDrawable, IInteractable
    {
        private Vector2 m_buttonPosition;
        const int m_xOffset = 48;
        const int m_yOffset = 10;

        public CenterButton()
        {
            m_buttonPosition = new Vector2(WarlockGame.Graphics.GraphicsDevice.Viewport.Width - m_xOffset, m_yOffset);
        }

        public void Draw()
        {
            WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["center"], m_buttonPosition, Color.White);
            WarlockGame.Batch.End();
        }

        public void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture("center");
        }

        public void InteractGesture(GestureSample gesture)
        {
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < m_buttonPosition.X + WarlockGame.TextureDictionary["center"].Width && gesture.Position.X > m_buttonPosition.X
                && gesture.Position.Y < m_buttonPosition.Y + WarlockGame.TextureDictionary["center"].Height && gesture.Position.Y > m_buttonPosition.Y)
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
