using BookStode.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStode.DL.Repositories.InMemoryRepositories
{
    public class AuthorRepository
    {

        private static List<Author> _authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Ivan Vazov",
                Age = 95,
                DateOfBirth = DateTime.Now,
            },
            new Author()
            {
                Id = 1,
                Name = "Elin Pelin",
                Age = 65,
                DateOfBirth = DateTime.Now,
            }
        };


        public IEnumerable<Author> GetAllAuthors()
        {
            return _authors;
        }

        public Author? GetById(int id)
        {
            return _authors.FirstOrDefault(x => x.Id == id);
        }

        public Author? AddAuthor(Author author)
        {
            try
            {
                _authors.Add(author);

            }
            catch (Exception e)
            {
                return null;
            }
            return author;
        }

        public Author UpdateAuthor(Author autor)
        {
            var existingUser = _authors.FirstOrDefault(x => x.Id == autor.Id);

            if (existingUser == null) return null;

            _authors.Remove(existingUser);

            _authors.Add(autor);

            return autor;
        }

        public Author? DeleteAutor(int authorId)
        {
            var author = _authors.FirstOrDefault(x => x.Id == authorId);

            _authors.Remove(author);
             
            return author;
        }

        public Author? GetAuthorByName(string name)
        {
           return _authors.FirstOrDefault(x => x.Name == name);
        }

        public Author GetAuthorByName(Author autor)
        {
            throw new NotImplementedException();
        }

       
    }
}
