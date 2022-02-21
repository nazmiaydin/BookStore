using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public CreateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname && x.BirthDate == Model.BirthDate);
            if (author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut!");

            author = new Entities.Author();
            author.Name = Model.Name;
            author.Surname = Model.Surname;
            author.BirthDate = Model.BirthDate;
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}