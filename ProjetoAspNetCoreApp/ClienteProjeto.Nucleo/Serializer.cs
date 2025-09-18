using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClienteProjeto.Nucleo
{
    public class Serializer
    {
        public static byte[] SerializeToBytes<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            // Serializa para JSON e converte para byte[]
            string json = JsonSerializer.Serialize(obj);
            return Encoding.UTF8.GetBytes(json);
        }

        public static string SerializeToBase64<T>(T obj)
        {
            byte[] bytes = SerializeToBytes(obj);
            return Convert.ToBase64String(bytes);
        }

    }
}
