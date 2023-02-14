using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Text;

namespace FoodSoftware.Common
{
    public interface ISqlUtility
    {
        SqlConnection GetNewConnection();
    }
    public class SqlUtility : ISqlUtility
    {
        IConfiguration Configuration;

        public SqlUtility(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public SqlConnection GetNewConnection()
        {
            var connectionString = Configuration.GetConnectionString("NetworkDb");
            var sc = new SqlConnection(connectionString);
            return sc;
        }
    }
}
