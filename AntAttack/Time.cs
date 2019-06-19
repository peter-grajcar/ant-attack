using System;

namespace AntAttack
{
    public class Time
    {
        private static DateTime _last = DateTime.Now;

        public static ulong T { get; set; }
        public static uint DeltaT { get; set; }

        public static void Tick()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = now - _last;

            DeltaT = (uint) span.TotalMilliseconds;
            T += DeltaT;

            _last = now;
        }
    }
}