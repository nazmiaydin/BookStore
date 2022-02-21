using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorViewModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı!");

            if (_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower() && x.Id != AuthorId))
                throw new InvalidOperationException("Aynı isimli bir yazar zaten mevcut");

            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}