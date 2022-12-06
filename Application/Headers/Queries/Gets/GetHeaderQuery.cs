using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Headers.Queries.Gets
{
    public record GetHeaderQuery: IRequest<HeaderVm>;

    
    public class GetHeaderQueryHandler: IRequestHandler<GetHeaderQuery, HeaderVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetHeaderQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HeaderVm> Handle(GetHeaderQuery request, CancellationToken cancellationToken)
        {
            return new HeaderVm
            {
                Lists = await _context.Headers
                .AsNoTracking()
                .ProjectTo<HeaderDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Code)                
                .ToListAsync(cancellationToken)
            };
        }
    }
    

}
