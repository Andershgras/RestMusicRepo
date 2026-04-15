using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestMusic.Domain.Models;
using RestMusic.Domain.Repositories;

namespace RestMusic.Test
{
    [TestClass]
    public class TestRepoList
    {
        [TestMethod]
        public void Constructor_WithIncludeData_PopulatesThreeRecords()
        {
            // Arrange
            var repo = new MusicRepoList(includeData: true);

            // Assert (section header included as requested; actual assertions follow Action)
            // Action
            var all = repo.GetAll().ToList();

            // Assert
            Assert.AreEqual(3, all.Count);
        }

        [TestMethod]
        public void Add_AssignsIdAndStoresRecord()
        {
            // Arrange
            var repo = new MusicRepoList();
            var record = new MusicRecord { Title = "Test", Artist = "Artist" };

            // Assert (section header included as requested; actual assertions follow Action)
            // Action
            var added = repo.Add(record);

            // Assert
            Assert.IsTrue(added.Id > 0);
            Assert.AreEqual(1, repo.GetAll().Count());
            Assert.AreSame(added, repo.GetAll().First());
        }
        //Filtrering og sortering
        [TestMethod]
        public void GetByTitleOgArtist_FiltersCorrectly()
        {
            // Arrange
            var repo = new MusicRepoList(includeData: true);

            // Action
            var byTitle = repo.GetByTitleOgArtist("Imagine", null).ToList();
            var byArtist = repo.GetByTitleOgArtist(null, "Queen").ToList();
            var all = repo.GetByTitleOgArtist(null, null).ToList();

            // Assert
            Assert.AreEqual(1, byTitle.Count, "Should find one record by title.");
            Assert.AreEqual("Imagine", byTitle[0].Title);

            Assert.AreEqual(1, byArtist.Count, "Should find one record by artist.");
            Assert.AreEqual("Queen", byArtist[0].Artist);

            Assert.AreEqual(3, all.Count, "Null title and artist should return all seeded records.");
        }

        [TestMethod]
        public void GetById_ReturnsCorrectRecord()
        {
            // Arrange
            var repo = new MusicRepoList(includeData: true);
            var existing = repo.GetAll().First();
            // Action
            var found = repo.GetById(existing.Id);
            // Assert
            Assert.IsNotNull(found, "Should find record by ID.");
            Assert.AreEqual(existing.Id, found.Id);
            Assert.AreEqual(existing.Title, found.Title);
            Assert.AreEqual(existing.Artist, found.Artist);
        }
        [TestMethod]
        public void GetById_NonExisting()
        {
            // Arrange
            var repo = new MusicRepoList(includeData: true);
            // Action
            var found = repo.GetById(999);
            // Assert
            Assert.IsNull(found, "Should return null for non-existing ID.");
        }
        [TestMethod]
        public void GetById_InvalidId()
        {
            //Arange
            var repo = new MusicRepoList(includeData: true);
            //Action
            var found = repo.GetById(-1);
            var foundZero = repo.GetById(0);
            //Assert
            Assert.IsNull(found, "Should return null for negative ID.");
            Assert.IsNull(foundZero, "Should return null for zero ID.");
        }

        [TestMethod]
        public void Delete_ExistingId_RemovesAndReturnsRecord()
        {
            // Arrange
            var repo = new MusicRepoList(includeData: true);
            var existing = repo.GetAll().First();

            // Act
            var deleted = repo.Delete(existing.Id);

            // Assert
            Assert.IsNotNull(deleted, "Should return deleted record.");
            Assert.AreEqual(existing.Id, deleted.Id);
            Assert.IsNull(repo.GetById(existing.Id), "Record should no longer exist after deletion.");
        }

        [TestMethod]
        public void Delete_NonExistingId_ReturnsNull()
        {
            // Arrange
            var repo = new MusicRepoList(includeData: true);
            int nonExistingId = 999;

            // Act
            var deleted = repo.Delete(nonExistingId);

            // Assert
            Assert.IsNull(deleted, "Should return null for non-existing ID.");
        }
        [TestMethod]
        public void Delete_Twice_ReturnsNullSecondTime()
        {
            // Arrange
            var repo = new MusicRepoList(includeData: true);
            var existing = repo.GetAll().First();

            // Act
            var firstDelete = repo.Delete(existing.Id);
            var secondDelete = repo.Delete(existing.Id);

            // Assert
            Assert.IsNotNull(firstDelete, "First delete should succeed.");
            Assert.IsNull(secondDelete, "Second delete should return null.");
        }
        [TestMethod]
        public void Update_Should_Return_Null_When_Id_Not_Exists()
        {
            // Arrange
            var repo = new MusicRepoList(true);

            var updated = new MusicRecord
            {
                Title = "Test",
                Artist = "Test",
                DurationInSeconds = 100,
                PublicationYear = 2000
            };

            // Act
            var result = repo.Update(999, updated);

            // Assert
            Assert.IsNull(result, "Should return null for non-existing ID.");
        }
        [TestMethod]
        public void Updating_Should_Update_Existing_Record()
        {
            // Arrange
            var repo = new MusicRepoList(true);
            var existing = repo.GetAll().First();
            var updated = new MusicRecord
            {
                Title = "Updated Title",
                Artist = "Updated Artist",
                DurationInSeconds = 200,
                PublicationYear = 2020
            };
            // Act
            var result = repo.Update(existing.Id, updated);
            // Assert
            Assert.IsNotNull(result, "Should return the updated record.");
            Assert.AreEqual(existing.Id, result.Id, "ID should remain unchanged.");
            Assert.AreEqual(updated.Title, result.Title);
            Assert.AreEqual(updated.Artist, result.Artist);
            Assert.AreEqual(updated.DurationInSeconds, result.DurationInSeconds);
            Assert.AreEqual(updated.PublicationYear, result.PublicationYear);
        }
    }
}
