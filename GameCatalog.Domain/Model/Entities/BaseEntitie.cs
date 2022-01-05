using System;

namespace GameCatalog.Domain.Model.Entities
{
    public abstract class BaseEntitie
    {
        protected BaseEntitie()
        {
            Id = Guid.NewGuid();
        }
        protected BaseEntitie(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
