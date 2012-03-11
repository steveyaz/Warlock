using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock.BattleGameMode
{
    public class BattleObjectBase : ImageScreenObject
    {
        public int OrigHitPoints { get; set; }
        public int HitPoints { get; set; }
        public int Radius { get; set; }

        private Vector2 m_battlePosition;
        public Vector2 BattlePosition
        {
            get
            {
                return m_battlePosition;
            }
            set
            {
                m_battlePosition = value;
                ScreenPosition = new Vector2(m_battlePosition.X - Size.X / 2, m_battlePosition.Y - Size.Y);
            }
        }

        protected Vector2 HP_BAR_OFFSET = new Vector2() { X = -10, Y = 0 };
        protected Vector2 HP_TILE_OFFSET = new Vector2() { X = -8, Y = 2 };
        protected const int HP_TILE_HEIGHT = 5;

        // override this to only interact with the base of the object
        public override bool InteractGesture(GestureSample gesture)
        {
            if (HitPoints > 0
                && TapDelegate != null
                && gesture.GestureType == GestureType.Tap
                && gesture.Position.X < ScreenPosition.X + Size.X && gesture.Position.X > ScreenPosition.X
                && gesture.Position.Y < ScreenPosition.Y + Size.Y && gesture.Position.Y > ScreenPosition.Y)
            {
                TapDelegate();
                return true;
            }
            return false;
        }

        public override void LoadContent()
        {
            // Trigger ScreenPosition to get set correctly
            BattlePosition = BattlePosition;
            base.LoadContent();
        }
    }
}
