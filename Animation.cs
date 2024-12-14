using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recap
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get; set; }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get; set; }
        public bool IsLooping { get; set; }
        public Texture2D Texture { get; private set; }

        private int StartY;
        private float Timer;

        public Animation(Texture2D texture, int frameCount, int startY, int frameWidth, int frameHeight)
        {
            Texture = texture;
            FrameCount = frameCount;
            StartY = startY;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;

            IsLooping = true;
            CurrentFrame = 0;
            FrameSpeed = 0.1f;
        }

        public bool IsComplete => CurrentFrame >= FrameCount - 1;

        public void Reset()
        {
            CurrentFrame = 0;
            Timer = 0f;
        }

        public void Update(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(Timer >= FrameSpeed)
            {
                Timer = 0;
                CurrentFrame++;
                if(CurrentFrame >= FrameCount)
                {
                    if(IsLooping)
                        CurrentFrame = 1;
                    else
                        CurrentFrame = FrameCount - 1;
                }
            }
        }

        public void Draw(SpriteBatch batch, Vector2 position, SpriteEffects flip)
        {
            Rectangle sourceRect = new Rectangle(
                CurrentFrame * FrameWidth,
                StartY,
                FrameWidth,
                FrameHeight
            );

            batch.Draw(
                Texture,
                new Rectangle(200, 200, FrameWidth, FrameHeight),
                sourceRect,
                Color.White,
                0f,
                Vector2.One,
                flip,
                0f
            );
        }
    }
}