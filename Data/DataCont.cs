using Microsoft.EntityFrameworkCore;
using Pagingtion.Models;

namespace Pagingtion.Data
{
    public class DataCont:DbContext
    {
        public DataCont(DbContextOptions<DataCont> options) : base(options)
        {

        }

        public DbSet<Stud> studs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectionString = @"Data Source=DESKTOP-BQMKOT4\SQLEXPRESS;Initial Catalog=Pagingtion;encrypt=false;Integrated Security=True";

            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
