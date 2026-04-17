using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestMusic.Domain.Models;
using RestMusic.Domain.Repositories;
using Xunit;

namespace RestMusic.Test
{
    public class MusicRepoDbTests
    {
        private MusicRecordDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<MusicRecordDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MusicRecordDbContext(options);

            context.MusicRecords.AddRange(
                new MusicRecord
                {
                    Title = "Bohemian Rhapsody",
                    Artist = "Queen",
                    DurationInSeconds = 354,
                    PublicationYear = 1975
                },
                new MusicRecord
                {
                    Title = "Imagine",
                    Artist = "John Lennon",
                    DurationInSeconds = 183,
                    PublicationYear = 1971
                },
                new MusicRecord
                {
                    Title = "Stairway to Heaven",
                    Artist = "Led Zeppelin",
                    DurationInSeconds = 482,
                    PublicationYear = 1971
                }
            );

            context.SaveChanges();
            return context;
        }

        [Fact]
        public void GetAll_Returns_All_Records()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            var result = repo.GetAll().ToList();

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void Add_Adds_Record_To_Database()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            var newRecord = new MusicRecord
            {
                Title = "Numb",
                Artist = "Linkin Park",
                DurationInSeconds = 185,
                PublicationYear = 2003
            };

            var added = repo.Add(newRecord);

            Assert.NotNull(added);
            Assert.True(added.Id > 0);
            Assert.Equal(4, context.MusicRecords.Count());
            Assert.Contains(context.MusicRecords, r => r.Title == "Numb" && r.Artist == "Linkin Park");
        }

        [Fact]
        public void Add_Null_Record_Throws_ArgumentNullException()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            Assert.Throws<ArgumentNullException>(() => repo.Add(null!));
        }

        [Fact]
        public void GetByTitleOgArtist_Filters_By_Title_Correctly()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            var result = repo.GetByTitleOgArtist("Imagine", null).ToList();

            Assert.Single(result);
            Assert.Equal("Imagine", result[0].Title);
        }

        [Fact]
        public void GetByTitleOgArtist_Filters_By_Artist_Correctly()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            var result = repo.GetByTitleOgArtist(null, "Queen").ToList();

            Assert.Single(result);
            Assert.Equal("Queen", result[0].Artist);
        }

        [Fact]
        public void GetByTitleOgArtist_With_No_Filters_Returns_All()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            var result = repo.GetByTitleOgArtist(null, null).ToList();

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetById_Existing_Id_Returns_Record()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);
            var existing = context.MusicRecords.First();

            var result = repo.GetById(existing.Id);

            Assert.NotNull(result);
            Assert.Equal(existing.Id, result!.Id);
            Assert.Equal(existing.Title, result.Title);
        }

        [Fact]
        public void GetById_NonExisting_Id_Returns_Null()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            var result = repo.GetById(999);

            Assert.Null(result);
        }

        [Fact]
        public void Delete_Existing_Id_Removes_Record()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);
            var existing = context.MusicRecords.First();

            var result = repo.Delete(existing.Id);

            Assert.NotNull(result);
            Assert.Equal(2, context.MusicRecords.Count());
            Assert.Null(context.MusicRecords.FirstOrDefault(r => r.Id == existing.Id));
        }

        [Fact]
        public void Delete_NonExisting_Id_Returns_Null()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            var result = repo.Delete(999);

            Assert.Null(result);
        }

        [Fact]
        public void Update_Existing_Id_Updates_Record()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);
            var existing = context.MusicRecords.First();

            var updatedRecord = new MusicRecord
            {
                Title = "Updated Title",
                Artist = "Updated Artist",
                DurationInSeconds = 200,
                PublicationYear = 2020
            };

            var result = repo.Update(existing.Id, updatedRecord);

            Assert.NotNull(result);
            Assert.Equal(existing.Id, result!.Id);
            Assert.Equal("Updated Title", result.Title);
            Assert.Equal("Updated Artist", result.Artist);
            Assert.Equal(200, result.DurationInSeconds);
            Assert.Equal(2020, result.PublicationYear);
        }

        [Fact]
        public void Update_NonExisting_Id_Returns_Null()
        {
            using var context = CreateContext();
            var repo = new MusicRepoDb(context);

            var updatedRecord = new MusicRecord
            {
                Title = "Updated Title",
                Artist = "Updated Artist",
                DurationInSeconds = 200,
                PublicationYear = 2020
            };

            var result = repo.Update(999, updatedRecord);

            Assert.Null(result);
        }
    }
}