using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;

namespace Anamnese.API.ORM.QueryExtensions
{
    public static class PacientFilterExtensions
    {
        public static IQueryable<PacientModel> ApplyFilters(this IQueryable<PacientModel> query, PacientFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Username))
                query = query.Where(p => p.Username.ToLower().Contains(filter.Username.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Uf))
                query = query.Where(p => p.Uf.ToLower() == filter.Uf.ToLower());

            if (!string.IsNullOrWhiteSpace(filter.Gender))
                query = query.Where(p => p.Gender.ToLower() == filter.Gender.ToLower());

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

            return query;
        }

    }
}
