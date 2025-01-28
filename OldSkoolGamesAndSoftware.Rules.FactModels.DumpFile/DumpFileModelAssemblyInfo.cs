// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DumpFileModelAssemblyInfo.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games and Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.FactModels.Dump
{
    using System.Diagnostics.CodeAnalysis;

    using OldSkoolGamesAndSoftware.Rules;

    /// <summary>
    /// Contains static information about the DumpFile Fact Model
    /// </summary>
    public class DumpFileModelAssemblyInfo
    {
        #region Fields

        /// <summary>
        /// The model types
        /// </summary>
        public static readonly ModelTypeTable ModelTypes = InnerGetAnnotationModelTypes();

        #endregion

        #region Methods

        /// <summary>
        ///  Gets the annotation model types.
        /// </summary>
        /// <returns>
        /// A <see cref="ModelTypeTable" /> instance which contains all
        /// ModelTypes defined in this assembly.
        /// </returns>
        private static ModelTypeTable InnerGetAnnotationModelTypes()
        {
            var modelTypes = new ModelTypeTable(1)
            {
                new ModelType(
                    DumpFile.ModelTypeId, 
                    "DumpFile", 
                    typeof(DumpFile),
                    new[]
                    {
                        new ObjectType(DumpFile.ObjectTypeId, "DumpFile"),
                        new ObjectType(DumpInfo.ObjectTypeId, "DumpInfo"),
                        new ObjectType(StackFrame.ObjectTypeId, "FunctionCallInfo"),
                        new ObjectType(ProcessThread.ObjectTypeId, "ThreadInfo")
                    })
            };

            return modelTypes;
        }

        #endregion
    }
}