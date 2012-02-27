using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    class WorldCity : IWorldEvent, IDrawable, IInteractable
    {
        public Vector2 m_cityPosition;
        private string m_cityID;

        public WorldCity(string cityID, int x, int y)
        {
            m_cityID = cityID;
            m_cityPosition = new Vector2(x, y);
        }

        public void Draw()
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(m_cityPosition);

            if (screenVector.X + WarlockGame.m_textures[m_cityID].Width > 0
                && screenVector.X < WarlockGame.m_graphics.PreferredBackBufferWidth
                && screenVector.Y + WarlockGame.m_textures[m_cityID].Height > 0
                && screenVector.Y < WarlockGame.m_graphics.PreferredBackBufferHeight)
            {
                WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures[m_cityID], screenVector, Color.White);
                WarlockGame.m_spriteBatch.End();
            }
        }

        public void InteractGesture(GestureSample gesture)
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(m_cityPosition);
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < screenVector.X + WarlockGame.m_textures[m_cityID].Width && gesture.Position.X > screenVector.X
                && gesture.Position.Y < screenVector.Y + WarlockGame.m_textures[m_cityID].Height && gesture.Position.Y > screenVector.Y)
            {
                WorldGameMode.m_Instance.MarkDestination(this);
                WorldGameMode.m_Instance.MovePlayer(m_cityPosition);
            }
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }

        public void PlayerEnter()
        {
            //Set World player's current location.
            WarlockGame.m_Instance.m_GameModes[GameModeIndex.City].Initialize();
            WarlockGame.m_Instance.ChangeGameMode(GameModeIndex.City);
        }
    }
}
