using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Enums;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;
using System;

namespace ModularAbyss.Scripts.Components.Items
{
    public abstract class Weapon : Item, IStat
    {
        public Stats Stats { get; set; } = new Stats();

        public Rectangle CollisionRectangle = Rectangle.Empty;
        protected bool asAttacked = false;
        protected Entity lastAttackedEntity;

        public Action Attack;

        public int Damage
        {
            get
            {
                return Stats.getStatValue(STATS_ID.DAMAGE);
            }

            set
            {
                Stats.setStatValue(STATS_ID.DAMAGE, value);
            }
        }

        private float _itemOffset = 20;

        protected float MAX_ITEM_OFFSET = 120;
        protected float MIN_ITEM_OFFSET = 50;

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

        protected const float TEXTURE_SCALE = 5;

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
            //On defini la derniere entiter attaquer
            lastAttackedEntity = entity;
            //Inflige les degats a l'entiter
            entity.HurtWithStats(Damage, Main.ActiveScene.Player);
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
            //La position central du joueur (sprite)
            Vector2 characterPos = new Vector2(player.Position.X + 160 + playerTextureSize / 2, player.Position.Y + 160 + playerTextureSize / 2);

            //L'angle de rotation depuis le joueur vers la souris
            double angleRadians = GameManager.AngleBetweenVectors(characterPos, mState.Position.ToVector2());

            //La position de l'item selon la rotation
            Vector2 offsetPos = new Vector2((float)Math.Cos(angleRadians) * itemOffset, (float)Math.Sin(angleRadians) * itemOffset);

            //La position final de l'item
            Vector2 itemPos = characterPos + offsetPos;

            spriteBatch.Draw(
                GetTexture2D(),
                itemPos,
                SpriteRectangle,
                Color.White,
                (float)angleRadians + (float)Math.PI / 2,
                new Vector2(SpriteRectangle.Width / 2, SpriteRectangle.Height),
                TEXTURE_SCALE,
                SpriteEffects.None,
                0f
            );

            Vector2 colOffsetPos = new Vector2((float)Math.Cos(angleRadians) * (itemOffset + 80), (float)Math.Sin(angleRadians) * (itemOffset + 80));
            Vector2 colPos = characterPos + colOffsetPos - new Vector2(SpriteRectangle.Width * 2, SpriteRectangle.Width * 2);

            CollisionRectangle = new Rectangle(colPos.ToPoint(), new Point(SpriteRectangle.Width * 4, SpriteRectangle.Width * 4));

            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(Main.getInstance().GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Blue });
                spriteBatch.Draw(_texture, colPos, CollisionRectangle, Color.White);
            }
        }
    }
}
