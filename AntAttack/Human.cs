namespace AntAttack
{
    public abstract class Human : Entity
    {
        private Vector3[] forward = { new Vector3(1, 0,0), new Vector3(0, -1,0), new Vector3(-1, 0,0), new Vector3(0, -1,0) };
        
        public enum State { Standing, Running, Jumping, Falling }
        
        public State CurrentState { get; set; }
        public int Health { get; set; }
        public bool Controllable { get; set; }
        
        
        protected Human(Map map) : base(map)
        {
            CurrentState = State.Standing;
            Health = 20;
            Controllable = false;
        }

        public void MoveForward()
        {
            
        }

        public void Jump()
        {
            
        }
    }
}