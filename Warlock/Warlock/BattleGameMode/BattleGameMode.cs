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
    public enum BattleGameState
    {
        SelectAction,
        SelectTarget,
        RunningBattle,
        AnimatingSpell,
        Paused,
        Victory
    }

    public class BattleGameMode : IGameMode
    {
        public static BattleGameMode m_Instance;

        public BattleGameState State { get; set; }

        private List<IDrawable> m_drawable;
        private string m_battleID;
        private HUD m_HUD;
        private Player m_player;
        private List<Enemy> m_enemies;
        private ActionButton m_selectedAction;
        private BattleObjectBase m_selectedTarget;
        private IDrawable m_castingSpell;

        public BattleGameMode(string battleID)
        {
            m_battleID = battleID;
        }

        public void Initialize()
        {
            m_Instance = this;
            State = BattleGameState.SelectAction;

            m_drawable = new List<IDrawable>();

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

            // Enable only certain gestures
            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        public void Draw()
        {
            // TODO: clear to black once we have backgrounds
            WarlockGame.Graphics.GraphicsDevice.Clear(Color.ForestGreen);

            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();

            if (State == BattleGameState.AnimatingSpell)
                m_castingSpell.Draw();
        }

        public void Update()
        {
            if (State == BattleGameState.RunningBattle)
            {
                // notify Enemies and Player of time unit increment
                foreach (Enemy enemy in m_enemies)
                    enemy.Update();
                m_player.Update();
            }
            else if (State == BattleGameState.AnimatingSpell)
            {
                m_player.Update();
                m_castingSpell.Update();
            }

            // Go back to world view
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.Instance.EnterWorldGameMode();

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.Tap)
                {
                    switch (State)
                    {
                        // execute user taps on HUD
                        case BattleGameState.SelectAction:
                            m_HUD.InteractGesture(gesture);
                            break;

                        // execute user taps on potential targets
                        case BattleGameState.SelectTarget:
                            foreach (Enemy enemy in m_enemies)
                                if (enemy.FDraw && enemy.InteractGesture(gesture))
                                    break;
                            break;

                        // pause battle mode so that user can make a new action
                        case BattleGameState.RunningBattle:
                            State = BattleGameState.Paused;
                            break;

                        // unpause a pause made by user or execute new action
                        case BattleGameState.Paused:
                            if (!m_HUD.InteractGesture(gesture))
                                State = BattleGameState.RunningBattle;
                            break;

                        // exit on victory
                        case BattleGameState.Victory:
                            WarlockGame.Instance.EnterWorldGameMode();
                            break;

                        default:
                            break;
                    }
                }
            }

            m_HUD.Update();
        }

        public void LoadContent()
        {
            foreach (IDrawable drawable in m_drawable)
                drawable.LoadContent();
        }

        public void ExecuteTap(ActionButton action)
        {
            m_selectedAction = action;
            State = BattleGameState.SelectTarget;
        }

        public void ExecuteTap(BattleObjectBase occupyingObject)
        {
            if (m_selectedAction.AssetName == "meteor_icon")
            {
                m_castingSpell = new MeteorSpell()
                {
                    Player = m_player,
                    Target = occupyingObject
                };
            }

            m_selectedTarget = occupyingObject;
            m_player.CastSpell(m_selectedAction.AssetName);
            State = BattleGameState.RunningBattle;
        }

        public void CastingFinished()
        {
            m_castingSpell.LoadContent();
            State = BattleGameState.AnimatingSpell;
        }

        public void SpellEffectFinished()
        {
            m_castingSpell = null;
            m_selectedTarget.FDraw = false;
            if (CheckVictoryConditions())
                State = BattleGameState.Victory;
            else
                State = BattleGameState.SelectAction;
        }

        public bool CheckVictoryConditions()
        {
            foreach (Enemy enemy in m_enemies)
                if (enemy.FDraw)
                    return false;

            return true;
        }
    }
}
