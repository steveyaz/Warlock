using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    class WorldGameMode : IGameMode
    {
        public static Vector2 WorldPosition;
        public static WorldGameMode m_Instance;

        private List<IDrawable> m_drawable;
        private List<IInteractable> m_interactable;

        private WorldOverlay m_worldoverlay;
        private WorldPlayer m_worldPlayer;

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
            m_worldPlayer = new WorldPlayer();
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
            WarlockGame.m_graphics.GraphicsDevice.Clear(Color.Blue);

            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();

            return;
        }

        public void Update()
        {
            // Go back to splash
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.m_Instance.ChangeGameMode(GameModeIndex.Splash);

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
        }

        public void LoadContent()
        {
            WarlockGame.m_Instance.EnsureTexture("graywizard");
            WarlockGame.m_Instance.EnsureTexture("mission");
            WarlockGame.m_Instance.EnsureTexture("worldmap-0-0");
            WarlockGame.m_Instance.EnsureTexture("worldmap-0-1");
            WarlockGame.m_Instance.EnsureTexture("worldmap-0-2");
            WarlockGame.m_Instance.EnsureTexture("worldmap-0-3");
            WarlockGame.m_Instance.EnsureTexture("worldmap-1-0");
            WarlockGame.m_Instance.EnsureTexture("worldmap-1-1");
            WarlockGame.m_Instance.EnsureTexture("worldmap-1-2");
            WarlockGame.m_Instance.EnsureTexture("worldmap-1-3");
            WarlockGame.m_Instance.EnsureTexture("worldmap-2-0");
            WarlockGame.m_Instance.EnsureTexture("worldmap-2-1");
            WarlockGame.m_Instance.EnsureTexture("worldmap-2-2");
            WarlockGame.m_Instance.EnsureTexture("worldmap-2-3");
            WarlockGame.m_Instance.EnsureTexture("worldmap-3-0");
            WarlockGame.m_Instance.EnsureTexture("worldmap-3-1");
            WarlockGame.m_Instance.EnsureTexture("worldmap-3-2");
            WarlockGame.m_Instance.EnsureTexture("worldmap-3-3");
        }

        public void CenterOnPlayer()
        {
            Vector2 newPosition = new Vector2();
            newPosition.X = m_worldPlayer.m_playerWorldPosition.X - WarlockGame.m_graphics.PreferredBackBufferWidth / 2;
            newPosition.Y = m_worldPlayer.m_playerWorldPosition.Y - WarlockGame.m_graphics.PreferredBackBufferHeight / 2;

            m_worldoverlay.CenterOn(newPosition);
        }

        public static Vector2 WorldToScreen(Vector2 worldVector)
        {
            return new Vector2(worldVector.X - WorldPosition.X, worldVector.Y - WorldPosition.Y);
        }
    }
}
