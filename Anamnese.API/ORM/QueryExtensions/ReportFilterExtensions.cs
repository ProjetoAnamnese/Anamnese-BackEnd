using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Filters;

namespace Anamnese.API.ORM.QueryExtensions
{
    public static class ReportFilterExtensions
    {
        public static IQueryable<ReportModel> ApplyFilters(this IQueryable<ReportModel> query, ReportFilter filters)
        {
            if (filters.PacientId.HasValue && filters.PacientId.Value > 0)
                query = query.Where(r => r.PacientId == filters.PacientId.Value);

            if (filters.CardiovascularIssues.HasValue)
                query = query.Where(r => r.CardiovascularIssues == filters.CardiovascularIssues.Value);

            if (filters.Smoker.HasValue)
                query = query.Where(r => r.Smoker == filters.Smoker.Value);

            if (filters.Diabates.HasValue)
                query = query.Where(r => r.Diabetes == filters.Diabates.Value);

            return query;
        }
    }
}
