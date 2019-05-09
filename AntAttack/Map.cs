using System;
using System.IO;

namespace AntAttack
{
    public class Map
    {
        public const char Wall = 'X';
        
        private int _width, _height, _depth;
        private char[,,] _map;

        public int Width => _width;
        public int Height => _height;
        public int Depth => _depth;

        public Map(string file)
        {
            StreamReader streamReader;
            using (streamReader = new StreamReader(file))
            {
                _width = int.Parse(streamReader.ReadLine());
                _height = int.Parse(streamReader.ReadLine());
                _depth = int.Parse(streamReader.ReadLine());
                _map = new char[_width, _height, _depth];

                for (int z = 0; z < _depth; z++)
                {
                    streamReader.ReadLine(); // empty line between levels
                    for (int y = 0; y < _height; y++)
                    {
                        string line = streamReader.ReadLine();
                        for (int x = 0; x < _width; x++)
                        {
                            _map[x, y, z] = line[x];
                        }
                    }
                }
            }
        }

        public char Get(int x, int y, int z)
        {
            return _map[x, y, z];
        }
        
    }
}