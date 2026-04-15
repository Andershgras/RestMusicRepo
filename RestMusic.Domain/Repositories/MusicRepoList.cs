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
                Add(new MusicRecord { Title = "Bohemian Rhapsody", Artist = "Queen", DurationInSeconds = 354, PublicationYear = 1975 });
                Add(new MusicRecord { Title = "Imagine", Artist = "John Lennon", DurationInSeconds = 183, PublicationYear = 1971 });
                Add(new MusicRecord { Title = "Stairway to Heaven", Artist = "Led Zeppelin", DurationInSeconds = 482, PublicationYear = 1971 });
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

        public MusicRecord? GetById(int id)
        {
            return _musicRecords.FirstOrDefault(r => r.Id == id);

        }
    }
}
