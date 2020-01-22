using System.IO;
using System.Reflection;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace AutoTests.Util
{
    public class BackupRestore
    {
        static Server _srv;
        static ServerConnection _conn;

        public static void BackupDatabase(string serverName, string databaseName, string filePath)
        {
            _conn = new ServerConnection();
            _conn.ServerInstance = serverName;
            _srv = new Server(_conn);
            try
            {
                var bkp = new Backup();

                bkp.Action = BackupActionType.Database;
                bkp.Database = databaseName;

                File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" +
                    filePath);
                bkp.Devices.AddDevice(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" +
                    filePath, DeviceType.File);
                bkp.Incremental = false;

                bkp.SqlBackup(_srv);

                _conn.Disconnect();
                _conn = null;
                _srv = null;
            }
            catch (SmoException ex)
            {
                throw new SmoException(ex.Message, ex.InnerException);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
        }

        public static void RestoreDatabase(string serverName, string databaseName, string filePath)
        {
            _conn = new ServerConnection();
            _conn.ServerInstance = serverName;
            _srv = new Server(_conn);

            try
            {
                var res = new Restore();

                _srv.KillAllProcesses(databaseName);

                res.Devices.AddDevice(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" +
                    filePath, DeviceType.File);

                var dataFile = new RelocateFile();
                string mdf = res.ReadFileList(_srv).Rows[0][1].ToString();
                dataFile.LogicalFileName = res.ReadFileList(_srv).Rows[0][0].ToString();
                dataFile.PhysicalFileName = _srv.Databases[databaseName].FileGroups[0].Files[0].FileName;

                var logFile = new RelocateFile();
                string ldf = res.ReadFileList(_srv).Rows[1][1].ToString();
                logFile.LogicalFileName = res.ReadFileList(_srv).Rows[1][0].ToString();
                logFile.PhysicalFileName = _srv.Databases[databaseName].LogFiles[0].FileName;

                res.RelocateFiles.Add(dataFile);
                res.RelocateFiles.Add(logFile);

                res.Database = databaseName;
                res.NoRecovery = false;
                res.ReplaceDatabase = true;

                _srv.KillDatabase(databaseName);
                res.SqlRestore(_srv);
                _conn.Disconnect();
            }
            catch (SmoException ex)
            {
                throw new SmoException(ex.Message, ex.InnerException);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
        }

        public static Server Getdatabases(string serverName)
        {
            _conn = new ServerConnection();
            _conn.ServerInstance = serverName;

            _srv = new Server(_conn);
            _conn.Disconnect();
            return _srv;
        }
    }
}
