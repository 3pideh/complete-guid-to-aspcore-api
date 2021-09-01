using my_book.Data.Models;
using my_book.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _dbContext;
        public AuthorsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };

            _dbContext.Add(_author);
            _dbContext.SaveChanges();
        }

        public List<Author> GetAllAuthors() => _dbContext.Authors.ToList();

        public Author GetAuthorById(int authorId) => _dbContext.Authors.FirstOrDefault(x => x.Id == authorId);

        public AuthorWithBooksVM GetAuthorWithBooks (int authorId)
        {
            var _author = _dbContext.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTites = n.Author_Books.Select(c => c.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }

        public Author UpdateAuthorById(int authorId, AuthorVM author)
        {
            var _author = _dbContext.Authors.FirstOrDefault(c => c.Id == authorId);
            if (_author != null)
            {
                _author.FullName = author.FullName;
                _dbContext.SaveChanges();
            }

            return _author;
        }

        public void DeleteAuthorById(int authorId)
        {
            var _author = _dbContext.Authors.FirstOrDefault(c => c.Id == authorId);
            _dbContext.Remove(_author);
            _dbContext.SaveChanges();
        }

    }
}
