namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;

public static class SupabaseMapper
{
    public static Postgrest.Constants.Ordering SortOrderToSupabaseFormat(SortOrder domainFormat)
    {
        switch(domainFormat)
        {
            case SortOrder.Ascending:
                return Postgrest.Constants.Ordering.Ascending;

            case SortOrder.Descending:
                return Postgrest.Constants.Ordering.Descending;

            default:
                throw new ArgumentException($"SortOrder {domainFormat.ToString()} is not a valid argument");
        }
    }    
}