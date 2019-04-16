using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ASUtestTask.Models;

namespace ASUtestTask.Controllers
{
    public class HomeController : Controller
    {
        private IPersonController personController = new PersonManagerController(new PersonContext());

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/v1/persons")]
        public List<Person> getPersonsInfo()
        {
            return personController.getPersonsInfo();
        }

        [HttpGet]
        [Route("api/v1/person/{id}")]
        public Person getPersonInfo(int id)
        {
            return personController.getPersonInfo(id);
        }

        [HttpDelete]
        [Route("api/v1/person/{id}")]
        public ActionResult removePerson(int id)
        {

            return personController.removePerson(id);
        }

        [HttpPut]
        [Route("api/v1/person")]
        public ActionResult addPerson(Person Person)
        {
            return personController.addPerson(Person);        
        }

        [HttpPost]
        [Route("api/v1/person/")]
        public ActionResult editPerson(Person Person)
        {
            return personController.editPerson(Person);
        }

    }
}
