using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace ThunderSuperSpeed
{
    class SQLiteDAL
    {
        /// <summary>
        /// sqlite链接对象
        /// </summary>
        private static SQLiteConnection conn = null;
        private static SQLiteDataReader reader = null;
        private const string SQL_LISTABLE = "SELECT name FROM sqlite_master WHERE type='table' order by name";
        private static string SQL_GETLIST = "SELECT rowid,* FROM @table";
        private static string SQL_UPDATE = "UPDATE @table SET UserData = @data WHERE rowid = @id";
        /// <summary>
        /// 启动破解
        /// </summary>
        /// <param name="path"></param>
        public static int DoCrack(string path)
        {
            try
            {
                string dbPath = "Data Source =" + path;
                conn = new SQLiteConnection(dbPath);
                conn.Open();

                string curTable = "";
                List<string> tableNames = GetTables();
                foreach (string tableName in tableNames)
                {
                    if (tableName.Contains("superspeed"))
                    {
                        curTable = tableName;
                        break;
                    }
                }

                if (curTable.Equals(""))
                {
                    throw new Exception("没有找到可破解的任务。");
                }

                return UpdateTable(curTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// 获取表名列表
        /// </summary>
        /// <returns></returns>
        private static List<string> GetTables()
        {
            List<string> tableNames = new List<string>();
            SQLiteCommand getTables = new SQLiteCommand(SQL_LISTABLE, conn);
            try
            {
                reader = getTables.ExecuteReader();
                while (reader.Read())
                {
                    tableNames.Add(reader["name"].ToString());
                }
                return tableNames;
            }
            catch
            {
                throw new Exception("读取文件失败，请检查文件位置是否正确。");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// update表中的数据
        /// </summary>
        /// <param name="tableName"></param>
        private static int UpdateTable(string tableName)
        {
            Dictionary<string, byte[]> dataDic = new Dictionary<string, byte[]>();
            SQL_GETLIST = SQL_GETLIST.Replace("@table", tableName);
            SQL_UPDATE = SQL_UPDATE.Replace("@table", tableName);

            SQLiteCommand getList = new SQLiteCommand(SQL_GETLIST, conn);
            try
            {
                reader = getList.ExecuteReader();
                while (reader.Read())
                {
                    dataDic.Add(reader["rowid"].ToString(), reader["UserData"] as byte[]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("没有找到可破解的任务。" + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            
            int count = 0;
            SQLiteTransaction trans = conn.BeginTransaction();
            try
            {
                foreach (KeyValuePair<string, byte[]> pair in dataDic)
                {
                    string data = Encoding.Default.GetString(pair.Value);
                    if (data.Contains("509") || data.Contains("508"))
                    {
                        string id = pair.Key;

                        data = data.Replace("509", "0");
                        data = data.Replace("508", "0");

                        SQLiteCommand cmd = new SQLiteCommand(SQL_UPDATE, conn);
                        cmd.Transaction = trans;
                        cmd.Parameters.Add(new SQLiteParameter("@data", Encoding.Default.GetBytes(data)));
                        cmd.Parameters.Add(new SQLiteParameter("@id", id));
                        count += cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("破解失败！" + ex.Message);
            }

            if (count == 0)
            {
                throw new Exception("没有找到可破解的任务。");
            }
            return count;
        }
    }
}
