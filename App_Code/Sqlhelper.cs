using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
public class Sqlhelper
{
    //读取webconfig中SQL连接字符串                
    public static string conStr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

    /// <summary>
    /// 执行查询
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="parameters">查询参数</param>
    /// <returns></returns>
    public static DataTable Serach(string sql, params SqlParameter[] parameters)
    {
        //建立连接
        using (SqlConnection conn = new SqlConnection(conStr))
        {
            //打开连接
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet st = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }
            }
            catch
            {   
                return new DataTable();
            }
        }
    }
    /// <summary>
    /// 执行ExcuteNonQuery操作
    /// </summary>
    /// <param name="sql">执行sql语句</param>
    /// <param name="parameters">相关参数</param>
    /// <returns>影响行数</returns>
    public static int ExcuteNonQuery(String sql, params SqlParameter[] parameters)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        object o =parameter.Value;
                        cmd.Parameters.Add(parameter);
                    }


                    return cmd.ExecuteNonQuery();
                }
            }
        }
        catch 
        {
            return 0;
        }
    }

    public static void UpdateDataSet(string selectCommand, DataSet dataSet, string tableName)
    {
        if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

        try
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand, new SqlConnection(conStr)))
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
                builder.QuotePrefix = "[";
                builder.QuoteSuffix = "]";
                
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ds.Tables[0].TableName = tableName;
                ds.Merge(dataSet);
                dataAdapter.Update(ds, tableName);
                dataSet.AcceptChanges();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    /// <summary>
    /// 批量将DataTable中的数据写入到指定表中
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="dt">数据</param>
    public static void InserterData(string tableName, DataTable dt)
    {
        System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(conStr);
        bcp.DestinationTableName = tableName;
        bcp.WriteToServer(dt);
    }
}  
