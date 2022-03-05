using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı!");

            if (_context.Books.Any(x => x.AuthorId == AuthorId))
                throw new InvalidOperationException("Kitabı yayında olan yazarı silemezsiniz! Önce kitabı siliniz!");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}