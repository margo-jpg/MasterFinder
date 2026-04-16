namespace MasterFinder.Domain.Base
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        protected Entity() { }

        protected Entity(int id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}