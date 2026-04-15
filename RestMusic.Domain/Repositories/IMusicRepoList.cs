using RestMusic.Domain.Models;

namespace RestMusic.Domain.Repositories
{
    public interface IMusicRepoList
    {
        MusicRecord Add(MusicRecord record);
        MusicRecord? Delete(int id);
        IEnumerable<MusicRecord> GetAll();
        MusicRecord? GetById(int id);
        IEnumerable<MusicRecord> GetByTitleOgArtist(string? title, string? artist);
        MusicRecord? Update(int id, MusicRecord updatedRecord);
    }
}