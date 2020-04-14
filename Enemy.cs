using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{ 
    class Mine
    {
        protected Texture2D texture;
        protected Vector2 vector;

        public Mine(Texture2D texture, float X, float Y, float speedX,
            float speedY) 
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, vector, Color.White);
        }

        public float X { get { return vector.X; } }
        public float Y { get { return vector.Y; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }
        protected Vector2 speed;
        protected bool isAlive = true;


        public bool CheckCollision1(Mine other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y),
                Convert.ToInt32(Height), Convert.ToInt32(Width));
            Rectangle otherRect =
                new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y),
                Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }
        public bool CheckCollision2(Tripod other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y),
                Convert.ToInt32(Height), Convert.ToInt32(Width));
            Rectangle otherRect =
                new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y),
                Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }     
  
        public void Update(GameWindow window)
        {
            vector.X += speed.X;

            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
                speed.X *= -1;
            vector.Y += speed.Y;
            if (vector.Y > window.ClientBounds.Height - texture.Height || vector.Y < 0)
                speed.Y *= -1;
        }

        public void Changedirection()
        {
            speed.X *= -1;
            speed.Y *= -1;
        }
    }

    class Tripod
    {
        protected Texture2D texture;
        protected Vector2 vector;

        public Tripod(Texture2D texture, float X, float Y, float speedX,
            float speedY)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, vector, Color.White);
        }

        public float X { get { return vector.X; } }
        public float Y { get { return vector.Y; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }
        protected Vector2 speed;
        protected bool isAlive = true;


        public bool CheckCollision1(Mine other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y),
                Convert.ToInt32(Height), Convert.ToInt32(Width));
            Rectangle otherRect =
                new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y),
                Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }

        public bool CheckCollision2(Tripod other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y),
                Convert.ToInt32(Height), Convert.ToInt32(Width));
            Rectangle otherRect =
                new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y),
                Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public void Update(GameWindow window)
        {
            vector.Y += speed.Y;
            if (vector.Y > window.ClientBounds.Height - texture.Height || vector.Y < 0)
                speed.Y *= -1;
            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
                speed.X *= -1;

        }

        public void Changedirection2()
        {
            speed.X *= -1;
            speed.Y *= -1;
        }
    }

}
