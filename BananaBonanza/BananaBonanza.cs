using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace BananaBonanza
{
    public class BananaBonanza : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Barrel barrel;
        List<Banana> bananas;
        List<Bomb> bombs;
        Vector2[] vectors;
        SpriteFont text;
        Random random = new Random();
        public bool end = false;
        public Monkey monkey;
        int bananaCounter = 0;
        double count = 0;
        Texture2D grass;

        public int score;
        public int heldBananas;

        public BananaBonanza()
        {
            monkey = new Monkey(this);
            vectors = new Vector2[] {new Vector2(0, 0), new Vector2(800, 0), new Vector2(400, 120)};
            barrel = new Barrel(this, graphics, 900, 590);
            graphics = new GraphicsDeviceManager(this);         
            Content.RootDirectory = "Content";
            score = 0;
            heldBananas = 0;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();           


            bananas = new List<Banana>();
            bombs = new List<Bomb>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            monkey.LoadContent(Content);
            grass = Content.Load<Texture2D>("grass");
            barrel.LoadContent(Content);
            text = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            count += gameTime.ElapsedGameTime.TotalSeconds;
            if(count >= 1)
            {
                if(bananaCounter == 8)
                {
                    int randomNum = (int)random.Next(0, vectors.Length);
                    Vector2 position = vectors[randomNum];
                    bombs.Add(new Bomb(this, graphics, random.NextDouble(), random.NextDouble(), position.X, position.Y));                   
                    bananaCounter = 0;
                    count = 0;
                }
                else
                {
                    int randomNum = (int)random.Next(0, vectors.Length);
                    Vector2 position = vectors[randomNum];
                    bananas.Add(new Banana(this, graphics, random.NextDouble(), random.NextDouble(), position.X, position.Y));
                    bananaCounter++;
                    count = 0;
                }
            }
            monkey.Update(gameTime);
            barrel.Update(gameTime);

            Banana bananaToRemove = null;
            bool remove = false;
            foreach (Banana b in bananas)
            {
                b.Update(gameTime);
                b.LoadContent(Content);                
                if(Collisions.CollidesWith(b.bounds, monkey.bounds) && heldBananas < 5)
                {
                    bananaToRemove = b;
                    remove = true;
                    heldBananas++;
                }
            }
            if(remove)
            {
                bananas.Remove(bananaToRemove);
            }           
            foreach(Bomb b in bombs)
            {
                b.Update(gameTime);
                b.LoadContent(Content);
                if(Collisions.CollidesWith(b.bounds, monkey.bounds))
                {
                    end = true;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(grass, new Rectangle(0, 450, 1042, 440), Color.White);
            foreach (Banana b in bananas)
            {
                b.Draw(spriteBatch);
            }
            foreach (Bomb b in bombs)
            {
                b.Draw(spriteBatch);
            }
            if(!end)
            {
                monkey.Draw(spriteBatch);
                barrel.Draw(spriteBatch);
            }           
            spriteBatch.DrawString(
                text,
                "Score: " + score.ToString(),
                new Vector2(175, 5),
                Color.Black
                );
            spriteBatch.DrawString(
                text,
                "Held Bananas: " + heldBananas.ToString(),
                new Vector2(5, 5),
                Color.Black
                );
            if(end)
            {
                spriteBatch.DrawString(
                text,
                "Game Over!!!",
                new Vector2(345, 5),
                Color.Black
                );
            }
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
