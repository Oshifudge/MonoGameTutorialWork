using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;


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
        private string[] objectSpawns;
        public int currentLevel;

        private int animFrameIndex;
        private double currentFrameTime;
        private double frameTimeLimit;
        private Vector2 tileSize;

        bool shouldDrawObstacles;

        Obstacle obstacle;
        List<Vector2> ObstacleSpawnPoints = new List<Vector2>();
        List<Obstacle> Obstacles = new List<Obstacle>();

        public Levels()
        {
            tileSize = new Vector2(128, 128);
            currentLevel = 1;
            BuildNewLevel();
            
        }

        public void LoadContent(ContentManager cM, string levelFileName, string platformFileName, string obstacleFileName, string obstacle2FileName)
        {
            wallTexture = cM.Load<Texture2D>(levelFileName);
            platformTexture = cM.Load<Texture2D>(platformFileName);
            obstacleTexture = cM.Load<Texture2D>(obstacleFileName);
            obstacleTexture2 = cM.Load<Texture2D>(obstacle2FileName);
            textureWH = new Vector2(wallTexture.Width, wallTexture.Height);
            animFrameIndex = 0;
            currentFrameTime = 0.0f;
            frameTimeLimit = 0.4f;
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
            SpawnObstacle();
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
                        spriteBatch.Draw(wallTexture, new Vector2(tileSize.X * col, tileSize.Y * row), Color.White);
                    }

                    if (levelContents[row][col] == 'P')
                    {
                        spriteBatch.Draw(platformTexture, new Vector2(platformTexture.Width * col, platformTexture.Height * row), Color.White);
                    }
                    //if (levelContents[row][col] == 'O')
                    //{
                    //    if (animFrameIndex == 0)
                    //    {
                    //        spriteBatch.Draw(obstacleTexture, new Vector2(obstacleTexture.Width * col, obstacleTexture.Height * row), Color.White);
                    //        animFrameIndex++;
                    //        currentFrameTime = 0.0f;
                    //    }
                    //    else if (animFrameIndex == 1)
                    //    {
                    //        spriteBatch.Draw(obstacleTexture2, new Vector2(obstacleTexture.Width * col, obstacleTexture.Height * row), Color.White);
                    //        animFrameIndex = 0;
                    //        currentFrameTime = 0.0f;
                    //    }
                    //}
                }
            }
            if (shouldDrawObstacles)
            {
                foreach (Obstacle obs in Obstacles)
                {
                    obs.Draw(spriteBatch, obstacleTexture);
                }
            }
        }

        public void SpawnObstacle()
        {
            objectSpawns = File.ReadAllLines(@"..\Levels\" + "ObjectSpawns.txt");
            for (int col = 0; col < GetArrayWidth(); col++)
            {
                for (int row = 0; row < GetArrayHeight(); row++)
                {
                    if (objectSpawns[row][col] == 'O')
                    {
                        ObstacleSpawnPoints.Add(new Vector2(tileSize.X * col, tileSize.Y * row));
                        
                        for (int i = 0; i < ObstacleSpawnPoints.Count; i++)
                        {
                            Console.WriteLine(ObstacleSpawnPoints[i]);
                            obstacle = new Obstacle(ObstacleSpawnPoints[i], new Rectangle(0, 0, 128, 128), obstacleTexture);

                        }
                        Obstacles.Add(obstacle);
                    }
                }
            }
            shouldDrawObstacles = true;
        }

        public void AnimateObstacles(double deltaTime)
        {
            currentFrameTime += deltaTime;
            if (currentFrameTime > frameTimeLimit)
            {
                animFrameIndex++;
                if (animFrameIndex == 2 )
                    animFrameIndex = 0;
                currentFrameTime = 0.0f;
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
