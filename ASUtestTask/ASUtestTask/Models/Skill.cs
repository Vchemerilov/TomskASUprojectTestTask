
namespace ASUtestTask.Models
{
    public class Skill
    {
        public long id { get; set; }

        public string name { get; set; }

        public byte level { get; set; }

        public long PersonId { get; set; }

        public Person Person;
    }
}
