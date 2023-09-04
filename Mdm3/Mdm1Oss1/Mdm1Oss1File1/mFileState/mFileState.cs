using Mdm.Oss.Decl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mdm.Oss.Std;

namespace Mdm.Oss.File
{
    public class mFileStateDef
    {
        #region Class Method (Function call) Result Returns - mFile et al.
        /// <summary>
        /// </summary>
        public StateIs mFileResult;
        /// <summary>
        /// </summary>
        public StateIs IoOpDoCallbackResult;
        #region Table X
        /// <summary>
        /// </summary>
        public StateIs TableOpenResult;
        /// <summary>
        /// </summary>
        public StateIs TableCreateDoResult;
        /// <summary>
        /// </summary>
        public StateIs TableCheckDoesExistResult;
        /// <summary>
        /// </summary>
        public StateIs TableTestAccessResult;
        /// <summary>
        /// </summary>
        public StateIs TableCloseResult;
        /// <summary>
        /// </summary>
        public StateIs TableCreateResult;
        /// <summary>
        /// </summary>
        public StateIs DatabaseFileLongResult;
        /// <summary>
        /// </summary>
        public StateIs TableNameLineBuildResult;
        #endregion
        #region mFile SqlData, Col, Dict, 
        /// <summary>
        /// </summary>
        public StateIs SqlColActionResult;
        /// <summary>
        /// </summary>
        public StateIs SqlCommandDoResult;
        /// <summary>
        /// </summary>
        public StateIs SqlCommandSetDefaultResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataAddResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataExecuteResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataDeleteResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataInsertResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataUpdateResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataReadOpenResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataReadCloseResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataWriteResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataReadResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDataGetResult;
        //
        /// <summary>
        /// </summary>
        public StateIs SqlColAddCmdBuildAllFromArrayResult;
        /// <summary>
        /// </summary>
        public StateIs SqlColAddCmdBuildAddFromArrayResult;
        /// <summary>
        /// </summary>
        public StateIs SqlColAddCmdBuildViewFromArrayResult;
        /// <summary>
        /// </summary>
        public StateIs SqlColAddCmdBuildResult;
        /// <summary>
        /// </summary>
        public StateIs SqlColAddCmdBuildAllResult;
        /// <summary>
        /// </summary>
        public StateIs SqlColConvertCharactersResult;
        /// <summary>
        /// </summary>

