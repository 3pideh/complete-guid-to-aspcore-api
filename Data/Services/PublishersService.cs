using my_book.Data.Models;
using my_book.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _dbContext;
        public PublishersService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };

            _dbContext.Add(_publisher);
            _dbContext.SaveChanges();

            return _publisher;
        }

        public List<Publisher> GetAllPublishers() => _dbContext.Publishers.ToList();

        public Publisher GetPublisherById(int publisherId) => _dbContext.Publishers.FirstOrDefault(x => x.Id == publisherId);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int PublisherId)
        {
            var _publisherData = _dbContext.Publishers.Where(n => n.Id == PublisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Author_Books.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }

        public Publisher UpdatePublisherById(int publisherId, PublisherVM publisher)
        {
            var _publisher = _dbContext.Publishers.FirstOrDefault(c => c.Id == publisherId);
            if (_publisher != null)
            {
                _publisher.Name = publisher.Name;
                _dbContext.SaveChanges();
            }

            return _publisher;
        }

        public void DeletePublisherById(int publisherId)
        {
            var _publisher = _dbContext.Publishers.FirstOrDefault(c => c.Id == publisherId);

            if (_publisher != null)
            {
                _dbContext.Remove(_publisher);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id {publisherId} does not exist"); 
            }
           
        }
    }
}
