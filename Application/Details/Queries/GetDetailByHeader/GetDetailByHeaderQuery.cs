using Application.Common.Interfaces;
using Application.Details.Queries.GetDetailWithPagination;
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

namespace Application.Details.Queries.GetDetailByHeader
{
    public record GetDetailByHeaderQuery(int HeaderId) : IRequest<DetailByHeaderVm>;

    public class GetDetailByHeaderQueryHandler: IRequestHandler<GetDetailByHeaderQuery, DetailByHeaderVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDetailByHeaderQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DetailByHeaderVm> Handle(GetDetailByHeaderQuery request, CancellationToken cancellationToken)
        {
            return new DetailByHeaderVm
            {
                Lists = await _context.Details
                .AsNoTracking()
                .ProjectTo<DetailDtoByHeader>(_mapper.ConfigurationProvider)
                .Where(x => x.HeaderId == request.HeaderId)
                .ToListAsync(cancellationToken)
            };                
        }
    }


}
