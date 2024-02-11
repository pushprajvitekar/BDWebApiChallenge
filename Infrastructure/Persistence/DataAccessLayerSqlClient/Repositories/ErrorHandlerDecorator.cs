using Domain.Exceptions;
using System.Data.Common;

namespace DataAccessLayerSqlClient.Repositories
{
    internal static class ErrorHandlerDecorator
    {
        internal static T1 TryCatch<T1>(Func<T1> actualMethod)
        {
            try
            {
                return actualMethod();
            }
            catch (InvalidOperationException sEx)
            {
                throw new InfrastructureException(sEx.Message, sEx);
            }
            catch (DbException sqlEx)
            {
                throw new InfrastructureException(sqlEx.Message, sqlEx);
            }
        }
    }
}
