using Microsoft.EntityFrameworkCore;
using System;

namespace ASUtestTask.Models
{
    public interface IContext : IDisposable
    {
        DbSet<Person> persons { get; }

        DbSet<Skill> skills { get; }

        int SaveChanges();
    }
}