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
    public class Monkey
    {
        BananaBonanza game;
        public BoundingRectangle bounds = new BoundingRectangle();

        Texture2D texture;

        public Monkey(BananaBonanza game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("monkey");
            bounds.Width = 100;
            bounds.Height = 100;
            bounds.X = 0;
            bounds.Y = 500;
        }

        public void Update(GameTime timeSpan)
        {
            var newKeyboardState = Keyboard.GetState();
            if (newKeyboardState.IsKeyDown(Keys.Up))
            {
                bounds.Y -= (float)(.5 * timeSpan.ElapsedGameTime.TotalMilliseconds);
            }

            if (newKeyboardState.IsKeyDown(Keys.Down))
            {
                bounds.Y += (float)(.5 * timeSpan.ElapsedGameTime.TotalMilliseconds);
            }

            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                bounds.X -= (float)(.5 * timeSpan.ElapsedGameTime.TotalMilliseconds);
            }

            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                bounds.X += (float)(.5 * timeSpan.ElapsedGameTime.TotalMilliseconds);
            }

            if (bounds.Y < 380) bounds.Y = 380;
            if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height) bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            if (bounds.X < 0) bounds.X = 0;
            if (bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width) bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
