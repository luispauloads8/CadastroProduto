using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.ConversaoEntidade
{
    public class PessoaDTOConverter : JsonConverter<PessoaDTO>
    {
        public override PessoaDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            // Verifica o tipo de pessoa no JSON
            if (!root.TryGetProperty("tipoPessoa", out var tipoProp))
                throw new JsonException("Campo 'tipoPessoa' é obrigatório.");

            TipoPessoa tipoPessoa;
            try
            {
                tipoPessoa = tipoProp.ValueKind switch
                {
                    JsonValueKind.String => Enum.Parse<TipoPessoa>(tipoProp.GetString(), true),
                    JsonValueKind.Number => (TipoPessoa)tipoProp.GetInt32(),
                    _ => throw new JsonException("TipoPessoa inválido.")
                };
            }
            catch
            {
                throw new JsonException("Valor inválido para TipoPessoa.");
            }

            // Decide o tipo de destino
            Type targetType = tipoPessoa switch
            {
                TipoPessoa.Fisica => typeof(PessoaFisicaDTO),
                TipoPessoa.Juridica => typeof(PessoaJuridicaDTO),
                _ => typeof(PessoaDTO)
            };

            // Desserializa novamente como o tipo correto
            var json = root.GetRawText();
            return (PessoaDTO)JsonSerializer.Deserialize(json, targetType, options)!;
        }

        public override void Write(Utf8JsonWriter writer, PessoaDTO value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}
