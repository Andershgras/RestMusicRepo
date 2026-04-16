using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestMusic.Domain.Models
{
    public class MusicRecordDbContext : DbContext
    {
        public MusicRecordDbContext(DbContextOptions<MusicRecordDbContext> options) : base(options)
        {
        }
        public DbSet<MusicRecord> MusicRecords { get; set; }
    }
}
