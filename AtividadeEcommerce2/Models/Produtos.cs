namespace AtividadeEcommerce2.Models
{
   // Produto.cs
{
public class Produto
    {

        //CRIANDO O ENCAPSULAMENTO DO OBJETO COM GET E SET
        public int CodProd { get; set; } //Acessores
                                         // Ao usar ?, você está explicitamente dizendo que a propriedade pode intencionalmente ter um valor nulo.
        public string? Nome { get; set; }
        public int? Quantidade { get; set; }
        public string? Descricao { get; set; }
        public double? Preco { get; set; }

        public List<Produto>? ListaProduto { get; set; }

    }
}