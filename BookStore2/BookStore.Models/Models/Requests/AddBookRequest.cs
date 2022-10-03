using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models.Requests
{
    public class AddBookRequest
    {
        public string Title { get; set; }

        public int AuthorId { get; set; }
    }
}
