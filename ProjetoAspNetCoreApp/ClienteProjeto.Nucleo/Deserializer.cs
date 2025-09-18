using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClienteProjeto.Nucleo
{
    public class Deserializer
    {
        public static object DeserializeFromBase64(string base64, Type tipoDestino)
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException(nameof(base64));

            byte[] bytes = Convert.FromBase64String(base64);
            string json = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize(json, tipoDestino);
        }

    }
}
