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
        public bool isAnimating;

        public Sprite(Animation animation, Vector2 position)
        {
            this.animation = animation;
            Position = position;

            isAnimating = false;
        }

        public bool IsAnimating => isAnimating;

        public void StartAnimation()
        {
            if(!isAnimating)
            {
                isAnimating = true;
                animation.Reset();
            }
        }

        public void SetReversed(bool isReversed) => IsReversed = isReversed;

        public void Update(GameTime gameTime, InputHandler input, bool IsJumping = false)
        {
            if(IsAnimating && IsJumping)
            {
                animation.Update(gameTime); 
                if(animation.IsComplete && IsJumping)
                {
                    isAnimating = false;
                }
            }
            else {
                animation.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            SpriteEffects flip = IsReversed ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            animation.Draw(batch, Position, flip);
        }
    }
}