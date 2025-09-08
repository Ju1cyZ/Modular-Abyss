using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.GameObject.Objects.Entity
{
    public abstract class Entity : GameObject, ITextureAnimated
    {
        /* Animation */
        protected Dictionary<string, Texture2D> _entityAnimations = new Dictionary<string, Texture2D>();
        protected Texture2D _currentAnim;
        protected int _currentAnimIndex = 0;
        protected float _animationTimeBetweenFrame = 0;

        protected int _textureY = 0;
        protected SpriteEffects _spriteEffect = SpriteEffects.None;

    }
}
