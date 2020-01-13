using ErdosNumber.Business.Models;
using ErdosNumber.Business.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace ErdosNumber.Test
{
    public class ErdosNumberBusinessServiceTest
    {
        private IErdosNumberBusinessService _iErdosNumberBusinessService;
        private List<Author> _authors;
        private List<Book> _books;
        private List<BookAuthor> _bookAuthors;

        public ErdosNumberBusinessServiceTest()
        {
            _authors = new List<Author> {
                new Author
                {
                    AuthorId = 2,
                    Name = "SomeAuthor"
                }};
            _books = new List<Book>();
            _bookAuthors = new List<BookAuthor>();
        }

        [Fact]
        public void SingleHop()
        {
            _bookAuthors.Add(
                new BookAuthor
                {
                    AuthorId = 2,
                    BookId = 1
                });

            _bookAuthors.Add(
                new BookAuthor
                {
                    AuthorId = 1,
                    BookId = 1
                });
            _iErdosNumberBusinessService = new ErdosNumberBusinessService(_authors, _books, _bookAuthors);
            var hops = _iErdosNumberBusinessService.Hops("SomeAuthor");

            Assert.Equal(1, hops);
        }

        [Fact]
        public void TwoHop()
        {
            _bookAuthors.AddRange(
                new List<BookAuthor>
                {
                    new BookAuthor
                    {
                        AuthorId = 2,
                        BookId = 1
                    },
                    new BookAuthor
                    {
                        AuthorId = 3,
                        BookId = 1
                    },
                    new BookAuthor
                    {
                        AuthorId = 3,
                        BookId = 2
                    },
                    new BookAuthor
                    {
                        AuthorId = 1,
                        BookId = 2
                    }
                });

            _iErdosNumberBusinessService = new ErdosNumberBusinessService(_authors, _books, _bookAuthors);
            var hops = _iErdosNumberBusinessService.Hops("SomeAuthor");

            Assert.Equal(2, hops);
        }

        [Fact]
        public void TwoandThreeHop()
        {
            _bookAuthors.AddRange(
                new List<BookAuthor>
                {
                    new BookAuthor
                    {
                        AuthorId = 2,
                        BookId = 1
                    },
                    new BookAuthor
                    {
                        AuthorId = 3,
                        BookId = 1
                    },
                    new BookAuthor
                    {
                        AuthorId = 4,
                        BookId = 1
                    },
                    new BookAuthor
                    {
                        AuthorId = 3,
                        BookId = 2
                    },
                    new BookAuthor
                    {
                        AuthorId = 1,
                        BookId = 2
                    },
                    new BookAuthor
                    {
                        AuthorId = 3,
                        BookId = 3
                    },
                    new BookAuthor
                    {
                        AuthorId = 4,
                        BookId = 3
                    }
                });

            _iErdosNumberBusinessService = new ErdosNumberBusinessService(_authors, _books, _bookAuthors);
            var hops = _iErdosNumberBusinessService.Hops("SomeAuthor");

            Assert.Equal(2, hops);
        }
    }
}
