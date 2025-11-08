using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.HudObjectNamespace
{
    public abstract class HudObject
    {
        protected Main Main;

        private Texture2D texture2D;
        
        public Vector2 Position;
        public Vector2 Size;
        public float Rotation = 0;
        public float Scale = 1;
        public Color BackgroundColor = Color.Transparent;

        protected bool _hidden = false;


        /* Hudobjects Events */
        public delegate void DestroyedHandler(HudObject obj);
        public event DestroyedHandler Destroyed;

        public HudObject(bool hidden = false)
        {
            Main = Main.getInstance();

            Main.OnInitialize += Initialize;
            Main.OnUpdate += Update;
            Main.OnDraw += Draw;

            if(hidden)
                Hide();
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void Update(GameTime gameTime, List<ICollider> colliders)
        {

        }

        protected abstract void Draw(GameTime gameTime, ref SpriteBatch spriteBatch);

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
        /// Cache l'element
        /// </summary>
        public void Hide()
        {
            _hidden = true;
            Main.OnDraw -= Draw;
        }

        /// <summary>
        /// Affiche l'element
        /// </summary>
        public void Show()
        {
            _hidden = false;
            Main.OnDraw += Draw;
        }

        /// <summary>
        /// Permet d'alterner entre Hide() et Show()
        /// </summary>
        public void Toggle()
        {
            if (_hidden)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        /// <summary>
        /// Permet de detruit le hudobject
        /// </summary>
        public virtual void Destroy()
        {
            //On se desabonne aux events
            Main.OnInitialize -= Initialize;
            Main.OnUpdate -= Update;
            Main.OnDraw -= Draw;

            Destroyed?.Invoke(this);
        }
    }
}
