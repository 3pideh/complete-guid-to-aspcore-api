using my_book.Data.Models;
using my_book.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
    public class BookService
    {
        private AppDbContext _dbContext;
        public BookService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                DateAdded = System.DateTime.Now,
                Description = book.Description,
                IsRead = book.IsRead,
                Genre = book.Genre,
                DateRead = book.IsRead ? book.DateRead : null,
                CoverUrl = book.CoverUrl,
                Rate = book.Rate.Value,
                PublisherId = book.PublisherId
            };


            _dbContext.Add(_book);
            _dbContext.SaveChanges();

            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Author_Book()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _dbContext.Add(_book_author);
                _dbContext.SaveChanges();
            }
        }

        public List<Book> GetAllBooks() => _dbContext.Books.ToList();

       // public Book GetBookById(int bookId) => _dbContext.Books.FirstOrDefault(x => x.Id == bookId);

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _dbContext.Books.Where(n => n.Id == bookId)
                .Select(book => new BookWithAuthorsVM()
                {
                    Title = book.Title,
                    DateAdded = System.DateTime.Now,
                    Description = book.Description,
                    IsRead = book.IsRead,
                    Genre = book.Genre,
                    DateRead = book.IsRead ? book.DateRead : null,
                    CoverUrl = book.CoverUrl,
                    Rate = book.Rate.Value,
                    PublisherName = book.Publisher.Name,
                    AuthorNames = book.Author_Books.Select(m => m.Author.FullName).ToList()
                }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _dbContext.Books.FirstOrDefault(c => c.Id == bookId);
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.DateAdded = System.DateTime.Now;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.Genre = book.Genre;
                _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.CoverUrl = book.CoverUrl;
                _book.Rate = book.Rate.Value;
                _dbContext.SaveChanges();
            }

            return _book;
        }

        public void DeleteBookById(int bookId)
        {
            var _book = _dbContext.Books.FirstOrDefault(c => c.Id == bookId);
            _dbContext.Remove(_book);
            _dbContext.SaveChanges();
        }

    }
}
