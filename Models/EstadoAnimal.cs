namespace RefugioAnimales.Models
{
    public class EstadoAnimal
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public ICollection<Animal>? Animales { get; set; }
    }
}
