using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WarlockDataTypes;

namespace Warlock
{
    public class WorldOverlay : IDrawable, IInteractable
    {
        private const int m_sizeX = 2400;
        private const int m_sizeY = 2400;
        private const int m_tilesize = 600;

        private List<IDrawable> m_drawable;

        private Vector2 m_pressLocation;

        public WorldOverlay()
        {
            m_drawable = new List<IDrawable>();
        }

        public void Draw()
        {
            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();
        }

        public void LoadContent()
        {
            WorldMapTileData[] worldMapTiles = WarlockGame.Instance.Content.Load<WorldMapTileData[]>(@"worldmap");

            // Load objects from XML
            foreach (WorldMapTileData tileData in worldMapTiles)
            {
                WorldObjectBase worldObjectBase = new WorldObjectBase(tileData.TileAssetName, null, tileData.TileXCoord, tileData.TileYCoord);
                m_drawable.Add(worldObjectBase);
            }

            foreach (IDrawable drawable in m_drawable)
                drawable.LoadContent();
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
                position = WorldGameMode.m_Instance.WorldPosition + m_pressLocation - touchLocation.Position;
                m_pressLocation = touchLocation.Position;
                CenterOn(position);
            }
        }

        public void CenterOn(Vector2 position)
        {
            if (position.X < 0)
                position.X = 0;
            else if (position.X > m_sizeX - WarlockGame.Graphics.PreferredBackBufferWidth)
                position.X = m_sizeX - WarlockGame.Graphics.PreferredBackBufferWidth;

            if (position.Y < 0)
                position.Y = 0;
            else if (position.Y > m_sizeY - WarlockGame.Graphics.PreferredBackBufferHeight)
                position.Y = m_sizeY - WarlockGame.Graphics.PreferredBackBufferHeight;
            WorldGameMode.m_Instance.CenterOn(position);
        }
    }
}
