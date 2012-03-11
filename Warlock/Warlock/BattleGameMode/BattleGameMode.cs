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
        Victory,
        Defeat
    }

    public class BattleGameMode : IGameMode
    {
        public static BattleGameMode m_Instance;

        public BattleGameState State { get; set; }
        public Player PlayerBattleObject { get; set; }

        private List<IDrawable> m_drawable;
        private string m_battleID;
        private HUD m_HUD;
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
                    PlayerBattleObject = new Player()
                    {
                        BattlePosition = new Vector2(objectData.BattleXCoord, objectData.BattleYCoord)
                    };
                }
                else if (objectData.ObjectType == BattleObjectType.Enemy)
                {
                    Enemy enemy = new Enemy()
                    {
                        EnemyAsset = objectData.BattleObjectAssetName,
                        BattlePosition = new Vector2(objectData.BattleXCoord, objectData.BattleYCoord)
                    };
                    enemy.Initialize();
                    m_enemies.Add(enemy);
                }
            }

            // these should always be added in opposite order so that objects drawn on top get interaction priority
            foreach (Enemy enemy in m_enemies)
                m_drawable.Add(enemy);
            m_drawable.Add(PlayerBattleObject);
            m_drawable.Add(m_HUD);

            foreach (IDrawable drawable in m_drawable)
                drawable.LoadContent();

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
                PlayerBattleObject.Update();
            }
            else if (State == BattleGameState.AnimatingSpell)
            {
                PlayerBattleObject.Update();
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

                        // exit on victory or defeat
                        case BattleGameState.Victory:
                        case BattleGameState.Defeat:
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
                    Player = PlayerBattleObject,
                    Target = occupyingObject
                };
            }

            m_selectedTarget = occupyingObject;
            PlayerBattleObject.CastSpell(m_selectedAction.AssetName);
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
            m_selectedTarget.HitPoints -= 4;
            if (m_selectedTarget.HitPoints <= 0)
                m_selectedTarget.AssetName += "_dead";
            if (!FEndBattleConditions())
                State = BattleGameState.SelectAction;
        }

        public bool FEndBattleConditions()
        {
            if (PlayerBattleObject.HitPoints <= 0)
            {
                State = BattleGameState.Defeat;
                return true;
            }

            foreach (Enemy enemy in m_enemies)
                if (enemy.HitPoints > 0)
                {
                    return false;
                }

            State = BattleGameState.Victory;
            return true;
        }

        public bool FIntersectsBattleObject(Vector2 center, BattleObjectBase self)
        {
            foreach (Enemy enemy in m_enemies)
            {
                if (enemy != self && Vector2.Distance(center, enemy.BattlePosition) < self.Radius + enemy.Radius)
                    return true;
            }

            if (PlayerBattleObject != self && Vector2.Distance(center, PlayerBattleObject.BattlePosition) < self.Radius + PlayerBattleObject.Radius)
                return true;

            return false;
        }
    }
}
