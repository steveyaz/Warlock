using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    public class ImageScreenObject : ScreenObjectBase, IDrawable
    {
        public void Draw()
        {
            if (ScreenPosition.X + Size.X > 0
                && ScreenPosition.X < WarlockGame.Graphics.PreferredBackBufferWidth
                && ScreenPosition.Y + Size.Y > 0
                && ScreenPosition.Y < WarlockGame.Graphics.PreferredBackBufferHeight)
            {
                WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                WarlockGame.Batch.Draw(WarlockGame.TextureDictionary[AssetName], ScreenPosition, Color.White);
                WarlockGame.Batch.End();
            }
        }

        public virtual void Update() { }

        public void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture(AssetName);
            Size = new Vector2(WarlockGame.TextureDictionary[AssetName].Width, WarlockGame.TextureDictionary[AssetName].Height);
        }
    }
}
