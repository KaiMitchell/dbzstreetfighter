using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static recap.Animation;

namespace recap
{
    public class Character
    {
        Vector2 Position;
        private Sprite Idle;
        private Sprite Walking;
        private Sprite Jumping;
        private bool IsJumping;
        private bool IsWalking;
        private bool IsReversed;

        public Character(Texture2D texture)
        {
            var IdleAnimation = new Animation(texture, 10, 465, 123, 205);
            var WalkingAnimation = new Animation(texture, 13, 870, 130, 210);
            var JumpingAnimation = new Animation(texture, 8, 1645, 158, 214);

            Idle = new Sprite(IdleAnimation, Position);
            Walking = new Sprite(WalkingAnimation, Position);
            Jumping = new Sprite(JumpingAnimation, Position);
        }

        public void HandleInput(InputHandler Input)
        {
            if(Input.IsKeyPressed(Keys.Left))
            {
                IsReversed = false;
            }
            else if(Input.IsKeyPressed(Keys.Right))
            {
                IsReversed = true;
            }

            if(Input.IsKeyPressed(Keys.Up) && !IsJumping)
            {
                IsJumping = true;
                Jumping.StartAnimation();
            }
            
            if(Input.IsKeyDown(Keys.Left) || Input.IsKeyDown(Keys.Right))
            {
                if(!IsWalking)
                {
                    IsWalking = true;   
                    Walking.StartAnimation();
                }
            }
            else
            {
                IsWalking = false;
            }

            if(Input.IsKeyDown(Keys.Left))
                Position.X -= 5;
            if(Input.IsKeyDown(Keys.Right))
                Position.X += 5;
            if(Input.IsKeyDown(Keys.Down))
                Position.Y += 5;
        }

        public void Update(GameTime gameTime, InputHandler Input)
        {
            HandleInput(Input);

            if(IsJumping)
            {
                Jumping.SetReversed(IsReversed);
                Jumping.Update(gameTime, Input, IsJumping);
                if(!Jumping.IsAnimating)
                {
                    IsJumping = false;
                }
            }
            else if(IsWalking)
            {
                Console.WriteLine("Walking");
                Walking.SetReversed(IsReversed);
                Walking.Update(gameTime, Input, IsJumping);
            }
            else 
            {
                Idle.SetReversed(IsReversed);
                Idle.Update(gameTime, Input, IsJumping);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if(IsJumping)
                Jumping.Draw(batch, Position);
            else if(IsWalking)
                Walking.Draw(batch, Position);
            else
                Idle.Draw(batch, Position);
        }
    }
}