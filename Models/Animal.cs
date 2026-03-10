namespace RefugioAnimales.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public int Edad { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }

        private static List<Animal> animales = new List<Animal>();

        public Animal(string nombre, string especie, int edad, string estado, string descripcion, string foto)
        {
            Id = animales.Count+1;
            Nombre = nombre;
            Especie = especie;
            Edad = edad;
            Estado = estado;
            Descripcion = descripcion;
            Foto = foto;
        }

        public static void crearAnimales()
        {
            if (animales.Count > 1) //Si la lista ya contiene datos cuando se llama la función, no añade ningún animal más.
                return;
            animales.Add(new Animal("Bruno", "Erizo", 2, "Adoptable", "Le gusta jugar", "erizo.jpg"));
            animales.Add(new Animal("Carlos", "Loro", 3, "Adoptable", "Canta ópera", "loro.jpg"));
            animales.Add(new Animal("Carlota", "Oveja", 1, "En tratamiento", "Siempre a disposición", "oveja.jpg"));
            animales.Add(new Animal("Brunilda", "Oveja", 4, "Adoptado", "Le gusta comer hierba", "oveja2.png"));
            animales.Add(new Animal("Astucia", "Colibrí", 3, "Reservado", "Vuela, va y vuelve", "colibri.jpg"));
        }

        public static List<Animal> getAnimales() { return animales; }
        public static Animal getAnimal(int id) { return animales.First(a => a.Id == id); }
    }
}
