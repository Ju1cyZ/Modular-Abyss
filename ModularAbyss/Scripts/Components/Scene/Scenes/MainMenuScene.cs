using ModularAbyss.Scripts.Components.HudObjectNamespace.Objects;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ModularAbyss.Scripts.Components.Scene.Scenes
{
    internal class MainMenuScene : Scene
    {
        protected override void CreateGameObjects()
        {

        }

        protected override void CreateHudObjects()
        {
            HudObjects.Add("TITLE", new Text("Modular Abyss"));
            (HudObjects.GetValueOrDefault("TITLE") as Text).Scale = 5;
            (HudObjects.GetValueOrDefault("TITLE") as Text).Position = new Vector2(50, 100);
        }

        protected override void UpdateHudObjects()
        {

        }
    }
}
