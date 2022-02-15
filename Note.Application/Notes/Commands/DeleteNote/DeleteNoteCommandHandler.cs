using System;
using Notes.Application.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler: IRequestHandler<DeleteNoteCommand>
    {
        private readonly INotesDbContext _DbContext;
        public DeleteNoteCommandHandler(INotesDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _DbContext.Notes.FindAsync(new object[] { request.Id}, cancellationToken);
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }
            _DbContext.Notes.Remove(entity);
            await _DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
