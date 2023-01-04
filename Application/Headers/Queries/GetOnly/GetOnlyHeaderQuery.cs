using Application.Common.Interfaces;
using Application.Headers.Queries.Gets;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Queries.GetOnly
{
    public record GetOnlyHeaderQuery: IRequest<List<HeaderOnlyDto>>;

    public class GetOnlyHeaderQueryHandler : IRequestHandler<GetOnlyHeaderQuery, List<HeaderOnlyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOnlyHeaderQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HeaderOnlyDto>> Handle(GetOnlyHeaderQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.Headers
                .AsNoTracking()
                .ProjectTo<HeaderOnlyDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Code)
                .ToListAsync(cancellationToken);

            return response;
        }
    }

}
