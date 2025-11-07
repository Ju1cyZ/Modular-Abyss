using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy;
using ModularAbyss.Scripts.Components.ItemNamespace;
using ModularAbyss.Scripts.Components.ItemNamespace.Items;
using ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons;
using ModularAbyss.Scripts.Components.StatNamespace;
using ModularAbyss.Scripts.Components.StatNamespace.StatsNamespace;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Enums;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.GameObjectNamespace.Objects
{
    public class Player : GameObject, ITextureAnimated, ICollider
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

        /* Player Attributes */
        public Stats Stats = new Stats();

        public List<Item> inventory = new List<Item>();

        SpriteFont baseFont;

        public int Health
        {
            get
            {
                return Stats.getStatValue(STATS_ID.HEALTH);
            }
            set
            {
                if (Stats.getStatValue(STATS_ID.HEALTH) + value >= Stats.getStat(STATS_ID.HEALTH).MAX_VALUE)
                {
                    Stats.setStatValue(STATS_ID.HEALTH, Stats.getStat(STATS_ID.HEALTH).MAX_VALUE);
                }
                else
                {
                    Stats.setStatValue(STATS_ID.HEALTH, value);
                }
            }
        }

        public int MAX_HEALTH
        {
            get
            {
                return Stats.getStat(STATS_ID.HEALTH).MAX_VALUE;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Define max Health
            Stats.setStatMaxValue(STATS_ID.HEALTH, 25, true);

            /* Animation */
            _playerAnimations = new Dictionary<string, Texture2D>()
            {
                {"ANIM_IDLE", game.Content.Load<Texture2D>(SPRITES.PLAYER_IDLE)},
                {"ANIM_WALK", game.Content.Load<Texture2D>(SPRITES.PLAYER_WALK)},
                {"ANIM_RUN", game.Content.Load<Texture2D>(SPRITES.PLAYER_RUN)},
            };
            _currentAnim = _playerAnimations.GetValueOrDefault("ANIM_RUN");

            DefineStats();

            inventory.Add(ITEMS.STEEL_KNIFE);
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

            ManageMovement(playerMoveDirection, playerMovementState, colliders, gameTime);

            /* Player Animation */
            ManagePlayerAnimation(playerMoveDirection, playerMovementState);
            #endregion

            ManageInputs(kState, mState);
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(game.GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Red });
                spriteBatch.Draw(_texture, new Vector2(Position.X + 160, Position.Y + 160), CollisionRect, Color.White);
            }

            Rectangle rect = ((ITextureAnimated)this).GenerateTextureRect(TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE * _textureY, _animationFrameCount, ref _currentAnimIndex, (float)gameTime.ElapsedGameTime.TotalSeconds, ref _animationTimeBetweenFrame);
            spriteBatch.Draw(GetTexture2D(), Position + GameManager.CameraPosition, rect, Color.White, 0, new Vector2(0, 0), new Vector2(5, 5), _spriteEffect, 0f);


            DrawItems(inventory, mState, spriteBatch);
        }

        public void DefineStats()
        {
            Stats.setStatValue(STATS_ID.MOVEMENT_SPEED, 1);
        }

        /// <summary>
        /// Permet de deplacer le joueur
        /// </summary>
        /// <param name="direction">La direction de deplacement</param>
        public void ManageMovement(PlayerMoveDirections direction, PlayerMovementStates states, List<ICollider> colliders, GameTime gameTime)
        {
            switch (states)
            {
                case PlayerMovementStates.idle:
                    currentSpeed = 0;
                    break;
                case PlayerMovementStates.walk:
                    currentSpeed = WALK_SPEED + Stats.getStatValue(STATS_ID.MOVEMENT_SPEED);
                    break;
                case PlayerMovementStates.run:
                    currentSpeed = RUN_SPEED + Stats.getStatValue(STATS_ID.MOVEMENT_SPEED);
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
            manageCollision(movement, colliders, gameTime);

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
        /// Permet de gerer les entrer clavier, souris pour le souris
        /// </summary>
        /// <param name="kState">Le KeyboardState</param>
        /// <param name="mState">Le MouseState</param>
        public void ManageInputs(KeyboardState kState, MouseState mState)
        {
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
            if (typeof(Weapon).IsAssignableFrom(inventory[0].GetType()))
            {
                (inventory[0] as Weapon).RenderItem(this, TEXTURE_SIZE, mState, ref spriteBatch);
            }
        }

        /// <summary>
        /// Permet de gerer les collisions avec le joueur et l'environement
        /// </summary>
        /// <param name="movement"></param>
        /// <param name="colliders"></param>
        private void manageCollision(Vector2 movement, List<ICollider> colliders, GameTime gameTime)
        {
            foreach (GameObject gameObject in colliders)
            {
                if (gameObject.GetType() != typeof(Player) && CollisionRect.Intersects(gameObject.CollisionRect))
                {
                    object test = gameObject.GetType();
                    // Enemy
                    if (typeof(Enemy).IsAssignableFrom(gameObject.GetType()))
                    {
                        //Enemy collision logic
                        (gameObject as Enemy).Attack(this, gameTime);
                    }
                    //Autre
                    else
                    {
                        Position -= movement;
                    }
                }
            }

            //Les murs
            foreach (Rectangle item in Main.wallTilemap.Colliders)
            {
                if (CollisionRect.Intersects(item))
                {
                    Position -= movement;
                }
            }
        }

        /// <summary>
        /// Permet d'infliger de degats au Joueur
        /// </summary>
        /// <param name="damage">Le nombre de degats</param>
        /// <param name="enemi">L'enemi qui inflige des degats</param>
        /// <param name="damageType">Le type de degats (STATS_ID.DAMAGE / STATS_ID.RANGED_DAMAGE etc...)</param>
        /// <returns>La vie apres avoir infliger les degats</returns>
        public int HurtWithStats(int damage, Enemy enemi, string damageType = STATS_ID.MELE_DAMAGE)
        {
            damage += enemi.Stats.getStatValue(damageType) - 1;

            int damageDealt = 0;

            if (Health - damage > 0)
            {
                Health -= damage;
                damageDealt = damage;
            }
            else if (Health - damage <= 0)
            {
                //Le degat reellement infliger
                damageDealt = Math.Abs(0 - Health);
                Health = 0;
                Die();
            }

            if (enemi.Stats.getStatValue(STATS_ID.LIFE_STEAL) > 0)
            {
                //Chance aleatoire de soigner le joueur
                int rnd = GameManager.Random.Next(0, 100);
                if (rnd <= 2 * enemi.Stats.getStatValue(STATS_ID.LIFE_STEAL))
                {
                    enemi.Heal(damage * rnd / 100);
                }
            }

            return Health;
        }

        public void Die()
        {

        }

        /// <summary>
        /// Permet de soigner le joueur
        /// </summary>
        /// <param name="amount">La quantiter de point de vie a donner</param>
        /// <returns>Les points de vie apres avoir soigner le joueur</returns>
        public int Heal(int amount)
        {
            return Health += amount;
        }
    }
}
