using api_music.DTOs;

namespace api_music.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Pager<T>(this IQueryable<T> queryable, PageDTO pageDTO)
        {
            int skip = (pageDTO.Page - 1) * pageDTO.CantRegPerPage;
            int take = pageDTO.CantRegPerPage;

            return queryable.Skip(skip).Take(take);
        }
    }
}
