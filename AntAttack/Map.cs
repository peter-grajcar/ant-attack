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
        private List<Entity> _entities;
        private List<Entity> _create;
        private List<Entity> _destroy;

        public int Width => _width;
        public int Height => _height;
        public int Depth => _depth;
        public List<Entity> Entities => _entities;

        public Map(string file)
        {
            StreamReader streamReader;
            using (streamReader = new StreamReader(file))
            {
                _width = int.Parse(streamReader.ReadLine());
                _height = int.Parse(streamReader.ReadLine());
                _depth = int.Parse(streamReader.ReadLine());
                _map = new char[_width, _height, _depth];
                _entities = new List<Entity>();
                _create = new List<Entity>();
                _destroy = new List<Entity>();

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

        private bool IsOnMap(Vector3 pos)
        {
            return (pos.X >= 0 && pos.X < Width) && (pos.Y >= 0 && pos.Y < Height) && (pos.Z >= 0 && pos.Z < Depth);
        }

        public char Get(int x, int y, int z)
        {
            return _map[x, y, z];
        }

        public char Get(Vector3 pos)
        {
            if (!IsOnMap(pos))
                return Map.Air;
            return Get(pos.X, pos.Y, pos.Z);
        }

        public bool Move(Entity entity, Vector3 to)
        {
            if (IsOnMap(to) && _map[to.X, to.Y, to.Z] == Map.Air)
            {
                _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Air;
                _map[to.X, to.Y, to.Z] = Map.Entity;
                entity.Position = to;
                return true;
            }

            return false;
        }

        public void AddEntity(Entity entity)
        {
            _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Entity;
            _create.Add(entity);
        }

        public void CreateAndDestroyEntities()
        {
            _entities.AddRange(_create);
            _create.Clear();
            foreach (Entity entity in _destroy)
            {
                _entities.Remove(entity);
            }
            _destroy.Clear();
        }

        public void RemoveEntity(Entity entity)
        {
            _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Air;
            _destroy.Add(entity);
        }
        
    }
}