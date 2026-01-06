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
        private Texture2D crystalTexture;
        private Texture2D heartTexture;
        private Texture2D endFlagTexture;
        private Vector2 textureWH;
        private string[] levelContents;
        private string[] objectSpawns;
        public int currentLevel;

        private int animFrameIndex;
        private double currentFrameTime;
        private double frameTimeLimit;
        private Vector2 tileSize;

        bool shouldDrawObjects;

        Obstacle obstacle;
        public Crystal crystal;
        public LivesUp livesUp;
        public EndFlag endFlag;
        List<Vector2> ObstacleSpawnPoints = new List<Vector2>();
        List<Obstacle> Obstacles = new List<Obstacle>();
        public List<Vector2> CrystalSpawnPoints = new List<Vector2>();
        public List<Crystal> Crystals = new List<Crystal>();
        public List<Vector2> LivesSpawnPoints = new List<Vector2>();
        public List<LivesUp> LivesUps = new List<LivesUp>();
        double deltaTime;

        public Levels()
        {
            tileSize = new Vector2(128, 128);
            currentLevel = 1;
            BuildNewLevel();
            
        }

        public void LoadContent(ContentManager cM, string levelFileName, string platformFileName, string obstacleFileName, string obstacle2FileName,
            string crystalFileName, string heartFileName, string endFlagFileName)
        {
            wallTexture = cM.Load<Texture2D>(levelFileName);
            platformTexture = cM.Load<Texture2D>(platformFileName);
            obstacleTexture = cM.Load<Texture2D>(obstacleFileName);
            obstacleTexture2 = cM.Load<Texture2D>(obstacle2FileName);
            crystalTexture = cM.Load<Texture2D>(crystalFileName);
            heartTexture = cM.Load<Texture2D>(heartFileName);
            endFlagTexture = cM.Load<Texture2D>(endFlagFileName);
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
                }
            }
            if (shouldDrawObjects)
            {
                foreach (Obstacle obs in Obstacles)
                {
                    obs.Draw(deltaTime, spriteBatch, obstacleTexture, obstacleTexture2);
                }
                foreach (Crystal crys in Crystals)
                {
                    crys.Draw(deltaTime, spriteBatch, crystalTexture);
                }

                foreach (LivesUp livesUp in LivesUps)
                {
                    livesUp.Draw(deltaTime, spriteBatch, heartTexture);
                }

                endFlag.Draw(deltaTime, spriteBatch, endFlagTexture);
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
                            obstacle = new Obstacle(ObstacleSpawnPoints[i], new Rectangle(0, 0, 128, 128), obstacleTexture);

                        }
                        Obstacles.Add(obstacle);
                    }

                    if (objectSpawns[row][col] == 'C')
                    {                         
                        CrystalSpawnPoints.Add(new Vector2(tileSize.X * col, tileSize.Y * row));

                        for (int i = 0; i < CrystalSpawnPoints.Count; i++)
                        {
                            crystal = new Crystal(CrystalSpawnPoints[i], new Rectangle(0, 0, 64, 64), crystalTexture);
                        }
                        Crystals.Add(crystal);
                    }

                    if(objectSpawns[row][col] == 'L')
                    {
                        LivesSpawnPoints.Add(new Vector2(tileSize.X * col, tileSize.Y * row));
                        for (int i = 0; i < LivesSpawnPoints.Count; i++)
                        {
                            livesUp = new LivesUp(LivesSpawnPoints[i], new Rectangle(0, 0, 64, 64), heartTexture);
                        }
                        LivesUps.Add(livesUp);
                    }

                    if(objectSpawns[row][col] == 'F')
                    {
                        endFlag = new EndFlag(new Vector2(tileSize.X * col, tileSize.Y * row), new Rectangle(0, 0, 128, 128), endFlagTexture);
                    }
                }
            }
            shouldDrawObjects = true;
        }

        public void GetDeltaTime(double playGameDeltaTime)
        {
            deltaTime = playGameDeltaTime;
        }

        public bool IsWall(float x, float y)
        {
            if (levelContents[(int)y/wallTexture.Height][(int)x/wallTexture.Width] == 'W')
            {
                return true; 
            }
            return false;
        }
        
        public bool IsObstacle(float x, float y)
        {
            if (objectSpawns[(int)y / obstacleTexture.Height][(int)x / obstacleTexture.Width] == 'O')
                return true;
            return false;
        }

        public bool IsCrystal(float x, float y)
        {
            
            if (objectSpawns[(int)y / obstacleTexture.Height][(int)x / obstacleTexture.Width] == 'C')
            {
                
                int i = CrystalSpawnPoints.IndexOf(new Vector2((int)x / obstacleTexture.Height, (int)y / obstacleTexture.Width));
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
