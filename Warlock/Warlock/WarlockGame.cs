using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Warlock
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class WarlockGame : Microsoft.Xna.Framework.Game
    {
        public static WarlockGame m_Instance;
        public static SpriteBatch m_spriteBatch;
        public static Dictionary<string, Texture2D> m_textures;
        public static SpriteFont m_spriteFont;
        public static GraphicsDeviceManager m_graphics;

        private Dictionary<GameModeIndex, IGameMode> m_GameModes;
        private GameModeIndex m_CurrentGameMode;

        public WarlockGame()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            m_Instance = this;
            m_textures = new Dictionary<string, Texture2D>();

            // initialize game to splash screen
            m_CurrentGameMode = GameModeIndex.Splash;
            m_GameModes = new Dictionary<GameModeIndex, IGameMode>((int)GameModeIndex.Count);

            // initialize the different modes
            m_GameModes.Add(GameModeIndex.Splash, new SplashGameMode());
            m_GameModes[GameModeIndex.Splash].Initialize();

            m_GameModes.Add(GameModeIndex.World, new WorldGameMode());
            m_GameModes[GameModeIndex.World].Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            m_textures.Add("graywizard", this.Content.Load<Texture2D>("graywizard"));
            m_textures.Add("mission", this.Content.Load<Texture2D>("mission"));
            m_textures.Add("worldmap-0-0", this.Content.Load<Texture2D>("worldmap-0-0"));
            m_textures.Add("worldmap-0-1", this.Content.Load<Texture2D>("worldmap-0-1"));
            m_textures.Add("worldmap-0-2", this.Content.Load<Texture2D>("worldmap-0-2"));
            m_textures.Add("worldmap-0-3", this.Content.Load<Texture2D>("worldmap-0-3"));
            m_textures.Add("worldmap-1-0", this.Content.Load<Texture2D>("worldmap-1-0"));
            m_textures.Add("worldmap-1-1", this.Content.Load<Texture2D>("worldmap-1-1"));
            m_textures.Add("worldmap-1-2", this.Content.Load<Texture2D>("worldmap-1-2"));
            m_textures.Add("worldmap-1-3", this.Content.Load<Texture2D>("worldmap-1-3"));
            m_textures.Add("worldmap-2-0", this.Content.Load<Texture2D>("worldmap-2-0"));
            m_textures.Add("worldmap-2-1", this.Content.Load<Texture2D>("worldmap-2-1"));
            m_textures.Add("worldmap-2-2", this.Content.Load<Texture2D>("worldmap-2-2"));
            m_textures.Add("worldmap-2-3", this.Content.Load<Texture2D>("worldmap-2-3"));
            m_textures.Add("worldmap-3-0", this.Content.Load<Texture2D>("worldmap-3-0"));
            m_textures.Add("worldmap-3-1", this.Content.Load<Texture2D>("worldmap-3-1"));
            m_textures.Add("worldmap-3-2", this.Content.Load<Texture2D>("worldmap-3-2"));
            m_textures.Add("worldmap-3-3", this.Content.Load<Texture2D>("worldmap-3-3"));
            m_spriteFont = this.Content.Load<SpriteFont>("Warlock");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            m_GameModes[m_CurrentGameMode].Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            m_GameModes[m_CurrentGameMode].Draw();

            base.Draw(gameTime);
        }

        public void ChangeGameMode(GameModeIndex gamemode)
        {
            m_CurrentGameMode = gamemode;
        }
    }
}
