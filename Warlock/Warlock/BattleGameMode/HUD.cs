using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Warlock.BattleGameMode
{
    public class HUD : IDrawable, IInteractable
    {
        ActionButton m_actionButton;

        public HUD()
        {
            // Center-on-player button
            m_actionButton = new ActionButton()
            {
                AssetName = "meteor",
                ScreenPosition = new Vector2(10, 434),
            };
        }

        public void Draw()
        {
            m_actionButton.Draw();
        }

        public void Update()
        {
            // Do nothing
        }

        public void LoadContent()
        {
            m_actionButton.LoadContent();
        }

        public bool InteractGesture(GestureSample gesture)
        {
            bool ret = false;
            ret |= m_actionButton.InteractGesture(gesture);
            return ret;
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }
    }
}
