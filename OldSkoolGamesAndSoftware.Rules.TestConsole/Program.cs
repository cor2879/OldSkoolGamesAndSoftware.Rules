// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games and Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using OldSkoolGamesAndSoftware.Logging;
    using OldSkoolGamesAndSoftware.Rules.Sql;
    using OldSkoolGamesAndSoftware.Rules.FactModels.Dump;
    // using OldSkoolGamesAndSoftware.Rules.FactModels.Dump.Legacy;

    /// <summary>
    /// The contains the primary execution code for the TestConsole
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The information
        /// </summary>
        private const TraceLevel Info = TraceLevel.Info;

        /// <summary>
        /// The command timeout
        /// </summary>
        private const int CommandTimeout = 180;

        /// <summary>
        /// The annotation connection string
        /// </summary>
        private static readonly string AnnotationConnectionString = ConfigurationManager.ConnectionStrings["AnnotationDBString"].ConnectionString;

        /// <summary>
        /// The winde connection string
        /// </summary>
        private static readonly string WindeConnectionString = ConfigurationManager.ConnectionStrings["WindeDBString"].ConnectionString;

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILogger Logger = new ConsoleLogger();

        /// <summary>
        /// The primary execution method of the Test Console
        /// </summary>
        /// <param name="args">The arguments.</param>
        internal static void Main(string[] args)
        {
            RuleLogger.Instance = Logger;

            var stopWatch = new Stopwatch();

            Logger.Log(Info, "Main(string[] args) - Getting rules from database.");

            stopWatch.Start();
            var rules = GetRules(AnnotationConnectionString).Result;
            stopWatch.Stop();

            Logger.Log(Info, "Main(string[] args) - Rules retrieved and instantiated.  Time Elapsed: {0}", stopWatch.Elapsed);

            foreach (var requestId in GetRequestIdsFromFile("Dumps.txt"))
            {
                Logger.Log(Info, "Main(string[] args) - Getting Dump file {0} from database.", requestId);

                stopWatch.Restart();
                var dump = GetDump(requestId, WindeConnectionString);
                stopWatch.Stop();

                Logger.Log(Info, "Main(string[] args) - Dump file {0} retrieved.  Time Elapsed: {1}", requestId, stopWatch.Elapsed);

                Logger.Log(Info, "Main(string[] args) - Running rule analysis.");

                stopWatch.Restart();

                Parallel.ForEach(
                    rules, 
                    rule =>
                    {
                        var dataPoint = rule.Evaluate(dump);

                        if (dataPoint != null)
                        {
                            Logger.Log(Info, "Main(string[] args) - Rule {0} found a match.", rule.LegacyId);
                        }
                    });

                stopWatch.Start();

                Logger.Log(Info, "Main(string[] args) - Rule Analysis Completed.  Time Elapsed: {0}", stopWatch.Elapsed);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Gets the request ids from a file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// A collection of RequestIds
        /// </returns>
        private static IEnumerable<Guid> GetRequestIdsFromFile(string path)
        {
            using (var stream = new StreamReader(path))
            {
                string line = null;

                while ((line = stream.ReadLine()) != null)
                {
                    yield return Guid.Parse(line);
                }
            }
        }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>
        /// A collection containing all the DumpFile rules found in the database.
        /// </returns>
        private static async Task<List<Rule>> GetRules(string connectionString)
        {
            // TODO
            return null;
        }

        /// <summary>
        /// Gets the dump.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>
        /// The dump corresponding to the specified Request Id
        /// </returns>
        private static DumpFile GetDump(Guid requestId, string connectionString)
        {
            // return Translator.GetDumpFileFromDatabase(requestId, connectionString, CommandTimeout);
            return null;
        }
    }
}
