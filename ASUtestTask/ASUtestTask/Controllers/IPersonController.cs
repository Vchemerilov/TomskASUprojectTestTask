using ASUtestTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASUtestTask.Controllers
{
    interface IPersonController
    {
        List<Person> getPersonsInfo();

        Person getPersonInfo(int id);

        ActionResult removePerson(int id);

        ActionResult addPerson(Person Person);

        ActionResult editPerson(Person Person);
    }
}
