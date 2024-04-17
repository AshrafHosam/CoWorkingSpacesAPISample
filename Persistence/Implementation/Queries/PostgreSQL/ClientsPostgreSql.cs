namespace Persistence.Implementation.Queries.PostgreSQL
{
    internal static class ClientsPostgreSql
    {
        public const string DeleteAllClients = @"DELETE FROM public.""Clients"" WHERE ""BrandId"" = @brandId";
    }
}
