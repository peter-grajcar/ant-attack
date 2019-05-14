using System;

namespace AntAttack
{
    public class Time
    {
        private static DateTime _last = DateTime.Now;
        private static ulong _t = 0;
        private static uint _dt = 0;

        public static ulong T => _t;
        public static uint DeltaT => _dt;

        public static void Tick()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = now - _last;

            _dt = (uint) span.TotalMilliseconds;
            _t += _dt;

            _last = now;
        }
    }
}