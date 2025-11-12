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
    internal abstract class Enemy : Entity, ILifeBar, IAi
    {
        protected double attackTime = 0;

        public ProgressBar HealthBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        protected override void Update(GameTime gameTime, List<ICollider> colliders)
        {
            base.Update(gameTime, colliders);
            (this as ILifeBar).Update(Health);
            attackTime -= gameTime.ElapsedGameTime.TotalSeconds;
            Position += IAi.moveTowardsPlayer(Position) * Stats.getStatValue(STATS_ID.MOVEMENT_SPEED);

            SetColliderBounds();
        }

        public abstract void Attack(Player p, GameTime gameTime);

        /// <summary>
        /// Defini la position de la bar de vie
        /// </summary>
        /// <returns>Sa position</returns>
        public abstract Vector2 GetHealthBarPos();
    }
}
