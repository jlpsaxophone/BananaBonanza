using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BananaBonanza
{
    public class Barrel
    {
        BananaBonanza game;
        GraphicsDeviceManager graphics;

        public BoundingRectangle bounds = new BoundingRectangle();

        Texture2D texture;

        

        public Barrel(BananaBonanza game, GraphicsDeviceManager graphics, float x, float y)
        {
            this.game = game;
            this.graphics = graphics;
            this.bounds.X = x;
            this.bounds.Y = y;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("barrel");
            bounds.Width = 96;
            bounds.Height = 138;
        }

        public void Update(GameTime timeSpan)
        {
            if(Collisions.CollidesWith(game.monkey.bounds, bounds) && !game.end)
            {
                game.score += game.heldBananas;
                game.heldBananas = 0;               
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
