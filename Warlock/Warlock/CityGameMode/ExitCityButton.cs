using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    class ExitCityButton : IDrawable, IInteractable
    {
        private Rectangle ExitButton;

        public ExitCityButton(int x, int y)
        {
            WarlockGame.m_Instance.EnsureTexture("leavecity");
            ExitButton = new Rectangle(x, y, WarlockGame.m_textures["leavecity"].Width, WarlockGame.m_textures["leavecity"].Height);
        }

        public void Draw()
        {
            WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures["leavecity"], ExitButton, Color.White);
            WarlockGame.m_spriteBatch.End();
        }

        public void InteractGesture(GestureSample gesture)
        {
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < ExitButton.X + WarlockGame.m_textures["leavecity"].Width && gesture.Position.X > ExitButton.X
                && gesture.Position.Y < ExitButton.Y + WarlockGame.m_textures["leavecity"].Height && gesture.Position.Y > ExitButton.Y)
            {
                Execute();
            }
        }

        public void InteractLocation(TouchLocation location)
        {

        }

        public void Execute()
        {
            WarlockGame.m_Instance.ChangeGameMode(GameModeIndex.World);
        }
    }
}
