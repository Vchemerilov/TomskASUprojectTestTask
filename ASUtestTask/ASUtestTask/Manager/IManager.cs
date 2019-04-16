using ASUtestTask.Models;
using System.Collections.Generic;

namespace ASUtestTask.Manager
{
    interface IManager
    {
        List<Person> getPersonsInfo();

        Person getPersonInfo(Person person);

        void removePerson(Person person);

        void addPerson(Person Person);

        void editPerson(Person Person);
    }
}
