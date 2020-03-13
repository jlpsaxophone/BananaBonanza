using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BananaBonanza
{
    public class Bomb
    {
        BananaBonanza game;
        GraphicsDeviceManager graphics;

        public BoundingRectangle bounds = new BoundingRectangle();

        Texture2D texture;

        public Vector2 velocity;

        public Bomb(BananaBonanza game, GraphicsDeviceManager graphics, double random1, double random2, float x, float y)
        {
            this.game = game;
            this.graphics = graphics;
            velocity = new Vector2(
                           (float)(random1),
                           (float)(random2)
                       );
            //shrinks it to unit vector (same speed, random direction)
            velocity.Normalize();
            bounds.X = x;
            bounds.Y = y;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bomb");
            bounds.Width = 42;
            bounds.Height = 66;
        }

        public void Update(GameTime timeSpan)
        {
            var newKeyboardState = Keyboard.GetState();
            Vector2 temp = new Vector2(bounds.X, bounds.Y);
            temp += (float)(.5 * timeSpan.ElapsedGameTime.TotalMilliseconds) * (velocity);
            bounds.X = temp.X;
            bounds.Y = temp.Y;

            if (bounds.Y < 0)
            {
                velocity.Y *= -1;
                float delta = 0 - bounds.Y;
                bounds.Y += 2 * delta;
            }

            if (bounds.Y > graphics.PreferredBackBufferHeight - 100)
            {
                velocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - 100 - bounds.Y;
                bounds.Y += 2 * delta;
            }

            if (bounds.X < 0)
            {
                velocity.X *= -1;
                float delta = 0 - bounds.X;
                bounds.X += 2 * delta;
            }

            if (bounds.X > graphics.PreferredBackBufferWidth - 100)
            {
                velocity.X *= -1;
                float delta = graphics.PreferredBackBufferWidth - 100 - bounds.X;
                bounds.X += 2 * delta;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                new Rectangle(
                    (int)bounds.X,
                    (int)bounds.Y,
                    (int)bounds.Width,
                    (int)bounds.Height),
                    Color.White);
        }
    }
}
