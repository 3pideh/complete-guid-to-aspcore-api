﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Models
{
    public class Author_Book
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        public int AuthorId { get; set; }

        //Navigation property 
        public Author Author { get; set; }
        public Book Book  { get; set; }
    }
}
