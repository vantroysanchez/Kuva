using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Details.Queries.GetDetailWithPagination
{
    public record GetDetailWithPaginationQuery: IRequest<PaginatedList<DetailDtoWithPagination>>
    {
        //public int ListId { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetDetailWithPaginationQueryHandler : IRequestHandler<GetDetailWithPaginationQuery, PaginatedList<DetailDtoWithPagination>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDetailWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DetailDtoWithPagination>> Handle(GetDetailWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Details
                .OrderBy(x => x.Id & x.HeaderId)
                .ProjectTo<DetailDtoWithPagination>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
