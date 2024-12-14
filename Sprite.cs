using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace recap
{
    public class Sprite
    {
        private Animation animation; 
        private Vector2 Position;      
        public bool IsReversed { get; private set; }

        public Sprite(Animation animation, Vector2 position)
        {
            this.animation = animation;
            Position = position;
        }

        public void SetReversed(bool isReversed) => IsReversed = isReversed;

        public void Update(GameTime gameTime, InputHandler Input)
        {
            animation.Update(gameTime); 
        }

        public void Draw(SpriteBatch batch)
        {
            SpriteEffects flip = IsReversed ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            animation.Draw(batch, Position, flip);
        }
    }
}