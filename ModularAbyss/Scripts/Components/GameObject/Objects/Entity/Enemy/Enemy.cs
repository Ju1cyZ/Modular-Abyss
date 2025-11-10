using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy
{
    public abstract class Enemy : Entity, ICollider
    {
        protected double attackTime = 0;


        /// <summary>
        /// construire
        /// </summary>
        /// <param name="needsToBeCreated">Permet de determiner si l'objet se creer tout seul ou doit etre creer avec la fonction Create()</param>
        public Enemy(bool needsToBeCreated = false) : base(needsToBeCreated)
        {

        }

        protected override void Update(GameTime gameTime, List<ICollider> colliders)
        {
            base.Update(gameTime, colliders);
            attackTime -= gameTime.ElapsedGameTime.TotalSeconds;

            Position += moveTowardsPlayer() * Stats.getStatValue(STATS_ID.MOVEMENT_SPEED);

            SetColliderBounds();
        }

        public virtual Vector2 moveTowardsPlayer()
        {
            Vector2 playerPos = Main.ActiveScene.Player.Position;
            Vector2 heading = playerPos - Position;
            heading.Normalize();
            return heading;

        }

        public abstract void SetColliderBounds();
        public abstract void Attack(Player p, GameTime gameTime);
    }
}
