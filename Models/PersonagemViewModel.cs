namespace ficha_criacao_personagem_rpg_csharp.Models
{
    public class PersonagemViewModel
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Classe { get; set; }
        public int Nivel { get; set; }
        public int Agilidade { get; set; }
        public int Ataque { get; set; }
        public int Carisma { get; set; }
        public int Defesa { get; set; }
        public int Destreza { get; set; }
        public int Inteligencia { get; set; }
        public int Sorte { get; set; }
        public int Vida { get; set; }
    }
}