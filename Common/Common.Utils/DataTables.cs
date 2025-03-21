using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public  class DataTables
    {
        public static DataTable ChangeColumnNames(DataTable dt, string[] columnNames)
        {
            DataTable dtOut = new DataTable();
            dtOut.Load(dt.CreateDataReader());
            if (dtOut.Columns.Count.Equals(columnNames.Count()))
            {                
                for(int i =0;i< dtOut.Columns.Count;i++)
                {
                    dtOut.Columns[i].ColumnName = columnNames[i];
                }
            }
            return dtOut;
        }
        public static DataTable ColumnsDataTypeToString(DataTable dt)
        {
            DataTable dtCloned = dt.Clone();
            for (int i = 0; i < dtCloned.Columns.Count; i++)
            {
                dtCloned.Columns[i].DataType = typeof(string);
            }
            dtCloned.Load(dt.CreateDataReader());
            
            return dtCloned;
        }
    }
}
