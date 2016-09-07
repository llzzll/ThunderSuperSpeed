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
        public static string DoCrack(string path)
        {
            try
            {
                // 初始化connection
                string dbPath = "Data Source =" + path;
                conn = new SQLiteConnection(dbPath);
                conn.Open();

                // 获取高速通道表名
                string curTable = GetTable();

                if (curTable.Equals(""))
                {
                    throw new Exception("没有找到可破解的任务。");
                }

                // 处理表中的数据
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
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        private static string GetTable()
        {
            SQLiteCommand getTables = new SQLiteCommand(SQL_LISTABLE, conn);
            try
            {
                reader = getTables.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["name"].ToString().Contains("superspeed"))
                    {
                        return reader["name"].ToString();
                    }
                }
                return "";
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
        private static string UpdateTable(string tableName)
        {
            Dictionary<string, byte[]> dataDic = new Dictionary<string, byte[]>();

            List<string> taskCountList = new List<string>();

            SQL_GETLIST = SQL_GETLIST.Replace("@table", tableName);
            SQL_UPDATE = SQL_UPDATE.Replace("@table", tableName);

            SQLiteCommand getList = new SQLiteCommand(SQL_GETLIST, conn);
            try
            {
                reader = getList.ExecuteReader();
                while (reader.Read())
                {
                    // 读取每一行并获取UserData的值
                    string data = Encoding.Default.GetString(reader["UserData"] as byte[]);
                    // 如果值中包含509或者508则替换后转回字节数组放入dic
                    if (data.Contains("\"Result\":509") || data.Contains("\"Result\":508"))
                    {
                        data = data.Replace("\"Result\":509", "\"Result\":0");
                        data = data.Replace("\"Result\":508", "\"Result\":0");
                        dataDic.Add(reader["rowid"].ToString(), Encoding.Default.GetBytes(data));
                        // 根据LocalTaskId计算任务数量
                        if (!taskCountList.Contains(reader["LocalTaskId"].ToString()))
                        {
                            taskCountList.Add(reader["LocalTaskId"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("查找任务失败：" + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            // 任务数和文件数
            int taskCount = taskCountList.Count;
            int fileCount = 0;

            // 新建事务
            SQLiteTransaction trans = conn.BeginTransaction();
            try
            {
                foreach (KeyValuePair<string, byte[]> pair in dataDic)
                {
                    SQLiteCommand cmd = new SQLiteCommand(SQL_UPDATE, conn);
                    cmd.Transaction = trans;
                    cmd.Parameters.Add(new SQLiteParameter("@data", pair.Value));
                    cmd.Parameters.Add(new SQLiteParameter("@id", pair.Key));
                    fileCount += cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("破解失败！" + ex.Message);
            }

            if (fileCount == 0)
            {
                throw new Exception("没有找到可破解的任务。");
            }
            return "共处理了" + taskCount + "个任务中的" + fileCount + "个文件";
        }
    }
}
