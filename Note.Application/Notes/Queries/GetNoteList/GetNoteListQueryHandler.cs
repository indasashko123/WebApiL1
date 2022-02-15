using MediatR;
using AutoMapper.QueryableExtensions;
using Notes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly INotesDbContext _DbContext;
        private readonly IMapper _Mapper;
        public GetNoteListQueryHandler(INotesDbContext DbContext, IMapper Mapper)
        {
            _DbContext = DbContext;
            _Mapper = Mapper;
        }
        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var entities = await _DbContext.Notes.Where(note => note.Id == request.UserId)
                .ProjectTo<NoteLookupDto>(_Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new NoteListVm { Notes = entities };
        }
    }
}
