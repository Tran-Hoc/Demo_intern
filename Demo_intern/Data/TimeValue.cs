using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_intern.Data
{
    [Table("TimeValue")]
    public class TimeValue
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name ="Time")]
        [DataType(DataType.Time)]
        public DateTime? Time { get; set; }

        [Display(Name = "Value")]
        [StringLength(200, ErrorMessage = "The value cannot exceed 200 characters. ")]
        public string? Value { get; set; }
    }
}
