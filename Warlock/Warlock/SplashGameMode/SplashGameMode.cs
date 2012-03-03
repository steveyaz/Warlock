using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Warlock.SplashGameModeNS
{
    class SplashGameMode : IGameMode
    {
        private List<IDrawable> m_drawable;
        private List<IInteractable> m_interactable;

        public void Initialize()
        {
            m_drawable = new List<IDrawable>();
            m_interactable = new List<IInteractable>();

            TextScreenObject newGameButton = new TextScreenObject()
            {
                Text = "New Game",
                TextColor = Color.BlanchedAlmond,
                AssetName = "warlock_button",
                ScreenPosition = new Vector2(WarlockGame.Graphics.GraphicsDevice.Viewport.Width / 2, WarlockGame.Graphics.GraphicsDevice.Viewport.Height / 2 - 30),
                TapDelegate = WarlockGame.Instance.StartNewGame
            };

            TextScreenObject exitGameButton = new TextScreenObject()
            {
                Text = "Exit",
                TextColor = Color.BlanchedAlmond,
                AssetName = "warlock_button",
                ScreenPosition = new Vector2(WarlockGame.Graphics.GraphicsDevice.Viewport.Width / 2, WarlockGame.Graphics.GraphicsDevice.Viewport.Height / 2 + 30),
                TapDelegate = WarlockGame.Instance.Exit
            };

            // these should always be added in opposite order so that objects drawn on top get interaction priority
            m_drawable.Add(newGameButton);
            m_drawable.Add(exitGameButton);
            
            m_interactable.Add(exitGameButton);
            m_interactable.Add(newGameButton);
            
            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        public void Draw()
        {
            // TODO: clear to black once we have a splash background
            WarlockGame.Graphics.GraphicsDevice.Clear(Color.Red);

            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();

            return;
        }

        public void LoadContent()
        {
            foreach (IDrawable drawable in m_drawable)
                drawable.LoadContent();
        }

        public void Update()
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.Instance.Exit();

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                foreach (IInteractable interactable in m_interactable)
                    if (interactable.InteractGesture(gesture))
                        break;
            }

            foreach (IDrawable drawable in m_drawable)
                drawable.Update();
        }
    }
}
