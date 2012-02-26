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

        private List<IDrawable> m_drawable;
        private List<IInteractable> m_interactable;

        CityBase CurrentCity;

        public CityGameMode(City cityName)
        {
            InitCity(cityName);
        }

        private void InitCity(City city)
        {
            switch (city)
            {
                case City.Albador:
                    CurrentCity = new Albador();
                    break;
                case City.Hibador:
                    CurrentCity = new Hibador();
                    break;
                case City.Midador:
                    CurrentCity = new Midador();
                    break;
                default:
                    break;
            }
        }

        public void Initialize()
        {
            m_Instance = this;
            m_drawable = new List<IDrawable>();
            m_interactable = new List<IInteractable>();


            m_drawable.Add(CurrentCity);
            m_interactable.Add(CurrentCity);

            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        public void Draw()
        {
            WarlockGame.m_graphics.GraphicsDevice.Clear(Color.Red);

            foreach (IDrawable drawable in m_drawable)
                drawable.Draw();

            return;
        }

        public void Update()
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                WarlockGame.m_Instance.Exit();

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                foreach (IInteractable interactable in m_interactable)
                    interactable.InteractGesture(gesture);
            }
        }

        

        public void LoadContent()
        {
            WarlockGame.m_Instance.EnsureTexture(CurrentCity.CityStr);
        }
    }
}
