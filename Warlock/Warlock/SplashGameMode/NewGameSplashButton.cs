using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    class NewGameSplashButton : IDrawable, IInteractable
    {
        private string m_buttontext;
        private Vector2 m_textPosition;

        public NewGameSplashButton()
        {
            m_buttontext = "New Game";
            m_textPosition = new Vector2(WarlockGame.m_graphics.GraphicsDevice.Viewport.Width / 2, WarlockGame.m_graphics.GraphicsDevice.Viewport.Height / 2 - 30);
        }

        public void Draw()
        {
            WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.m_spriteBatch.DrawString(WarlockGame.m_spriteFont, m_buttontext, m_textPosition, Color.LightGreen, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            WarlockGame.m_spriteBatch.End();
        }

        public void InteractGesture(GestureSample gesture)
        {
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < m_textPosition.X + WarlockGame.m_spriteFont.MeasureString(m_buttontext).X && gesture.Position.X > m_textPosition.X
                && gesture.Position.Y < m_textPosition.Y + WarlockGame.m_spriteFont.MeasureString(m_buttontext).Y && gesture.Position.Y > m_textPosition.Y)
            {
                Execute();
            }
        }

        public void InteractLocation(TouchLocation touchLocation)
        {

        }

        private void Execute()
        {
            WarlockGame.m_Instance.ChangeGameMode(GameModeIndex.World);
        }
    }
}
