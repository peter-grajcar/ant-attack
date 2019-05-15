namespace AntAttack
{
    public abstract class Human : Entity
    {
        public enum State { Standing, Running, Jumping, Falling }
        
        protected State CurrentState { get; set; }
        
        protected Human(Map map) : base(map)
        {
            CurrentState = State.Standing;
        }
    }
}