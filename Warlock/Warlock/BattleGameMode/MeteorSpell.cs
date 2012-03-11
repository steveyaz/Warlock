using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock.BattleGameMode
{
    class MeteorSpell : IDrawable
    {
        public BattleObjectBase Player { get; set; }
        public BattleObjectBase Target { get; set; }
        public bool IsAnimating { get; set; }

        private Vector2 m_currentLocation;
        private Vector2 m_velocity;
        private float m_angle;
        private string m_currentAsset;
        private bool m_reachedDestination;
        private int m_frameCount;

        public void Initialize()
        {
            m_currentLocation = Player.ScreenPosition;
            m_reachedDestination = false;
            m_frameCount = 0;

            m_velocity = Target.ScreenPosition - Player.ScreenPosition;
            m_velocity.Normalize();
            m_angle = (float)Math.Asin(m_velocity.Y);
            m_velocity *= 14;
            m_currentAsset = "meteor_anim_flight1";
        }

        public void Draw()
        {
            WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.Batch.Draw(WarlockGame.TextureDictionary[m_currentAsset], m_currentLocation, null, Color.White, m_angle, Vector2.Zero, 1, SpriteEffects.None, 0);
            WarlockGame.Batch.End();
        }

        public void Update()
        {
            if (m_reachedDestination)
            {
                m_frameCount++;
                if (m_frameCount > 16)
                    BattleGameMode.m_Instance.SpellEffectFinished();
                else if (m_frameCount > 8)
                    m_currentAsset = "meteor_anim_explosion2";
            }
            else if (Vector2.Distance(m_currentLocation, Target.ScreenPosition) < 10)
            {
                m_currentAsset = "meteor_anim_explosion1";
                m_currentLocation = Target.ScreenPosition;
                m_reachedDestination = true;
            }
            else
            {
                m_currentLocation += m_velocity;
            }
        }

        public void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture("meteor_anim_flight1");
            WarlockGame.Instance.EnsureTexture("meteor_anim_explosion1");
            WarlockGame.Instance.EnsureTexture("meteor_anim_explosion2");
            Initialize();
        }
    }
}
