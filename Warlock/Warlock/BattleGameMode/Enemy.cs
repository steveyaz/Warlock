using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarlockDataTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Warlock.BattleGameMode
{
    public class Enemy : BattleObjectBase
    {
        public string EnemyAsset { get; set; }
        private int m_attackTime;
        private const int m_speed = 1;
        private List<Vector2> m_moves;

        public Enemy()
        {
            TapDelegate = ExecuteTap;
            m_attackTime = 0;
            Radius = 32;
        }

        public override void Draw()
        {
            WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["hp_bar"], ScreenPosition + HP_BAR_OFFSET, Color.White);

            Vector2 tilePosition = ScreenPosition + HP_TILE_OFFSET;
            for (int i = 10; i > 0; i--)
            {
                if (HitPoints * 10 / OrigHitPoints >= i)
                {
                    WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["hp_full"], tilePosition, Color.White);
                }
                else
                {
                    WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["hp_empty"], tilePosition, Color.White);
                }
                tilePosition.Y += HP_TILE_HEIGHT;
            }

            if (BattleGameMode.m_Instance.State == BattleGameState.SelectTarget && HitPoints > 0)
            {
                Vector2 ringPosition = new Vector2(BattlePosition.X - 32, BattlePosition.Y - 16);
                WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["selection_ring"], ringPosition, Color.Gold);
            }

            WarlockGame.Batch.End();
            base.Draw();
        }

        public override void Update()
        {
            if (BattleGameMode.m_Instance.State == BattleGameState.RunningBattle && HitPoints > 0)
            {
                if (Vector2.Distance(BattlePosition, BattleGameMode.m_Instance.PlayerBattleObject.BattlePosition) < 70)
                {
                    if (m_attackTime == 30)
                    {
                        BattleGameMode.m_Instance.PlayerBattleObject.HitPoints -= 1;
                        BattleGameMode.m_Instance.FEndBattleConditions();
                        m_attackTime = 0;
                    }
                    else
                    {
                        m_attackTime++;
                    }
                }
                else
                {
                    bool fFindNewPath = true;

                    if (m_moves != null)
                    {
                        fFindNewPath = false;
                        int moves = Math.Min(m_speed, m_moves.Count);
                        for (int i = 0; i < moves; i++)
                        {
                            fFindNewPath = BattleGameMode.m_Instance.FIntersectsBattleObject(m_moves[i], this);
                            if (fFindNewPath)
                                break;
                        }
                    }

                    if (fFindNewPath)
                        m_moves = BattleUtil.FindPath(BattlePosition, BattleGameMode.m_Instance.PlayerBattleObject.BattlePosition, BattleGameMode.m_Instance.PlayerBattleObject.Radius + Radius + 3, this);

                    int moves2 = Math.Min(m_speed, m_moves.Count);
                    for (int i = 0; i < moves2; i++)
                    {
                        if (i == moves2 - 1)
                            BattlePosition = m_moves[0];
                        m_moves.RemoveAt(0);
                    }
                }
            }
        }

        public override void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture("hp_bar");
            WarlockGame.Instance.EnsureTexture("hp_full");
            WarlockGame.Instance.EnsureTexture("hp_empty");
            WarlockGame.Instance.EnsureTexture("selection_ring");
            WarlockGame.Instance.EnsureTexture(AssetName + "_dead");
            base.LoadContent();
        }

        public void Initialize()
        {
            EnemyData enemyData = WarlockGame.Instance.Content.Load<EnemyData>(@EnemyAsset);
            AssetName = enemyData.BattleImageAsset;
            HitPoints = enemyData.HitPoints;
            OrigHitPoints = enemyData.HitPoints;
        }

        public void ExecuteTap()
        {
            BattleGameMode.m_Instance.ExecuteTap(this);
        }
    }
}
