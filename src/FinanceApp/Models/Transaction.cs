using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models;

public class Transaction
{
    [Key]
    public int id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

}