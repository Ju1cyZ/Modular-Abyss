using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.Scene;
using ModularAbyss.Scripts.Components.Scene.Scenes;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;
using ModularAbyss.Scripts.Components.InputDevices;
using ModularAbyss.Scripts.Components;
using System.Diagnostics;
using ModularAbyss.Scripts.Components.Gameobjects.Projectiles;
using ModularAbyss.Scripts.Components.Gameobjects;

namespace ModularAbyss
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static Main _instance;
        public static Scene ActiveScene;
        //public static MouseObject Mouse;

        /* EVENTS */
        public delegate void OnInitializeHandler();
        public event OnInitializeHandler OnInitialize;
        public delegate void OnLoadContentHandler();
        public event OnLoadContentHandler OnLoadContent;
        public delegate void OnUpdateHandler(GameTime gameTime, List<ICollider> colliders);
        public event OnUpdateHandler OnUpdate;
        public delegate void OnDrawHandler(GameTime gameTime, ref SpriteBatch spriteBatch);
        public event OnDrawHandler OnDraw;

        private Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = GLOBAL.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = GLOBAL.WINDOW_HEIGHT;

            _instance = this;

            //_graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;
            //_graphics.ApplyChanges();
        }

        /// <summary>
        /// Permet d'initialiser les elements
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Permet de charger le contenu
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ActiveScene = new MainMenuScene();
            OnLoadContent?.Invoke();
        }

        /// <summary>
        /// L'update. Est appeller toutes les secondes
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update(gameTime, ActiveScene.CollidersObjects);
            
            if (ActiveScene.isInitialized)
            {
                OnUpdate?.Invoke(gameTime, ActiveScene.CollidersObjects);
            }
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                new Fireball(ActiveScene.Player.Position, new Vector2(0,0));
            }
        }

        /// <summary>
        /// Dessine les elements du Jeux
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (ActiveScene.isInitialized)
            {
                OnDraw?.Invoke(gameTime, ref _spriteBatch);
            }
            InputManager.Draw(gameTime, ref _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Permet de recuperer l'instance Main
        /// </summary>
        /// <returns>Le Main</returns>
        public static Main getInstance()
        {
            if (_instance == null)
            {
                _instance = new Main();
            }
            return _instance;
        }

        /// <summary>
        /// Permet de charger une nouvelle scene
        /// </summary>
        /// <typeparam name="T">Le Type de la scene a charger</typeparam>
        public static void LoadScene<T>() where T : Scene, new()
        {
            ActiveScene.DestroyScene();
            ActiveScene = new T();
        }

    }
}
