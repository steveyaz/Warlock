using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using WarlockDataTypes;

namespace Warlock.WorldGameModeNS
{
    public class WorldOverlay : IDrawable, IInteractable
    {
        private int m_sizeX;
        private int m_sizeY;

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

        public void Update()
        {
            foreach (IDrawable drawable in m_drawable)
                drawable.Update();
        }

        public void LoadContent()
        {
            // Load objects from XML
            WorldMapData worldMapData = WarlockGame.Instance.Content.Load<WorldMapData>(@"worldmap");
            m_sizeX = worldMapData.Width;
            m_sizeY = worldMapData.Height;

            WorldMapTileData[] worldMapTiles = WarlockGame.Instance.Content.Load<WorldMapTileData[]>(@"worldmaptiles");
            foreach (WorldMapTileData tileData in worldMapTiles)
            {
                WorldObjectBase worldObjectBase = new WorldObjectBase()
                {
                    AssetName = tileData.TileAssetName,
                    WorldPosition = new Vector2(tileData.TileXCoord, tileData.TileYCoord),
                };

                m_drawable.Add(worldObjectBase);
            }

            foreach (IDrawable drawable in m_drawable)
                drawable.LoadContent();
        }

        public bool InteractGesture(GestureSample gesture)
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
            return false;
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
