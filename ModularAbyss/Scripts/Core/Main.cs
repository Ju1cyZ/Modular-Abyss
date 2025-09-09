using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using System.Collections.Generic;
using ModularAbyss.Scripts.Structures;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy.Enemies;
using System.Linq;
using ModularAbyss.Scripts.Interfaces;
using System;

namespace ModularAbyss
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static Main _instance;

        //Tous les gameObjects
        public static Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();

        public static List<ICollider> CollidersObjects = new List<ICollider>();

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
        }

        /// <summary>
        /// Permet d'initialiser les elements
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            CreateGameObjects();

            //Recupere tous les elemets de la liste qui implementes l'interface ICollider et l'ajoute a colliders
            foreach (ICollider collider in GameObjects.Values.Where(gameObject => typeof(ICollider).IsAssignableFrom(gameObject.GetType())))
            {
                collider.SetColliderBounds();
                CollidersObjects.Add(collider);
            }

            OnInitialize?.Invoke();
        }

        /// <summary>
        /// Permet de charger le contenu
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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
            OnUpdate?.Invoke(gameTime, CollidersObjects);
            base.Update(gameTime);
        }

        /// <summary>
        /// Dessine les elements du Jeux
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            OnDraw?.Invoke(gameTime, ref _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Create all games objects
        /// </summary>
        private void CreateGameObjects()
        {
            GameObjects.Add("Slime", new Slime());
            GameObjects.GetValueOrDefault("Slime").Position = new Vector2(500, 200);

            GameObjects.Add("Slime_01", new Slime());
            GameObjects.GetValueOrDefault("Slime").Position = new Vector2(700, 200);

            GameObjects.Add("Player", new Player());
        }

        /// <summary>
        /// Permet de recuperer l'instance Main
        /// </summary>
        /// <returns>Le Main</returns>
        public static Main getInstance()
        {
            return _instance;
        }

    }
}
