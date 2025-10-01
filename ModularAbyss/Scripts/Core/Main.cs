using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy.Enemies;
using ModularAbyss.Scripts.Components.HudObject;
using ModularAbyss.Scripts.Components.HudObject.Objects;
using ModularAbyss.Scripts.Components.TilemapNamespace;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;
using System.Linq;

namespace ModularAbyss
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static Main _instance;

        //Tous les gameObjects
        public static Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();

        //Tous les hudObjects
        public static Dictionary<string, HudObject> HudObjects = new Dictionary<string, HudObject>();

        public static List<ICollider> CollidersObjects
        {
            get
            {
                List<ICollider> newList = new List<ICollider>();
                //Recupere tous les elemets de la liste qui implementes l'interface ICollider et l'ajoute a colliders
                foreach (ICollider collider in GameObjects.Values.Where(gameObject => typeof(ICollider).IsAssignableFrom(gameObject.GetType())))
                {
                    collider.SetColliderBounds();
                    newList.Add(collider);
                }
                return newList;
            }
        }

        /* EVENTS */
        public delegate void OnInitializeHandler();
        public event OnInitializeHandler OnInitialize;
        public delegate void OnLoadContentHandler();
        public event OnLoadContentHandler OnLoadContent;
        public delegate void OnUpdateHandler(GameTime gameTime, List<ICollider> colliders);
        public event OnUpdateHandler OnUpdate;
        public delegate void OnDrawHandler(GameTime gameTime, ref SpriteBatch spriteBatch);
        public event OnDrawHandler OnDraw;

        internal static Tilemap floorTilemap;
        internal static TilemapCollider wallTilemap;

        public Player Player {
            get {
                return GameObjects.GetValueOrDefault("Player") as Player;
            }
        }

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
            CreateHudObjects();

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

            floorTilemap = new Tilemap(TILESET.FLOOR, 10, 5);
            floorTilemap.Position = new Vector2(160, 145);

            wallTilemap = new TilemapCollider(TILESET.WALL, 12, 7);

            defineTiles();

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
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            floorTilemap.Draw(gameTime, ref _spriteBatch);
            wallTilemap.Draw(gameTime, ref _spriteBatch);
            OnDraw?.Invoke(gameTime, ref _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Créer les gameobjects
        /// </summary>
        private void CreateGameObjects()
        {
            GameObjects.Add("Slime", new Slime());
            GameObjects.GetValueOrDefault("Slime").Position = new Vector2(500, 200);
            (GameObjects.GetValueOrDefault("Slime") as Entity).Destroyed += DestroyGameobject;

            GameObjects.Add("Slime_01", new Slime());
            GameObjects.GetValueOrDefault("Slime_01").Position = new Vector2(700, 200);
            (GameObjects.GetValueOrDefault("Slime_01") as Entity).Destroyed += DestroyGameobject;

            GameObjects.Add("Player", new Player());
        }


        /// <summary>
        /// Créer les hudObjects
        /// </summary>
        private void CreateHudObjects()
        {

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
        /// Defini les tiles id dans les tilemaps
        /// </summary>
        public void defineTiles()
        {
            #region Floor
            floorTilemap.SetTile(0, 0, 0);
            for (int i = 0; i < 8; i++)
            {
                floorTilemap.SetTile(1, 1 + i, 0);
            }
            floorTilemap.SetTile(2, 9, 0);

            for (int y = 0; y < 3; y++)
            {
                floorTilemap.SetTile(4, 0, 1 + y);
                for (int i = 0; i < 8; i++)
                {
                    floorTilemap.SetTile(5, 1 + i, 1 + y);
                }
                floorTilemap.SetTile(6, 9, 1 + y);
            }

            floorTilemap.SetTile(8, 0, 4);
            for (int i = 0; i < 8; i++)
            {
                floorTilemap.SetTile(9, 1 + i, 4);
            }
            floorTilemap.SetTile(10, 9, 4);
            #endregion

            #region Wall
            wallTilemap.SetTile(13, 1, 0);
            for (int i = 0; i < 8; i++)
            {
                wallTilemap.SetTile(14, 2 + i, 0);
            }
            wallTilemap.SetTile(15, 10, 0);

            for (int y = 0; y < 6; y++)
            {
                wallTilemap.SetTile(6, 0, 0 + y);
                wallTilemap.SetTile(10, 11, 0 + y);
            }

            wallTilemap.SetTile(18, 0, 6);
            for (int i = 0; i < 10; i++)
            {
                wallTilemap.SetTile(19, 1 + i, 6);
            }
            wallTilemap.SetTile(22, 11, 6);

            /* COllision */

            wallTilemap.Colliders.Add(new Rectangle(new Point(0, 0), new Point(1920, 120)));
            wallTilemap.Colliders.Add(new Rectangle(new Point(0, 1080 - 120), new Point(1920, 120)));

            wallTilemap.Colliders.Add(new Rectangle(new Point(0, 0), new Point(160, 1080)));
            wallTilemap.Colliders.Add(new Rectangle(new Point(1920 - 160, 0), new Point(160, 1080)));

            #endregion
        }

        public void DestroyGameobject(GameObject gameObject)
        {
            //On retire l'element du dictionnaire
            foreach (var item in GameObjects.Where(e => e.Value == gameObject))
            {
                //GameObjects.GetValueOrDefault(item.Key).;
                GameObjects.Remove(item.Key);
            }
        }
    }
}
