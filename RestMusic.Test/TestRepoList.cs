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
    }
}
