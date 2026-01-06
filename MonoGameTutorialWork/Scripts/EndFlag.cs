using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;
using MonoGameTutorialWork.Scripts;
using System;
using SharpDX.MediaFoundation;

public class EndFlag
{
    public int count = 5;
    private Rectangle spriteDimensions;
    private Texture2D sprite;
    private Vector2 currentPos;

    private int animFrameIndex;
    private double currentFrameTime;
    private double frameTimeLimit;

    public EndFlag(Vector2 position, Rectangle spriteRectangle, Texture2D endFlagTexture)
	{
        currentPos = position;
        spriteDimensions = spriteRectangle;
        sprite = endFlagTexture;
        animFrameIndex = 1;
        currentFrameTime = 0.0f;
        frameTimeLimit = 0.4f;
    }

    public void Draw(double deltaTime, SpriteBatch spriteBatch, Texture2D endFlagTexture)
    {
        currentFrameTime += deltaTime;
        if (currentFrameTime > frameTimeLimit)
        {
            animFrameIndex++;
            if (animFrameIndex > 7)
                animFrameIndex = 0;
            currentFrameTime = 0.0f;
        }
        spriteBatch.Draw(endFlagTexture, currentPos, new Rectangle(spriteDimensions.X + animFrameIndex * spriteDimensions.Width, spriteDimensions.Y * spriteDimensions.Height, spriteDimensions.Width, spriteDimensions.Height), color: Color.White);
    }

    public Vector2 GetCurrentPos()
    {
        return currentPos;
    }

    public Rectangle GetSpriteDimensions()
    {
        return spriteDimensions;
    }
}
