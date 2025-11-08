using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy.Enemies;
using ModularAbyss.Scripts.Components.HudObjectNamespace;
using ModularAbyss.Scripts.Components.HudObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.TilemapNamespace;
using ModularAbyss.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Initialize();
            LoadContent();
            isInitialized = true;
        }

        /// <summary>
        /// Permet d'initialiser les elements
        /// </summary>
        protected virtual void Initialize()
        {
            CreateGameObjects();
            CreateHudObjects();

            foreach (GameObject item in GameObjects.Values)
            {
                item.Initialize();
            }

            //Recupere tous les elemets de la liste qui implementes l'interface ICollider et l'ajoute a colliders
            foreach (ICollider collider in GameObjects.Values.Where(gameObject => typeof(ICollider).IsAssignableFrom(gameObject.GetType())))
            {
                collider.SetColliderBounds();
                CollidersObjects.Add(collider);
            }
        }

        protected virtual void Update(GameTime gameTime, List<ICollider> colliders)
        {
            UpdateHudObjects();
        }

        protected virtual void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {

        }

        protected virtual void LoadContent()
        {
        }


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
        public void DestroyScene()
        {
            foreach (GameObject item in GameObjects.Values)
            {
                item.Destroy();
            }

            foreach (HudObject item in HudObjects.Values)
            {
                item.Destroy();
            }
        }

    }
}
