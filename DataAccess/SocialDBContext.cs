using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SocialDBContext : DbContext
    {
 
        public SocialDBContext(DbContextOptions opt): base(opt)
        {
            
        }

        public DbSet<ToDoItem> ToDoItem { get; set; }
    }
}
