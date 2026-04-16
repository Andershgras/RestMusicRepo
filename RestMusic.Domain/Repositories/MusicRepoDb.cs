using System;
using System.Collections.Generic;
using RestMusic.Domain.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestMusic.Domain.Repositories
{
    public class MusicRepoDb : IMusicRepoList
    {
        private readonly MusicRecordDbContext _context;

        public MusicRepoDb(MusicRecordDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MusicRecord> GetAll()
        {
            return _context.MusicRecords.ToList();
        }
        public MusicRecord Add(MusicRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }
            _context.MusicRecords.Add(record);
            _context.SaveChanges();
            return record;
        }
        public IEnumerable<MusicRecord> GetByTitleOgArtist(string? title, string? artist)
        {
            IQueryable<MusicRecord> query = _context.MusicRecords;

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(r => r.Title != null && r.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(artist))
            {
                query = query.Where(r => r.Artist != null && r.Artist.Contains(artist));
            }

            return query.ToList();
        }
        public MusicRecord? GetById(int id)
        {
            return _context.MusicRecords.FirstOrDefault(r => r.Id == id);
        }
        public MusicRecord? Delete(int id)
        {
            var record = GetById(id);
            if (record != null)
            {
                _context.MusicRecords.Remove(record);
                _context.SaveChanges();
                return record;
            }
            return null;
        }
        public MusicRecord? Update(int id, MusicRecord updatedRecord)
        {
            var existingRecord = GetById(id);
            if (existingRecord != null)
            {   
                existingRecord.Title = updatedRecord.Title;
                existingRecord.Artist = updatedRecord.Artist;
                existingRecord.DurationInSeconds = updatedRecord.DurationInSeconds;
                existingRecord.PublicationYear = updatedRecord.PublicationYear;
                _context.SaveChanges();
                return existingRecord;
            }
            return null;
        }

    }
}
