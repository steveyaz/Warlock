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

            m_GameModes.Add(GameModeIndex.City, new CityGameMode(CityEnum.Albador));
            m_GameModes[GameModeIndex.City].Initialize();

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

            // Give game modes a chance to load content
            foreach (IGameMode gameMode in m_GameModes.Values)
                gameMode.LoadContent();
            
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

        public void EnsureTexture(string assetName)
        {
            if (!m_textures.ContainsKey(assetName))
                m_textures.Add(assetName, WarlockGame.m_Instance.Content.Load<Texture2D>(assetName));
        }
    }
}
