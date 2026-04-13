using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestMusic.Domain.Models
{
    public class MusicRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public string Artist { get; set; }= string.Empty;
        public int DurationInSeconds { get; set; } = 0;

        public int publicationYear { get; set; } = 0;

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Artist: {Artist}, Duration: {DurationInSeconds} seconds, Publication Year: {publicationYear}";
        }
    }
}
