using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace spaceShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WINDOW_HEIGHT = 600;
        const int WINDOW_WIDTH = WINDOW_HEIGHT/9*16;

        const int MAX_PLAYER_SHOTS = 25;

        Player player;
        List<Weapon> shots = new List<Weapon>();
        const int playerNumLives = 3;

        int playerShotDelay = 0;
        bool previousButtonStateY = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load in the player character
            player = new spaceShooter.Player(Content.Load<Texture2D>(@"sprites\playerShip1_red"), new Vector2(graphics.PreferredBackBufferWidth/4, graphics.PreferredBackBufferHeight/2), playerNumLives);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
            //Ensures gamepad connected, otherwise defaults to mouse controls.
            if (!gamepad.IsConnected)
            {
                player.Update(gameTime, mouse, new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT));
            } else
            {
                playerShotDelay -= gameTime.ElapsedGameTime.Milliseconds;
                player.Update(gameTime, gamepad, new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT));
                if (gamepad.IsButtonDown(Buttons.A))
                {
                    
                    //Limit the number of lasers on the screen
                    if(shots.Count < MAX_PLAYER_SHOTS && playerShotDelay <= 0)
                    {
                        shots.Add(player.Shoot(gameTime));
                        playerShotDelay = 125;
                    }
                    else
                    {

                    }

                    foreach (Weapon bullet in shots)
                    {
                        bullet.Sprite = Content.Load<Texture2D>(@"sprites\laserBlue03");
                    }
                }
                foreach (Weapon bullet in shots.ToList())
                {
                    bullet.Update(gameTime, new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT));
                    if(bullet.Location.X < 0 || bullet.Location.Y < 0 || bullet.Location.X > WINDOW_WIDTH || bullet.Location.Y > WINDOW_HEIGHT)
                    {
                        shots.Remove(bullet);
                    }
                }
                if (gamepad.IsButtonDown(Buttons.Y) && playerShotDelay <=0)
                {
                    player.SelectWeapon();
                    playerShotDelay = 125;
                }
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach(Weapon bullet in shots)
            {
                bullet.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