        public StateIs SqlDictInsertResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDictInsertBuildResult;
        /// <summary>
        /// </summary>
        public StateIs SqlDictInsertDoResult;
        /// <summary>
        /// </summary>
        public StateIs SqlColClearResult;
        //
        /// <summary>
        /// </summary>
        public StateIs SqlDictProcessDbResult;
        /// <summary>
        /// </summary>
        public StateIs SqlResetResult;
        #endregion
        #region Text, Ascii
        /// <summary>
        /// </summary>
        public StateIs TextFileCloseResult;
        /// <summary>
        /// </summary>
        public StateIs TextFileCreateResult;
        /// <summary>
        /// </summary>
        public StateIs TextFileDeleteResult;
        /// <summary>
        /// </summary>
        public StateIs TextFileDoesExistResult;
        /// <summary>
        /// </summary>
        public StateIs TextFileOpenResult;
        /// <summary>
        /// </summary>
        public StateIs TextFileProcessMainResult;
        /// <summary>
        /// </summary>
        public StateIs TextFileResetResult;
        /// <summary>
        /// </summary>
        public StateIs TextFileWriteResult;
        //
        /// <summary>
        /// </summary>
        public StateIs SetAppResult;
        #endregion
        #region public StateIs FileDoResult;
        /// <summary>
        /// </summary>
        public StateIs FileDoOpenResult;
        /// <summary>
        /// </summary>
        public StateIs FileDoCreateResult;
        /// <summary>
        /// </summary>
        public StateIs FileDoDeleteResult;
        /// <summary>
        /// </summary>
        public StateIs FileDoClearResult;
        /// <summary>
        /// </summary>
        public StateIs FileDoCloseResult;
        /// <summary>
        /// </summary>
        public StateIs FileDoCheckResult;
        /// <summary>
        /// </summary>
        public StateIs FileDoGetResult;
        #endregion
        #region Data CRUD
        /// <summary>
        /// </summary>
        public StateIs AppmFileObjectSet;
        /// <summary>
        /// </summary>
        public StateIs AppmFileObjectGet;
        /// <summary>
        /// </summary>
        public StateIs DataAddResult;
        /// <summary>
        /// </summary>
        public StateIs DataExecuteResult;
        /// <summary>
        /// </summary>
        public StateIs DataDeleteResult;
        /// <summary>
        /// </summary>
        public StateIs DataInsertResult;
        /// <summary>
        /// </summary>
        public StateIs DataUpdateResult;
        /// <summary>
        /// </summary>
        public StateIs DataReadOpenResult;
        /// <summary>
        /// </summary>
        public StateIs DataReadCloseResult;
        /// <summary>
        /// </summary>
        public StateIs DataWriteResult;
        /// <summary>
        /// </summary>
        public StateIs DataReadResult;
        /// <summary>
        /// </summary>
        public StateIs DataGetResult;
        /// <summary>
        /// </summary>
        public StateIs DataWriteMoveUpResult;
        #region Ascii
        /// <summary>
        /// </summary>
        public StateIs AsciiFileClearResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileCreateResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileCreatePassedNameResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileDeleteResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileDeletePassedNameResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileCloseResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileResetResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileFileStreamReaderCheckResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileReadAllResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileReadBlockResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileReadLineResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileReadBlockSeekResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileWriteResult;
        /// <summary>
        /// </summary>
        public StateIs AsciiFileWritePassedNameResult;
        #endregion
        #region Connection
        /// <summary>
        /// </summary>
        public StateIs MainFileProcessingResult;
        /// <summary>
        /// </summary>
        public StateIs ConnCheckDoesExistResult;
        /// <summary>
        /// </summary>
        public StateIs ConnCloseResult;
        /// <summary>
        /// </summary>
        public StateIs ConnCmdBuildResult;
        /// <summary>
        /// </summary>
        public StateIs ConnCreatePassedConnResult;
        /// <summary>
        /// </summary>
        public StateIs ConnCreateResult;
        /// <summary>
        /// </summary>
        public StateIs ConnCreateBuildResult;
        /// <summary>
        /// </summary>
        public StateIs ConnCreateCmdBuildResult;
        /// <summary>
        /// </summary>
        public StateIs ConnOpenPassedNameResult;
        /// <summary>
        /// </summary>
        public StateIs ConnOpenResult;
        /// <summary>
        /// </summary>
        public StateIs ConnResetResult;
        #endregion
        #region Database
        /// <summary>
        /// </summary>
        public StateIs DatabaseFileCloseErrorResult;
        /// <summary>
        /// </summary>
        public StateIs DatabaseCreateCmdBuildResult;
        /// <summary>
        /// </summary>
        public StateIs ConnectionCreateResult;
        /// <summary>
        /// </summary>
        public StateIs DatabaseFileCreationErrorResult;
        /// <summary>
        /// </summary>
        public StateIs ExceptionDatabaseFileGeneralResult;
        /// <summary>
        /// </summary>
        public StateIs DatabaseFileNameLongCreateBuildResult;
        /// <summary>
        /// </summary>
        public StateIs DatabaseFieldsGetDefaultsResult;
        /// <summary>
        /// </summary>
        public StateIs DatabaseFileOpenErrorResult;
        /// <summary>
        /// </summary>
        public StateIs DatabaseResetResult;
        #endregion
        #region Open Close Read Write Seek
        public StateIs FileCloseResult;
        public StateIs FileCloseHandleResult;
        public StateIs FileClosemFileDef1Result;
        public StateIs FileOpenResult;
        public StateIs FileOpenHandleResult;
        public StateIs FileReadAllResult;
        public StateIs FileReadLineResult;
        public StateIs FileSeekResult;
        public StateIs FileWriteResult;
        #endregion
        #endregion
        #region Common Exception Code
        public StateIs ExceptCommonFileResult;
        public StateIs ExceptGeneralFileResult;
        public StateIs ExceptIoResult;
        public StateIs ExceptSqlResult;
        #endregion
        #region File Processing
        public StateIs FileIndexPointerIncrementResult;
        public StateIs FileNameBuildFullResult;
        public StateIs FileNameLineBuildResult;
        // Ascii
        public StateIs AsciiFileReadRecordResult;
        public StateIs ColPointerIncrementResult;
        // Move Shift Item.ItemData by x Attrs
        public StateIs ItemDataShiftResult;
        #endregion
        #endregion
        #region Controls to avoid infinite loops
        public bool ObjectListLoading;
        // This prevents infinite loops
        // in init of meta data.
        public bool DoingDefaults;
        public bool CopyIsDone;
        // FileTypeName
        public bool DoingFileType;
        public bool DoingFileTypeName;
        // FileSubTypeName
        public bool DoingFileSubType;
        public bool DoingFileSubTypeName;
        // Connection
        public bool ConnDoReset;
        public bool DoingCopy;
        #endregion
    }
}
