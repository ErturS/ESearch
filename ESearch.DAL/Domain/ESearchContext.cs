using ESearch.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.DAL.Domain
{
    public class ESearchContext:DbContext
    {
        public ESearchContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }
       
        public DbSet<QueryResult> QueryResults { get; set; }
        
        public static ESearchContext Create()
        {
            return new ESearchContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
