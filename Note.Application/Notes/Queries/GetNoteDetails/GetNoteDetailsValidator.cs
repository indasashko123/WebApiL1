using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        public GetNoteDetailsValidator()
        {
            RuleFor(noteDetails => noteDetails.Id).NotEqual(Guid.Empty);
            RuleFor(noteDetails => noteDetails.UserId).NotEqual(Guid.Empty);
        }
    }
}
