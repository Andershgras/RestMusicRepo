using System.ComponentModel.DataAnnotations;

namespace RestMusic.Domain.Models
{
    public class MusicRecord
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Artist is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Artist must be between 1 and 100 characters.")]
        public string Artist { get; set; } = string.Empty;

        [Range(1, 36000, ErrorMessage = "DurationInSeconds must be greater than 0.")]
        public int DurationInSeconds { get; set; }

        [Range(1900, 2100, ErrorMessage = "PublicationYear must be between 1900 and 2100.")]
        public int PublicationYear { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Artist: {Artist}, Duration: {DurationInSeconds} seconds, Publication Year: {PublicationYear}";
        }
    }
}