using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application
{
    public class FabricaDeServicos
    {
        public static T Crie<T>() where T : new()
        {
            return new T();
        }
    }
}
