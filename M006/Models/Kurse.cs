using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M006.Models;

[Table("Kurse")]
public partial class Kurse
{
    [Key]
    [Column("ID")]
	[Required]
    public int ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
	[Column("Kursname")]
	public string KursName { get; set; }

    public int? DauerInTagen { get; set; }

    public byte? Aktiv { get; set; }

    [InverseProperty("Kurs")]
    public virtual ICollection<KursInhalte> KursInhaltes { get; set; } = new List<KursInhalte>();
}