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
    }
}
