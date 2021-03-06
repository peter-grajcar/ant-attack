using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AntAttack
{
    public class Level
    {
        public Level()
        {
            Ants = new List<Vector3>();
        }
        public Vector3 Rescuer { get; set; }
        public Vector3 Rescuee { get; set; }
        public List<Vector3> Ants  { get; set; }
    }

    public class Levels
    {
        private List<Level> _levels;
        public int Count => _levels.Count;


        public Levels()
        {
            _levels = new List<Level>();
            
            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.levels));
            StreamReader streamReader;
            using (streamReader = new StreamReader(stream))
            {
                string[] arr = streamReader.ReadLine().Split();
                int levels = int.Parse(arr[0]);
                for (int i = 0; i < levels; i++)
                {
                    Level level = new Level();
                    Vector3 v;
                    
                    /* Ignore first line */
                    streamReader.ReadLine();
                    /* Rescuer */
                    arr = streamReader.ReadLine().Split();
                    v = new Vector3(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
                    level.Rescuer = v;
                    /* Rescuee */
                    arr = streamReader.ReadLine().Split();
                    v = new Vector3(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
                    level.Rescuee = v;
                    /* Ants */
                    arr = streamReader.ReadLine().Split();
                    int ants = int.Parse(arr[0]);
                    for (int j = 0; j < ants; j++)
                    {
                        arr = streamReader.ReadLine().Split();
                        v = new Vector3(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
                        level.Ants.Add(v);
                    }
                    
                    _levels.Add(level);
                }
            }
            stream.Close();
        }

        public Level GetLevel(int i)
        {
            return _levels[i];
        }
    }
}