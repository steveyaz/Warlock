using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock.WorldGameModeNS
{
    public class WorldPlayer : ImageScreenObject
    {
        private Vector2 m_playerWorldPosition;
        public Vector2 PlayerWorldPosition
        {
            get
            {
                return m_playerWorldPosition;
            }
        }

        private bool m_moving;
        public bool Moving
        {
            get
            {
                return m_moving;
            }
        }

        private Vector2 m_toPosition;
        private Vector2 m_lastDelta;
        private Vector2 m_velocity;

        public WorldPlayer(int x, int y)
        {
            m_playerWorldPosition = new Vector2(x, y);
            m_moving = false;
            AssetName = "graywizard";
        }

        public override void Update()
        {
            if (m_moving)
            {
                m_playerWorldPosition += m_velocity;
                WorldGameMode.m_Instance.CenterOnPlayer();
                if (m_lastDelta.Length() < (m_toPosition - PlayerWorldPosition).Length())
                {
                    m_moving = false;
                    WorldGameMode.m_Instance.ArrivedAtDestination();
                }
                else
                {
                    m_lastDelta = m_toPosition - PlayerWorldPosition;
                }
            }
            ScreenPosition = WorldGameMode.WorldToScreen(m_playerWorldPosition);
        }

        public void MoveTo(Vector2 toPosition)
        {
            m_toPosition = toPosition;
            m_lastDelta = m_toPosition - PlayerWorldPosition;
            m_velocity = m_toPosition - PlayerWorldPosition;
            m_velocity.Normalize();
            m_velocity *= 6;
            m_moving = true;
        }

        public void ZoomToDestination()
        {
            if (m_moving)
            {
                m_playerWorldPosition = m_toPosition;
            }
        }
    }
}
