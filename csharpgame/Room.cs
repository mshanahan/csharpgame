using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class Room
    {
        public String Directory { get; set; }

        public Room(String directory)
        {
            this.Directory = directory;
        }

        public void Make(int offsetX, int offsetY, List<Tuple<int, Action<Tile>>> WeightedSpawnerList)
        {
            StreamReader reader = new StreamReader(this.Directory);
            Environment env = Environment.Current();
            int y = 0;
            string currentRow;
            while ((currentRow = reader.ReadLine()) != null)
            {
                for (int x = 0; x < currentRow.Length; x++)
                {
                    char currentTile = currentRow[x];
                    Tile ThisTile = null;

                    if (Char.ToUpper(currentTile) == 'S')
                    {
                        ThisTile = new TileFloorStone(x + offsetX, y + offsetY);
                        env.Add(ThisTile);
                    }
                    if (Char.ToUpper(currentTile) == 'W')
                    {
                        ThisTile = new TileWallStone(x + offsetX, y + offsetY);
                        env.Add(ThisTile);
                    }
                    if (Char.ToUpper(currentTile) == 'G')
                    {
                        ThisTile = new TileWaterStagnant(x + offsetX, y + offsetY);
                        env.Add(ThisTile);
                    }
                    if (Char.IsLower(currentTile) && ThisTile != null)
                    {
                        //sum all the weights
                        int summedWeight = 0;
                        foreach (Tuple<int, Action<Tile>> tuple in WeightedSpawnerList)
                        {
                            int weight = tuple.Item1;
                            summedWeight = summedWeight + weight;
                        }

                        //roll a random number
                        int rand = env.Random.Next(1, summedWeight + 1);

                        //subtract weights from rand until 0 is reached
                        bool found = false;
                        for (int i = 0; i < WeightedSpawnerList.Count; i++)
                        {
                            rand = rand - WeightedSpawnerList[i].Item1;
                            if (rand <= 0)
                            {
                                WeightedSpawnerList[i].Item2(ThisTile); //call spawn on the randomly chosen monster
                                found = true;
                            }
                            if (found) break;
                        }
                    }
                }
                y++;
            }
        }
    }
}
