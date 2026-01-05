using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Components.Gameobjects.Projectiles;
using ModularAbyss.Scripts.Components.HudObjects;
using ModularAbyss.Scripts.Components.Tilemaps;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;

namespace ModularAbyss.Scripts.Components.Scene.Scenes
{
    public class MainMenuScene : Scene
    {
        Tilemap props;

        static Vector2 _playableMapSize = new Vector2(30, 20);
        static Vector2 _playableMapPosition = new Vector2(0, 0);


        protected override void LoadContent()
        {
            // Fond
            tilemap = new Tilemap(TILESET.STONE_GROUND, 24, 14); // Floor
            tilemapCollider = new TilemapCollider(TILESET.WALL, 24, 14);
            props = new Tilemap(TILESET.GRASS, 24, 14); // Props
            defineTiles();

            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            GameManager.Camera.isSticky = true;
            CameraObject.Position = new Vector2(0, 0);
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            // Fond
            props.Draw(gameTime, ref spriteBatch);
            tilemapCollider.Draw(gameTime, ref spriteBatch);
            tilemap.Draw(gameTime, ref spriteBatch);
            base.Draw(gameTime, ref spriteBatch);
        }

        protected override void CreateGameObjects()
        {
            GameObjects.Add("Player", new Player());
            Player.Position = new Vector2(1300, 400);
            Player.OnDeath += (player) =>
            {
                // PLAYER DED :(
            };
        }

        protected override void CreateHudObjects()
        {
            HudObjects.Add("TITLE", new Text("Modular Abyss", new Vector2(30, 70), 5));

            HudObjects.Add("BOOK", new Panel(UI_COMPONENTS.BOOK_PANEL, new Vector2(-306, 192), size: new Vector2(5, 4), scale: 7));
            HudObjects.Add("PAPER", new Panel(UI_COMPONENTS.BOOK_PAPER_RIGHT_PANEL, new Vector2(-306, 192), size: new Vector2(5, 4), scale: 7));

            /* BUTTONS */

            HudObjects.Add("PLAY", new Button(new Vector2(50, 320), 7));
            (HudObjects.GetValueOrDefault("PLAY") as Button).Text = "JOUER";
            (HudObjects.GetValueOrDefault("PLAY") as Button).OnClick += (btn) =>
            {
                Main.LoadScene<GameScene>();
            };

            HudObjects.Add("SETTINGS", new Button(new Vector2(50, 470), 7));
            (HudObjects.GetValueOrDefault("SETTINGS") as Button).Text = "PARAMETRE";
            (HudObjects.GetValueOrDefault("SETTINGS") as Button).OnClick += (btn) =>
            {
                // Open Settings
            };

            HudObjects.Add("QUIT", new Button(new Vector2(50, 620), 7));
            (HudObjects.GetValueOrDefault("QUIT") as Button).Text = "QUITTER";
            (HudObjects.GetValueOrDefault("QUIT") as Button).OnClick += (btn) =>
            {
                Main.getInstance().Exit();
            };
        }
        
        /// <summary>
        /// Defini les tiles id dans les tilemaps
        /// </summary>
        private void defineTiles()
        {
            tilemap.LoadFromCSV(TILEMAPS.MAIN_MENU_FLOOR);
            tilemapCollider.LoadFromCSV(TILEMAPS.MAIN_MENU_WALL);
            props.LoadFromCSV(TILEMAPS.MAIN_MENU_PROPS);
        }

        public override Vector2 GetPlayableMapSize()
        {
            return _playableMapSize;
        }

        public override Vector2 GetPlayableMapPosition()
        {
            return _playableMapPosition;
        }
    }
}
