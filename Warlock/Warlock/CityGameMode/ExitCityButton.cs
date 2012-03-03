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
            WarlockGame.Instance.EnsureTexture("leavecity");
            ExitButton = new Rectangle(x, y, WarlockGame.TextureDictionary["leavecity"].Width, WarlockGame.TextureDictionary["leavecity"].Height);
        }

        public void Draw()
        {
            WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["leavecity"], ExitButton, Color.White);
            WarlockGame.Batch.End();
        }

        public virtual void Update() { }

        public bool InteractGesture(GestureSample gesture)
        {
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < ExitButton.X + WarlockGame.TextureDictionary["leavecity"].Width && gesture.Position.X > ExitButton.X
                && gesture.Position.Y < ExitButton.Y + WarlockGame.TextureDictionary["leavecity"].Height && gesture.Position.Y > ExitButton.Y)
            {
                Execute();
                return true;
            }
            return false;
        }

        public void InteractLocation(TouchLocation location)
        {

        }

        public void Execute()
        {
            WarlockGame.Instance.EnterWorldGameMode();
        }

        public void LoadContent()
        {
            WarlockGame.Instance.EnsureFont("warlock_button");
        }
    }
}
