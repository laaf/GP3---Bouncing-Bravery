using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;



using System.Diagnostics;

namespace Bouncing_Bravery
{
    class Map
    {
        public List<Tile> tiles;
        public List<Tile> voids;
        private int currentLine;
        private int currentRow;
        public Vector3 defaultPlayerPos;
        public DataManager data;

        Random random = new Random();

        private List<string> lineList;

        public Map(DataManager d)
        {
            tiles = new List<Tile>();
            voids = new List<Tile>();
            currentLine = 0;
            currentRow = 0;
            lineList = new List<string>();
            data = d;
            ReadMap();
            GenerateMap();
        }

        private void Interpreter(string roomType)
        {
            if (roomType == "empty")
            {
                AddToParser("oooooooo");
                AddToParser("oooooooo");
                AddToParser("oooooooo");
                AddToParser("oooooooo");
                AddToParser("oooooooo");
                AddToParser("oooooooo");
                AddToParser("oooooooo");
                AddToParser("oooooooo");
            }

            else if (roomType == "roomS")
            {
                AddToParser("oooooxooooo");
                AddToParser("ooooosooooo");
                AddToParser("oooooxooooo");
                AddToParser("ooooxxxoooo");
                AddToParser("oooxxxxxooo");
                AddToParser("ooo!#xxxxxx");
                AddToParser("oooxxxxxooo");
                AddToParser("ooooxxxoooo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "roomE")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("ooooxxxoooo");
                AddToParser("oooxxxxxooo");
                AddToParser("xxxxxxxxooo");
                AddToParser("oooxxxxxooo");
                AddToParser("ooooxexoooo");
                AddToParser("ooooooooooo");
                AddToParser("ooooooooooo");
                AddToParser("ooooooooooo");
            }
            else if (roomType == "full")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("xx" + "xxxxxxx" + "xx");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "1")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "ooxxoxo" + "oo");
                AddToParser("oo" + "oxxooXo" + "oo");
                AddToParser("oo" + "oxooxxo" + "oo");
                AddToParser("xx" + "oxxoooo" + "xx");
                AddToParser("oo" + "ooxx@ox" + "oo");
                AddToParser("oo" + "oooxxox" + "oo");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "2")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "oooxxoo" + "oo");
                AddToParser("oo" + "ooo##oo" + "oo");
                AddToParser("oo" + "XooxxxX" + "oo");
                AddToParser("xx" + "ooxxooo" + "xx");
                AddToParser("oo" + "oo!oxoo" + "oo");
                AddToParser("oo" + "oxooooo" + "oo");
                AddToParser("oo" + "oo!ooXo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "3")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "oxooooo" + "oo");
                AddToParser("oo" + "oxxoox@" + "oo");
                AddToParser("oo" + "oooooxx" + "oo");
                AddToParser("xx" + "oXoxooo" + "xx");
                AddToParser("oo" + "oxoo!oo" + "oo");
                AddToParser("oo" + "oooooxo" + "oo");
                AddToParser("oo" + "oooxxoo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "4")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "@x00000" + "oo");
                AddToParser("oo" + "oxooxxx" + "oo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("xx" + "ooo@ooo" + "xx");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("oo" + "Xxoooxx" + "oo");
                AddToParser("oo" + "oooooxx" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "5")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "oooxooo" + "oo");
                AddToParser("oo" + "ooxxxoo" + "oo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("xx" + "oXxoooo" + "xx");
                AddToParser("oo" + "oxooxoo" + "oo");
                AddToParser("oo" + "oooxxoo" + "oo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "6")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "xxoooox" + "oo");
                AddToParser("oo" + "oooooo!" + "oo");
                AddToParser("oo" + "oooooxx" + "oo");
                AddToParser("xx" + "xo@xooo" + "xx");
                AddToParser("oo" + "xoxxooo" + "oo");
                AddToParser("oo" + "!ooooxx" + "oo");
                AddToParser("oo" + "xxooo!x" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "7")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "xooooxx" + "oo");
                AddToParser("oo" + "oooooxx" + "oo");
                AddToParser("oo" + "oo@oooo" + "oo");
                AddToParser("xx" + "xooooox" + "xx");
                AddToParser("oo" + "xxoxxox" + "oo");
                AddToParser("oo" + "oxooxoo" + "oo");
                AddToParser("oo" + "oxooxoo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "8")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "oxooooo" + "oo");
                AddToParser("oo" + "oxooo@o" + "oo");
                AddToParser("oo" + "ox#xooo" + "oo");
                AddToParser("xx" + "ox#xooo" + "xx");
                AddToParser("oo" + "ox#xxxo" + "oo");
                AddToParser("oo" + "oooooxo" + "oo");
                AddToParser("oo" + "Xooooxo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "9")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "oooxxoo" + "oo");
                AddToParser("oo" + "oooXxoo" + "oo");
                AddToParser("oo" + "ooo#ooo" + "oo");
                AddToParser("xx" + "oooxooo" + "xx");
                AddToParser("oo" + "oox@ooo" + "oo");
                AddToParser("oo" + "oo##ooo" + "oo");
                AddToParser("oo" + "ooxxooo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "10")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "oooooxx" + "oo");
                AddToParser("oo" + "oxxxooo" + "oo");
                AddToParser("oo" + "oxXxooo" + "oo");
                AddToParser("xx" + "ox!xo@o" + "xx");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("oo" + "oooooox" + "oo");
                AddToParser("oo" + "oxXooxx" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "11")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "oxxxxox" + "oo");
                AddToParser("oo" + "xoooxox" + "oo");
                AddToParser("oo" + "xxxoxox" + "oo");
                AddToParser("xx" + "ooooooo" + "xx");
                AddToParser("oo" + "xoxox!x" + "oo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("oo" + "oxxxoox" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "12")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "oxoooxo" + "oo");
                AddToParser("oo" + "oxo#ox@" + "oo");
                AddToParser("oo" + "oxoxoxo" + "oo");
                AddToParser("xx" + "o#o@oxo" + "xx");
                AddToParser("oo" + "o@oxoxo" + "oo");
                AddToParser("oo" + "!@o#oxo" + "oo");
                AddToParser("oo" + "oxoxoxo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "13")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("oo" + "xxxxx@x" + "oo");
                AddToParser("oo" + "ooxxxoo" + "oo");
                AddToParser("xx" + "oo!xxoo" + "xx");
                AddToParser("oo" + "ooxxxoo" + "oo");
                AddToParser("oo" + "Xxxxxxx" + "oo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "14")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("oo" + "oo@o@oo" + "oo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("xx" + "o@ooo@o" + "xx");
                AddToParser("oo" + "oo@o@oo" + "oo");
                AddToParser("oo" + "ooo@ooo" + "oo");
                AddToParser("oo" + "ooooooo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "15")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "xooooox" + "oo");
                AddToParser("oo" + "oXoooXo" + "oo");
                AddToParser("oo" + "oo@o@oo" + "oo");
                AddToParser("xx" + "ooo#ooo" + "xx");
                AddToParser("oo" + "oo@o@oo" + "oo");
                AddToParser("oo" + "oXoooXo" + "oo");
                AddToParser("oo" + "xooooox" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "16")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "xooxoox" + "oo");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oo" + "xoxoxox" + "oo");
                AddToParser("xx" + "xoxoxox" + "xx");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oo" + "xxxxxxx" + "oo");
                AddToParser("oo" + "xoxoxox" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "17")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "xxoooxx" + "oo");
                AddToParser("oo" + "xxXxxXx" + "oo");
                AddToParser("oo" + "xxXxxXx" + "oo");
                AddToParser("xx" + "oXooooX" + "xx");
                AddToParser("oo" + "ooXooXo" + "oo");
                AddToParser("oo" + "oooxxoo" + "oo");
                AddToParser("oo" + "xxoox!!" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "18")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "o@ooo@o" + "oo");
                AddToParser("oo" + "@o@o@o@" + "oo");
                AddToParser("oo" + "@oo@oo@" + "oo");
                AddToParser("xx" + "@ooooo@" + "xx");
                AddToParser("oo" + "o@ooo@o" + "oo");
                AddToParser("oo" + "oo@o@oo" + "oo");
                AddToParser("oo" + "ooo@ooo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
            else if (roomType == "19")
            {
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
                AddToParser("oo" + "o!@#o!o" + "oo");
                AddToParser("oo" + "Xo!!o!!" + "oo");
                AddToParser("oo" + "oxoxoxo" + "oo");
                AddToParser("xx" + "o#o@oxo" + "xx");
                AddToParser("oo" + "o@oxoxo" + "oo");
                AddToParser("oo" + "!@o#oxo" + "oo");
                AddToParser("oo" + "oxoxoxo" + "oo");
                AddToParser("oooooxooooo");
                AddToParser("oooooxooooo");
            }
        }

        private void ReadMap()
        {
            int i, j = 1;
            int max = GameConstants.amountOfRows * GameConstants.amountOfBlocksPerRow - 2;
            int count = 0;
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Luciano\Desktop\XNA\mapLayout.txt");
            //foreach (string line in lines)
            Interpreter("roomS");
            for(i = 0; i < GameConstants.amountOfRows; i++)
            {
                for(; j < GameConstants.amountOfBlocksPerRow; j++)
                {
                    string word = random.Next(20).ToString();
                    currentLine = 0;
                    Interpreter(word);
                    count++;
                    if (count == max)
                        break;
                }
                if (count == max)
                    break;
                j = 0;
                currentRow++;
            }
            currentRow--;
            Interpreter("roomE");
        }


        private void AddToParser(string s)
        {

            if (lineList.Count <= currentLine + currentRow * GameConstants.amountOfLinesPerBlock)
            {
                for (int i = 0; i < GameConstants.amountOfLinesPerBlock; i++)
                    lineList.Add("");

            }
            lineList[currentLine + currentRow * GameConstants.amountOfLinesPerBlock] += s;
            currentLine++;

        }

        private void GenerateMap()
        {
            currentLine = 0;
            foreach (string s in lineList)
            {
                ParseMap(s);
            }
        }

        private void ParseMap(string str)
        {
            int currentColumn = 0;

            foreach (char c in str)
            {
                Vector3 currentPos = new Vector3(currentColumn * GameConstants.tileSize, GameConstants.floorHeight, currentLine * GameConstants.tileSize);
                switch (c)
                {

                    case 'x':
                        //a tile, might have star with small chance
                        AddTile(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize);
                        if (random.Next(100) < 5) //5% change of star
                        {
                            CreateStar(currentPos);
                        }
                        break;
                    case 'X':
                        //a tile, might have a star with greater chance
                        AddTile(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize);
                        if (random.Next(100) < 50) //50% change of star
                        {
                            CreateStar(currentPos);
                        }
                        break;
                    case 's':
                        //it's the player starting position
                        AddTile(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize);
                        defaultPlayerPos = currentPos;
                        data.positionPortal1 = currentPos;
                        break;
                    case 'e':
                        //it's the game end
                        AddTile(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize);
                        data.positionPortal2 = currentPos;
                        break;
                    case '@':
                        //a tile, with a forced star
                        AddTile(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize);
                        CreateStar(currentPos);
                        break;
                    case '!':
                        //a tile, with a passable danger
                        AddTile(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize);
                        data.dangerList.Add(DangerFactory.PassableDanger(new Vector3(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize), data));
                        break;
                    case '#':
                        //a tile, with an unpassable danger
                        AddTile(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize);
                        data.dangerList.Add(DangerFactory.UnpassableDanger(new Vector3(currentColumn * GameConstants.tileSize, 6, currentLine * GameConstants.tileSize), data));
                        break;
                    case 'o':
                    default:
                        //Do nothing, it's a hole
                        voids.Add(new Tile(new Vector3(currentColumn * GameConstants.tileSize, 0, currentLine * GameConstants.tileSize), Vector3.Up, Vector3.UnitZ, GameConstants.tileSize, GameConstants.tileSize));
                        break;
                }
                currentColumn++;
            }
            currentLine++;
        }

        private void AddTile(float x, float y, float z)
        {
            tiles.Add(new Tile(new Vector3(x, y, z), Vector3.Up, Vector3.UnitZ, GameConstants.tileSize, GameConstants.tileSize));
        }

        public bool ExistsTile(float x, float y, float z)
        {
            foreach (Tile t in tiles)
            {
                if ((x >= t.UpperRight.X && x <= t.UpperLeft.X) //is inside the horizontal limit
                    && (z >= t.Origin.Z && z <= t.Origin.Z + GameConstants.tileSize) //and the vertical limit
                    && (y == GameConstants.floorHeight)) //and is touching the floor
                {
                    return true;
                }
            }


            return false;
        }

        public void CreateStar(Vector3 pos)
        {
            data.starList[data.NumStars].position = pos;
            data.starList[data.NumStars].isActive = true;
            data.starList[data.NumStars].RotationMatrix = Matrix.CreateRotationY(MathHelper.PiOver2);

            if (data.NumStars < GameConstants.MaxNumStars - 1)
                data.NumStars++;
        }

        public bool ExistsTile(Vector3 pos)
        {
            float x = pos.X;
            float y = pos.Y;
            float z = pos.Z;

            foreach (Tile t in tiles)
            {

                if ((x >= t.UpperRight.X && x <= t.UpperLeft.X) //is inside the horizontal limit
                    && (z <= t.UpperLeft.Z && z >= t.LowerLeft.Z) //and the vertical limit
                    && (y == GameConstants.floorHeight)) //and is touching the floor
                {
                    return true;
                }
            }
            return false;
        }
    }
}
