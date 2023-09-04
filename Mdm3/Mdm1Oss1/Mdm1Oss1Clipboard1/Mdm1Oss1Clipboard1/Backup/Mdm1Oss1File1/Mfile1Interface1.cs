using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Oss.FileUtil
{
    /// <summary>
    /// <para> This interface is out out date and 
    /// needs revision.  It's purpose is to define
    /// a basic set methods to access the Mfile 
    /// File Application Object.</para>
    /// </summary>
    /// <remarks>
    /// Needs updating after implementing either
    /// derived classes or generics.
    /// </remarks>
    interface Mfile1Interface1
    {
            long MainFileProcessing();
            long TextFileProcessMain(String FileName);
            /// ==================================================================
            /// =          Action Being Performed   
            /// ==================================================================

            /// Source File Name Line
            String FileNameLine { get;}

            /// Source File Action
            String FileAction { get;}

            /// Source File Action Name
            String FileAction_Name { get;}
            /*
             *
             *  0    - Null
             *  1    - DoesExist
             *  2    - Create
             *  4    - Open
             *  8    - Close
             *  16   - Delete
             *  32   - Empty (Delete All)
             *  64   - Shrink
             *  128  - Expand
             *  256  - Lock
             *  512  - Unlock
             *  1024 - Defragment
             *  2048 - bRead Only
             *  4096 - Rebuild
             *  8192 - Rebuild Statistics
             *  16384 - x
             *  32768 - x
             *  65536 - x
             * 
             */

            /// ==================================================================


            /// ==================================================================

            /// Source File Name
            // TODO String FileName { get;}
            String FileNameAlias { get;}
            // TODO int FileId { get;}
            String FileShortName { get;}
            String FileShort83Name { get;}

            Guid gFileNameGuid { get;}


            /// ==================================================================

            /// Root Domain Information

            /// ==================================================================

            /// Xml Xpath Information

            /// ==================================================================

            /// Ip Domain Information

            /// ==================================================================

            /// Root Owner Entity Information
            /// Root Owner Path Patern Information
            /// Root Owner Clustering Information
            /// Root Owner Replication Information


            /// ==================================================================

            /// Source System Information


            /// ==================================================================

            /// Source Database Information


            /// ==================================================================

            /// Source Database FileGroup Information


            /// ==================================================================

            /// Source Database FileName Information


            /// ==================================================================

            /// Source Drive Name
            String FileDriveName { get;}
            String FileDriveLetter { get;}
            String FileDriveLetterAlias { get;}
            int FileDriveSystemId { get;}
            String FileDriveShortName { get;}

            /// ==================================================================

            /// Source Path Name
            String sPathName { get;}
            String sPathNameAlias { get;}
            int iPathId { get;}
            String sPathShortName { get;}

            /// ==================================================================

            /// Source Parent Name
            String sParentName { get;}
            String sParentNameAlias { get;}
            int iParentId { get;}
            String sParentShortName { get;}

            /// ==================================================================

            /// Source ConsolodationParent Name
            String sConsolodationParentName { get;}
            String sConsolodationParentNameAlias { get;}
            int iConsolodationParentId { get;}
            String sConsolodationParentShortName { get;}


        }
}
