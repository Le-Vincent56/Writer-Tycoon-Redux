namespace WriterTycoon.WorkCreation.Genres
{
    public abstract class GenreFactory
    {
        public abstract Genre CreateGenre(string name, bool unlocked);
    }

    public class StandardGenreFactory : GenreFactory
    {
        public override Genre CreateGenre(string name, bool unlocked) => new Genre(name, unlocked);
    }
}
