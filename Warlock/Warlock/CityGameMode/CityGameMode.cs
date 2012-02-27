using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Warlock
{
    class CityGameMode : IGameMode
    {
        public static CityGameMode m_Instance;
        private ExitCityButton m_exitButton;

        private List<IDrawable> m_drawable;
        private List<IInteractable> m_interactable;

        CityBase CurrentCity;


        // STEVEYAZ - added this to show you what I was thinking for passing the city context information to the game mode
        public string CityAssetName { get; set; }


        /*private void InitCity(CityEnum city)
        {
            switch (city)
            {
                case CityEnum.Albador:
                    CurrentCity = new Albador();
                    break;
                case CityEnum.Hibador:
                    CurrentCity = new Hibador();
                    break;
                case CityEnum.Midador:
                    CurrentCity = new Midador();
                    break;
                default:
                    break;
            }
        }*/

        public void Initialize()
        {
            m_Instance = this;
            m_drawable = new List<IDrawable>();
            m_interactable = new List<IInteractable>();

            //WorldGameMode wgm = (WorldGameMode)WarlockGame.Instance.m_worldGameMode;
            //WorldCity CurrentCity = (WorldCity)wgm.m_worldEventDestination;
            //InitCity(CityEnum.Albador);

            // STEVEYAZ - not actually what we should do, but showing how I thought CityAssetName could be used
            if (CityAssetName == "albador")
                CurrentCity = new Albador();
            else
                CurrentCity = new Hibador();

            m_drawable.Add(this.CurrentCity);
            m_interactable.Add(this.CurrentCity);

            m_exitButton = new ExitCityButton(5, 5);
            m_drawable.Add(m_exitButton);
            m_interactable.Add(m_exitButton);

            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        public void Draw()
        {
            // Build draw list based on player location.
            WarlockGame.Graphics.GraphicsDevice.Clear(Color.Red);

            foreach (IDrawable idraw in m_drawable)
                idraw.Draw();

            return;
        }

        public void Update()
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.Instance.EnterWorldGameMode();

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                foreach (IInteractable interactable in m_interactable)
                    interactable.InteractGesture(gesture);
            }
        }

        public void LoadContent()
        {
            foreach (IDrawable idraw in m_drawable)
                idraw.LoadContent();
        }
    }
}
