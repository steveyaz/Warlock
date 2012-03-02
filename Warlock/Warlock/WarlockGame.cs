using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using Warlock.SplashGameModeNS;
using Warlock.WorldGameModeNS;

namespace Warlock
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class WarlockGame : Microsoft.Xna.Framework.Game
    {
        private static WarlockGame m_warlockGame;
        public static WarlockGame Instance
        {
            get
            {
                return m_warlockGame;
            }
        }

        private static SpriteBatch m_batch;
        public static SpriteBatch Batch
        {
            get
            {
                return m_batch;
            }
        }

        private static Dictionary<string, Texture2D> m_textureDictionary;
        public static Dictionary<string, Texture2D> TextureDictionary
        {
            get
            {
                return m_textureDictionary;
            }
        }

        private static Dictionary<string, SpriteFont> m_fontDictionary;
        public static Dictionary<string, SpriteFont> FontDictionary
        {
            get
            {
                return m_fontDictionary;
            }
        }

        private static GraphicsDeviceManager m_graphics;
        public static GraphicsDeviceManager Graphics
        {
            get
            {
                return m_graphics;
            }
        }

        private static GameTime m_currentGameTime;
        public static GameTime CurrentGameTime
        {
            get
            {
                return m_currentGameTime;
            }
        }

        private IGameMode m_currentGameMode;
        private WorldGameMode m_worldGameMode;

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
            m_warlockGame = this;
            m_textureDictionary = new Dictionary<string, Texture2D>();
            m_fontDictionary = new Dictionary<string, SpriteFont>();

            // initialize game to splash screen
            ChangeGameMode(new SplashGameMode());

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_batch = new SpriteBatch(GraphicsDevice);
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
            m_currentGameTime = gameTime;
            m_currentGameMode.Update();

            base.Update(gameTime);
        } 

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            m_currentGameMode.Draw();

            base.Draw(gameTime);
        }

        public void EnsureTexture(string assetName)
        {
            if (!TextureDictionary.ContainsKey(assetName))
                TextureDictionary.Add(assetName, Content.Load<Texture2D>(assetName));
        }

        public void EnsureFont(string assetName)
        {
            if (!FontDictionary.ContainsKey(assetName))
                FontDictionary.Add(assetName, Content.Load<SpriteFont>(assetName));
        }

        public void StartNewGame()
        {
            m_worldGameMode = new WorldGameMode();
            m_worldGameMode.Initialize();
            m_worldGameMode.LoadContent();
            m_worldGameMode.Update();
            m_currentGameMode = m_worldGameMode;
        }

        public void ChangeGameMode(IGameMode gameMode)
        {
            m_currentGameMode = gameMode;
            m_currentGameMode.Initialize();
            m_currentGameMode.LoadContent();
        }

        public void EnterWorldGameMode()
        {
            m_currentGameMode = m_worldGameMode;
        }
    }
}
