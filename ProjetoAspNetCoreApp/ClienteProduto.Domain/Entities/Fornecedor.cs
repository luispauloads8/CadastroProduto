﻿namespace ClienteProjeto.Domain.Entities;

public class Fornecedor : Entity
{

    //public string Descricao { get; set; }
    //public string CNPJ { get; set; }
    //public string Telefone { get; set; }
    //public string Endereco { get; set; }
    //public string CEP { get; set; }
    public string Observacao { get; set; }
    
    //public string Email { get; set; }
    public Cidade Cidade { get; set; }
    public Pessoa Pessoa { get; set; }

    #region "EF"
    public int CidadeId { get; set; }
    public int PessoaId { get; set; }

    #endregion
}
