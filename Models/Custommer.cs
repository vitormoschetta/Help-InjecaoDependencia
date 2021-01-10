namespace Help_InjecaoDependencia.Models
{
    public class Custommer
    {
        public Custommer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}