using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    class WorldPlayer : DrawableBase, IInteractable
    {
        private Vector2 m_position;

        public WorldPlayer()
        {
            m_position = new Vector2(WarlockGame.m_graphics.PreferredBackBufferWidth / 2, WarlockGame.m_graphics.PreferredBackBufferHeight / 2);
        }

        public override void Draw()
        {
            WarlockGame.m_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.m_spriteBatch.Draw(WarlockGame.m_textures["graywizard"], m_position, Color.White);
            WarlockGame.m_spriteBatch.End();
        }

        public void InteractGesture(GestureSample gesture)
        {
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }
    }
}
