using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Items;
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

namespace ModularAbyss.Scripts.Components.Gameobjects
{
    public class Player : GameObject, ITextureAnimated, ICollider, IStat
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

        public static int TextureSize
        {
            get { return TEXTURE_SIZE; }
        }

        /* Movement */
        public const int WALK_SPEED = 300;
        public const int RUN_SPEED = 450;
        public int currentSpeed = 0;
        public PlayerMovementStates PlayerMovementStates;

        /* Player Attributes */
        public bool isInvicible = false;

        public Stats Stats { get; set; } = new Stats();

        private bool _isDead = false;

        public bool IsDead
        {
            get
            {
                return _isDead;
            }
        }

        public List<Item> inventory = new List<Item>();

        /* Gameobject Events */
        public delegate void OnDeathHandler(Player obj);
        public event OnDeathHandler OnDeath;

        public int Health
        {
            get
            {
                return Stats.getStatValue(STATS_ID.HEALTH);
            }
            set
            {
                if (!isInvicible)
                {
                    if (value >= Stats.getStat(STATS_ID.HEALTH).MAX_VALUE)
                    {
                        Stats.setStatValue(STATS_ID.HEALTH, Stats.getStat(STATS_ID.HEALTH).MAX_VALUE);
                    }
                    else
                    {
                        Stats.setStatValue(STATS_ID.HEALTH, value);
                    }
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

        public Player() : base(false) { }

        public override void Initialize()
        {
            base.Initialize();

            // Define max Health
            Stats.setStatMaxValue(STATS_ID.HEALTH, 25, true);

            /* Animation */
            _playerAnimations = new Dictionary<string, Texture2D>()
            {
                {"ANIM_IDLE", Main.Content.Load<Texture2D>(SPRITES.PLAYER_IDLE)},
                {"ANIM_WALK", Main.Content.Load<Texture2D>(SPRITES.PLAYER_WALK)},
                {"ANIM_RUN", Main.Content.Load<Texture2D>(SPRITES.PLAYER_RUN)},
                {"ANIM_DEATH", Main.Content.Load<Texture2D>(SPRITES.PLAYER_DEATH)},
            };
            _currentAnim = _playerAnimations.GetValueOrDefault("ANIM_RUN");

            DefineStats();
        }

        protected override void Update(GameTime gameTime)
        {
            /* INPUT */

            #region Player Movement/Animation
            /* Player Movement */
            PlayerMoveDirections playerMoveDirection = ManageMovementInputs();
            PlayerMovementStates playerMovementState = ManageMovementStatesInputs(playerMoveDirection);

            ManageMovement(playerMoveDirection, playerMovementState, gameTime);

            /* Player Animation */
            ManagePlayerAnimation(playerMoveDirection, playerMovementState);

            #endregion

            ManageInputs();
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(Main.GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Red });
                spriteBatch.Draw(_texture, new Vector2(Position.X + 160, Position.Y + 160), Bounds, Color.White);
            }

            Rectangle rect = ((ITextureAnimated)this).GenerateTextureRect(TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE * _textureY, _animationFrameCount, ref _currentAnimIndex, (float)gameTime.ElapsedGameTime.TotalSeconds, ref _animationTimeBetweenFrame, IsDead);
            spriteBatch.Draw(GetTexture2D(), Position + GameManager.CameraPosition, rect, Color.White, 0, new Vector2(0, 0), new Vector2(5, 5), _spriteEffect, 0f);

            if (!IsDead)
            {
                DrawItems(inventory, spriteBatch);
            }
        }

        public void DefineStats()
        {
            Stats.setStatValue(STATS_ID.MOVEMENT_SPEED, 1);
        }

        /// <summary>
        /// Permet de deplacer le joueur
        /// </summary>
        /// <param name="direction">La direction de deplacement</param>
        public void ManageMovement(PlayerMoveDirections direction, PlayerMovementStates states, GameTime gameTime)
        {
            switch (states)
            {
                case PlayerMovementStates.idle:
                    currentSpeed = 0;
                    break;
                case PlayerMovementStates.walk:
                    currentSpeed = 1;
                    break;
                case PlayerMovementStates.run:
                    currentSpeed = 2;
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

            Vector2 movementWithDeltatime = movement * Stats.getStatValue(STATS_ID.MOVEMENT_SPEED) * (float)gameTime.ElapsedGameTime.TotalSeconds * 200;

            Position += movementWithDeltatime;

            SetColliderBounds();

            //Collision detection
            manageCollision(movementWithDeltatime, Main.ActiveScene.CollidersObjects, gameTime);

        }

        /// <summary>
        /// Permet de determiner la direction du mouvement a partir des "inputs"
        /// </summary>
        /// <param name="kState">Le KeyboardState</param>
        /// <returns>La direction trouver</returns>
        public PlayerMoveDirections ManageMovementInputs()
        {
            //La direction par defaut
            PlayerMoveDirections direction = PlayerMoveDirections.None;

            bool input = InputManager.MOVE_RIGHT;



            //Defini la direction dans l'axe X
            if (InputManager.MOVE_RIGHT)
            {
                direction = PlayerMoveDirections.Right;
            }
            else if (InputManager.MOVE_LEFT)
            {
                direction = PlayerMoveDirections.Left;
            }

            //Defini la direction dans l'axe Y
            if (InputManager.MOVE_UP)
            {
                direction = PlayerMoveDirections.Up;
                //Defini la direction dans l'axe X
                if (InputManager.MOVE_RIGHT)
                {
                    direction = PlayerMoveDirections.UpRight;
                }
                else if (InputManager.MOVE_LEFT)
                {
                    direction = PlayerMoveDirections.UpLeft;
                }
            }
            else if (InputManager.MOVE_DOWN)
            {
                direction = PlayerMoveDirections.Down;
                //Defini la direction dans l'axe X
                if (InputManager.MOVE_RIGHT)
                {
                    direction = PlayerMoveDirections.DownRight;
                }
                else if (InputManager.MOVE_LEFT)
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
        public PlayerMovementStates ManageMovementStatesInputs(PlayerMoveDirections direction)
        {
            //L'etat par defaut
            PlayerMovementStates state = PlayerMovementStates.idle;

            if (InputManager.KeyboardObject.kState.GetPressedKeys().Contains(KEYBINDS.MOVE_SPRINT) && direction != PlayerMoveDirections.None)
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
        public void ManageInputs()
        {
            //Attaquer
            if (InputManager.MouseObject.LeftButtonPressed)
            {
                if (inventory.Count > 0 && typeof(Weapon).IsAssignableFrom(inventory[0].GetType()))
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
            if (IsDead)
            {
                return;
            }

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
            return _currentAnim ?? Main.Content.Load<Texture2D>(SPRITES.PLAYER_IDLE);
        }

        /// <summary>
        /// Defini le box collider
        /// </summary>
        public override void SetColliderBounds()
        {
            Bounds = new Rectangle(new Point((int)Position.X + 160, (int)Position.Y + 160), new Point(TEXTURE_SIZE, TEXTURE_SIZE));
        }

        /// <summary>
        /// Dessine les items des slots principaux (1-5)
        /// </summary>
        /// <param name="inventory">L'inventaire qui contient les items</param>
        public void DrawItems(List<Item> inventory, SpriteBatch spriteBatch)
        {
            foreach (Item item in inventory)
            {
                if (typeof(Weapon).IsAssignableFrom(item.GetType()))
                {
                    (item as Weapon).RenderItem(this, TEXTURE_SIZE, ref spriteBatch);
                }
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
                if (gameObject.GetType() != typeof(Player) && Bounds.Intersects(gameObject.Bounds))
                {
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
            foreach (Rectangle item in Main.ActiveScene.tilemapCollider.Colliders)
            {
                if (Bounds.Intersects(item))
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
            Main.OnUpdate -= Update;

            _isDead = true;
            _currentAnim = _playerAnimations.GetValueOrDefault("ANIM_DEATH");
            _animationFrameCount = 6;
            _currentAnimIndex = 0;

            OnDeath?.Invoke(this);
        }

        /// <summary>
        /// Permet de soigner le joueur
        /// </summary>
        /// <param name="amount">La quantiter de point de vie a donner</param>
        /// <returns>Les points de vie apres avoir soigner le joueur</returns>
        public int Heal(int amount)
        {
            if (amount < 0)
            {
                return Health;
            }
            return Health += amount;
        }

        protected override void OnTwoGameObjectsCollision(ICollider object1, ICollider object2) { return; }
    }
}
