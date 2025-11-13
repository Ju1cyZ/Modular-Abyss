using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Components.HudObjects;
using ModularAbyss.Scripts.Components.Tilemaps;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;

namespace ModularAbyss.Scripts.Components.Scene.Scenes
{
    public class MainMenuScene : Scene
    {
        protected override void LoadContent()
        {
            // Fond
            tilemap = new Tilemap(TILESET.FLOOR, 10, 5);
            tilemap.Position = new Vector2(160, 145);
            tilemapCollider = new TilemapCollider(TILESET.WALL, 12, 7);
            defineTiles();

            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            // Fond
            tilemap.Draw(gameTime, ref spriteBatch);
            tilemapCollider.Draw(gameTime, ref spriteBatch);
            base.Draw(gameTime, ref spriteBatch);
        }

        protected override void CreateGameObjects()
        {
            GameObjects.Add("Player", new Player());
            Player.Position = new Vector2(1400, 600);
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
            #region Floor
            tilemap.SetTile(0, 3, 0);
            for (int i = 0; i < 5; i++)
            {
                tilemap.SetTile(1, 4 + i, 0);
            }
            tilemap.SetTile(2, 9, 0);

            for (int y = 0; y < 3; y++)
            {
                tilemap.SetTile(4, 3, 1 + y);
                for (int i = 0; i < 5; i++)
                {
                    tilemap.SetTile(5, 4 + i, 1 + y);
                }
                tilemap.SetTile(6, 9, 1 + y);
            }

            tilemap.SetTile(8, 3, 4);
            for (int i = 0; i < 5; i++)
            {
                tilemap.SetTile(9, 4 + i, 4);
            }
            tilemap.SetTile(10, 9, 4);
            #endregion

            #region Wall

            for (int Y = 0; Y < 7; Y++)
            {
                for (int X = 0; X < 3; X++)
                {
                    tilemapCollider.SetTile(5, X, Y);
                }
            }

            tilemapCollider.SetTile(13, 4, 0);

            for (int i = 0; i < 5; i++)
            {
                tilemapCollider.SetTile(14, 5 + i, 0);
            }
            tilemapCollider.SetTile(15, 10, 0);

            for (int y = 0; y < 6; y++)
            {
                tilemapCollider.SetTile(6, 3, 0 + y);
                tilemapCollider.SetTile(10, 11, 0 + y);
            }

            tilemapCollider.SetTile(18, 3, 6);
            for (int i = 0; i < 7; i++)
            {
                tilemapCollider.SetTile(19, 4 + i, 6);
            }
            tilemapCollider.SetTile(22, 11, 6);

            /* Collision */

            tilemapCollider.Colliders.Add(new Rectangle(new Point(160 * 3, 0), new Point(1920, 120)));
            tilemapCollider.Colliders.Add(new Rectangle(new Point(0, 1080 - 120), new Point(1920, 120)));

            tilemapCollider.Colliders.Add(new Rectangle(new Point(160 * 3, 0), new Point(160, 1080)));
            tilemapCollider.Colliders.Add(new Rectangle(new Point(1920 - 160, 0), new Point(160, 1080)));

            #endregion
        }
    }
}
