using System.Data;

namespace JohannesBorg
{
    public static class DbConnectionExtensions
    {
        public static void Query<TEntity>(this IDbConnection me)
        {
            var test = "test";
            using (var command = me.CreateCommand())
            {
                command.CommandText = $"Select * From {test}";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var value = reader["column"];
                        var value2 = reader["column1"];
                    }
                }
            }
        }
    }
}