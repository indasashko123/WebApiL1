using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext _DbContext;
        public CreateNoteCommandHandler (INotesDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<Guid> Handle(CreateNoteCommand requers, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = requers.UserId,
                Title = requers.Title,
                Details = requers.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };
            await _DbContext.Notes.AddAsync(note, cancellationToken);
            await _DbContext.SaveChangesAsync(cancellationToken);
            return note.Id;
        }
    }
}
