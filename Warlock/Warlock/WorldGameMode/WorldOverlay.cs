using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Warlock
{
    class WorldOverlay : IDrawable, IInteractable
    {
        private const int m_sizeX = 2400;
        private const int m_sizeY = 2400;
        private const int m_tilesize = 600;

        private Vector2 m_pressLocation;

        private string[,] m_maptiles;

        public WorldOverlay()
        {
            m_maptiles = new string[4,4];
            m_maptiles[0,0] = "worldmap-0-0";
            m_maptiles[0,1] = "worldmap-0-1";
            m_maptiles[0,2] = "worldmap-0-2";
            m_maptiles[0,3] = "worldmap-0-3";
            m_maptiles[1,0] = "worldmap-1-0";
            m_maptiles[1,1] = "worldmap-1-1";
            m_maptiles[1,2] = "worldmap-1-2";
            m_maptiles[1,3] = "worldmap-1-3";
            m_maptiles[2,0] = "worldmap-2-0";
            m_maptiles[2,1] = "worldmap-2-1";
            m_maptiles[2,2] = "worldmap-2-2";
            m_maptiles[2,3] = "worldmap-2-3";
            m_maptiles[3,0] = "worldmap-3-0";
            m_maptiles[3,1] = "worldmap-3-1";
            m_maptiles[3,2] = "worldmap-3-2";
            m_maptiles[3,3] = "worldmap-3-3";
        }

        public void Draw()
        {
            WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Vector2 tilevector = new Vector2(-(WorldGameMode.WorldPosition.X % m_tilesize), -(WorldGameMode.WorldPosition.Y % m_tilesize));
            int x = 0;
            int y = 0;

            while (tilevector.X < WarlockGame.m_graphics.PreferredBackBufferWidth)
            {
                while (tilevector.Y < WarlockGame.m_graphics.PreferredBackBufferHeight)
                {
                    WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures[m_maptiles[((int)WorldGameMode.WorldPosition.X / m_tilesize) + x, ((int)WorldGameMode.WorldPosition.Y / m_tilesize) + y]], tilevector, Color.White);
                    y++;
                    tilevector.Y += m_tilesize;
                }
                x++;
                tilevector.X += m_tilesize;
                y = 0;
                tilevector.Y = -(WorldGameMode.WorldPosition.Y % m_tilesize);
            }
            
            WarlockGame.m_spriteBatch.End();
        }

        public void InteractGesture(GestureSample gesture)
        {
            switch (gesture.GestureType)
            {
                case GestureType.DoubleTap:
                    break;
                case GestureType.Pinch:
                    break;
                default:
                    break;
            }
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            if (touchLocation.State == TouchLocationState.Pressed)
                m_pressLocation = touchLocation.Position;
            else if (touchLocation.State == TouchLocationState.Moved)
            {
                Vector2 position = new Vector2();
                position = WorldGameMode.WorldPosition + m_pressLocation - touchLocation.Position;
                m_pressLocation = touchLocation.Position;
                CenterOn(position);
            }
        }

        public void CenterOn(Vector2 position)
        {
            if (position.X < 0)
                position.X = 0;
            else if (position.X > m_sizeX - WarlockGame.m_graphics.PreferredBackBufferWidth)
                position.X = m_sizeX - WarlockGame.m_graphics.PreferredBackBufferWidth;

            if (position.Y < 0)
                position.Y = 0;
            else if (position.Y > m_sizeY - WarlockGame.m_graphics.PreferredBackBufferHeight)
                position.Y = m_sizeY - WarlockGame.m_graphics.PreferredBackBufferHeight;
            WorldGameMode.WorldPosition = position;
        }
    }
}
