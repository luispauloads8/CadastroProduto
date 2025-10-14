using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain
{
    public abstract class EntidadeComId
    {
        public virtual long Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            return Equals(Id, (obj as EntidadeComId)?.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Id.ToString();
    }
}
