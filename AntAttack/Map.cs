using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AntAttack
{
    public class Map
    {
        public const char Wall = 'X';
        public const char Entity = 'E';
        public const char Air = '.';
        public const char SafeZone = 'S';
        
        private int _width, _height, _depth;
        private char[,,] _map;
        private List<Entity> _entities;
        private List<Entity> _create;
        private List<Entity> _destroy;
        private List<Vector3> _safeSpots;

        public int Width => _width;
        public int Height => _height;
        public int Depth => _depth;
        public List<Entity> Entities => _entities;

        public Map()
        {
            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.map));
            StreamReader streamReader;
            using (streamReader = new StreamReader(stream))
            {
                _width = int.Parse(streamReader.ReadLine());
                _height = int.Parse(streamReader.ReadLine());
                _depth = int.Parse(streamReader.ReadLine());
                _map = new char[_width, _height, _depth];
                _entities = new List<Entity>();
                _create = new List<Entity>();
                _destroy = new List<Entity>();
                _safeSpots = new List<Vector3>();

                for (int z = 0; z < _depth; z++)
                {
                    streamReader.ReadLine(); // empty line between levels
                    for (int y = 0; y < _height; y++)
                    {
                        string line = streamReader.ReadLine();
                        for (int x = 0; x < _width; x++)
                        {
                            if (line[x] == SafeZone)
                            {
                                _map[x, y, z] = Air;
                                _safeSpots.Add(new Vector3(x, y, z));
                            }
                            else
                            {
                                _map[x, y, z] = line[x];
                            }
                        }
                    }
                }
            }
            stream.Close();
        }

        /*
         * Validates given position
         */
        public bool IsOnMap(Vector3 pos)
        {
            return (pos.X >= 0 && pos.X < Width) && (pos.Y >= 0 && pos.Y < Height) && (pos.Z >= 0 && pos.Z < Depth);
        }

        public bool IsSafe(Entity entity)
        {
            return _safeSpots.FindIndex(v => v == entity.Position) != -1;
        }
        
        /*
         * Returns what is on the map at given position
         */
        public char Get(Vector3 pos)
        {
            if (pos.Z < 0)
                return Map.Wall;
            if (!IsOnMap(pos))
                return Map.Air;
            return _map[pos.X, pos.Y, pos.Z];
        }

        /*
         * Moves the entity on the map(but does not change the entity's Position parameter!)
         */
        public bool Move(Entity entity, Vector3 to)
        {
            if (IsOnMap(to) && _map[to.X, to.Y, to.Z] == Map.Air)
            {
                _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Air;
                _map[to.X, to.Y, to.Z] = Map.Entity;
                return true;
            }

            return false;
        }

        /*
         * Adds entity to create list
         */
        public void AddEntity(Entity entity)
        {
            _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Entity;
            _create.Add(entity);
        }

        /*
         * Retrieves entities from create and destroy lists and makes changes
         * to the main entity list
         */
        public void CreateAndDestroyEntities()
        {
            _entities.AddRange(_create);
            _create.Clear();
            foreach (Entity entity in _destroy)
            {
                _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Air;
                _entities.Remove(entity);
            }
            _destroy.Clear();
        }

        /*
         * Adds entity to remove list
         */
        public void RemoveEntity(Entity entity)
        {
            _map[entity.Position.X, entity.Position.Y, entity.Position.Z] = Map.Air;
            _destroy.Add(entity);
        }

        public void RemoveAllEntities()
        {
            _destroy.AddRange(Entities);
        }
        
    }
}