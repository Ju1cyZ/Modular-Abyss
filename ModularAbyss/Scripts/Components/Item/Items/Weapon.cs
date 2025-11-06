using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons;
using ModularAbyss.Scripts.Components.StatNamespace.StatsNamespace;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Enums;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.ItemNamespace.Items
{
    public abstract class Weapon : Item
    {
        public Rectangle CollisionRectangle = Rectangle.Empty;
        protected bool asAttacked = false;
        protected Entity lastAttackedEntity;

        public Action Attack;

        public int Damage;

        private float _itemOffset = 20;

        private const float MAX_ITEM_OFFSET = 40;
        private const float MIN_ITEM_OFFSET = 20;

        public float itemOffset
        {
            get
            {
                return _itemOffset;
            }
            set
            {
                if (value > MAX_ITEM_OFFSET)
                {
                    _itemOffset = MAX_ITEM_OFFSET;
                }
                else if (value < MIN_ITEM_OFFSET)
                {
                    _itemOffset = MIN_ITEM_OFFSET;
                }
                else
                {
                    _itemOffset = value;
                }
            }
        }

        protected float textureScale = 5;

        public Weapon()
        {
            Attack = _attack;
            Main.getInstance().OnUpdate += Update;
        }

        public Weapon(string id, string name, string description, ItemTier itemTier, string texturePath, Rectangle spriteRectangle) : base(id, name, description, itemTier, texturePath, spriteRectangle)
        {
            Attack = _attack;
        }

        protected virtual void Update(GameTime gameTime, List<Interfaces.ICollider> colliders) { }

        /// <summary>
        /// L'attaque par defaut de l'arme
        /// </summary>
        protected abstract void _attack();

        /// <summary>
        /// Permet d'infliger les degats a l'enemi
        /// </summary>
        /// <param name="entity">L'entiter a attacker</param>
        protected virtual void HurtEntity(Entity entity)
        {
            Attack();
            
            //On empeche d'infiliger plusieur fois les degats
            asAttacked = true;

            //On defini la derniere entiter attaquer
            lastAttackedEntity = entity;

            //Inflige les degats a l'entiter
            entity.HurtWithStats(Damage, Main.getInstance().Player);
        }


        /// <summary>
        /// Permet d'afficher l'item dans le Jeu et definir la position des collisions
        /// </summary>
        /// <param name="player">Le joueur</param>
        /// <param name="playerTextureSize">La taille de la texture du Joueur</param>
        /// <param name="mState">L'etat dans la souris</param>
        /// <param name="spriteBatch">Le spriteBatch</param>
        public virtual void RenderItem(Player player, float playerTextureSize, MouseState mState, ref SpriteBatch spriteBatch)
        {
            Item item = this;

            //La position central du joueur (sprite)
            Vector2 characterPos = new Vector2(player.Position.X + 160 + playerTextureSize / 2, player.Position.Y + 160 + playerTextureSize / 2);
            
            //L'angle de rotation depuis le joueur vers la souris
            double angleRadians = GameManager.AngleBetweenVectors(characterPos, mState.Position.ToVector2());
            
            //La position de l'item selon la rotation
            Vector2 offsetPos = new Vector2((float)Math.Cos(angleRadians) * 50, (float)Math.Sin(angleRadians) * 50);
            
            //La position final de l'item
            Vector2 itemPos = characterPos + offsetPos;

            spriteBatch.Draw(
                item.GetTexture2D(),
                itemPos, item.SpriteRectangle,
                Color.White, (float)angleRadians + (float)Math.PI / 2,
                new Vector2(SpriteRectangle.Width / 2, itemOffset),
                textureScale,
                SpriteEffects.None,
                0f);

            if (typeof(SwingWeapon).IsAssignableFrom(item.GetType()))
            {
                Vector2 colOffsetPos = new Vector2((float)Math.Cos(angleRadians) * 120, (float)Math.Sin(angleRadians) * 120);
                Vector2 colPos = characterPos + colOffsetPos - new Vector2(item.SpriteRectangle.Width * 2, item.SpriteRectangle.Width * 2);

                (item as SwingWeapon).CollisionRectangle = new Rectangle(colPos.ToPoint(), new Point(item.SpriteRectangle.Width * 4, item.SpriteRectangle.Width * 4));
                if (GLOBAL.DEBUGING)
                {
                    Texture2D _texture = new Texture2D(Main.getInstance().GraphicsDevice, 1, 1);
                    _texture.SetData(new Color[] { Color.Blue });
                    spriteBatch.Draw(_texture, colPos, (item as SwingWeapon).CollisionRectangle, Color.White);
                }
            }
        }
        
    }
}
