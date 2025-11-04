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
        private Vector2 textureWH;
        private string[] levelContents;
        public int currentLevel;

        public Levels()
        {
            currentLevel = 1;
            BuildNewLevel();
        }

        public void LoadContent(ContentManager cM, string levelFileName, string platformFileName)
        {
            wallTexture = cM.Load<Texture2D>(levelFileName);
            platformTexture = cM.Load<Texture2D>(platformFileName);
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
            if (levelContents[(int)y / platformTexture.Height][(int)x / platformTexture.Width] == 'W')
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
