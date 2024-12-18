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
        public bool IsReversed { get; private set; }
        public bool isAnimating;

        public Sprite(Animation animation)
        {
            this.animation = animation;

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

        public void Update(GameTime gameTime, InputHandler input, bool IsWalking = false)
        {
            // if(IsAnimating && IsJumping)
            // {
                animation.Update(gameTime); 
                if(animation.IsComplete && !IsWalking)
                {
                    isAnimating = false;
                }
            // }
            // else {
            //     animation.Update(gameTime);
            // }
        }

        public void Draw(SpriteBatch batch, Vector2 position)
        {
            SpriteEffects flip = IsReversed ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            animation.Draw(batch, position, flip);
        }
    }
}