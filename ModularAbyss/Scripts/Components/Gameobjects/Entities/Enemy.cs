using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.HudObjects;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Gameobjects.Entities
{
    public abstract class Enemy : Entity, IAi
    {
        protected double attackTime = 0;

        /// <summary>
        /// construire
        /// </summary>
        /// <param name="needsToBeCreated">Permet de determiner si l'objet se creer tout seul ou doit etre creer avec la fonction Create()</param>
        public Enemy(bool needsToBeCreated = false) : base(needsToBeCreated) { }

        public override void Initialize()
        {
            base.Initialize();
            (this as ILifeBar).Initialize(MAX_HEALTH);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            (this as ILifeBar).Update(Health);
            attackTime -= gameTime.ElapsedGameTime.TotalSeconds;
            Position += IAi.moveTowardsPlayer(Position) * Stats.getStatValue(STATS_ID.MOVEMENT_SPEED) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            SetColliderBounds();
        }

        public abstract void Attack(Player p, GameTime gameTime);

    }
}
