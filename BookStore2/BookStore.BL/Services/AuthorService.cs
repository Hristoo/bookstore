using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStode.DL.interfaces;
using BookStode.DL.Interfaces;
using BookStode.DL.Repositories.InMemoryRepositories;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        public readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Author? AddAuthor(Author autor)
        {
            _authorRepository.AddAuthor(autor);
            return autor;
        }

        public Author? DeleteAutor(int authorId)
        {
            var author = _authorRepository.GetById(authorId);

            _authorRepository.DeleteAutor(authorId);

            return author;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public Author? GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public Person UpdateAuthor(Author autor)
        {
            return _authorRepository.UpdateAuthor(autor);
        }
    }
}
