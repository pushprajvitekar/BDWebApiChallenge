using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayerSqlClient.Common
{
    internal abstract class SqlCommandBase<T>
    {
        protected SqlCommandBase(params object[] parameters)
        {
            SetParameters(parameters);
        }
        protected virtual CommandType CommandType => CommandType.Text;
        protected abstract string CommandText { get; }

        public Dictionary<string, object> Parameters { get; protected set; } = new Dictionary<string, object>();

        protected abstract void SetParameters(params object[] parameters);
        protected abstract T Transform(IDataReader reader);

        private SqlCommand Command(SqlConnection connection)
        {
            return new SqlCommand(CommandText, connection);
        }

        internal T ExecuteScalar(string connString)
        {
            var res = SqlCommandExecutor.ExecuteScalar(Command, connString, Parameters, CommandType);
            return (T)res;
        }
        internal IEnumerable<T> ExecuteReader(string connString)
        {
            return SqlCommandExecutor.ExecuteReader(Command, connString, Transform, Parameters, CommandType);
        }


    }
}
