using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator ()
        {
            RuleFor(deleteCommand => deleteCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteCommand => deleteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
