using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.HudObjectNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.HudObjectNamespace.Objects
{
    internal class Button : HudObject
    {
        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetTexture2D(), Position, null, Color.White, 0, new Vector2(0, 0), new Vector2(Scale, Scale), SpriteEffects.None, 0f);
        }

        protected override void setTexture2D(ref Texture2D texture2D)
        {
            throw new NotImplementedException();
        }
    }
}
