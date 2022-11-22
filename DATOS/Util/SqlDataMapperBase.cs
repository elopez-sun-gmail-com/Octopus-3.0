using System.Data;
using System.Data.SqlClient;

namespace DATOS.Util
{
    public class SqlDataMapperBase
    {
        private IDbConnection _connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        public SqlDataMapperBase(string connectionStrings)
        {
            this._connection = new SqlConnection(connectionStrings); // new SqlConnection(ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }
        /// <summary>
        /// 
        /// </summary>
       /* public SqlDataMapperBase()
        {
            this._connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RuvConnection"].ConnectionString);
        }*/

        public IDbConnection getConnection()
        {
            return this._connection;
        }

    }
}
