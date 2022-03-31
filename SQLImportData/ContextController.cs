using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SQLImportData
{
    public class ContextController : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private StepsStypes _StepCurrent;

        public StepsStypes StepCurrent
        {
            get { return _StepCurrent; }
            set
            {
                if (_StepCurrent == value) return;
                _StepCurrent = value;
                OnPropertyChanged("StepCurrent");
            }
        }

        private string _file;

        public string File
        {
            get { return _file; }
            set
            {
                if (_file == value) return;
                _file = value;
                OnPropertyChanged("File");
            }
        }

        private string _table;

        public string Table
        {
            get { return _table; }
            set
            {
                if (_table == value) return;
                _table = value;
                OnPropertyChanged("Table");
            }
        }

        private string _dataSource= ".\\SQLExpress";

        public string DataSource
        {
            get { return _dataSource; }
            set
            {
                if (_dataSource == value) return;
                _dataSource = value;
                OnPropertyChanged("DataSource");
            }
        }

        internal bool CheckTable()
        {
            var cnn = Connection.Cnn(DataSource);
            var count = cnn.Query<int>($"select count(1) from information_schema.tables where table_name = '{Table}'").FirstOrDefault();
            return count > 0;
        }

        public DataTable DataTable { get; set; }

        public ContextController()
        {
        }

        internal void Import()
        {
            CreateTable();
            var listColumnsParam = new List<string>();
            var listColumns = new List<string>();

            foreach (DataColumn col in DataTable.Columns)
            {
                listColumnsParam.Add($"@{col.ColumnName.Replace(" ", "_")}");
                listColumns.Add($"[{col.ColumnName}]");
            }

            var sql = $"insert into dbo.[{Table}]";
            sql += $" ({string.Join(",", listColumns)})";
            sql += $" values ({string.Join(",", listColumnsParam)})";

            foreach (DataRow row in DataTable.Rows)
            {
                using (var command = Connection.Cnn(DataSource).CreateCommand())
                {
                    command.CommandText = sql;
                    foreach (DataColumn item in DataTable.Columns)
                        command.Parameters.AddWithValue($"@{item.ColumnName.Replace(" ", "_")}", row[item.ColumnName]);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateTable()
        {
            var sql = Helpers.CreateTABLE(Table, DataTable);
            using (var command = Connection.Cnn(DataSource).CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
    }
}