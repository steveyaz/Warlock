using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    class Battle : IWorldEvent, IDrawable, IInteractable
    {
        public Vector2 m_battlePosition;
        private string m_battleID;

        public Battle(string battleID, int x, int y)
        {
            m_battleID = battleID;
            m_battlePosition = new Vector2(x, y);
        }

        public void Draw()
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(m_battlePosition);

            if (screenVector.X + WarlockGame.m_textures[m_battleID].Width > 0
                && screenVector.X < WarlockGame.m_graphics.PreferredBackBufferWidth
                && screenVector.Y + WarlockGame.m_textures[m_battleID].Height > 0
                && screenVector.Y < WarlockGame.m_graphics.PreferredBackBufferHeight)
            {
                WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures[m_battleID], screenVector, Color.White);
                WarlockGame.m_spriteBatch.End();
            }
        }

        public void InteractGesture(GestureSample gesture)
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(m_battlePosition);
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < screenVector.X + WarlockGame.m_textures[m_battleID].Width && gesture.Position.X > screenVector.X
                && gesture.Position.Y < screenVector.Y + WarlockGame.m_textures[m_battleID].Height && gesture.Position.Y > screenVector.Y)
            {
                WorldGameMode.m_Instance.MarkDestination(this);
                WorldGameMode.m_Instance.MovePlayer(m_battlePosition);
            }
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }

        public void PlayerEnter()
        {
            WarlockGame.m_Instance.ChangeGameMode(GameModeIndex.Battle);
        }
    }
}
