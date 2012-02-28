using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Warlock.WorldGameModeNS
{
    public class WorldObjectBase : ImageScreenObject
    {
        public string ObjectID { get; set; }
        public Vector2 WorldPosition { get; set; }
        public Dictionary<string, ScreenObjectTapDelegate> ContextMenuItems { get; set; }

        public override void Update()
        {
            ScreenPosition = WorldGameMode.WorldToScreen(WorldPosition);
        }
    }
}
