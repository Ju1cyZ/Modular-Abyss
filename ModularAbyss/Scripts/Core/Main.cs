using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Components.Scene;
using ModularAbyss.Scripts.Components.Scene.Scenes;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ModularAbyss
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static Main _instance;
        public static Scene ActiveScene;

        int frameCount = 0;
        double elapsedTime = 0;
        public static int FPS = 0;

        /* EVENTS */
        public delegate void OnInitializeHandler();
        public delegate void OnLoadContentHandler();
        public delegate void OnUpdateHandler(GameTime gameTime);
        public delegate void OnDrawHandler(GameTime gameTime, ref SpriteBatch spriteBatch);
        public delegate void OnPlayerDrawHandler(GameTime gameTime, ref SpriteBatch spriteBatch);
        public delegate void OnInGameHudDrawHandler(GameTime gameTime, ref SpriteBatch spriteBatch); // ex : Bar de vie des enemies
        public delegate void OnHudDrawHandler(GameTime gameTime, ref SpriteBatch spriteBatch);

        public delegate void OnTwoGameObjectsCollisionHandler(ICollider object1, ICollider object2);

        public event OnInitializeHandler OnInitialize;
        public event OnLoadContentHandler OnLoadContent;
        public event OnUpdateHandler OnUpdate;
        public event OnDrawHandler OnDraw;
        public event OnPlayerDrawHandler OnPlayerDraw;
        public event OnInGameHudDrawHandler OnInGameHudDraw;
        public event OnHudDrawHandler OnHudDraw;

        public event OnTwoGameObjectsCollisionHandler OnTwoGameObjectsCollision;

        private double timeBerforeNextCollisionCheck = 0;

        private Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = GLOBAL.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = GLOBAL.WINDOW_HEIGHT;

            _instance = this;

            GameManager.Camera = new CameraObject();

            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            _graphics.ApplyChanges();
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
            LoadScene<MainMenuScene>();
            //ActiveScene = new MainMenuScene();
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
                OnUpdate?.Invoke(gameTime);
            }

            if (ActiveScene.isInitialized && timeBerforeNextCollisionCheck < 0)
            {
                timeBerforeNextCollisionCheck = GetTimeBetweenCollisionCheck(); // temps entre verification de collision
                Debug.Print($"Time between collision detections : {timeBerforeNextCollisionCheck}");
                CheckCollision(ActiveScene.CollidersObjects, gameTime);
            }
            else if(ActiveScene.isInitialized)
            {
                timeBerforeNextCollisionCheck -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            frameCount++;

            if (elapsedTime >= 1.0)
            {
                FPS = frameCount;
                frameCount = 0;
                elapsedTime = 0;
            }

            base.Update(gameTime);
            //Debug.Print($"FPS : {FPS}");
        }

        /// <summary>
        /// Dessine les elements du Jeux
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            Color backgroundColor = new Color();
            
            backgroundColor.R = 29;
            backgroundColor.G = 28;
            backgroundColor.B = 43;

            GraphicsDevice.Clear(backgroundColor);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (ActiveScene.isInitialized)
            {
                OnDraw?.Invoke(gameTime, ref _spriteBatch);
                OnPlayerDraw?.Invoke(gameTime, ref _spriteBatch);
                OnInGameHudDraw?.Invoke(gameTime, ref _spriteBatch);
                OnHudDraw?.Invoke(gameTime, ref _spriteBatch);
            }
            InputManager.Draw(gameTime, ref _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Permet de verifier les collisions entre tous les ICollider
        /// </summary>
        /// <param name="colliders">La liste de ICollider</param>
        /// <param name="gameTime">Le deltatime</param>
        public void CheckCollision(List<ICollider> colliders, GameTime gameTime)
        {
            ConcurrentBag<(ICollider A, ICollider B)> collisions = new ConcurrentBag<(ICollider A, ICollider B)>();
           
            Vector2 force = Vector2.Zero;
            float separationRadius = 80f;   // distance minimale entre ennemis
            float separationStrength = 1.5f;

            Vector2 mapSize = ActiveScene.GetPlayableMapSize();
            Vector2 mapPos = ActiveScene.GetPlayableMapPosition();

            Parallel.For(0, colliders.Count, I =>
            {
                for (int II = I + 1; II < colliders.Count; II++) // Commence a i + 1 pour eviter les doublons
                {
                    ICollider a = colliders[I];
                    ICollider b = colliders[II];

                    if (a != b && a.Bounds.Intersects(b.Bounds))
                    {
                        if (a.GetType() != b.GetType())
                        {
                            collisions.Add((a, b));
                        }
                    }

                    // Separation des gameobjects
                    if (a.GetType() != typeof(Player))
                    {
                        Vector2 diff = (a as GameObject).Position - (b as GameObject).Position;
                        float distance = diff.Length();

                        if (distance > 0 && distance < separationRadius)
                        {
                            diff.Normalize();
                            force = diff * (separationRadius - distance);

                            Vector2 mouvement = force * separationStrength * (float)gameTime.ElapsedGameTime.TotalSeconds;

                            // On applique la force au gameobject
                            (a as GameObject).Position += mouvement;

                            // On verifie si le ICollider est en dehors de la map
                            if ((a as GameObject).Position.X < mapPos.X - (a as GameObject).Bounds.Width || (a as GameObject).Position.X > mapPos.X + mapSize.X - (a as GameObject).Bounds.Width * 2 ||
                                (a as GameObject).Position.Y < mapPos.Y - (a as GameObject).Bounds.Height || (a as GameObject).Position.Y > mapPos.Y + mapSize.Y - (a as GameObject).Bounds.Height * 2)
                            {
                                // Si oui, on annule le mouvenement
                                (a as GameObject).Position -= mouvement;
                            }
                        }

                    }
                }
            });

            // On declanche les events de maniere non parallel (sequentielle)
            foreach ((ICollider A, ICollider B) detectedCollision in collisions)
            {
                OnTwoGameObjectsCollision?.Invoke(detectedCollision.A, detectedCollision.B);
            }
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
            if (ActiveScene != null)
                ActiveScene.DestroyScene();

            ActiveScene = new T();
            ActiveScene.Initialize();
        }

        /// <summary>
        /// Permet de calculer le temps entre deux verification de collision
        /// </summary>
        /// <returns></returns>
        public double GetTimeBetweenCollisionCheck()
        {
            double time = (double)ActiveScene.CollidersObjects.Count / 20000;
            if (time >= 0.04)
            {
                return 0.04;
            }
            return time;
        }

    }
}
