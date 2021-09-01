using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book.Data.Services;
using my_book.Data.ViewModels;
using my_book.Exceptions;

namespace my_book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        public PublishersService _publisherService;

        public PublishersController(PublishersService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost("add-Publishers")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name : {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-Publishers")]
        public IActionResult GetAllPublishers()
        {
            var allPublishers = _publisherService.GetAllPublishers();
            return Ok(allPublishers);
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var response = _publisherService.GetPublisherData(id);
            return Ok(response);
        }

        [HttpGet("get-Publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publisherService.GetPublisherById(id);

            if (_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
            //Exceptions : System exceptions.application exceptions
            //common exception type
            //NullRefrence , IndexOutOfRangeException,
            //IOException,Stackoverflow,Outofmwmoryexception,invalidCastException
        }

        [HttpPut("update-Publisher-by-id/{id}")]
        public IActionResult UpdatePublisherById(int id, [FromBody] PublisherVM PublisherVM)
        {
            var updatePublisher = _publisherService.UpdatePublisherById(id, PublisherVM);
            return Ok(updatePublisher);
        }

        [HttpDelete("delete-Publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
