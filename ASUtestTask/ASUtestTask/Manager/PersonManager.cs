using ASUtestTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASUtestTask.Manager
{
    public class PersonManager : IManager
    {
        private IContext db;

        public PersonManager(IContext PersonContext) {
            db = PersonContext;
        }

        public List<Person> getPersonsInfo()
        {
            return db.persons.Include(e => e.skills).ToList();
        }

        public Person getPersonInfo(Person person)
        {
            return db.persons.Include(e => e.skills).FirstOrDefault(t => t.id == person.id);
        }

        public void removePerson(Person person)
        {
            db.persons.Remove(person);
            db.SaveChanges();

            List<Skill> skills = db.skills.Where(e => e.PersonId == person.id).ToList();

            if (skills != null)
            {
                db.skills.RemoveRange(skills);
                db.SaveChanges();
            }

        }

        public void addPerson(Person Person)
        {
            Person person = new Person { name = Person.name, displayName = Person.displayName };
              
            db.persons.Add(person);
            db.SaveChanges();

            foreach (var element in Person.skills)
            {
                db.skills.Add(new Skill { name = element.name, level = element.level, Person = person, PersonId = person.id });
            }

            db.SaveChanges();

        }

        public void editPerson(Person Person)
        {
            Person updatePerson = db.persons.FirstOrDefault(e => e.id == Person.id);

            if (updatePerson != null)
            {
                if (updatePerson.name != Person.name)
                {
                    updatePerson.name = Person.name;
                    db.SaveChanges();
                }

                if (updatePerson.displayName != Person.displayName)
                {
                    updatePerson.displayName = Person.displayName;
                    db.SaveChanges();
                }

                var editPersonSkills = db.skills.Where(t => t.PersonId == updatePerson.id).ToList();

                foreach (Skill skill in Person.skills)
                {
                    Skill updateSkill = editPersonSkills.FirstOrDefault(e => e.id == skill.id);

                    if (updateSkill.name == skill.name && updateSkill.level != skill.level)
                    {
                        updateSkill.level = skill.level;
                        db.SaveChanges();
                    }

                    if (updateSkill.name != skill.name)
                    {
                        db.skills.Add(new Skill { name = skill.name, level = skill.level, Person = Person, PersonId = Person.id });
                        db.SaveChanges();
                    }
                  
                }
            }           
        }
    }
}
