using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Warlock.BattleGameMode
{
    static class BattleUtil
    {
        public static List<Vector2> FindPath(Vector2 current, Vector2 target, int radius, BattleObjectBase self)
        {
            return FindPath(current, target, radius, self, 0);
        }

        private static List<Vector2> FindPath(Vector2 current, Vector2 target, int radius, BattleObjectBase self, int direction)
        {
            bool fOverlapped = false;
            Vector2 newTestLocation = current;
            Vector2 newPositionDirection = Vector2.Transform(Vector2.Normalize(target - current), Matrix.CreateRotationZ((float)(direction * Math.PI / 8)));
            List<Vector2> moves = new List<Vector2>();

            // we've arrived at destination
            if (Vector2.Distance(newTestLocation, target) <= radius)
            {
                moves.Add(newTestLocation);
                return moves;
            }

            // try going along (possibly) rotated path
            newTestLocation += newPositionDirection;
            fOverlapped = BattleGameMode.m_Instance.FIntersectsBattleObject(newTestLocation, self);


            // can't go straight and went straight last time
            if (fOverlapped && direction == 0)
            {
                // should we turn left or right?
                List<Vector2> pathLeft, pathRight;
                pathLeft = FindPath(current, target, radius, self, -1);
                pathRight = FindPath(current, target, radius, self, 1);
                if (pathLeft.Count < pathRight.Count)
                {
                    return pathLeft;
                }
                else
                {
                    return pathRight;
                }
            }
            // can't go along path and turned last time
            else if (fOverlapped)
            {
                // turn in that same direction again
                direction += direction > 0 ? 1 : -1;
                return FindPath(current, target, radius, self, direction);
            }
            // successfully moved along path
            else
            {
                moves.Add(newTestLocation);

                // continue turning in that same direction
                if (direction < 0)
                    direction = -1;
                else if (direction > 0)
                    direction = 1;
                // continue travelling towards the target as long as we can
                else
                {
                    while (!BattleGameMode.m_Instance.FIntersectsBattleObject(newTestLocation += newPositionDirection, self) && Vector2.Distance(newTestLocation + newPositionDirection, target) > radius)
                    {
                        newTestLocation += newPositionDirection;
                        moves.Add(newTestLocation);
                    }
                }

                moves.AddRange(FindPath(newTestLocation, target, radius, self, direction));
                return moves;
            }
        }
    }
}
