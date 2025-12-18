using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Gameobjects
{
    public abstract class Projectile : GameObject
    {
        // La direction du projectile
        private Vector2 _direction;
        // La cible
        private Vector2 _target;
        // La position de depart
        private Vector2 _startPos;

        // utiliser comme identifiant dans le liste de gameobjects
        private string projectileId;

        public int Speed = 2000;

        public delegate void OnTouchHandler(Projectile projectile, ICollider target);
        public event OnTouchHandler OnTouch;

        public Vector2 Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }

        private Rectangle _rect;


        public Projectile(Vector2 target, Vector2 startPos) : base()
        {
            _target = target;
            _startPos = startPos;
            Position = _startPos;
            setTextureRectangle(ref _rect);
            setDirectionFromVector(_target);

            projectileId = Guid.NewGuid().ToString();

            Main.ActiveScene.GameObjects.Add(projectileId, this);
        }

        protected override void Update(GameTime gameTime)
        {
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            SetColliderBounds();

            // Detruit le projectile si est trop loin du joueur
            if (Vector2.Distance(Main.ActiveScene.Player.Position, Position) >= 5000)
            {
                Destroy();
            }
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            //base.Draw(gameTime, ref spriteBatch);

            double angleRadians = GameManager.AngleBetweenVectors(_startPos, _target);

            spriteBatch.Draw(GetTexture2D(), Position + GameManager.CameraPosition, _rect, Color.White, (float)angleRadians, new Vector2(0, 0), new Vector2(5, 5), SpriteEffects.None, 0f);
        }


        protected void Touch(Projectile projectile, ICollider object2) 
        {
            OnTouch?.Invoke(projectile, object2);
            Destroy();
        }

        /// <summary>
        /// Permet de definir la direction avec la position de la cible
        /// </summary>
        /// <param name="target">La position de la cible</param>
        public void setDirectionFromVector(Vector2 target)
        {
            Direction = Vector2.Normalize(target - Position);
        }

        public abstract void setTextureRectangle(ref Rectangle rect);

        public override void Destroy()
        {
            base.Destroy();
            Main.ActiveScene.GameObjects.Remove(projectileId);
            OnTouch = null; // desabonne TOUT le monde
        }
    }
}
