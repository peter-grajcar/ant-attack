using System;
using System.Collections.Generic;
using System.IO;

namespace AntAttack
{
    public class Map
    {
        public const char Wall = 'X';
        public const char Entity = 'E';
        public const char Air = '.';
        
        private int _width, _height, _depth;
        private char[,,] _map;
        private Entity[,,] _entities;

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
                _entities = new Entity[_width, _height, _depth];

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

        public bool Move(Entity entity, Vector3 to)
        {
            if (_map[to.X, to.Y, to.Z] == Map.Air)
            {
                _entities[to.X, to.Y, to.Z] = entity;
                _map[to.X, to.Y, to.Z] = Map.Entity;
                _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Air;
                entity.Position = to;
                return true;
            }

            return false;
        }

        public Entity GetEntity(int x, int y, int z)
        {
            return _entities[x, y, z];
        }

        public void AddEntity(Entity entity)
        {
            _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Entity;
            _entities[entity.Position.X, entity.Position.Y, entity.Position.Z] = entity;
        }
        
    }
}