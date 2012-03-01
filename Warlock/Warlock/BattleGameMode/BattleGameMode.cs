using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using WarlockDataTypes;

namespace Warlock.BattleGameMode
{
    public class BattleGameMode : IGameMode
    {
        public static BattleGameMode m_Instance;

        private List<IDrawable> m_drawable;
        private List<IInteractable> m_interactable;

        private string m_battleID;
        private HUD m_HUD;
        private Player m_player;
        private List<Enemy> m_enemies;

        private ActionButton m_selectedAction;

        public BattleGameMode(string battleID)
        {
            m_battleID = battleID;
        }

        public void Initialize()
        {
            m_Instance = this;

            m_drawable = new List<IDrawable>();
            m_interactable = new List<IInteractable>();

            m_HUD = new HUD();
            
            // Battle objects from XML
            BattleObjectData[] battleObjects = WarlockGame.Instance.Content.Load<BattleObjectData[]>(@m_battleID);
            m_enemies = new List<Enemy>();
            foreach (BattleObjectData objectData in battleObjects)
            {
                if (objectData.ObjectType == BattleObjectType.Player)
                {
                    m_player = new Player()
                    {
                        AssetName = "graywizard",
                        ScreenPosition = new Vector2(objectData.BattleXCoord, objectData.BattleYCoord)
                    };
                }
                else if (objectData.ObjectType == BattleObjectType.Enemy)
                {
                    m_enemies.Add(new Enemy()
                    {
                        AssetName = objectData.BattleObjectAssetName,
                        ScreenPosition = new Vector2(objectData.BattleXCoord, objectData.BattleYCoord)
                    });
                }
            }

            // these should always be added in opposite order so that objects drawn on top get interaction priority
            foreach (Enemy enemy in m_enemies)
                m_drawable.Add(enemy);
            m_drawable.Add(m_player);
            m_drawable.Add(m_HUD);

            m_interactable.Add(m_HUD);
            m_interactable.Add(m_player);
            foreach (Enemy enemy in m_enemies)
                m_interactable.Add(enemy);

            // Enable only certain gestures
            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        public void Draw()
        {
            // TODO: clear to black once we have backgrounds
            WarlockGame.Graphics.GraphicsDevice.Clear(Color.ForestGreen);

            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();
        }

        public void Update()
        {
            // Go back to world view
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.Instance.EnterWorldGameMode();

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                foreach (IInteractable interactable in m_interactable)
                    if (interactable.InteractGesture(gesture))
                        break;
            }
            else
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                // No multitouch here
                if (touchCollection.Count == 1)
                    foreach (IInteractable interactable in m_interactable)
                        interactable.InteractLocation(touchCollection[0]);
            }

            foreach (IDrawable drawable in m_drawable)
                drawable.Update();
        }

        public void LoadContent()
        {
            foreach (IDrawable drawable in m_drawable)
                drawable.LoadContent();
        }

        public void ExecuteTap(ActionButton action)
        {
            m_selectedAction = action;
        }

        public void ExecuteTap(BattleObjectBase occupyingObject)
        {
            if (m_selectedAction != null)
            {
                switch (m_selectedAction.AssetName)
                {
                    case "meteor":
                        occupyingObject.FDraw = false;
                        m_selectedAction = null;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
