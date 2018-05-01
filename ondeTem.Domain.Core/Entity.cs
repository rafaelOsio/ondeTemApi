using System;
using System.ComponentModel.DataAnnotations;

namespace ondeTem.Domain.Core
{
    /// <summary>
    /// classe base para implementar um id próprio para qualquer entidade que vai 
    /// cada entidade e sempre será comparável com outra entidade da mesma insatncia pelo ID.
    /// A entidade possui o mesmo id que a outra. 
    /// Por isso implementamos o Equals.
    /// </summary>
    public abstract class Entity
    {
        public long Id { get; set; }
        
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        /// <summary>
        /// Cássio: Override no ToString na classe base, pois é 
        ///         interessante porque vai conseguir entender o nome da entidade e qual é o id dela.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]";
        }
    }
}
