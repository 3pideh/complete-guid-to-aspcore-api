﻿using my_book.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.ViewModels
{
    public class AuthorVM
    {
        public string FullName { get; set; }
    }

    public class AuthorWithBooksVM
    {
        public string FullName { get; set; }

        public List<string> BookTites { get; set; } 
    }


}
