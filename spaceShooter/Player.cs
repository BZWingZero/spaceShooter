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
        Vector2 velocity = Vector2.Zero;

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
        /// <param name="mouse">Mouse State</param>
        public void Update(GameTime gameTime, MouseState mouse)
        {
            //move based on velocity
            drawRectangle.X += (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds);
            drawRectangle.Y += (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds);
        }
        #endregion

    }
}
