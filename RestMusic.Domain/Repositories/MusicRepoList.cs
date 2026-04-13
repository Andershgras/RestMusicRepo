using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestMusic.Domain.Models;

namespace RestMusic.Domain.Repositories
{
    public class MusicRepoList
    {
        private List<MusicRecord> _musicRecords = new List<MusicRecord>();
        private int _nextId = 1;
        public MusicRepoList(bool includeData = false)

        {
            if (includeData)
            {
                Add(new MusicRecord { Id = _nextId++, Title = "Bohemian Rhapsody", Artist = "Queen", DurationInSeconds = 354, publicationYear = 1975 });
                Add(new MusicRecord { Id = _nextId++, Title = "Imagine", Artist = "John Lennon", DurationInSeconds = 183, publicationYear = 1971 });
                Add(new MusicRecord { Id = _nextId++, Title = "Stairway to Heaven", Artist = "Led Zeppelin", DurationInSeconds = 482, publicationYear = 1971 });
            }
        }

        public IEnumerable<MusicRecord> GetAll()
        {
            return _musicRecords;
        }
        public MusicRecord Add(MusicRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }
            record.Id = _nextId++;
            _musicRecords.Add(record);
            return record;
        }

        public IEnumerable<MusicRecord> GetByTitleOgArtist(string? title, string? artist)
        {
            return _musicRecords.Where(r =>
                (string.IsNullOrEmpty(title) || r.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(artist) || r.Artist.Contains(artist, StringComparison.OrdinalIgnoreCase))
            );


        }
    }
}
