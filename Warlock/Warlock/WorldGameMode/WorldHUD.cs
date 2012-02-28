using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Warlock.WorldGameModeNS
{
    class WorldHUD : IDrawable, IInteractable
    {
        ImageScreenObject m_centerButton;
        WorldContextMenu m_worldContextMenu;

        public WorldHUD()
        {
            // Center-on-player button
            m_centerButton = new ImageScreenObject()
            {
                AssetName = "center",
                ScreenPosition = new Vector2(WarlockGame.Graphics.GraphicsDevice.Viewport.Width - 48, 10),
                TapDelegate = WorldGameMode.m_Instance.CenterOnPlayer
            };

            // Context menus
            m_worldContextMenu = new WorldContextMenu();
        }

        public void Draw()
        {
            m_centerButton.Draw();
            m_worldContextMenu.Draw();
        }

        public virtual void Update() { }

        public void LoadContent()
        {
            m_centerButton.LoadContent();
            m_worldContextMenu.LoadContent();
        }

        public bool InteractGesture(GestureSample gesture)
        {
            bool ret = false;
            ret |= m_centerButton.InteractGesture(gesture);
            ret |= m_worldContextMenu.InteractGesture(gesture);
            return ret;
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }

        public void ShowContextMenu(Dictionary<string, ScreenObjectTapDelegate> contextMenuItems)
        {
            m_worldContextMenu.Show(contextMenuItems);
        }

        public void HideContextMenu()
        {
            m_worldContextMenu.Hide();
        }
    }
}
