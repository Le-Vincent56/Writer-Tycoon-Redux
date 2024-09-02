namespace WriterTycoon.WorkCreation.Genres
{
    public abstract class GenreFactory
    {
        public abstract Genre CreateGenre(GenreType genreType, bool unlocked);
    }

    public class StandardGenreFactory : GenreFactory
    {
        public override Genre CreateGenre(GenreType genreType, bool unlocked) => new Genre(genreType, unlocked);
    }
}
