using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace recap;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private InputHandler Input;

    // Sprite Walking;
    Texture2D texture;
    Sprite Idle;
    Sprite Walking;
    Sprite Jumping;

    private bool IsReversed;
    private bool IsJumping;
    private bool IsWalking;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Input = new InputHandler();
        IsJumping = false;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here  
        texture = Content.Load<Texture2D>("modified");      
        var IdleAnimation = new Animation(texture, 10, 465, 123, 205);
        var WalkingAnimation = new Animation(texture, 13, 870, 130, 210);
        var JumpingAnimation = new Animation(texture, 8, 1645, 158, 214);
        Idle =  new Sprite(IdleAnimation, new Vector2(300, 400));
        Walking =  new Sprite(WalkingAnimation, new Vector2(300, 400));
        Jumping = new Sprite(JumpingAnimation, new Vector2(300, 400));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        Input.Update();

        if(Input.WasKeyPressed(Keys.Right))
        {
            IsReversed = true;
        }
        else if(Input.WasKeyPressed(Keys.Left))
        {
            IsReversed = false;
        }

        if(Input.WasKeyPressed(Keys.Up) && !IsJumping)
        {
            IsJumping = true;
            Jumping.StartAnimation();
        };
        if(Input.IsKeyDown(Keys.Left) || Input.IsKeyDown(Keys.Right) && !IsJumping)
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
        };

        if(IsJumping)
        {
            Console.Write("Jumping!");
            Jumping.SetReversed(IsReversed);
            Jumping.Update(gameTime, Input, IsJumping);
            if(!Jumping.IsAnimating)
            {
                IsJumping = false;
            }
        }
        else if(IsWalking)
        {
            Console.Write("Walking!");
            Walking.SetReversed(IsReversed);
            Walking.Update(gameTime, Input, IsJumping);
        }
        else 
        {
            Idle.SetReversed(IsReversed);
            Idle.Update(gameTime, Input, IsJumping);
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        if(IsJumping)
            Jumping.Draw(_spriteBatch);
        else if(IsWalking)
            Walking.Draw(_spriteBatch);
        else    
            Idle.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
