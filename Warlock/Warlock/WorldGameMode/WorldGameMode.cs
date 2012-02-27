using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using WarlockDataTypes;

namespace Warlock
{
    public class WorldGameMode : IGameMode
    {
        public static WorldGameMode m_Instance;

        private Vector2 m_worldPosition;
        public Vector2 WorldPosition
        {
            get
            {
                return m_worldPosition;
            }
        }

        private List<IDrawable> m_drawable;
        private List<IInteractable> m_interactable;

        private WorldOverlay m_worldoverlay;
        private WorldPlayer m_worldPlayer;
        private WorldObjectBase m_worldEventDestination;

        public void Initialize()
        {
            m_Instance = this;
            m_drawable = new List<IDrawable>();
            m_interactable = new List<IInteractable>();

            // World map
            m_worldoverlay = new WorldOverlay();
            m_drawable.Add(m_worldoverlay);
            m_interactable.Add(m_worldoverlay);

            // Player on the map
            m_worldPlayer = new WorldPlayer(200, 200);
            m_drawable.Add(m_worldPlayer);
            m_interactable.Add(m_worldPlayer);

            // Center-on-player button
            CenterButton centerButton = new CenterButton();
            m_drawable.Add(centerButton);
            m_interactable.Add(centerButton);

            CenterOnPlayer();

            // Enable only certain gestures
            TouchPanel.EnabledGestures = GestureType.DoubleTap | GestureType.Pinch | GestureType.Tap;
        }

        public void Draw()
        {
            WarlockGame.Graphics.GraphicsDevice.Clear(Color.Blue);

            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();

            return;
        }

        public void Update()
        {
            // Go back to splash
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.Instance.ChangeGameMode(new SplashGameMode());

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                foreach (IInteractable interactable in m_interactable)
                    interactable.InteractGesture(gesture);
            }
            else
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                // No multitouch here
                if (touchCollection.Count == 1)
                    foreach (IInteractable interactable in m_interactable)
                        interactable.InteractLocation(touchCollection[0]);
            }

            m_worldPlayer.Update();
        }

        public void LoadContent()
        {
            WorldMapObjectData[] worldMapObjects = WarlockGame.Instance.Content.Load<WorldMapObjectData[]>(@"worldmapobjects");

            // Load objects from XML
            foreach (WorldMapObjectData objectData in worldMapObjects)
            {
                WorldObjectBase worldObjectBase;

                if (objectData.ObjectType == WorldMapObjectType.City)
                {
                    worldObjectBase = new WorldCity(objectData.WorldMapAssetName, objectData.ObjectID, objectData.WorldMapXCoord, objectData.WorldMapYCoord);
                    m_drawable.Add(worldObjectBase);
                    m_interactable.Add(worldObjectBase);
                }
                else if (objectData.ObjectType == WorldMapObjectType.Battle)
                {
                    worldObjectBase = new WorldBattle(objectData.WorldMapAssetName, objectData.ObjectID, objectData.WorldMapXCoord, objectData.WorldMapYCoord);
                    m_drawable.Add(worldObjectBase);
                    m_interactable.Add(worldObjectBase);
                }
            }

            foreach (IDrawable drawable in m_drawable)
                drawable.LoadContent();
        }

        public void CenterOnPlayer()
        {
            Vector2 newPosition = new Vector2();
            newPosition.X = m_worldPlayer.PlayerWorldPosition.X - WarlockGame.Graphics.PreferredBackBufferWidth / 2;
            newPosition.Y = m_worldPlayer.PlayerWorldPosition.Y - WarlockGame.Graphics.PreferredBackBufferHeight / 2;

            m_worldoverlay.CenterOn(newPosition);
        }

        public void CenterOn(Vector2 newCenter)
        {
            m_worldPosition = newCenter;
        }

        public void MovePlayer(Vector2 toPosition)
        {
            m_worldPlayer.MoveTo(toPosition);
        }

        public void MarkDestination(WorldObjectBase worldEvent)
        {
            m_worldEventDestination = worldEvent;
        }

        public void ArrivedAtDestination()
        {
            m_worldEventDestination.PlayerEnter();
        }

        public static Vector2 WorldToScreen(Vector2 worldVector)
        {
            return new Vector2(worldVector.X - m_Instance.WorldPosition.X, worldVector.Y - m_Instance.WorldPosition.Y);
        }
    }
}
