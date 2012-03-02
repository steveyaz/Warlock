using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Warlock.BattleGameMode
{
    public class HUD : IDrawable, IInteractable
    {
        ActionButton m_actionButton;
        TextScreenObject m_battleTime;
        TextScreenObject m_instructions;

        public HUD()
        {
            // Center-on-player button
            m_actionButton = new ActionButton()
            {
                AssetName = "meteor",
                ScreenPosition = new Vector2(10, 422)
            };

            // Battle time counter
            m_battleTime = new TextScreenObject()
            {
                AssetName = "Warlock",
                ScreenPosition = new Vector2(4, 4),
                TextColor = Color.White,
                Text = BattleGameMode.m_Instance.BattleTime.ToString()
            };

            m_instructions = new TextScreenObject()
            {
                AssetName = "Warlock",
                ScreenPosition = new Vector2(250, 40),
                TextColor = Color.Black,
                Text = "Choose an action"
            };
        }

        public void Draw()
        {
            if (BattleGameMode.m_Instance.Paused && !BattleGameMode.m_Instance.ChooseTarget && !BattleGameMode.m_Instance.Victory)
                m_actionButton.Draw();
            if (BattleGameMode.m_Instance.Paused)
                m_instructions.Draw();
            m_battleTime.Draw();
        }

        public void Update()
        {
            m_battleTime.Text = BattleGameMode.m_Instance.BattleTime.ToString();

            if (BattleGameMode.m_Instance.Victory)
                m_instructions.Text = "Victory!";
            else if (BattleGameMode.m_Instance.ChooseTarget)
                m_instructions.Text = "Choose a target";
            else if (!BattleGameMode.m_Instance.JustFinishedExecutingCast)
                m_instructions.Text = "Choose a new action or tap to unpause";
            else if (BattleGameMode.m_Instance.JustFinishedExecutingCast)
                m_instructions.Text = "Choose an action";
        }

        public void LoadContent()
        {
            m_actionButton.LoadContent();
            m_battleTime.LoadContent();
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
