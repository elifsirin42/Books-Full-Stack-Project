﻿using AutoMapper;
using Books.API.Filters;
using Books.Business;
using Books.Business.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Business.Extensions;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private IBookService service;
        private IMapper mapper;
        public BooksController(IBookService bookService)
        {
            service = bookService;
        }

        [HttpGet("GetAllBook")]
        [AllowAnonymous]
        public IActionResult Get()
        {

            //var result = service.GetAllBook().Where(x => x.Authors.Any(y => y.Author.Name == "Ayşe"));
            var result = service.GetAllBook();
            //var result = service.GetAllBook().Where(x => x.Authors.Contains(x);
            //return Ok(result.ToList());
            return Ok(new
            {
               bookListResponse = result.ToList(),
                //value = result.Where(x => x.Authors.Any(y => y.BookId==2)).ToList()
                //value = result.Where(x => x.Authors.Any(y => y.Author.Id==1)).ToList()
            });
            //var result = service.GetAllBook();
            //return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            var bookListResponse = service.GetBookById(id);
            
            if (bookListResponse != null)
            {
                return Ok(new
                {
                    bookListResponse = bookListResponse,
                  //  value = bookListResponse.Authors
                });
                // return Ok(bookListResponse);
            }
            return NotFound();
        }

        [HttpGet("GetBookByPublisherName/{publisherName}")]
        [AllowAnonymous]
        public IActionResult GetBookByPublisherName(string publisherName)
        {

            var booksdto = service.GetBookByPublisherName(publisherName);
            if (booksdto != null)
            {
                return Ok(booksdto);
                // return Ok(bookListResponse);
            }
            return NotFound();
            
        }
        [HttpGet("GetBookByAuthorName/{authorName}")]
        [AllowAnonymous]
        public IActionResult GetBookByAuthorName(string authorName)
        {
            var booksdto = service.GetBookByAuthorName(authorName);
            if (booksdto != null)
            {
                return Ok(booksdto);
            }
            return NotFound();

        }
        [HttpGet("GetBookByBookTitle/{bookTitle}")]
        [AllowAnonymous]
        public IActionResult GetBookByBookTitle(string bookTitle)
        {

            var booksdto = service.GetBookByBookTitle(bookTitle);
            if (booksdto != null)
            {
                return Ok(booksdto);
                // return Ok(bookListResponse);
            }
            return NotFound();

        }

        //Add value proccess
        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult AddPublisher(AddNewBookRequest request)
        {
            if (ModelState.IsValid)
            {
                int publishereId = service.AddBook(request);
                return CreatedAtAction(nameof(GetById), routeValues: new { id = publishereId }, value: null);
            }

            return BadRequest(ModelState);
        }

        //Update value proccess
        [HttpPut("{id}")]
        [BookExists]
        public IActionResult UpdateBook(int id, EditBookRequest request)
        {
            
            if (ModelState.IsValid)
            {
                int newItemId = service.UpdateBook(request);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //Delete value proccess
        [HttpDelete("{id}")]
        [BookExists] 
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Delete(int Id)
        {
            service.DeleteBook(Id);
            return Ok();

        }
    }
}

