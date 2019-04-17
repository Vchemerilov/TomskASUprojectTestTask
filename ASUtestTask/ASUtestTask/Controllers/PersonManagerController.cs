using ASUtestTask.Manager;
using ASUtestTask.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace ASUtestTask.Controllers
{
    public class PersonManagerController : Controller, IPersonController
    {
        private IManager personManager = new PersonManager(new PersonContext());
        private IContext db;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PersonManagerController(IContext PersonContext)
        {
            db = PersonContext;
        }

        public List<Person> getPersonsInfo()
        {
            try
            {
                return personManager.getPersonsInfo();
            }
            catch (Exception ex) {
                logger.Error(ex.Message);
                return null;
            }
        }

        public Person getPersonInfo(long id)
        {
            try
            {
                Person person = db.persons.FirstOrDefault(e => e.id == id);

                if (person == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                return personManager.getPersonInfo(person);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);             
                return null;
            }
        }

        public ActionResult removePerson(long id)
        {
            try
            {
                Person person = db.persons.FirstOrDefault(e => e.id == id);

                if (person == null)
                {
                    return NotFound();
                }

                personManager.removePerson(person);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return new EmptyResult();
            }
        }

        public ActionResult addPerson(Person Person)
        {
            try
            {
                if ((Person.name == null || Person.displayName == null || Person.skills == null) ||
                    (Person.name.Trim() == "" || Person.displayName.Trim() == ""))
                {
                    return BadRequest();
                }

                foreach (var skill in Person.skills)
                {
                    if ((skill.name == null) || (skill.name.Trim() == "") || !((skill.level > 0) && (skill.level < 11)))
                    {
                        return BadRequest();
                    }
                }
                personManager.addPerson(Person);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return new EmptyResult();
            }
        }

        public ActionResult editPerson(Person Person)
        {
          try
          { 
                Person person = db.persons.FirstOrDefault(e => e.id == Person.id);

                if (person == null)
                {
                    return NotFound();
                }

                if ((Person.name == null || Person.displayName == null || Person.skills == null) ||
                    (Person.name.Trim() == "" || Person.displayName.Trim() == ""))
                {
                    return BadRequest();
                }

                foreach (var skill in Person.skills)
                {
                    if ((skill.name == null) || (skill.name.Trim() == "") || !((skill.level > 0) && (skill.level < 11)))
                    {
                        return BadRequest();
                    }
                }
                personManager.editPerson(Person);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return new EmptyResult();
            }
        }
    }
}
