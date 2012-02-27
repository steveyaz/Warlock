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
        private string m_buttonText;
        private Vector2 m_textPosition;

        public NewGameSplashButton()
        {
            m_buttonText = "New Game";
            m_textPosition = new Vector2(WarlockGame.Graphics.GraphicsDevice.Viewport.Width / 2, WarlockGame.Graphics.GraphicsDevice.Viewport.Height / 2 - 30);
        }

        public void Draw()
        {
            WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.Batch.DrawString(WarlockGame.FontDictionary["Warlock"], m_buttonText, m_textPosition, Color.LightGreen, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            WarlockGame.Batch.End();
        }

        public void LoadContent()
        {
            WarlockGame.Instance.EnsureFont("Warlock");
        }

        public void InteractGesture(GestureSample gesture)
        {
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < m_textPosition.X + WarlockGame.FontDictionary["Warlock"].MeasureString(m_buttonText).X && gesture.Position.X > m_textPosition.X
                && gesture.Position.Y < m_textPosition.Y + WarlockGame.FontDictionary["Warlock"].MeasureString(m_buttonText).Y && gesture.Position.Y > m_textPosition.Y)
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
            // Start a new game
            WarlockGame.Instance.StartNewGame();
        }
    }
}
