using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObject.Objects.Entity.Enemy;
using ModularAbyss.Scripts.Components.ItemNamespace;
using ModularAbyss.Scripts.Components.ItemNamespace.Items;
using ModularAbyss.Scripts.Components.StatNamespace;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Enums;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.GameObject.Objects
{
    internal class Player : GameObject, ITextureAnimated, ICollider
    {
        /* Animation */
        private Dictionary<string, Texture2D> _playerAnimations = new Dictionary<string, Texture2D>();
        private Texture2D _currentAnim;
        private int _currentAnimIndex = 0;
        private float _animationTimeBetweenFrame = 0;
        private int _animationFrameCount = 8;

        private int _textureY = 0;
        private SpriteEffects _spriteEffect = SpriteEffects.None;

        /* Texture*/
        private const int TEXTURE_SIZE = 80;

        /* Movement */
        public const int WALK_SPEED = 6;
        public const int RUN_SPEED = 9;
        public int currentSpeed = 0;
        public PlayerMovementStates PlayerMovementStates;

        /* Attack */
        private bool asAttacked = false;

        /* Player Properties */
        public Stats Stats = new Stats();
        public List<Item> inventory = new List<Item>();

        protected override void Initialize()
        {
            base.Initialize();
            /* Animation */
            _playerAnimations = new Dictionary<string, Texture2D>()
            {
                {"ANIM_IDLE", game.Content.Load<Texture2D>(SPRITES.PLAYER_IDLE)},
                {"ANIM_WALK", game.Content.Load<Texture2D>(SPRITES.PLAYER_WALK)},
                {"ANIM_RUN", game.Content.Load<Texture2D>(SPRITES.PLAYER_RUN)},
            };
            _currentAnim = _playerAnimations.GetValueOrDefault("ANIM_RUN");

            DefineStats();

            inventory.Add(ITEMS.STEEL_SWORD);
        }

        protected override void Update(GameTime gameTime, List<ICollider> colliders)
        {
            /* INPUT */
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            #region Player Movement/Animation
            /* Player Movement */
            PlayerMoveDirections playerMoveDirection = ManageMovementInputs(kState);
            PlayerMovementStates playerMovementState = ManageMovementStatesInputs(kState, playerMoveDirection);

            ManageMovement(playerMoveDirection, playerMovementState, colliders);

            /* Player Animation */
            ManagePlayerAnimation(playerMoveDirection, playerMovementState);
            #endregion

            //Attaquer
            if (mState.LeftButton == ButtonState.Pressed)
            {
                asAttacked = true;
                if (typeof(Weapon).IsAssignableFrom(inventory[0].GetType()))
                {
                    (inventory[0] as Weapon).Attack();
                }
            }
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            Texture2D _texture = new Texture2D(game.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.Red });
            if (GLOBAL.DEBUGING)
            {
                spriteBatch.Draw(_texture, new Vector2(Position.X + 160, Position.Y + 160), CollisionRect, Color.White);
            }

            Rectangle rect = ((ITextureAnimated)this).GenerateTextureRect(TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE * _textureY, _animationFrameCount, ref _currentAnimIndex, (float)gameTime.ElapsedGameTime.TotalSeconds, ref _animationTimeBetweenFrame);
            spriteBatch.Draw(GetTexture2D(), Position + GameManager.CameraPosition, rect, Color.White, 0, new Vector2(0, 0), new Vector2(5, 5), _spriteEffect, 0f);


            DrawItems(inventory, mState, spriteBatch);
        }

        public void DefineStats()
        {
            Stats.setStatValue(STATS_ID.SPEED, 1);
        }

        /// <summary>
        /// Permet de deplacer le joueur
        /// </summary>
        /// <param name="direction">La direction de deplacement</param>
        public void ManageMovement(PlayerMoveDirections direction, PlayerMovementStates states, List<ICollider> colliders)
        {
            switch (states)
            {
                case PlayerMovementStates.idle:
                    currentSpeed = 0;
                    break;
                case PlayerMovementStates.walk:
                    currentSpeed = WALK_SPEED + Stats.getStatValue(STATS_ID.SPEED);
                    break;
                case PlayerMovementStates.run:
                    currentSpeed = RUN_SPEED + Stats.getStatValue(STATS_ID.SPEED);
                    break;
            }

            Vector2 movement = new Vector2(0, 0);

            switch (direction)
            {
                case PlayerMoveDirections.None:
                    break;
                case PlayerMoveDirections.Left:
                    movement = new Vector2(-currentSpeed, 0);
                    break;
                case PlayerMoveDirections.Right:
                    movement = new Vector2(currentSpeed, 0);
                    break;
                case PlayerMoveDirections.Down:
                    movement = new Vector2(0, currentSpeed);
                    break;
                case PlayerMoveDirections.Up:
                    movement = new Vector2(0, -currentSpeed);
                    break;
                case PlayerMoveDirections.UpRight:
                    movement = new Vector2(currentSpeed - currentSpeed * 0.3f, -(currentSpeed - currentSpeed * 0.3f));
                    break;
                case PlayerMoveDirections.UpLeft:
                    movement = new Vector2(-(currentSpeed - currentSpeed * 0.3f), -(currentSpeed - currentSpeed * 0.3f));
                    break;
                case PlayerMoveDirections.DownRight:
                    movement = new Vector2(currentSpeed - currentSpeed * 0.3f, currentSpeed - currentSpeed * 0.3f);
                    break;
                case PlayerMoveDirections.DownLeft:
                    movement = new Vector2(-(currentSpeed - currentSpeed * 0.3f), currentSpeed - currentSpeed * 0.3f);
                    break;
            }

            Position += movement;

            SetColliderBounds();

            //Collision detection
            manageCollision(movement, colliders);

            

        }

        /// <summary>
        /// Permet de determiner la direction du mouvement a partir des "inputs"
        /// </summary>
        /// <param name="kState">Le KeyboardState</param>
        /// <returns>La direction trouver</returns>
        public PlayerMoveDirections ManageMovementInputs(KeyboardState kState)
        {
            //La direction par defaut
            PlayerMoveDirections direction = PlayerMoveDirections.None;

            //Defini la direction dans l'axe X
            if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_RIGHT))
            {
                direction = PlayerMoveDirections.Right;
            }
            else if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_LEFT))
            {
                direction = PlayerMoveDirections.Left;
            }

            //Defini la direction dans l'axe Y
            if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_UP))
            {
                direction = PlayerMoveDirections.Up;
                //Defini la direction dans l'axe X
                if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_RIGHT))
                {
                    direction = PlayerMoveDirections.UpRight;
                }
                else if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_LEFT))
                {
                    direction = PlayerMoveDirections.UpLeft;
                }
            }
            else if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_DOWN))
            {
                direction = PlayerMoveDirections.Down;
                //Defini la direction dans l'axe X
                if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_RIGHT))
                {
                    direction = PlayerMoveDirections.DownRight;
                }
                else if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_LEFT))
                {
                    direction = PlayerMoveDirections.DownLeft;
                }
            }

            return direction;
        }

        /// <summary>
        /// Permet de determiner l'etat de mouvement a partir des "inputs"
        /// </summary>
        /// <param name="kState">Le KeyboardState</param>
        /// <param name="direction">La direction du mouvement</param>
        /// <returns></returns>
        public PlayerMovementStates ManageMovementStatesInputs(KeyboardState kState, PlayerMoveDirections direction)
        {
            //L'etat par defaut
            PlayerMovementStates state = PlayerMovementStates.idle;

            if (kState.GetPressedKeys().Contains(KEYBINDS.MOVE_SPRINT) && direction != PlayerMoveDirections.None)
            {
                state = PlayerMovementStates.run;
            }
            //Verifie si le joueur bouge ou pas
            else if (direction != PlayerMoveDirections.None)
            {
                state = PlayerMovementStates.walk;
            }

            return state;
        }

        /// <summary>
        /// Permet de gerer les animations du joueurs
        /// </summary>
        /// <param name="playerMoveDirection">La direction du mouvement</param>
        /// <param name="playerMovementState">L'etat du mouvement</param>
        public void ManagePlayerAnimation(PlayerMoveDirections playerMoveDirection, PlayerMovementStates playerMovementState)
        {
            switch (playerMovementState)
            {
                case PlayerMovementStates.idle:
                    _currentAnim = _playerAnimations.GetValueOrDefault("ANIM_IDLE");
                    _animationFrameCount = 4;
                    return;
                case PlayerMovementStates.walk:
                    _currentAnim = _playerAnimations.GetValueOrDefault("ANIM_WALK");
                    _animationFrameCount = 8;
                    break;
                case PlayerMovementStates.run:
                    _currentAnim = _playerAnimations.GetValueOrDefault("ANIM_RUN");
                    _animationFrameCount = 8;
                    break;
            }

            switch (playerMoveDirection)
            {
                case PlayerMoveDirections.Left:
                    _textureY = 0;
                    _spriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                case PlayerMoveDirections.Right:
                    _spriteEffect = SpriteEffects.None;
                    _textureY = 0;
                    break;
                case PlayerMoveDirections.Down:
                case PlayerMoveDirections.DownRight:
                case PlayerMoveDirections.DownLeft:
                    _textureY = 1;
                    break;
                case PlayerMoveDirections.Up:
                case PlayerMoveDirections.UpRight:
                case PlayerMoveDirections.UpLeft:
                    _textureY = 2;
                    break;
            }
        }

        /// <summary>
        /// Defini la texture 2D du joueur
        /// </summary>
        /// <param name="texture2D">La variable dans la Classe GameObject</param>
        protected override void setTexture2D(ref Texture2D texture2D)
        {
            texture2D = _currentAnim;
        }

        /// <summary>
        /// Permet de recuperer la texture du GameObject
        /// </summary>
        /// <returns>La texture</returns>
        public override Texture2D GetTexture2D()
        {
            return _currentAnim;
        }

        /// <summary>
        /// Defini le box collider
        /// </summary>
        public void SetColliderBounds()
        {
            CollisionRect = new Rectangle(new Point((int)Position.X + 160, (int)Position.Y + 160), new Point(TEXTURE_SIZE, TEXTURE_SIZE));
        }

        /// <summary>
        /// Dessine les items des slots principaux (1-5)
        /// </summary>
        /// <param name="inventory">L'inventaire qui contient les items</param>
        public void DrawItems(List<Item> inventory, MouseState mState, SpriteBatch spriteBatch)
        {
            if (inventory[0] != null)
            {
                Item item = inventory[0];

                //La position central du joueur (sprite)
                Vector2 characterPos = new Vector2(Position.X + 160 + TEXTURE_SIZE / 2, Position.Y + 160 + TEXTURE_SIZE / 2);
                //L'angle de rotation depuis le joueur vers la souris
                double angleRadians = AngleBetweenVectors(characterPos, mState.Position.ToVector2());
                //La position de l'item selon la rotation
                Vector2 offsetPos = new Vector2((float)Math.Cos(angleRadians) * 50, (float)Math.Sin(angleRadians) * 50);
                //La position final de l'item
                Vector2 itemPos = characterPos + offsetPos;

                spriteBatch.Draw(item.GetTexture2D(), itemPos, item.SpriteRectangle, Color.White, (float)angleRadians + (float)Math.PI / 2, new Vector2(7.5f, 20), 5, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Permet de recuperer l'angle entre deux positions 
        /// </summary>
        /// <param name="vector1">La p1</param>
        /// <param name="vector2">La p2</param>
        /// <returns>L'angle en Radian</returns>
        private double AngleBetweenVectors(Vector2 vector1, Vector2 vector2)
        {
            return Math.Atan2(vector2.Y - vector1.Y, vector2.X - vector1.X);
        }

        /// <summary>
        /// Permet de gerer les collisions avec le joueur et l'environement
        /// </summary>
        /// <param name="movement"></param>
        /// <param name="colliders"></param>
        private void manageCollision(Vector2 movement, List<ICollider> colliders)
        {
            foreach (GameObject gameObject in colliders)
            {
                if (gameObject.GetType() != typeof(Player) && CollisionRect.Intersects(gameObject.CollisionRect))
                {
                    // Enemy
                    if (gameObject.GetType() == typeof(Enemy))
                    {

                    }
                    //Autre
                    else
                    {
                        Position -= movement;
                    }
                }
            }
        }
    }
}
