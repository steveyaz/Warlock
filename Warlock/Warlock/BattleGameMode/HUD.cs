using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Warlock.BattleGameMode
{
    public class HUD : IDrawable, IInteractable
    {
        ActionButton m_actionButton;
        TextScreenObject m_instructions;

        public HUD()
        {
            // Center-on-player button
            m_actionButton = new ActionButton()
            {
                AssetName = "meteor_icon",
                ScreenPosition = new Vector2(10, 422)
            };

            m_instructions = new TextScreenObject()
            {
                AssetName = "warlock_standard",
                ScreenPosition = new Vector2(250, 40),
                TextColor = Color.Black,
                Text = "Choose an action"
            };
        }

        public void Draw()
        {
            if (BattleGameMode.m_Instance.State == BattleGameState.SelectAction || BattleGameMode.m_Instance.State == BattleGameState.Paused)
                m_actionButton.Draw();
            if (BattleGameMode.m_Instance.State == BattleGameState.Paused
                || BattleGameMode.m_Instance.State == BattleGameState.SelectAction
                || BattleGameMode.m_Instance.State == BattleGameState.SelectTarget
                || BattleGameMode.m_Instance.State == BattleGameState.Victory
                || BattleGameMode.m_Instance.State == BattleGameState.Defeat)
                m_instructions.Draw();
        }

        public void Update()
        {
            switch (BattleGameMode.m_Instance.State)
            {
                case BattleGameState.Paused:
                    m_instructions.Text = "Choose a new action or tap to unpause";
                    break;
                case BattleGameState.SelectAction:
                    m_instructions.Text = "Choose an action";
                    break;
                case BattleGameState.SelectTarget:
                    m_instructions.Text = "Choose a target";
                    break;
                case BattleGameState.Victory:
                    m_instructions.Text = "Victory!";
                    break;
                case BattleGameState.Defeat:
                    m_instructions.Text = "Defeat...";
                    break;
                default:
                    m_instructions.Text = "";
                    break;
            }
        }

        public void LoadContent()
        {
            m_actionButton.LoadContent();
            m_instructions.LoadContent();
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
