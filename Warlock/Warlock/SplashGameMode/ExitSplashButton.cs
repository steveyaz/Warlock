using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    class ExitSplashButton : IDrawable, IInteractable
    {
        private string m_buttonText;
        private Vector2 m_textPosition;

        public ExitSplashButton()
        {
            m_buttonText = "Exit";
            m_textPosition = new Vector2(WarlockGame.m_graphics.GraphicsDevice.Viewport.Width / 2, WarlockGame.m_graphics.GraphicsDevice.Viewport.Height / 2 + 30);
        }

        public void Draw()
        {
            WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.m_spriteBatch.DrawString(WarlockGame.m_spriteFont, m_buttonText, m_textPosition, Color.LightGreen, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            WarlockGame.m_spriteBatch.End();
        }

        public void InteractGesture(GestureSample gesture)
        {
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < m_textPosition.X + WarlockGame.m_spriteFont.MeasureString(m_buttonText).X && gesture.Position.X > m_textPosition.X
                && gesture.Position.Y < m_textPosition.Y + WarlockGame.m_spriteFont.MeasureString(m_buttonText).Y && gesture.Position.Y > m_textPosition.Y)
            {
                Execute();
            }
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }

        private void Execute()
        {
            // Exit the game
            WarlockGame.m_Instance.Exit();
        }
    }
}
