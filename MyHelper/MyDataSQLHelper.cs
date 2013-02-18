using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
//引入配置文件命名空间；          

namespace MyHelper4Web
{
    /// <summary>
    /// 执行常用SQL语句的类
    /// </summary>
    public class MyDataSQLHelper
    {
        /// <summary>
        /// 执行SQL语句，获取数据表
        /// </summary>
        /// <param name="SQLString">查询表格的SQL语句</param>
        /// <param name="DataBaseName">要连接的数据名</param>
        /// <returns>查询到的表结果</returns>
        public DataTable ShowTable(string SQLString,string DataBaseName)  
        {
            SqlConnection objConn = new SqlConnection();
            objConn.ConnectionString = "Data Source=.\\SQLExpress;Database="+DataBaseName+";Integrated Security=SSPI"; 
            objConn.ConnectionString = "Data Source=(Local);Initial Catalog="+DataBaseName+";Integrated Security=True";
            objConn.Open();
     
            string cmdtxt = SQLString;
            SqlDataAdapter objAdapter = new SqlDataAdapter(cmdtxt, objConn);
            DataSet objDataSet = new DataSet();

            objAdapter.Fill(objDataSet);
            objConn.Close();
            return (objDataSet.Tables[0]);
        }

        #region 三种方式返回值
        
        /// <summary>
        /// 通过【return】关键字返回DataSet
        /// </summary>
        /// <param name="SQLString">要执行的SQL语句</param>
        /// <param name="DataBaseName">要连接的数据库名</param>
        /// <returns></returns>
        public DataSet GetDataSet(string SQLString,string DataBaseName)
        {
            SqlConnection objConn=new SqlConnection();      

            objConn.ConnectionString = "Data Source=.\\SQLExpress;Database="+DataBaseName+";Integrated Security=SSPI";

            objConn.ConnectionString = "Data Source=(Local);Initial Catalog="+DataBaseName+";Integrated Security=True";

            objConn.Open();

            //创建command对象,获取数据库表的信息:
            string cmdtxt=SQLString;
            SqlCommand objComm2=new SqlCommand(cmdtxt,objConn);

            SqlDataAdapter objAdapter =new SqlDataAdapter(objComm2);

            DataSet objDataSet=new DataSet();

            //执行SQL语句,将执行的结果填充到DataSet中:
            objAdapter.Fill(objDataSet);
            objConn.Close();

            return (objDataSet);
        }

        //Through the parameter return value:
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="SqlString">要执行的SQL语句</param>
        /// <param name="objDataSet">数据集</param>
        /// <param name="DataBaseName">数据库名</param>
        
        public void GetDataSet(string SqlString, out DataSet objDataSet,string DataBaseName)
        {
            SqlConnection objConn = new SqlConnection();
            objConn.ConnectionString = "SERVER = (local);Database="+DataBaseName+";Integrated Security=SSPI";
            objConn.Open();

            //创建command对象，获取数据表信息：
            String cmdtxt = SqlString;
            SqlCommand objcomm2 = new SqlCommand(cmdtxt, objConn);

            SqlDataAdapter objAdapter = new SqlDataAdapter(objcomm2);

            objDataSet = new DataSet();

            objAdapter.Fill(objDataSet);
            objConn.Close();
        }

        /// <summary>
        /// //第三种：使用【引用类型】返回DataReader：
        /// </summary>
        /// <param name="SQLString">要执行的SQL语句</param>
        /// <param name="DataBaseName">要连接的数据库名</param>
        /// <returns>返回SqlDataReader</returns>
        public SqlDataReader getread(string SQLString,string DataBaseName)  
        {
            string cmdtxt = SQLString;

            SqlConnection objConn = new SqlConnection();

            objConn.ConnectionString = "Data Source=.\\SQLExpress;Database="+DataBaseName+";Integrated Security=SSPI";

            objConn.ConnectionString = "Data Source=(Local);Initial Catalog="+DataBaseName+";Integrated Security=True";


            objConn.Open();
            SqlCommand sqlcom = new SqlCommand(SQLString, objConn);
            SqlDataReader sqldr = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return sqldr;
        }
        #endregion

