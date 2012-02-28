using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Xml;
using WarlockDataTypes;

namespace Warlock.WorldGameModeNS
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

        private WorldHUD m_worldHUD;
        private WorldOverlay m_worldoverlay;
        private WorldPlayer m_worldPlayer;
        private WorldObjectBase m_worldObjectDestination;

        public void Initialize()
        {
            m_Instance = this;
            m_drawable = new List<IDrawable>();
            m_interactable = new List<IInteractable>();

            // World map
            m_worldoverlay = new WorldOverlay();
            
            // Player on the map
            m_worldPlayer = new WorldPlayer(200, 200);
            
            // HUD
            m_worldHUD = new WorldHUD();

            // World objects from XML
            WorldMapObjectData[] worldMapObjects = WarlockGame.Instance.Content.Load<WorldMapObjectData[]>(@"worldmapobjects");
            List<WorldObjectBase> worldObjects = new List<WorldObjectBase>();

            foreach (WorldMapObjectData objectData in worldMapObjects)
            {
                if (objectData.ObjectType == WorldMapObjectType.City)
                {
                    worldObjects.Add(new WorldCity()
                    {
                        AssetName = objectData.WorldMapAssetName,
                        ObjectID = objectData.ObjectID,
                        WorldPosition = new Vector2(objectData.WorldMapXCoord, objectData.WorldMapYCoord),
                    });
                }
                else if (objectData.ObjectType == WorldMapObjectType.Battle)
                {
                    worldObjects.Add(new WorldBattle()
                    {
                        AssetName = objectData.WorldMapAssetName,
                        ObjectID = objectData.ObjectID,
                        WorldPosition = new Vector2(objectData.WorldMapXCoord, objectData.WorldMapYCoord),
                    });
                }
            }

            // these should always be added in opposite order so that objects drawn on top get interaction priority
            m_drawable.Add(m_worldoverlay);
            foreach (WorldObjectBase worldObjectBase in worldObjects)
                m_drawable.Add(worldObjectBase);
            m_drawable.Add(m_worldPlayer);
            m_drawable.Add(m_worldHUD);

            m_interactable.Add(m_worldHUD);
            m_interactable.Add(m_worldPlayer);
            foreach (WorldObjectBase worldObjectBase in worldObjects)
                m_interactable.Add(worldObjectBase);
            m_interactable.Add(m_worldoverlay);

            CenterOnPlayer();

            // Enable only certain gestures
            TouchPanel.EnabledGestures = GestureType.DoubleTap | GestureType.Pinch | GestureType.Tap;
        }

        public void Draw()
        {
            WarlockGame.Graphics.GraphicsDevice.Clear(Color.Blue);

            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();
        }

        public void Update()
        {
            // Go back to splash
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.Instance.ChangeGameMode(new SplashGameModeNS.SplashGameMode());

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                // World Player gets priority if moving
                if (m_worldPlayer.Moving)
                    m_worldPlayer.ZoomToDestination();
                else
                    foreach (IInteractable interactable in m_interactable)
                        if (interactable.InteractGesture(gesture))
                            break;
            }
            else
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                // No multitouch here
                if (touchCollection.Count == 1 && !m_worldPlayer.Moving)
                    m_worldoverlay.InteractLocation(touchCollection[0]);
            }

            foreach (IDrawable drawable in m_drawable)
                drawable.Update();
        }

        public void LoadContent()
        {
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
            m_worldHUD.HideContextMenu();
            m_worldPlayer.MoveTo(toPosition);
        }

        public void MarkDestination(WorldObjectBase worldEvent)
        {
            m_worldObjectDestination = worldEvent;
        }

        public void ArrivedAtDestination()
        {
            m_worldHUD.ShowContextMenu(m_worldObjectDestination.ContextMenuItems);
        }

        public static Vector2 WorldToScreen(Vector2 worldVector)
        {
            return new Vector2(worldVector.X - m_Instance.WorldPosition.X, worldVector.Y - m_Instance.WorldPosition.Y);
        }
    }
}
