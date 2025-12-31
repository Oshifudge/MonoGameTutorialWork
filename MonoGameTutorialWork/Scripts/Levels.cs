using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;
using System;


namespace MonoGameTutorialWork.Scripts
{
    internal class Levels
    {
        private Texture2D wallTexture;
        private Texture2D platformTexture;
        private Texture2D obstacleTexture;
        private Texture2D obstacleTexture2;
        private Vector2 textureWH;
        private string[] levelContents;
        public int currentLevel;

        private int animFrameIndex;
        private double currentFrameTime;
        private double frameTimeLimit;

        public Levels()
        {
            currentLevel = 1;
            BuildNewLevel();
        }

        public void LoadContent(ContentManager cM, string levelFileName, string platformFileName, string obstacleFileName, string obstacle2FileName)
        {
            animFrameIndex = 0;
            currentFrameTime = 0.0f;
            frameTimeLimit = 0.4f;

            wallTexture = cM.Load<Texture2D>(levelFileName);
            platformTexture = cM.Load<Texture2D>(platformFileName);
            obstacleTexture = cM.Load<Texture2D>(obstacleFileName);
            obstacleTexture2 = cM.Load<Texture2D>(obstacle2FileName);
            textureWH = new Vector2(wallTexture.Width, wallTexture.Height);
        }

        private int GetArrayWidth()
        {
            return levelContents[0].Length;
        }
        private int GetArrayHeight()
        {
            return levelContents.Length;
        }

        public Vector2 GetLevelSize()
        {
            return new Vector2(levelContents[0].Length * textureWH.X, levelContents.Length * textureWH.Y);
        }

        private void LevelAdder()
        {
            currentLevel++;
        }

        private void BuildNewLevel()
        {
            levelContents = File.ReadAllLines(@"..\Levels\Level " + currentLevel + ".txt");
            foreach (var line in levelContents)
            {
                Console.WriteLine(line);
            }
        }
        public void ResetLevels()
        {
            currentLevel = 1;
            BuildNewLevel();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int col = 0; col < GetArrayWidth(); col++)
            {
                for (int row = 0; row < GetArrayHeight(); row++)
                {
                    if (levelContents[row][col] == 'W')
                    {
                        spriteBatch.Draw(wallTexture, new Vector2(wallTexture.Width * col, wallTexture.Height * row), Color.White);
                    }

                    if (levelContents[row][col] == 'P')
                    {
                        spriteBatch.Draw(platformTexture, new Vector2(platformTexture.Width * col, platformTexture.Height * row), Color.White);
                    }
                    if (levelContents[row][col] == 'O')
                    {
                        spriteBatch.Draw(obstacleTexture, new Vector2(obstacleTexture.Width * col, obstacleTexture.Height * row), Color.White);

                        //obstacle = new Obstacle(new Vector2(obstacleTexture.Width / 2 * col, obstacleTexture.Height * row), currentLevel, new Rectangle(0, 0, 64, 64));
                        //obstacle.LoadContent(CM, "ObstacleSpriteSheet");
                    }
                }
            }
        }

        public void AnimateObstacles(SpriteBatch spriteBatch)
        {
            //timer ticks / if exceeds limit, change frame
            currentFrameTime += deltaTime;
            if (currentFrameTime > frameTimeLimit)
            {
                for (int col = 0; col < GetArrayWidth(); col++)
                {
                    for (int row = 0; row < GetArrayHeight(); row++)
                    {
                        if (levelContents[row][col] == 'O')
                        {
                            if (animFrameIndex == 0)
                            {
                                spriteBatch.Draw(obstacleTexture, new Vector2(obstacleTexture.Width * col, obstacleTexture.Height * row), Color.White);
                                animFrameIndex++;
                                currentFrameTime = 0.0f;
                            }
                            else if (animFrameIndex == 1)
                            {
                                spriteBatch.Draw(obstacleTexture2, new Vector2(obstacleTexture.Width * col, obstacleTexture.Height * row), Color.White);
                                animFrameIndex = 0;
                                currentFrameTime = 0.0f;
                            }
                        }
                    }
                }
            }
        }

        public bool IsWall(float x, float y)
        {
            if (levelContents[(int)y/wallTexture.Height][(int)x/wallTexture.Width] == 'W')
            {
                return true;
            }
            return false;
        }

        public bool IsPlatform(float x, float y)
        {
            if (levelContents[(int)y / platformTexture.Height][(int)x / platformTexture.Width] == 'P')
                return true;
            return false;
        }

        public bool IsInSameRow(int int1, int int2)
        {
            int first = int1 / (int)wallTexture.Height;
            int second = int2 / (int)wallTexture.Height;
            if(first == second)
                return true;
            return false;
        }
    }
}
