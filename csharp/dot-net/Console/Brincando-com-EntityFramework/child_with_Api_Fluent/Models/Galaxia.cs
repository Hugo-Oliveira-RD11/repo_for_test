using System;

namespace child_with_Api_Fluent.Models
{
    public enum tamanho
    {
        Pequena,
        Media,
        Grande,
        Gigante
    }
    public class Galaxia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public tamanho tamanho{ get; set; }
        public Planeta QuantidadeP { get; set; }
        public SupersBuracosNegros QuantidadeSBN { get; set; }
    }
}