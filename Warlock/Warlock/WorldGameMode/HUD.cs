using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Warlock.WorldGameModeNS
{
    class HUD : IDrawable, IInteractable
    {
        ImageScreenObject m_centerButton;
        ContextMenu m_contextMenu;

        public HUD()
        {
            // Center-on-player button
            m_centerButton = new ImageScreenObject()
            {
                AssetName = "center",
                ScreenPosition = new Vector2(WarlockGame.Graphics.GraphicsDevice.Viewport.Width - 48, 10),
                TapDelegate = WorldGameMode.m_Instance.CenterOnPlayer
            };

            // Context menus
            m_contextMenu = new ContextMenu();
        }

        public void Draw()
        {
            m_centerButton.Draw();
            m_contextMenu.Draw();
        }

        public void Update()
        {
            // Do nothing
        }

        public void LoadContent()
        {
            m_centerButton.LoadContent();
            m_contextMenu.LoadContent();
        }

        public bool InteractGesture(GestureSample gesture)
        {
            bool ret = false;
            ret |= m_centerButton.InteractGesture(gesture);
            ret |= m_contextMenu.InteractGesture(gesture);
            return ret;
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }

        public void ShowContextMenu(Dictionary<string, ScreenObjectTapDelegate> contextMenuItems)
        {
            m_contextMenu.Show(contextMenuItems);
        }

        public void HideContextMenu()
        {
            m_contextMenu.Hide();
        }
    }
}
