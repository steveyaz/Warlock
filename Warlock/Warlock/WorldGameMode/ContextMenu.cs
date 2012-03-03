using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Warlock.WorldGameModeNS
{
    public class ContextMenu : IDrawable, IInteractable
    {
        private const int MENU_BOTTOM_LEFT_CORNER_OFFSET_X = -200;
        private const int MENU_BOTTOM_LEFT_CORNER_OFFSET_Y = -20;
        private const int MENU_ITEM_SIZE = 40;

        private List<TextScreenObject> m_contextMenuItems;

        public ContextMenu()
        {
            m_contextMenuItems = new List<TextScreenObject>();
        }

        public void Draw()
        {
            foreach (TextScreenObject contextMenuItem in m_contextMenuItems)
                contextMenuItem.Draw();
        }

        public virtual void Update() { }

        public void LoadContent()
        {
            foreach (TextScreenObject contextMenuItem in m_contextMenuItems)
                contextMenuItem.LoadContent();
        }

        public bool InteractGesture(GestureSample gesture)
        {
            foreach (TextScreenObject contextMenuItem in m_contextMenuItems)
                if (contextMenuItem.InteractGesture(gesture))
                    return true;

            return false;
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }

        public void Show(Dictionary<string, ScreenObjectTapDelegate> contextMenuItems)
        {
            Vector2 position = new Vector2();
            position.X = WarlockGame.Graphics.PreferredBackBufferWidth + MENU_BOTTOM_LEFT_CORNER_OFFSET_X;
            position.Y = WarlockGame.Graphics.PreferredBackBufferHeight + MENU_BOTTOM_LEFT_CORNER_OFFSET_Y;

            foreach (string key in contextMenuItems.Keys)
            {
                position.Y -= MENU_ITEM_SIZE;
                m_contextMenuItems.Add(new TextScreenObject()
                {
                    Text = key,
                    TextColor = Color.Black,
                    ScreenPosition = position,
                    TapDelegate = contextMenuItems[key],
                    AssetName = "warlock_button"
                });
            }

            LoadContent();
        }

        public void Hide()
        {
            m_contextMenuItems.Clear();
        }
    }
}
