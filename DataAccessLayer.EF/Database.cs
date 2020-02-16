using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EF
{
	internal class Database : DbContext
	{
		public Database(DbContextOptions options) : base(options)
		{

		}

		public DbSet<User> Users { get; set; }
	}
}
