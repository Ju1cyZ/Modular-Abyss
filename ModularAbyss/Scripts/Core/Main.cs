using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy.Enemies;
using ModularAbyss.Scripts.Components.HudObjectNamespace;
using ModularAbyss.Scripts.Components.HudObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.Scene;
using ModularAbyss.Scripts.Components.Scene.Scenes;
using ModularAbyss.Scripts.Components.TilemapNamespace;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ModularAbyss
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static Main _instance;
        public static Scene ActiveScene;
        public static MouseObject Mouse;

        /* EVENTS */
        public delegate void OnInitializeHandler();
        public event OnInitializeHandler OnInitialize;
        public delegate void OnLoadContentHandler();
        public event OnLoadContentHandler OnLoadContent;
        public delegate void OnUpdateHandler(GameTime gameTime, List<ICollider> colliders);
        public event OnUpdateHandler OnUpdate;
        public delegate void OnDrawHandler(GameTime gameTime, ref SpriteBatch spriteBatch);
        public event OnDrawHandler OnDraw;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = GLOBAL.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = GLOBAL.WINDOW_HEIGHT;

            _instance = this;
            IsMouseVisible = false;
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
            Mouse = new MouseObject();
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

            if (ActiveScene.isInitialized)
            {
                OnUpdate?.Invoke(gameTime, ActiveScene.CollidersObjects);
            }
            base.Update(gameTime);
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
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Permet de recuperer l'instance Main
        /// </summary>
        /// <returns>Le Main</returns>
        public static Main getInstance()
        {
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
