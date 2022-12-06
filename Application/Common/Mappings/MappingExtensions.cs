using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
            => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

        public static PaginatedList<TDestination> PaginatedEnumerable<TDestination>(this IEnumerable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
            => PaginatedList<TDestination>.Create(queryable.ToList(), pageNumber, pageSize);

        /*public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
            => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();*/
    }
}
