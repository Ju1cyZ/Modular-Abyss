using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Scene.Scenes;
using ModularAbyss.Scripts.Components.Tilemaps;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;
using System.Linq;

namespace ModularAbyss.Scripts.Components.Scene
{
    public abstract class Scene
    {
        protected Main main;
        public Tilemap tilemap;
        public TilemapCollider tilemapCollider;
        public bool isInitialized = false;

        //Tous les gameObjects
        public Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();

        //Tous les hudObjects
        public Dictionary<string, HudObject> HudObjects = new Dictionary<string, HudObject>();

        public Player Player
        {
            get
            {
                return GameObjects.GetValueOrDefault("Player") as Player;
            }
        }

        public List<ICollider> CollidersObjects
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

        public Scene()
        {
            //Recupere l'instance de Game1 
            main = Main.getInstance();

            //On s'abonne aux events
            main.OnUpdate += Update;
            main.OnDraw += Draw;
        }

        /// <summary>
        /// Permet d'initialiser les elements
        /// </summary>
        public virtual void Initialize()
        {
            CreateGameObjects();
            CreateHudObjects();

            foreach (GameObject item in GameObjects.Values)
            {
                item.Initialize();
            }
            LoadContent();
            isInitialized = true;
        }

        protected virtual void Update(GameTime gameTime)
        {
            UpdateHudObjects();

            if (InputManager.KeyboardObject.kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.M))
            {
                List<Enemy> spawnedEnemies = (Main.ActiveScene as GameScene).currentWave.SpawnEnemies();
            }
        }

        protected virtual void Draw(GameTime gameTime, ref SpriteBatch spriteBatch) 
        {
            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(Main.getInstance().GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.LightCyan });
                spriteBatch.Draw(_texture, new Rectangle(GetPlayableMapPosition().ToPoint() + CameraObject.Position.ToPoint(), GetPlayableMapSize().ToPoint()), Color.White);
            }
        }

        protected virtual void LoadContent() { }


        /// <summary>
        /// Créer les gameobjects
        /// </summary>
        protected abstract void CreateGameObjects();

        /// <summary>
        /// Créer les hudObjects
        /// </summary>
        protected abstract void CreateHudObjects();

        /// <summary>
        /// Mets a jour les HudObjects
        /// </summary>
        protected virtual void UpdateHudObjects() { }

        public void DestroyGameobject(GameObject gameObject)
        {
            //On retire l'element du dictionnaire
            foreach (var item in GameObjects.Where(e => e.Value == gameObject))
            {
                GameObjects.Remove(item.Key);
            }
        }

        public void DestroyHudobject(HudObject hudObject)
        {
            //On retire l'element du dictionnaire
            foreach (var item in HudObjects.Where(e => e.Value == hudObject))
            {
                HudObjects.Remove(item.Key);
            }
        }

        /// <summary>
        /// Permet de detruire une scene
        /// </summary>
        public virtual void DestroyScene()
        {
            main.OnUpdate -= Update;
            main.OnDraw -= Draw;

            foreach (GameObject item in GameObjects.Values)
            {
                item.Destroy();
            }

            foreach (HudObject item in HudObjects.Values)
            {
                item.Destroy();
            }
        }

        public abstract Vector2 GetPlayableMapSize();

        public abstract Vector2 GetPlayableMapPosition();

    }
}
