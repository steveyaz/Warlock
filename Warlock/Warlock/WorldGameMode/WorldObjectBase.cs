using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    public class WorldObjectBase : IDrawable, IInteractable
    {
        public string AssetName { get; set; }
        public string ObjectID { get; set; }
        public Vector2 ObjectPosition { get; set; }

        public WorldObjectBase(string assetName, string objectID, int x, int y)
        {
            AssetName = assetName;
            ObjectID = objectID;
            ObjectPosition = new Vector2(x, y);
        }

        public virtual void PlayerEnter() { }

        public void Draw()
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(ObjectPosition);

            if (screenVector.X + WarlockGame.TextureDictionary[AssetName].Width > 0
                && screenVector.X < WarlockGame.Graphics.PreferredBackBufferWidth
                && screenVector.Y + WarlockGame.TextureDictionary[AssetName].Height > 0
                && screenVector.Y < WarlockGame.Graphics.PreferredBackBufferHeight)
            {
                WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                WarlockGame.Batch.Draw(WarlockGame.TextureDictionary[AssetName], screenVector, Color.White);
                WarlockGame.Batch.End();
            }
        }

        public void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture(AssetName);
        }

        public void InteractGesture(GestureSample gesture)
        {
            Vector2 screenVector = WorldGameMode.WorldToScreen(ObjectPosition);
            if (gesture.GestureType == GestureType.Tap
                && gesture.Position.X < screenVector.X + WarlockGame.TextureDictionary[AssetName].Width && gesture.Position.X > screenVector.X
                && gesture.Position.Y < screenVector.Y + WarlockGame.TextureDictionary[AssetName].Height && gesture.Position.Y > screenVector.Y)
            {
                WorldGameMode.m_Instance.MarkDestination(this);
                WorldGameMode.m_Instance.MovePlayer(ObjectPosition);
            }
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }
    }
}
