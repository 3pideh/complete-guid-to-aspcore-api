﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Models
{
    public class Publisher
    {
        public Publisher()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        //Navigation Property
        public List<Book> Books { get; set; }

    }
}
