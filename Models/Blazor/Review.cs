using NPoco;
using System.ComponentModel.DataAnnotations;

namespace nackademin24_umbraco.Models.Blazor
{
    [TableName("Reviews")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class Review
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Please select a rating")]
        public int Rating { get; set; }

        public string ImdbId { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}