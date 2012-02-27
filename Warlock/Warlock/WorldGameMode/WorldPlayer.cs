using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    public class WorldPlayer : IDrawable, IInteractable
    {
        private Vector2 m_playerWorldPosition;
        public Vector2 PlayerWorldPosition
        {
            get
            {
                return m_playerWorldPosition;
            }
        }

        private Vector2 m_toPosition;
        private Vector2 m_lastDelta;
        private Vector2 m_velocity;
        private bool m_moving;

        public WorldPlayer(int x, int y)
        {
            m_playerWorldPosition = new Vector2(x, y);
            m_moving = false;
        }

        public void Draw()
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(PlayerWorldPosition);

            if (screenVector.X + WarlockGame.TextureDictionary["graywizard"].Width > 0
                && screenVector.X < WarlockGame.Graphics.PreferredBackBufferWidth
                && screenVector.Y + WarlockGame.TextureDictionary["graywizard"].Height > 0
                && screenVector.Y < WarlockGame.Graphics.PreferredBackBufferHeight)
            {
                WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["graywizard"], screenVector, Color.White);
                WarlockGame.Batch.End();
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
                if (m_lastDelta.Length() < (m_toPosition - PlayerWorldPosition).Length())
                {
                    m_moving = false;
                    WorldGameMode.m_Instance.ArrivedAtDestination();
                }
                else
                {
                    m_lastDelta = m_toPosition - PlayerWorldPosition;
                }
            }
        }

        public void MoveTo(Vector2 toPosition)
        {
            m_toPosition = toPosition;
            m_lastDelta = m_toPosition - PlayerWorldPosition;
            m_velocity = m_toPosition - PlayerWorldPosition;
            m_velocity.Normalize();
            m_velocity *= 6;
            m_moving = true;
        }

        public void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture("graywizard");
        }
    }
}
