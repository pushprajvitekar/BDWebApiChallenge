using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayerSqlClient.Common
{
    internal static class SqlCommandExecutor
    {
      
        private static void AddParameters(SqlCommand command, IDictionary<string, object>? parameters)
        {
            if (parameters == null) return;
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
            }
        }

        public static IEnumerable<T> ExecuteReader<T>(
        Func<SqlConnection, SqlCommand> cmdFnc,
        string connString,
        Func<IDataReader, T> transform,
        IDictionary<string, object>? parameters = null,
        CommandType cmdType = CommandType.Text)
        {
            
            using var connection = new SqlConnection(connString);
            using var command = cmdFnc(connection);
            command.CommandType = cmdType;
            AddParameters(command, parameters);
            connection.Open();
            using var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                yield return transform(reader);
            }
        }

        public static object ExecuteScalar(Func<SqlConnection, SqlCommand> cmdFnc,
                         string connectionString,
                         IDictionary<string, object>? parameters = null,
                         CommandType cmdType = CommandType.Text)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = cmdFnc(connection);
            command.CommandType = cmdType;
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteScalar();
        }

    }
}
