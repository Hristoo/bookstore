using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;

namespace BookStode.DL.Interfaces
{
    public interface IBookRepository
    {
        Book? AddBook(Book book);
        Book? DeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id);
        Book UpdateBook(Book book);
    }
}
