using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Applicatipn.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = default;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("silinicek kitap bulunmadı");
        }

         [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
        {
            var book = new Book() { Title = "Test_Book", PageCount = 100, PublishDate = new System.DateTime(1998, 01, 01), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var result = _context.Books.SingleOrDefault(x=>x.Id == book.Id);
            result.Should().BeNull();
        }
    }
}