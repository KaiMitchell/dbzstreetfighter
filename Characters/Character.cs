using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static recap.Animation;

namespace recap
{
    public class Character
    {
        private Sprite Idle;
        private Sprite Walking;
        private Sprite Jumping;
        private Sprite Punch;
        private bool IsJumping;
        private bool IsWalking;
        private bool IsReversed;
        private bool IsPunching;
        public bool IsFalling;

        public Vector2 Velocity;
        Vector2 Position;

        private const float GRAVITY = 0.5f;
        private const float JUMP_FORCE = -10f;
        private const float MAX_FALL_SPEED = 10f;
        public int GROUND = 200;

        public Character(Texture2D texture)
        {
            Position = new Vector2(300, GROUND);

            var IdleAnimation = new Animation(texture, 10, 465, 123, 205);
            var WalkingAnimation = new Animation(texture, 13, 870, 130, 210);
            var JumpingAnimation = new Animation(texture, 8, 1645, 158, 214);
            var PunchAnimation = new Animation(texture, 6, 5145, 178, 184);

            Idle = new Sprite(IdleAnimation);
            Walking = new Sprite(WalkingAnimation);
            Jumping = new Sprite(JumpingAnimation);
            Punch = new Sprite(PunchAnimation);
        }

        public void HandleInput(InputHandler Input)
        {
            if(Input.IsKeyDown(Keys.Left) && !IsPunching)
            {
                Velocity.X += -5f;
                IsReversed = false;
                IsWalking = true;
            }
            else if(Input.IsKeyDown(Keys.Right) && !IsPunching)
            {
                Velocity.X += 5f;
                IsReversed = true;
                IsWalking = true;
            }
            else 
                IsWalking = false;

            if(Input.IsKeyPressed(Keys.Up) && Position.Y >= GROUND)
            {
                IsJumping = true;
                Velocity.Y = JUMP_FORCE;
                Jumping.StartAnimation();
            }

            if(Input.IsKeyPressed(Keys.A) && !IsJumping)
            {
                IsPunching = true;
                IsWalking = false;
                Punch.StartAnimation();
            }
        }

        public void Update(GameTime gameTime, InputHandler Input)
        {
            HandleInput(Input);
            Position.X = Velocity.X;
            if(Position.Y < GROUND || IsJumping)
            {
                Velocity.Y += GRAVITY;
                if(Velocity.Y > MAX_FALL_SPEED)
                    Velocity.Y = MAX_FALL_SPEED;

                Position.Y += Velocity.Y;
            }

            if(Position.Y >= GROUND)
            {
                Position.Y = GROUND;
                Velocity.Y = 0;
                IsJumping = false;
            }

            if(IsJumping)
            {
                Jumping.SetReversed(IsReversed);
                Jumping.Update(gameTime, Input, IsWalking);
            }
            else if(IsWalking)
            {
                Console.WriteLine("Walking");
                Walking.SetReversed(IsReversed);
                Walking.Update(gameTime, Input, IsWalking);
            }
            else if(IsPunching)
            {
                Console.WriteLine("Punch");
                Punch.SetReversed(IsReversed);
                Punch.Update(gameTime, Input, IsWalking);
                if(!Punch.IsAnimating)
                    IsPunching = false;
            }
            else 
            {
                Idle.SetReversed(IsReversed);
                Idle.Update(gameTime, Input, false);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if(IsJumping)
                Jumping.Draw(batch, Position);
            else if(IsWalking)
                Walking.Draw(batch, Position);
            else if(IsPunching)
                Punch.Draw(batch, Position);
            else
                Idle.Draw(batch, Position);
        }
    }
}