using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain
{
    public abstract class EntidadeComNome : EntidadeComId
    {
        public string Nome { get; set; }

        public override string ToString() => Nome;

        public int CompareTo(object obj)
        {
            if (obj as EntidadeComNome != null)
                return (this.Nome ?? string.Empty)
                        .CompareTo(((EntidadeComNome)obj).Nome ?? string.Empty);

            return -1;
        }
    }
}
