using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.GameObjectNamespace
{
    public abstract class GameObject
    {
        private Texture2D texture2D;
        public Rectangle CollisionRect;
        protected Main game;

        private float posX = 0;
        private float posY = 0;

        /* Gameobject Events */
        public delegate void DestroyedHandler(GameObject obj);
        public event DestroyedHandler Destroyed;

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

        public GameObject()
        {
            //Recupere l'instance de Game1 
            game = Main.getInstance();

            //On s'abonne aux events
            game.OnInitialize += Initialize;
            game.OnLoadContent += LoadContent;
            game.OnUpdate += Update;
            game.OnDraw += Draw;
        }

        public virtual void Initialize() { }

        protected virtual void LoadContent() { }

        protected virtual void Update(GameTime gameTime, List<ICollider> colliders)
        {

        }

        protected virtual void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetTexture2D(), Position + GameManager.CameraPosition, Color.White);
        }

        /// <summary>
        /// Defini la texture du GameObject
        /// </summary>
        /// <param name="texture2D">La variable qui va etre modifier</param>
        protected abstract void setTexture2D(ref Texture2D texture2D);

        /// <summary>
        /// Fait un movement lineaire vers une position/destination
        /// </summary>
        /// <param name="Destination">La destination du mouvement</param>
        /// <returns>True si le gameObject est arrive a destination</returns>
        public bool LinearMovement(Vector2 Destination, GameTime gameTime, float speed = 100)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float distance = Vector2.Distance(Position, Destination);

            //Le gameObject est arriver a destination
            if (distance <= 10)
            {
                return true;
            }

            //Defini si il doit reculer ou pas 
            float movementX = Position.X > Destination.X ? -speed : speed;
            //Defini si il doit monter ou pas
            float movementY = Position.Y > Destination.Y ? -speed : speed;

            //Cas ou la position N est egale a la destination N donc on bouge pas dans l'axe N
            movementX = Position.X == Destination.X ? 0 : movementX;
            movementY = Position.Y == Destination.Y ? 0 : movementY;

            //Effectue le mouvement
            posX += movementX * deltaTime;
            posY += movementY * deltaTime;

            return false;
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
        /// Permet de definir la position a partir d'une fonction
        /// </summary>
        /// <param name="managePos">La fonction qui retourne un Vector2</param>
        /// <returns>L'objet lui meme</returns>
        public GameObject SetPos(Func<GameObject, Vector2> managePos)
        {
            Position = managePos(this);
            return this;
        }

        /// <summary>
        /// Permet de definir la position a partir d'une fonction
        /// </summary>
        /// <param name="managePos">La fonction qui retourne un Vector2</param>
        /// <param name="offsetX">Un decalage sur l'axe X</param>
        /// <param name="offsetY">Un decalage sur l'axe Y</param>
        /// <returns>L'objet lui meme</returns>
        public GameObject SetPos(Func<GameObject, Vector2> managePos, int offsetX = 0, int offsetY = 0)
        {
            Vector2 tempPos = managePos(this);
            Position = new Vector2(tempPos.X + offsetX, tempPos.Y + offsetY);
            return this;
        }
    
        /// <summary>
        /// Permet de detruit le gameobject
        /// </summary>
        public virtual void Destroy()
        {
            //On se desabonne aux events
            game.OnInitialize -= Initialize;
            game.OnLoadContent -= LoadContent;
            game.OnUpdate -= Update;
            game.OnDraw -= Draw;

            Destroyed?.Invoke(this);
        }
    }
}
