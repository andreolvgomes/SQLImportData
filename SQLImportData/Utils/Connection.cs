using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLImportData
{
    public static class Connection
    {
        private static SqlConnection _cnn = null;

        public static SqlConnection Cnn(string datasource)
        {
            if(_cnn == null)
                _cnn = CreateInstance(datasource);
            if (_cnn.State != System.Data.ConnectionState.Open)
                _cnn.Open();
            return _cnn;
        }

        public static bool Test(string datasource)
        {
            try
            {
                _cnn = CreateInstance(datasource);
                _cnn.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static SqlConnection CreateInstance(string datasource)
        {
            var connectionString = $@"server={datasource};database=bdsic;user id=sa;pwd=sic742;";
            _cnn = new SqlConnection(connectionString);
            return _cnn;
        }
    }
}