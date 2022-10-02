using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStode.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Interfaces 
{
    public interface IAuthorService : IAuthorRepository
    {

        public Author? AddAuthor(Author autor);

        public Author? DeleteAutor(int authorId);

        public IEnumerable<Author> GetAllAuthors();

        public Author? GetById(int id);

        public Person UpdateAuthor(Author autor);
    }
}
