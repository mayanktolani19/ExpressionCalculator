using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpressionCalculator.Database.Models;

public class ExpressionRecord
{
    [Key]
    public int Id { get; init; }

    [Required]
    [MaxLength(500)]
    public string Expression { get; init; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Result { get; init; }

    [Required]
    public DateTime CreatedAt { get; init; }
}