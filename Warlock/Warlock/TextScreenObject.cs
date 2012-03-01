using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    class TextScreenObject : ScreenObjectBase
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public override void Draw()
        {
            if (ScreenPosition.X + Size.X > 0
                && ScreenPosition.X < WarlockGame.Graphics.PreferredBackBufferWidth
                && ScreenPosition.Y + Size.Y > 0
                && ScreenPosition.Y < WarlockGame.Graphics.PreferredBackBufferHeight)
            {
                WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                WarlockGame.Batch.DrawString(WarlockGame.FontDictionary[AssetName], Text, ScreenPosition, TextColor, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                WarlockGame.Batch.End();
            }
        }

        public override void LoadContent()
        {
            WarlockGame.Instance.EnsureFont(AssetName);
            Size = WarlockGame.FontDictionary[AssetName].MeasureString(Text);
        }
    }
}
