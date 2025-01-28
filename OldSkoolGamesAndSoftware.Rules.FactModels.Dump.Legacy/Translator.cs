using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Xml.Schema;

using WinDe.BusinessObjects;
using WinDe.Data.SqlServer;
using OldSkoolGamesAndSoftware.Rules.FactModels.Dump;
using OldSkoolGamesAndSoftware.Utilities;
using Old = WinDe.BusinessObjects;

namespace Ms.Diagnostics.Ude.FactModels.Dump.Legacy
{
    using System;

    using Ms.Diagnostics.Ude.FactModels.Dump;

    public class Translator
    {
        #region Fields

        #endregion

        #region Constructors



        #endregion

        #region Methods

        public static DumpFile GetDumpFileFromDatabase(Guid requestId, string connectionString, int? commandTimeout = null)
        {
            using (var dal = (commandTimeout != null) ? new MemoryDumpSqlDal(connectionString, commandTimeout.Value) : new MemoryDumpSqlDal(connectionString))
            {
                DumpFile dump;

                using (var dataTable = dal.GetWinDeSessionData(requestId))
                {
                    if (dataTable.Rows.Count == 0)
                    {
                        return null;
                    }

                    var row = dataTable.Rows[0];

                    dump = new DumpFile()
                    {
                        Identity =  (row["Identity"] != DBNull.Value) ? Tools.Convert(row["Identity"], 0L) : 0L,
                        RequestId = (row["RequestId"] != DBNull.Value) ? Tools.Convert(row["RequestId"], Guid.Empty) : Guid.Empty,
                        FileGuid = (row["FileGuid"] != DBNull.Value) ? Tools.Convert(row["FileGuid"], Guid.Empty) : Guid.Empty,
                        ServerName = (row["ServerName"] != DBNull.Value) ? row["ServerName"].ToString() : null
                    };
                }

                LoadDumpInfoFromDatabase(dump, dal);
                LoadThreadsFromDatabase(dump, dal);

                return dump;
            }
        }

        public static DumpFile GetDumpFile(WinDeSession dump)
        {
            var dumpFile = new DumpFile()
            {
                FileGuid = dump.FileGuid,
                RequestId = dump.RequestId,
            };

            LoadDumpInfo(dumpFile, dump.DumpInfo.Values);
            LoadThreads(dumpFile, dump.Threads.Values);

            return dumpFile;
        }

        private static void LoadDumpInfoFromDatabase(DumpFile dump, MemoryDumpSqlDal dal)
        {
            using (var dataTable = dal.GetWinDeDumpInfo(dump.RequestId))
            {
                var list = new List<DumpInfo>();

                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(new DumpInfo()
                    {
                        File = dump,
                        Id = (row["ID"] != DBNull.Value) ? Tools.Convert(row["ID"], 0L) : 0L,
                        PropertyName = (row["PropertyName"] != DBNull.Value) ? row["PropertyName"].ToString() : null,
                        QWord = (row["QWord"] != DBNull.Value) ? Tools.Convert(row["QWord"], 0L) : 0L,
                        StringValue = (row["StringValue"] != DBNull.Value) ? row["StringValue"].ToString() : null
                    });
                }

                dump.DumpInfo = list;
            }
        }

        private static void LoadDumpInfo(DumpFile dump, IEnumerable<Old.DumpInfo> dumpInfo)
        {
            dump.DumpInfo = dumpInfo.Select(info =>
                new DumpInfo()
                {
                    File = dump,
                    Id = info.Id,
                    PropertyName = info.PropertyName,
                    QWord = info.QWord,
                    StringValue = info.StringValue
                }).ToList();
        }

        private static void LoadThreadsFromDatabase(DumpFile dump, MemoryDumpSqlDal dal)
        {
            var threadTable = new Dictionary<long, ProcessThread>();

            using (var dataTable = dal.GetWinDeThreadData(dump.RequestId))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var id = (row["ID"] != DBNull.Value) ? Tools.Convert(row["ID"], 0L) : 0L;

                    threadTable.Add(
                        id,
                        new ProcessThread()
                        {
                            File = dump,
                            Id = id,
                            IsHandlingException = (row["IsHandlingException"] != DBNull.Value) && Tools.Convert(row["IsHandlingException"], false),
                            ProcessId = (row["Process"] != DBNull.Value) ? Tools.Convert(row["Process"], 0L) : 0L,
                            UniqueThreadId = (row["UniqueThread"] != DBNull.Value) ? Tools.Convert(row["UniqueThread"], 0L) : 0L
                        });
                }
            }

            if (threadTable.Count > 0)
            {
                LoadFramesFromDatabase(dump.RequestId, threadTable, dal);
            }

            dump.Threads = threadTable.Values.ToList();
        }

        private static void LoadFramesFromDatabase(Guid requestId, Dictionary<long, ProcessThread> threadTable, MemoryDumpSqlDal dal)
        {
            using (var dataTable = dal.GetWinDeFunctionCallData(requestId))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var thread = threadTable[Tools.Convert(row["Thread_Link"], 0L)];

                    thread.CallStack.Add(new StackFrame()
                    {
                        File = thread.File,
                        Thread = thread,
                        Id = (row["ID"] != DBNull.Value) ? Tools.Convert(row["ID"], 0L) : 0L,
                        Frame = (row["Frame"] != DBNull.Value) ? Tools.Convert(row["Frame"], 0) : 0,
                        FunctionOffset = (row["FunctionOffset"] != DBNull.Value) ? Tools.Convert(row["FunctionOffset"], 0) : 0,
                        FunctionName = (row["FunctionName"] != DBNull.Value) ? row["FunctionName"].ToString() : null,
                        ModuleName = (row["ModuleName"] != DBNull.Value) ? row["ModuleName"].ToString() : null,
                        Param1 = (row["Param1"] != DBNull.Value) ? Tools.Convert(row["Param1"], 0L) : 0L,
                        Param2 = (row["Param2"] != DBNull.Value) ? Tools.Convert(row["Param2"], 0L) : 0L,
                        Param3 = (row["Param3"] != DBNull.Value) ? Tools.Convert(row["Param3"], 0L) : 0L,
                        Param4 = (row["Param4"] != DBNull.Value) ? Tools.Convert(row["Param4"], 0L) : 0L
                    });
                }
            }
        }

        private static void LoadThreads(DumpFile dumpFile, IEnumerable<ThreadInfo> threads)
        {
            dumpFile.Threads = threads.Select(thread =>
            {
                var newThread = new ProcessThread
                {
                    File = dumpFile,
                    IsHandlingException = thread.IsHandlingException,
                    Id = thread.Id,
                    ProcessId =  thread.Process.UniqueProcessId,
                    UniqueThreadId = thread.UniqueThreadId
                };

                LoadCallStack(newThread, thread.CallStack);
                return newThread;
            }).ToList();
        }

        private static void LoadCallStack(ProcessThread thread, IEnumerable<FunctionCallInfo> callStack)
        {
            thread.CallStack = callStack.Select(frame =>
                new StackFrame()
                {
                    File = thread.File,
                    Frame = frame.Frame,
                    FunctionName = frame.FunctionName,
                    FunctionOffset =  frame.FunctionOffset,
                    Id = frame.Id,
                    ModuleName = frame.ModuleName,
                    Param1 = frame.Param1,
                    Param2 = frame.Param2,
                    Param3 = frame.Param3,
                    Param4 = frame.Param4
                }).ToList();
        }

        #endregion

        #region Properties



        #endregion
    }
}
