using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    class WorldPlayer : IDrawable, IInteractable
    {
        public Vector2 m_playerWorldPosition;

        private Vector2 m_toPosition;
        private Vector2 m_lastDelta;
        private Vector2 m_velocity;
        private bool m_moving = false;

        public WorldPlayer(int x, int y)
        {
            m_playerWorldPosition = new Vector2(x, y);
        }

        public void Draw()
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(m_playerWorldPosition);

            if (screenVector.X + WarlockGame.m_textures["graywizard"].Width > 0
                && screenVector.X < WarlockGame.m_graphics.PreferredBackBufferWidth
                && screenVector.Y + WarlockGame.m_textures["graywizard"].Height > 0
                && screenVector.Y < WarlockGame.m_graphics.PreferredBackBufferHeight)
            {
                WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures["graywizard"], screenVector, Color.White);
                WarlockGame.m_spriteBatch.End();
            }
        }

        public void InteractGesture(GestureSample gesture)
        {
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }

        public void Update()
        {
            if (m_moving)
            {
                m_playerWorldPosition += m_velocity;
                WorldGameMode.m_Instance.CenterOnPlayer();
                if (m_lastDelta.Length() < (m_toPosition - m_playerWorldPosition).Length())
                {
                    m_moving = false;
                    WorldGameMode.m_Instance.ArrivedAtDestination();
                }
                else
                {
                    m_lastDelta = m_toPosition - m_playerWorldPosition;
                }
            }
        }

        public void MoveTo(Vector2 toPosition)
        {
            m_toPosition = toPosition;
            m_lastDelta = m_toPosition - m_playerWorldPosition;
            m_velocity = m_toPosition - m_playerWorldPosition;
            m_velocity.Normalize();
            m_velocity *= 6;
            m_moving = true;
        }
    }
}
