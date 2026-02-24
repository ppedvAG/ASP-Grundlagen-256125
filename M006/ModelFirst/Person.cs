using System.ComponentModel.DataAnnotations;

namespace M006.ModelFirst;

public class Person
{
	[Key]
	public int ID { get; set; }

	[StringLength(100)]
	public string FirstName { get; set; }

	[StringLength(100)]
	public string LastName { get; set; }

	[StringLength(100)]
	public string Address { get; set; }

	[StringLength(100)]
	public string City { get; set; }

	[StringLength(100)]
	public string Region { get; set; }

	[StringLength(100)]
	public string PostalCode { get; set; }

	[StringLength(100)]
	public string Country { get; set; }
}
