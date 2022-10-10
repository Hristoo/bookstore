using AutoMapper;
using BookStode.DL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;
using BookStore2.AutoMapper;
using BookStore2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookStoreTest
{
    public class AuthorTest
    {

        private IList<Author> _authors = new List<Author>()
        {
          new Author()
          {
              Id = 1,
              Age   = 45,
              DateOfBirth = DateTime.Now,
              Name = "Penka",
              NickName = "Peneto"
          },
          new Author()
          {
              Id = 2,
              Age   = 25,
              DateOfBirth = DateTime.Now,
              Name = "Pinka",
              NickName = "Pineto"
          }
        };

        private IList<Author> _books = new List<Author>()
        {
          new Author()
          {
              Title = "title",
              AuthorId = 1,
          },
          new Author()
          {
              Title = "title2",
              AuthorId = 2,
          }
        };

        private readonly IMapper _mapper;
        private Mock<ILogger<AuthorService>> _loggerMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<ILogger<AuthorController>> _loggerAuthorControllerMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;

        public AuthorTest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMappings()));

            _mapper = mockMapperConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<AuthorService>>();
            _loggerAuthorControllerMock = new Mock<ILogger<AuthorController>>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task Author_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            _authorRepositoryMock.Setup(x => x.GetAllAuthors()).ReturnsAsync(_authors);

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //Act
            var result = await controller.GetAllAuthors();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var authors = okObjectResult.Value as IEnumerable<Author>;

            Assert.NotNull(authors);
            Assert.NotEmpty(authors);
            Assert.Equal(expectedCount, authors.Count());
        }

        [Fact]
        public async Task Athor_GetById_Check()
        {
            //setup
            var authorId = 1;
            var expectedAuthor = _authors.First(x => x.Id == authorId);

            _authorRepositoryMock.Setup(x => x.GetById(authorId)).ReturnsAsync(expectedAuthor);

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.GetById(authorId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var author = okObjectResult.Value as Author;
            Assert.NotNull(author);
            Assert.Equal(authorId, author.Id);
        }

        [Fact]
        public async Task Author_GetById_NotFound()
        {
            //setup
            var authorId = 3;

            _authorRepositoryMock.Setup(x => x.GetById(authorId)).ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.GetById(authorId);

            //Assert
            var notFoundObjectResult = result as NotFoundResult;
            Assert.NotNull(notFoundObjectResult);
        }

        [Fact]

        public async Task AddAuthorOk()
        {
            //setup
            var authorRequest = new AddAuthorRequest()
            {
                Name = "Penka",
                NickName = "Peneto",
                Age = 25,
                DateOfBirth = DateTime.Now
            };

            _authorRepositoryMock.Setup(x => x.AddAuthor(It.IsAny<Author>())).Callback(() =>
            {
                _authors.Add(new Author()
                {
                    Id = 3,
                    Age = authorRequest.Age,
                    Name = authorRequest.Name,
                    DateOfBirth = DateTime.Now,
                    NickName = authorRequest.NickName

                });
            }).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == 3));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.AddAuthor(authorRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as AddAuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(3, resultValue.Author.Id);
        }

        [Fact]
        public async Task Author_AddAuthorIfExist()
        {
            //setup
            var authorRequest = new AddAuthorRequest()
            {
                Name = "Penka",
                NickName = "Peni",
                Age = 55,
                DateOfBirth = DateTime.Now
            };

            _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name)).ReturnsAsync(_authors.FirstOrDefault(x => x.Name == authorRequest.Name));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.AddAuthor(authorRequest);

            //Assert
            var badObjectRequest = result as BadRequestObjectResult;
            Assert.NotNull(badObjectRequest);

            var resultValue = badObjectRequest.Value as AddAuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Author already exist", resultValue.Message);
        }

        [Fact]
        public async Task Author_UpdateAuthorOk()
        {
            //setup
            var authorRequest = new UpdateAuthorRequest()
            {
                Name = "Penka",
                NickName = "Peni",
                Age = 55,
                DateOfBirth = DateTime.Now,
                Id = 1
            };

            _authorRepositoryMock.Setup(x => x.UpdateAuthor(It.IsAny<Author>())).Callback(() =>
            {
                var author = _authors.FirstOrDefault(x => x.Id == authorRequest.Id);

                author.Name = authorRequest.Name;
                author.Age = authorRequest.Age;
                author.DateOfBirth = authorRequest.DateOfBirth;
                author.NickName = authorRequest.NickName;

                _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name)).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Name == authorRequest.Name));

            }).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == authorRequest.Id));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.UpdateAuthor(authorRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as UpdateAuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Author updated", resultValue.Message);
        }

        [Fact]
        public async Task Author_UpdateAuthor_NotExist()
        {
            //setup
            var authorRequest = new UpdateAuthorRequest()
            {
                Name = "Penka",
                NickName = "Peni",
                Age = 55,
                DateOfBirth = DateTime.Now,
                Id = 1
            };

            _authorRepositoryMock.Setup(x => x.UpdateAuthor(It.IsAny<Author>())).Callback(() =>
            {
                var author = _authors.FirstOrDefault(x => x.Id == authorRequest.Id);

                author.Name = authorRequest.Name;
                author.Age = authorRequest.Age;
                author.DateOfBirth = authorRequest.DateOfBirth;
                author.NickName = authorRequest.NickName;

                _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name)).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Name == authorRequest.Name));

            }).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == authorRequest.Id));

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.UpdateAuthor(authorRequest);

            //Assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);

            var resultValue = badRequestObjectResult.Value as UpdateAuthorResponse;
            Assert.Null(resultValue.Author);
            Assert.Equal("Author not exist", resultValue.Message);
        }


        [Fact]
        public async Task Author_DeleteAuthor_Ok()
        {
            //setup
            var authorRequest = new UpdateAuthorRequest()
            {
                Name = "Penka",
                NickName = "Peni",
                Age = 45,
                DateOfBirth = DateTime.Now,
                Id = 1
            };

            var authorsCount = _authors.Count();

                var author = _authors.FirstOrDefault(x => x.Id == authorRequest.Id);

            _authorRepositoryMock.Setup(x => x.GetById(authorRequest.Id)).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == authorRequest.Id));
            _authorRepositoryMock.Setup(x => x.DeleteAutor(authorRequest.Id)).Callback(() =>
            {

                _authors.Remove(author);

            }).ReturnsAsync(() => author);

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.DeleteAuthor(authorRequest.Id);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as Author;
            Assert.NotNull(resultValue);
            Assert.Equal(authorsCount - 1, 1);
        }

        [Fact]
        public async Task Author_DeleteAuthor_NotExist()
        {
            //setup
            var authorRequest = new UpdateAuthorRequest()
            {
                Name = "Panka",
                NickName = "Peni",
                Age = 45,
                DateOfBirth = DateTime.Now,
                Id = 3
            };

            var authorsCount = _authors.Count();
            var author = _authors.FirstOrDefault(x => x.Id == authorRequest.Id);

            _authorRepositoryMock.Setup(x => x.GetById(authorRequest.Id)).ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == authorRequest.Id));
            _authorRepositoryMock.Setup(x => x.DeleteAutor(authorRequest.Id)).Callback(() =>
            {
                _authors.Remove(author);

            }).ReturnsAsync(() => author);

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.DeleteAuthor(authorRequest.Id);

            //Assert
            var badObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badObjectResult);

            var resultValue = badObjectResult.Value as Author;
            Assert.NotNull(resultValue);
            Assert.Equal(authorsCount , 2);
        }

        [Fact]
        public async Task Author_DeleteAuthor_IfHaveBook()
        {
            //setup
            var authorRequest = new UpdateAuthorRequest()
            {
                Name = "Panka",
                NickName = "Peni",
                Age = 45,
                DateOfBirth = DateTime.Now,
                Id = 1
            };

            var authorsCount = _authors.Count();
            var author = _authors.FirstOrDefault(x => x.Id == authorRequest.Id);

            _bookRepositoryMock.Setup(x => x.GetBookByAuthorId(author.Id)).ReturnsAsync(() => _books.FirstOrDefault(x => x.AuthorId == authorRequest.Id));
            _authorRepositoryMock.Setup(x => x.DeleteAutor(authorRequest.Id)).Callback(() =>
            {
                _authors.Remove(author);

            }).ReturnsAsync(() => author);

            //inject
            var service = new AuthorService(_authorRepositoryMock.Object, _mapper, _loggerMock.Object, _bookRepositoryMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service);

            //act
            var result = await controller.DeleteAuthor(authorRequest.Id);

            //Assert
            var badObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badObjectResult);

            var resultValue = badObjectResult.Value as Author;
            Assert.NotNull(resultValue);
            Assert.Equal(authorsCount, 2);
        }

    }
}