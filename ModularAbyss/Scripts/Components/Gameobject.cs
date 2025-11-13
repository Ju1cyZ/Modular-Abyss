using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using System.Collections.Generic;

namespace ModularAbyss.Scripts.Components
{
    public abstract class GameObject : ICollider, IDestroyer
    {
        private Texture2D texture2D;
        private Rectangle _collisionRect;
        protected Main Main;
        private bool _created = false;

        private float posX = 0;
        private float posY = 0;

        public delegate void OnDestroyedHandler(GameObject gameObject);
        public event OnDestroyedHandler OnDestroyed;

        /// <summary>
        /// Le rectange de collision
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return _collisionRect;
            }
            set
            {
                _collisionRect = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(posX, posY);
            }
            set
            {
                posX = value.X;
                posY = value.Y;
            }
        }

        /// <summary>
        /// Construire 
        /// </summary>
        /// <param name="needsToBeCreated">Permet de determiner si l'objet se creer tout seul ou doit etre creer avec la fonction Create()</param>
        public GameObject(bool needsToBeCreated = false)
        {
            //Recupere l'instance de Game1 
            Main = Main.getInstance();

            //On s'abonne aux events
            Main.OnInitialize += Initialize;
            Main.OnLoadContent += LoadContent;
            if (!needsToBeCreated)
            {
                Create();
            }
        }

        /// <summary>
        /// Permet d'initializer la variable de bases du gameObject
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Permet de charger le contenu
        /// </summary>
        protected virtual void LoadContent() { }

        /// <summary>
        /// La fonction qui est appele tous les ticks
        /// </summary>
        /// <param name="gameTime">Le temps entre l'appel precedent et l'appel actuel</param>
        /// <param name="colliders"></param>
        protected virtual void Update(GameTime gameTime, List<ICollider> colliders) { }

        /// <summary>
        /// Dessine le gameObject, est appele tous les ticks apres update
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        protected virtual void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetTexture2D(), Position + GameManager.CameraPosition, Color.White);
        }

        /// <summary>
        /// Permet de creer un objet
        /// </summary>
        public virtual void Create()
        {
            if (!_created)
            {
                Main.OnUpdate += Update;
                Main.OnDraw += Draw;
                _created = true;
            }
        }

        /// <summary>
        /// Permet de recuperer la texture du GameObject
        /// </summary>
        /// <returns>La texture</returns>
        public virtual Texture2D GetTexture2D()
        {
            if (texture2D == null)
            {
                //Defini la texture
                setTexture2D(ref texture2D);
            }
            return texture2D;
        }

        /// <summary>
        /// Defini la texture du GameObject
        /// </summary>
        /// <param name="texture2D">La variable qui va etre modifier</param>
        protected abstract void setTexture2D(ref Texture2D texture2D);

        /// <summary>
        /// Defini la texture du GameObject
        /// </summary>
        /// <param name="texture2D">La variable qui va etre modifier</param>
        public abstract void SetColliderBounds();

        /// <summary>
        /// Permet de detruire proprement le GameObject
        /// </summary>
        public virtual void Destroy()
        {
            // On se desabonne des events du Main
            Main.OnInitialize -= Initialize;
            Main.OnLoadContent -= LoadContent;
            if (_created)
            {
                Main.OnUpdate -= Update;
                Main.OnDraw -= Draw;
            }

            OnDestroyed?.Invoke(this);
        }
    }
}
