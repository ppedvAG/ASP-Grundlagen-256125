using Microsoft.EntityFrameworkCore;

namespace M006.ModelFirst;

public class PersonDBContext : DbContext
{
	public PersonDBContext()
	{
		
	}

	public DbSet<Person> Personen { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PersonDB;Integrated Security=True;Encrypt=False");
	}
}