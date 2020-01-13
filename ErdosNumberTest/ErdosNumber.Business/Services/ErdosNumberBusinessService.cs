using ErdosNumber.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace ErdosNumber.Business.Services
{
    public class ErdosNumberBusinessService : IErdosNumberBusinessService
    {
        private const int _erdosAuthorId = 1;

        private readonly List<Author> _authors;
        private readonly List<Book> _books;
        private readonly List<BookAuthor> _bookAuthors;

        public ErdosNumberBusinessService(List<Author> authors, List<Book> books, List<BookAuthor> bookAuthors)
        {
            _authors = authors;
            _books = books;
            _bookAuthors = bookAuthors;
        }

        public int Hops(string authorName)
        {
            int hops = 0;
            var author = _authors.First(a => a.Name == authorName);
            var bookIds = _bookAuthors.Where(a => a.AuthorId == author.AuthorId).Select(a => a.BookId);
            var coAuthorIds = _bookAuthors.Where(a => bookIds.Contains(a.BookId) && a.AuthorId != author.AuthorId).Select(a => a.AuthorId).ToList();

            while (!ErdosIsFound(coAuthorIds, ref hops));

            return hops;
        }

        private bool ErdosIsFound(List<int> coAuthorIds, ref int hops)
        {
            hops++;
            if (coAuthorIds.Any(a => a == _erdosAuthorId))
            {
                return true;
            }
            else
            {
                var bookIds = _bookAuthors.Where(a => coAuthorIds.Contains(a.AuthorId)).Select(a => a.BookId);
                coAuthorIds.AddRange(_bookAuthors.Where(a => bookIds.Contains(a.BookId) && !coAuthorIds.Contains(a.AuthorId)).Select(a => a.AuthorId));
                return ErdosIsFound(coAuthorIds, ref hops);
            }
        }
    }
}
