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

        public int BattleTime { get; set; }
        public bool Paused { get; set; }
        public bool ChooseTarget { get; set; }
        public bool JustFinishedExecutingCast { get; set; }
        public bool Victory { get; set; }

        private const int m_framesPerBattleTimeUnit = 30;
        private int m_frame;

        private List<IDrawable> m_drawable;
        private string m_battleID;
        private HUD m_HUD;
        private Player m_player;
        private List<Enemy> m_enemies;

        public BattleGameMode(string battleID)
        {
            m_battleID = battleID;
        }

        public void Initialize()
        {
            m_Instance = this;
            m_frame = 0;
            BattleTime = 0;
            Paused = true;
            ChooseTarget = false;
            JustFinishedExecutingCast = true;
            Victory = false;

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
        }

        public void Update()
        {
            if (!Paused)
            {
                m_frame++;
                if (m_frame == m_framesPerBattleTimeUnit)
                {
                    m_frame = 0;
                    // next battle time unit
                    BattleTime++;
                    // notify Enemies and Player of time unit increment
                    foreach (Enemy enemy in m_enemies)
                        enemy.BattleTimeUnitIncrement();
                    m_player.BattleTimeUnitIncrement();
                }
            }

            // Go back to world view
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.Instance.EnterWorldGameMode();

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                // exit on victory
                if (Victory)
                {
                    WarlockGame.Instance.EnterWorldGameMode();
                }
                // pause battle mode so that user can make a new action
                if (!Paused && gesture.GestureType == GestureType.Tap)
                {
                    Paused = true;
                }
                // unpause a pause made by user
                else if (gesture.GestureType == GestureType.Tap && Paused && !ChooseTarget && !m_HUD.InteractGesture(gesture) && !JustFinishedExecutingCast)
                {
                    Paused = false;
                }
                // execute user action
                else
                {
                    foreach (Enemy enemy in m_enemies)
                        if (enemy.InteractGesture(gesture))
                            break;
                }
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
            Paused = false;
            JustFinishedExecutingCast = false;
            m_player.CastSpell(action.AssetName);
        }

        public void ExecuteTap(BattleObjectBase occupyingObject)
        {
            if (ChooseTarget)
                m_player.ExecuteSpell(occupyingObject);
            ChooseTarget = false;
            JustFinishedExecutingCast = true;
        }

        public void CastingFinished()
        {
            Paused = true;
            ChooseTarget = true;
        }

        public void CheckVictoryConditions()
        {
            foreach (Enemy enemy in m_enemies)
                if (enemy.FDraw)
                    return;
            Victory = true;
        }
    }
}