        /// <summary>
        ///获取DataReader
        /// </summary>
        /// <param name="SQLString">要执行的SQL语句</param>
        /// <param name="parms"></param>
        /// <param name="DataBaseName">要连接的数据库名</param>
        /// <returns>返回SqlDataReader</returns>
        public static SqlDataReader getread(string SQLString, SqlParameter[] parms,string DataBaseName)
        {
            string cmdtxt = SQLString;

            SqlConnection objConn = new SqlConnection();
            
            objConn.ConnectionString = "Data Source=.\\SQLExpress;Database="+DataBaseName+";Integrated Security=SSPI";

            objConn.ConnectionString = "Data Source=(Local);Initial Catalog="+DataBaseName+";Integrated Security=True";

            objConn.Open();
            SqlCommand sqlcom = new SqlCommand(SQLString, objConn);
            try
            {
                if (parms != null)
                    sqlcom.Parameters.AddRange(parms);
                SqlDataReader sqldr = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
                sqlcom.Parameters.Clear();
                sqlcom.Dispose();
                return sqldr;
            }
            catch
            {
                objConn.Close();
                throw;
            }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="SqlString">要执行的SQL语句</param>
        /// <param name="objDataSet">数据集</param>
        /// <param name="DataBaseName">要连接的数据库名</param>
        public void GetDataSet(String SqlString, DataSet objDataSet,string DataBaseName)
        {
            SqlConnection objConn = new SqlConnection();

            objConn.ConnectionString = "Data Source=.\\SQLExpress;Database="+DataBaseName+";Integrated Security=SSPI";

            objConn.ConnectionString = "Data Source=(Local);Initial Catalog="+DataBaseName+";Integrated Security=True";

            objConn.Open();

            String cmdtxt = SqlString;
            SqlCommand objcomm2 = new SqlCommand(cmdtxt, objConn);

            SqlDataAdapter objAdapter = new SqlDataAdapter(objcomm2);
            
            objAdapter.Fill(objDataSet);
            objConn.Close();
        }


        /// <summary>
        /// 执行添加，修改，删除的方法
        /// </summary>
        /// <param name="SqlString">要执行的语句</param>
        /// <param name="DataBaseName">要连接的数据名</param>
        /// <returns>返回bool</returns>
        public bool GetExecute(string SqlString,string DataBaseName )
        {
            SqlConnection objConn = new SqlConnection();
            objConn.ConnectionString = "Data Source=.\\SQLExpress;Database="+DataBaseName+";Integrated Security=SSPI";
            objConn.ConnectionString = "Data Source=(Local);Initial Catalog="+DataBaseName+";Integrated Security=True";
            objConn.Open();

            SqlCommand objSqlCommand = new SqlCommand();
            objSqlCommand.CommandText = SqlString; //接收字符串
            objSqlCommand.Connection = objConn;

            if (objSqlCommand.ExecuteNonQuery() > 0)
            { objConn.Close(); return (true); }
            else
            { objConn.Close(); return (false); }
        }


        /// <summary>
        /// //方法重载：执行存储过程：
        /// </summary>
        /// <param name="objSqlCommand">参数</param>
        /// <param name="DataBaseName">要连接的数据名</param>
        /// <returns>/bool</returns>
        ///
        public bool GetExecute(SqlCommand objSqlCommand,string DataBaseName)
        {
            SqlConnection objConn = new SqlConnection();

            objConn.ConnectionString = "Data Source=.\\SQLExpress;Database="+DataBaseName+";Integrated Security=SSPI";

            objConn.ConnectionString = "Data Source=(Local);Initial Catalog="+DataBaseName+";Integrated Security=True";

            objConn.Open();

            objSqlCommand.Connection = objConn;

            //bool result = true;

                try
                {
                        if (objSqlCommand.ExecuteNonQuery() > 0)
                        { 
                            objConn.Close(); 
                        }
                        else
                        {   
                            objConn.Close();
                        }
                        return true;
                }

                catch (SystemException e) 
                {
                        MessageBox.Show(e.Message);
                        return false;
                }
                
        }
    }

}


