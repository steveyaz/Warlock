using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    class BattleGameMode : IGameMode
    {
        private List<IDrawable> m_drawable;
        private List<IInteractable> m_interactable;

        public void Initialize()
        {
            m_drawable = new List<IDrawable>();
            m_interactable = new List<IInteractable>();
        }

        public void Draw()
        {
            WarlockGame.m_graphics.GraphicsDevice.Clear(Color.ForestGreen);

            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();

            return;
        }

        public void Update()
        {
            // Go back to splash
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.m_Instance.ChangeGameMode(GameModeIndex.World);

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
        }
    }
}
