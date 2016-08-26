using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spaceShooter
{

    /// <summary>
    /// Player Class for a top-down space shooter
    /// </summary>
    class Player
    {
        #region Fields

        //Object status
        bool active = true;
        int lives;
        const int playerSpeed = 6;
        const int thumbstickDeflectionAmount = 20;
        Vector2 velocity = Vector2.Zero;

        int selectedWeapon = 0;

        //Drawing Support
        Texture2D sprite;
        Rectangle drawRectangle;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the player character
        /// </summary>
        /// <param name="sprite">Sprite</param>
        /// <param name="location">Location of center of sprite</param>
        /// <param name="lives">Number of player lives</param>
        public Player(Texture2D sprite, Vector2 location, int lives)
        {
            this.sprite = sprite;
            this.lives = lives;

            //Draw rectangle implimentation
            drawRectangle = new Rectangle((int)(location.X - sprite.Width / 2), (int)(location.Y - sprite.Height / 2), sprite.Width, sprite.Height);
        }
        #endregion

        #region Properties
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Gets the collision rectangle for the player
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets the remaining lives of the player
        /// </summary>
        public int Lives
        {
            get { return lives; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draws the player
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        /// <summary>
        /// Updates the player position
        /// </summary>
        /// <param name="gameTime">Game Time</param>
        /// <param name="gamepad">Gamepad State</param>
        public void Update(GameTime gameTime, GamePadState gamepad, Vector2 window)
        {
            //move based on velocity
            //drawRectangle.X += (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds);
            //drawRectangle.Y += (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds);
            drawRectangle.X += (int)(gamepad.ThumbSticks.Left.X * thumbstickDeflectionAmount);
            drawRectangle.Y -= (int)(gamepad.ThumbSticks.Left.Y * thumbstickDeflectionAmount);

            //Clamp to window
            if (drawRectangle.Left < 0)
            {
                drawRectangle.X = 0;
            }
            if (drawRectangle.Right > window.X)
            {
                drawRectangle.X = (int)window.X - drawRectangle.Width;
            }
            if (drawRectangle.Top < 0)
            {
                drawRectangle.Y = 0;
            }
            if (drawRectangle.Bottom > window.Y)
            {
                drawRectangle.Y = (int)window.Y - drawRectangle.Height;
            }
        }

        public void Update(GameTime gameTime, MouseState mouse, Vector2 window)
        {
            //move based on velocity
            drawRectangle.X += (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds);
            drawRectangle.Y += (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        /// Fires a new projectile
        /// </summary>
        /// <param name="gameTime"></param>
        public Weapon Shoot(GameTime gameTime)
        {
            Weapon shot = new Weapon(selectedWeapon, new Vector2(drawRectangle.X+sprite.Width, drawRectangle.Y+sprite.Height/2), 6);
            return shot;
        }

       public void SelectWeapon()
        {
            if (selectedWeapon <= 4)
            {
                selectedWeapon++;
            }
            else
            {
                selectedWeapon = 0;
            }
        }
        #endregion

    }
}
