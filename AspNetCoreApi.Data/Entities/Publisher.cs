﻿using AspNetCoreApi.Models;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Entities
{
    public partial class Publisher : BaseEntity
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}