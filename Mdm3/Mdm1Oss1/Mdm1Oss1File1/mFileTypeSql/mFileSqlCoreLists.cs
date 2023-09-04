using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;

#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)

namespace Mdm.Oss.File.Type.Sql
{
    public partial class mFileSql
    {
        // List and Defaults xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        #region SqlLoadFindLists
        #region Sql LoadFindLists Declarations
        /// <summary>
        /// <para>Selection Lists can be lists of any sort but generally 
        /// apply to lists that will be used for validation or used
        /// to populate combo boxes or other user interface controls.</para>
        /// <para> There is a predefined hierarchy of lists used to validate
        /// database and disk file selection. </para>
        /// <para> This is:</para> 
        /// <para> ..System</para>
        /// <para> ..Service</para>
        /// <para> ..Server</para>
        /// <para> ..Database</para>
        /// <para> ..FileGroup</para>
        /// <para> ..Table</para>
        /// <para> ..DiskFile</para>
        /// <para> ..FileOwner</para>
        /// <para> . </para>
        /// <para> Lists consist of the four components:</para>
        /// <para> ..Main</para>
        /// <para> ..Curr(ent)</para>
        /// <para> ..Prev(ious)</para>
        /// <para> ..DropDown</para>
        /// <para> The usage is self-evident except in the case of 
        /// DropDown.  It is loaded with the hard coded defaults and
        /// defaults loaded from file.  Dropdown is the list returned
        /// for user interface purposes.</para>
        /// <para> . </para>
        /// <para> The lists services are primarily supported
        /// by the following methods:</para>
        /// <para> ..ObjectListLoad</para>
        /// <para> ..ObjectListClearData</para>
        /// <para> ..GenericListLoad</para>
        /// <para> ..ObjectListLoadAscii</para>
        /// <para> ..ObjectParamLoad</para>
        /// <para> ..ObjectParamLoadAscii</para>
        /// <para> . </para>
        /// <para> Each predefined list type has a structured
        /// set of method names in order to operate.</para>
        /// <para> These are:</para>
        /// <para> ..[ListName]ListLoad</para>
        /// <para> ..[ListName]NameChanged</para>
        /// <para> ..[ListName]ListCheck</para>
        /// <para> ..[ListName]NameGetDefault</para>
        /// <para> ..[ListName]ListGet</para>
        /// <para> Please note there is a slight naming convention
        /// error here.  ListName s/b = FieldName but is not.  One
        /// example, System (the ListName) # SystemName (the field).</para>
        /// <para> The issues is the use of Name in the structured
        /// names.  This should be switch to Field so that it will
        /// be compatable with any field (beyond just Name(s)). In
        /// the second item:</para>
        /// <para> SystemNameChanged becomes SystemFieldChanged</para>
        /// </summary>
        public class SelectionList
        {
            public List<String> Main;
            public List<String> Curr;
            public List<String> Prev;
            public List<String> DropDown;
            public SelectionList()
            {
                Main = new List<string>();
                DropDown = new List<string>();
                Curr = new List<string>();
                Prev = new List<string>();
                DropDown = new List<string>();
            }
            public void PrepareNext()
            {
                if (Curr.Count > 0) { Prev = Curr.ToList(); }
                Curr.Clear();
                Main.Clear();
            }
        }
        public SelectionList ObjectList;
        public SelectionList SystemList;
        public SelectionList ServiceList;
        public SelectionList ServerList;
        public SelectionList DatabaseList;
        public SelectionList TableList;
        public SelectionList DiskFileList;
        public SelectionList FileOwnerList;
        public SelectionList FileGroupList;

        public void Initialize()
        {
            if (!ClassFeatureFlag.InitializeFileSql)
            {
                ClassFeatureFlag.InitializeFileSql = true;
                base.InitializeMFile();
                InitializeMetaData();
            }
        }

        public void InitializeMetaData()
        {
            ClassFeatureFlag.InitializeFileSql = true;
            ObjectList = new SelectionList();
            SystemList = new SelectionList();
            ServiceList = new SelectionList();
            ServerList = new SelectionList();
            DatabaseList = new SelectionList();
            TableList = new SelectionList();
            DiskFileList = new SelectionList();
            FileOwnerList = new SelectionList();
            FileGroupList = new SelectionList();

        }
        // Controls to avoid infinite loops
        #endregion
        #region Object List Load
        public StateIs ObjectListLoadResult;
        public StateIs ObjectListLoad(ref mFileMainDef FmainPassed)
        {
            ObjectListLoadResult = StateIs.Started;
            ObjectList = new SelectionList();
            ObjectListLoadResult = ObjectListLoad(ref FmainPassed, ref ObjectList.Curr);
            return ObjectListLoadResult;
        }

        public StateIs ObjectListLoad(ref mFileMainDef FmainPassed, ref List<String> ItemListPassed)
        {
            ObjectListLoadResult = StateIs.Started;
            ItemListPassed = new List<String>();
            // Object[] Columns = new Object[100];
            try
            {
                ObjectListLoadResult = StateIs.Successful;
                while (FmainPassed.DbIo.SqlDbDataReader.Read())
                {
                    // FmainPassed.DbIo.SqlDbDataReader.GetValues(Columns);
                    ItemListPassed.Add(FmainPassed.DbIo.SqlDbDataReader.GetValue(0).ToString());
                    ObjectListLoadResult = StateIs.EmptyResult;
                }
            }
            catch (Exception e)
            {
                ObjectListLoadResult = StateIs.EmptyResult;
                // Empty Set result from Reader Command
                // throw;
            }
            return ObjectListLoadResult;
        }

        public void ObjectListClearData()
        {
            ObjectList.Main = null;
            SystemList.Main = null;
            ServiceList.Main = null;
            ServerList.Main = null;
            DatabaseList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
            TableList.Main = null;
        }
        #endregion
        #region Generic Object List Load
        public StateIs GenericListLoadResult;
        public StateIs GenericListLoad(
            ref mFileMainDef FmainPassed,
            bool DoClearTargetPassed,
            bool DoGetUiVs,
            String ListNamePassed,
            String FieldNamePassed,
            SelectionList ObjectListPassed
            )
        {
            GenericListLoadResult = StateIs.Started;
            FileState.ObjectListLoading = true;
            if (ObjectListPassed == null)
            {
                ObjectListPassed = new SelectionList();
            }
            else { ObjectListPassed.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check Database List
            //
            String CommandCurrent = sEmpty;
            // CommandCurrent += "USE [" + Faux.Fs.DatabaseName + "]; ";
            //CommandCurrent += "USE master";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT " + FieldNamePassed + " FROM " + ListNamePassed;
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;

            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.SQL;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.Default;
            // ???? FmainPassed.Fs.FileIo.Mode = FileIo_ModeIs.Sql;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Sql;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.Database;
            GenericListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            //if ((GenericListLoadResult & StateIs.MaskSuccessfulAll) > 0) {
            GenericListLoadResult = ObjectListLoad(ref FmainPassed, ref ObjectListPassed.Curr);
            //}
            if (ObjectListPassed.Curr.Count == 0)
            {
                //
                // Check System List
                //
                FmainPassed.Fs.FileId.FileNameLine = StdBaseDef.DriveOs + "\\Data\\" + ListNamePassed + ".ItemList";
                FmainPassed.Fs.FileId.FileNameSetFromLine(null);
                FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
                FmainPassed.FileStatus.bpDoKeepConn = false;
                //
                FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
                FmainPassed.Fs.FileType = FileType_Is.TEXT;
                FmainPassed.Fs.FileSubType = FileType_SubTypeIs.ItemList;
                FmainPassed.Fs.Direction = FileAction_DirectionIs.Input;
                FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Line;
                FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.DiskFile;
                //
                GenericListLoadResult = AsciiFileReadRecord(ref FmainPassed);
                //if ((GenericListLoadResult & StateIs.MaskSuccessfulAll) > 0) {
                SystemListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref ObjectListPassed.Curr);
                //}
            }
            ObjectListPassed.Main = ObjectListPassed.Curr.ToList();
            ObjectList.DropDown = ObjectListPassed.Curr.ToList();
            //
            String GenericDefault = ObjectParamLoad(ref FmainPassed, StdBaseDef.DriveOs + "\\Data\\" + ListNamePassed + "StdDefault" + ".Param");
            if (GenericDefault.Length != 0)
            {
                if (GenericDefault.Length > 0)
                {
                    if (!ObjectListPassed.DropDown.Contains(GenericDefault))
                    {
                        ObjectListPassed.DropDown.Add(GenericDefault);
                    }
                }
            }
            GenericDefault = ObjectParamLoad(ref FmainPassed, StdBaseDef.DriveOs + "\\Data\\" + ListNamePassed + "MdmDefault" + ".Param");
            if (GenericDefault.Length != 0)
            {
                if (GenericDefault.Length > 0)
                {
                    if (!ObjectListPassed.DropDown.Contains(GenericDefault))
                    {
                        ObjectListPassed.DropDown.Add(GenericDefault);
                    }
                }
            }
            if (!ObjectListPassed.DropDown.Contains(ListNamePassed + @"99")) { ObjectListPassed.DropDown.Add(ListNamePassed + @"99"); }
            if (ObjectListPassed.Prev.Count > 0) { ObjectListPassed.DropDown.AddRange(ObjectListPassed.Prev); }
            //
            FileState.ObjectListLoading = false;
            return GenericListLoadResult;
        }

        #endregion
        #region Ascii List Load
        public StateIs ObjectListLoadAsciiResult;
        /// <summary> 
        /// Using the passed standard list, load
        /// the record data of the passed file into the list.
        /// </summary> 
        /// <param name="ItemListPassed">List to load.</param> 
        /// <remarks></remarks> 
        public StateIs ObjectListLoadAscii(ref mFileMainDef FmainPassed, ref List<String> ItemListPassed)
        {
            ObjectListLoadAsciiResult = StateIs.Started;
            ItemListPassed = new List<String>();
            // Object[] Columns = new Object[100];
            System.Type ObjectType;
            try
            {
                for (int ItemIndex = 0; ItemIndex < Mrecord.Items.Length; ItemIndex++)
                {
                    ObjectType = Mrecord.Items[ItemIndex].GetType();
                    // ItemListPassed.Add((String)((String[])(Mrecord.Items[ItemIndex]))[0]);
                    if (ObjectType == typeof(Object[]) || ObjectType == typeof(String[]))
                    {
                        ItemListPassed.Add((String)((String[])(Mrecord.Items[ItemIndex])).First());
                    }
                    else if (ObjectType == typeof(Object) || ObjectType == typeof(String))
                    {
                        ItemListPassed.Add((String)Mrecord.Items[ItemIndex]);
                    }
                }
                if (ItemListPassed.Count > 0)
                {
                    ObjectListLoadAsciiResult = StateIs.Successful;
                }
                else { ObjectListLoadAsciiResult = StateIs.EmptyResult; }
            }
            catch { ObjectListLoadAsciiResult = StateIs.EmptyResult; }
            return ObjectListLoadAsciiResult;
        }
        #endregion
        #region Ascii Parameter Load
        public StateIs ObjectParamLoadResult;
        /// <summary> 
        /// Load the requested parameter from file.
        /// </summary> 
        /// <param name="ParamFullNamePassed">Fully qualified path name to paramater</param> 
        /// <remarks></remarks> 
        public String ObjectParamLoad(ref mFileMainDef FmainPassed, String ParamFullNamePassed)
        {
            ObjectParamLoadResult = StateIs.Started;
            String ParamResult = null;
            //
            // Check System List
            //
            FmainPassed.Fs.FileId.FileNameLine = ParamFullNamePassed;
            // FmainPassed.Fs.FileId.FileNameLine = StdBaseDef.DriveOs + "\\Data\\" + ParamFullNamePassed + ".Param";
            FmainPassed.Fs.FileId.FileNameSetFromLine(null);
            FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
            FmainPassed.FileStatus.bpDoKeepConn = false;
            //
            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.TEXT;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.ItemList;
            FmainPassed.Fs.Direction = FileAction_DirectionIs.Input;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Line;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.DiskFile;
            //
            GenericListLoadResult = AsciiFileReadRecord(ref FmainPassed);
            //if ((GenericListLoadResult & StateIs.MaskSuccessfulAll) > 0) {
            ServerListLoadResult = ObjectParamLoadAscii(ref FmainPassed, ref ParamResult);
            // }
            return ParamResult;
        }

        public StateIs ObjectParamLoadAsciiResult;
        /// <summary> 
        /// Load the passed Parameter Ascii Result
        /// </summary> 
        /// <param name="ItemParamPassed"></param> 
        /// <remarks>Not Finished!!!</remarks> 
        public StateIs ObjectParamLoadAscii(ref mFileMainDef FmainPassed, ref String ItemParamPassed)
        {
            ObjectParamLoadAsciiResult = StateIs.Started;
            ItemParamPassed = null;
            // Object[] Columns = new Object[100];
            System.Type ObjectType;
            try
            {
                for (int ItemIndex = 0; ItemIndex < Mrecord.Items.Length; ItemIndex++)
                {
                    ObjectType = Mrecord.Items[ItemIndex].GetType();
                    // ItemListPassed.Add((String)((String[])(Mrecord.Items[ItemIndex]))[0]);
                    if (ObjectType == typeof(Object[]) || ObjectType == typeof(String[]))
                    {
                        ItemParamPassed = (String)((String[])(Mrecord.Items[ItemIndex])).First();
                        return (ObjectParamLoadAsciiResult = StateIs.Successful);
                    }
                    else if (ObjectType == typeof(Object) || ObjectType == typeof(String))
                    {
                        ItemParamPassed = (String)Mrecord.Items[ItemIndex];
                        return (ObjectParamLoadAsciiResult = StateIs.Successful);
                    }
                }
                ObjectParamLoadAsciiResult = StateIs.Invalid;
            }
            catch { ObjectParamLoadAsciiResult = StateIs.EmptyResult; }
            return ObjectParamLoadAsciiResult;
        }
        #endregion
        #region System Lists Management
        public StateIs SystemListLoadResult;
        /// <summary> 
        /// Load the list of Systems
        /// </summary> 
        /// <param name="DoClearTargetPassed"></param> 
        /// <param name="DoGetUiVs"></param> 
        public StateIs SystemListLoad(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            SystemListLoadResult = StateIs.Started;
            //LocalMessage.LogEntry = 
            //((ImTrace)ConsoleSender).TraceMdmDoImpl(ConsoleFormUses.DatabaseLog, 8, ref Sender, bIsMessage,
            //    TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
            //    FileDoResult, false,
            //    iNoErrorLevel, iNoErrorSource,
            //    bDoNotDisplay, MessageNoUserEntry,
            //    LocalMessage.LogEntry);

            FileState.ObjectListLoading = true;
            if (SystemList.Main == null)
            {
                SystemList.Main = new List<String>();
            }
            else { SystemList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check System List
            //
            FmainPassed.Fs.FileId.FileNameLine = StdBaseDef.DriveOs + "\\System\\System.ItemList";
            FmainPassed.Fs.FileId.FileNameSetFromLine(null);
            FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
            FmainPassed.FileStatus.bpDoKeepConn = false;
            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.TEXT;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.ItemList;
            FmainPassed.Fs.Direction = FileAction_DirectionIs.Input;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Line;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.DiskFile;
            //
            // ToDo fix handling of missing files.
            SystemListLoadResult = AsciiFileReadRecord(ref FmainPassed);
            if ((SystemListLoadResult & StateIs.MaskSuccessfulAll) > 0)
            {
                SystemListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref SystemList.Curr);
            }
            //
            // Empty List Exception
            //
            SystemList.DropDown = SystemList.Curr.ToList();
            SystemList.Main = SystemList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbSystemDefault.Length > 0)
            {
                if (!SystemList.DropDown.Contains(FmainPassed.DbMaster.MstrDbSystemDefault))
                {
                    SystemList.DropDown.Add(FmainPassed.DbMaster.MstrDbSystemDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbSystemDefaultMdm.Length > 0)
            {
                if (!SystemList.DropDown.Contains(FmainPassed.DbMaster.MstrDbSystemDefaultMdm))
                {
                    SystemList.DropDown.Add(FmainPassed.DbMaster.MstrDbSystemDefaultMdm);
                }
            }
            if (!SystemList.DropDown.Contains(@"System99")) { SystemList.DropDown.Add(@"System99"); }
            //
            if (SystemList.Prev.Count > 0 && !SystemList.Prev.SequenceEqual(SystemList.Curr))
            {
                SystemList.DropDown.AddRange(SystemList.Prev);
            }
            //
            FileState.ObjectListLoading = false;
            return SystemListLoadResult;
        }

        /// <summary> 
        /// OnChange in Systems
        /// Reset dependent data.
        /// </summary> 
        public void SystemNameChanged()
        {
            ServiceList.Main = null;
            ServerList.Main = null;
            DatabaseList.Main = null;
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        /// <summary> 
        /// All lists are to be reset.
        /// </summary> 
        public void AllCoreNameChanged()
        {
            SystemList.Main = null;
            ServiceList.Main = null;
            ServerList.Main = null;
            DatabaseList.Main = null;
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        public StateIs SystemListCheckResult;
        /// <summary> 
        /// Check if the System is in the System List.
        /// </summary> 
        /// <param name="DoClearTargetPassed"></param> 
        /// <param name="DoGetUiVs"></param> 
        public StateIs SystemListCheck(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            SystemListCheckResult = StateIs.Started;
            if (SystemList.Main == null || DoClearTargetPassed) { SystemListCheckResult = SystemListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (SystemList.Main.Contains(FmainPassed.Fs.SystemName))
            {
                SystemListCheckResult = StateIs.DoesExist;
            }
            else
            {
                SystemListCheckResult = StateIs.DoesNotExist;
            }
            return SystemListCheckResult;
        }

        public StateIs SystemNameResult;
        /// <summary> 
        /// Get the default System Name.
        /// </summary> 
        public String SystemNameGetDefault(ref mFileMainDef FmainPassed)
        {
            SystemNameResult = StateIs.Started;
            String SystemName = null;
            if (!FileState.ObjectListLoading)
            {
                if (SystemList.Main == null) { SystemNameResult = SystemListLoad(ref FmainPassed, false, false); }
                SystemName = SystemList.Main.FirstOrDefault();
            }
            if (SystemName == null) { SystemName = FmainPassed.DbMaster.MstrDbSystemDefault; }
            if (SystemName.Length == 0) { SystemName = FmainPassed.DbMaster.MstrDbSystemDefaultMdm; }
            if (SystemName.Length == 0) { SystemName = "System99"; }
            // SystemName = @"localhost";
            return SystemName;
        }

        public StateIs SystemListGetResult;
        /// <summary> 
        /// Get the System List.
        /// </summary> 
        /// <param name="DoClearTargetPassed"></param> 
        /// <param name="DoGetUiVs"></param> 
        public List<String> SystemListGetTo(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            SystemListGetResult = StateIs.Started;
            if (SystemList.Main == null || DoClearTargetPassed)
            {
                SystemListGetResult = SystemListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            }
            else
            {
                SystemListGetResult = StateIs.Successful;
            }
            if (DoGetUiVs)
            {
                return SystemList.DropDown;
            }
            else { return SystemList.Main; }
        }
        #endregion
        #region Service Lists & Default
        public StateIs ServiceListLoadResult;
        /// <summary> 
        /// Load the Service List.
        /// </summary> 
        public StateIs ServiceListLoad(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            ServiceListLoadResult = StateIs.Started;
            FileState.ObjectListLoading = true;
            if (ServiceList.Main == null)
            {
                ServiceList.Main = new List<String>();
            }
            else { ServiceList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Load Service List
            //
            FmainPassed.Fs.FileId.FileNameLine = StdBaseDef.DriveOs + "\\Data\\DatabaseService.ItemList";
            FmainPassed.Fs.FileId.FileNameSetFromLine(null);
            FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
            FmainPassed.FileStatus.bpDoKeepConn = false;
            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.TEXT;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.ItemList;
            FmainPassed.Fs.Direction = FileAction_DirectionIs.Input;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Line;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.DiskFile;
            //
            ServiceListLoadResult = AsciiFileReadRecord(ref FmainPassed);
            if ((ServiceListLoadResult & StateIs.MaskSuccessfulAll) > 0)
            {
                ServiceListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref ServiceList.Curr);
            }
            //
            // Empty List Exception
            //
            ServiceList.DropDown = ServiceList.Curr.ToList();
            ServiceList.Main = ServiceList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbServiceDefault.Length > 0)
            {
                if (!ServiceList.DropDown.Contains(FmainPassed.DbMaster.MstrDbServiceDefault))
                {
                    ServiceList.DropDown.Add(FmainPassed.DbMaster.MstrDbServiceDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbServiceDefaultMdm.Length > 0)
            {
                if (!ServiceList.DropDown.Contains(FmainPassed.DbMaster.MstrDbServiceDefaultMdm))
                {
                    ServiceList.DropDown.Add(FmainPassed.DbMaster.MstrDbServiceDefaultMdm);
                }
            }
            if (!ServiceList.DropDown.Contains(@"Service99")) { ServiceList.DropDown.Add(@"Service99"); }
            if (ServiceList.Prev.Count > 0) { ServiceList.DropDown.AddRange(ServiceList.Prev); }
            //
            FileState.ObjectListLoading = false;
            return ServiceListLoadResult;
        }

        public StateIs ServiceNameResult;
        /// <summary> 
        /// Get the default Service Name.
        /// </summary> 
        public String ServiceNameGetDefault(ref mFileMainDef FmainPassed)
        {
            ServiceNameResult = StateIs.Started;
            String ServiceName = null;
            if (!FileState.ObjectListLoading)
            {
                if (ServiceList.Main == null) { ServiceNameResult = ServiceListLoad(ref FmainPassed, false, false); }
                ServiceName = ServiceList.Main.FirstOrDefault();
            }
            if (ServiceName == null) { ServiceName = FmainPassed.DbMaster.MstrDbServiceDefault; }
            if (ServiceName.Length == 0) { ServiceName = FmainPassed.DbMaster.MstrDbServiceDefaultMdm; }
            if (ServiceName.Length == 0) { ServiceName = "Service99"; }
            // ServiceName = @"localhost";
            return ServiceName;
        }

        /// <summary> 
        /// On Service Name changed.
        /// Clear dependent lists.
        /// </summary> 
        public void ServiceNameChanged()
        {
            ServerList.Main = null;
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        public StateIs ServiceListCheckResult;
        /// <summary> 
        /// Check if the Service is in the Service List.
        /// </summary> 
        public StateIs ServiceListCheck(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            ServiceListCheckResult = StateIs.Started;
            if (ServiceList.Main == null || DoClearTargetPassed) { ServiceListCheckResult = ServiceListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (ServiceList.Main.Contains(FmainPassed.Fs.ServiceName))
            {
                ServiceListCheckResult = StateIs.DoesExist;
            }
            else
            {
                ServiceListCheckResult = StateIs.DoesNotExist;
            }
            return ServiceListCheckResult;
        }

        public StateIs ServiceListGetResult;
        /// <summary> 
        /// Get this Service List.
        /// </summary> 
        public List<String> ServiceListGetTo(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            ServiceListGetResult = StateIs.Started;
            if (ServiceList.Main == null || DoClearTargetPassed)
            {
                ServiceListGetResult = ServiceListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            }
            else
            {
                ServiceListGetResult = StateIs.Successful;
            }
            if (DoGetUiVs)
            {
                return ServiceList.DropDown;
            }
            else { return ServiceList.Main; }
        }
        #endregion
        #region Server Lists & Default
        public StateIs ServerListLoadResult;
        /// <summary> 
        /// Load the Server List.
        /// </summary> 
        public StateIs ServerListLoad(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            ServerListLoadResult = StateIs.Started;
            FileState.ObjectListLoading = true;
            if (ServerList.Main == null)
            {
                ServerList.Main = new List<String>();
            }
            else { ServerList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check Server List
            //
            String CommandCurrent = sEmpty;
            CommandCurrent = sEmpty;
            //CommandCurrent += "USE master";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT name FROM sys.servers ";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;
            //
            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.SQL;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.MS;
            if (FmainPassed.Fs.SystemName.Length == 0) { FmainPassed.Fs.SystemName = FmainPassed.DbMaster.MstrDbSystemDefaultMdm; }
            if (FmainPassed.Fs.ServiceName.Length == 0) { FmainPassed.Fs.ServiceName = FmainPassed.DbMaster.MstrDbServiceDefaultMdm; }
            if (FmainPassed.Fs.ServerName.Length == 0) { FmainPassed.Fs.ServerName = FmainPassed.DbMaster.MstrDbServerDefault; }
            // if (FmainPassed.Fs.DatabaseName.Length == 0) { FmainPassed.Fs.DatabaseName = FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm; }
            FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.Default;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Sql;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.Database;
            //
            ServerListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            ServerListLoadResult = ObjectListLoad(ref FmainPassed, ref ServerList.Curr);
            //
            if (ServerList.Curr.Count == 0)
            {
                //
                // Check Text maintained Database Table List
                //
                FmainPassed.Fs.FileId.FileNameLine = StdBaseDef.DriveOs + "\\Data\\DatabaseServer.ItemList";
                FmainPassed.Fs.FileId.FileNameSetFromLine(null);
                FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
                FmainPassed.FileStatus.bpDoKeepConn = false;
                FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
                FmainPassed.Fs.FileType = FileType_Is.TEXT;
                FmainPassed.Fs.FileSubType = FileType_SubTypeIs.ItemList;
                FmainPassed.Fs.Direction = FileAction_DirectionIs.Input;
                FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Line;
                FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.DiskFile;
                //
                ServerListLoadResult = AsciiFileReadRecord(ref FmainPassed);
                if ((ServerListLoadResult & StateIs.MaskSuccessfulAll) > 0)
                {
                    ServerListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref ServerList.Curr);
                }
            }
            //
            // Empty List Exception
            //
            ServerList.DropDown = ServerList.Curr.ToList();
            ServerList.Main = ServerList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbServerDefault.Length > 0)
            {
                if (!ServerList.DropDown.Contains(FmainPassed.DbMaster.MstrDbServerDefault))
                {
                    ServerList.DropDown.Add(FmainPassed.DbMaster.MstrDbServerDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbServerDefaultMdm.Length > 0)
            {
                if (!ServerList.DropDown.Contains(FmainPassed.DbMaster.MstrDbServerDefaultMdm))
                {
                    ServerList.DropDown.Add(FmainPassed.DbMaster.MstrDbServerDefaultMdm);
                }
            }
            if (!ServerList.DropDown.Contains(@"Server99")) { ServerList.DropDown.Add(@"Server99"); }
            //
            if (ServerList.Prev.Count > 0 && !ServerList.Prev.SequenceEqual(ServerList.Curr)) { ServerList.DropDown.AddRange(ServerList.Prev); }
            //
            FileState.ObjectListLoading = false;
            return ServerListLoadResult;
        }

        public StateIs ServerListCheckResult;
        /// <summary> 
        /// Check if the Server is in the Server List.
        /// </summary> 
        public StateIs ServerListCheck(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            ServerListCheckResult = StateIs.Started;
            if (ServerList.Main == null || DoClearTargetPassed) { ServerListCheckResult = ServerListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (ServerList.Main.Contains(FmainPassed.Fs.ServerName))
            {
                ServerListCheckResult = StateIs.DoesExist;
            }
            else
            {
                ServerListCheckResult = StateIs.DoesNotExist;
            }
            return ServerListCheckResult;
        }

        public StateIs ServerNameResult;
        /// <summary> 
        /// Get the default Server Name.
        /// </summary> 
        public String ServerNameGetDefault(ref mFileMainDef FmainPassed)
        {
            ServerNameResult = StateIs.Started;
            String ServerName = null;
            if (!FileState.ObjectListLoading)
            {
                if (ServerList.Main == null) { ServerNameResult = ServerListLoad(ref FmainPassed, false, false); }
                ServerName = ServerList.Main.FirstOrDefault();
            }
            if (ServerName == null)
            {
                if (FmainPassed.Fs.SystemName.Length > 0 && FmainPassed.Fs.ServiceName.Length > 0)
                {
                    ServerName = FmainPassed.Fs.SystemName + @"\" + FmainPassed.Fs.ServiceName;
                }
            }
            if (ServerName == null) { ServerName = FmainPassed.DbMaster.MstrDbServerDefault; }
            if (ServerName.Length == 0) { ServerName = FmainPassed.DbMaster.MstrDbServerDefaultMdm; }
            if (ServerName.Length == 0) { ServerName = "Server99"; }
            // ServerName = @"localhost";
            return ServerName;
        }

        /// <summary> 
        /// On Server Name Changed.
        /// Clear dependent lists.
        /// </summary> 
        public void ServerNameChanged()
        {
            DatabaseList.Main = null;
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        private StateIs ServerListGetResult;
        /// <summary> 
        /// Get the Server List.
        /// </summary> 
        public List<String> ServerListGetTo(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            ServerListGetResult = StateIs.Started;
            if (ServerList.Main == null || DoClearTargetPassed)
            {
                ServerListGetResult = ServerListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            }
            else
            {
                ServerListGetResult = StateIs.Successful;
            }
            if (DoGetUiVs)
            {
                return ServerList.DropDown;
            }
            else { return ServerList.Main; }
        }
        #endregion
        #region Database Lists & Default
        public StateIs DatabaseListLoadResult;
        /// <summary> 
        /// Load the Database List.
        /// </summary> 
        public StateIs DatabaseListLoad(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            DatabaseListLoadResult = StateIs.Started;
            FileState.ObjectListLoading = true;
            if (DatabaseList.Main == null)
            {
                DatabaseList.Main = new List<String>();
            }
            else { DatabaseList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check Database List
            //
            String CommandCurrent = sEmpty;
            // CommandCurrent += "USE [" + Faux.Fs.DatabaseName + "]; ";
            //CommandCurrent += "USE master";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT name FROM sys.databases ";
            CommandCurrent += "WHERE [name] NOT IN ";
            CommandCurrent += "( ";
            CommandCurrent += "'master' , 'msdb', 'model', 'tempdb', ";
            CommandCurrent += "'resource', 'distribution' ";
            CommandCurrent += ")";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;

            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.SQL;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.Default;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Sql;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.Database;
            DatabaseListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            if ((DatabaseListLoadResult & StateIs.MaskSuccessfulAll) > 0)
            {
                DatabaseListLoadResult = ObjectListLoad(ref FmainPassed, ref DatabaseList.Curr);
            }
            if (DatabaseList.Curr.Count == 0)
            {
                //
                // Check System List
                //
                FmainPassed.Fs.FileId.FileNameLine = StdBaseDef.DriveOs + "\\Data\\Database.ItemList";
                FmainPassed.Fs.FileId.FileNameSetFromLine(null);
                FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
                FmainPassed.FileStatus.bpDoKeepConn = false;
                //
                FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
                FmainPassed.Fs.FileType = FileType_Is.TEXT;
                FmainPassed.Fs.FileSubType = FileType_SubTypeIs.ItemList;
                FmainPassed.Fs.Direction = FileAction_DirectionIs.Input;
                FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Line;
                FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.DiskFile;
                //
                DatabaseListLoadResult = AsciiFileReadRecord(ref FmainPassed);
                if ((DatabaseListLoadResult & StateIs.MaskSuccessfulAll) > 0)
                {
                    DatabaseListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref DatabaseList.Curr);
                }
            }
            DatabaseList.Main = DatabaseList.Curr.ToList();
            DatabaseList.DropDown = DatabaseList.Curr.ToList();
            //
            if (FmainPassed.DbMaster.MstrDbDatabaseDefault.Length > 0)
            {
                if (!DatabaseList.DropDown.Contains(FmainPassed.DbMaster.MstrDbDatabaseDefault))
                {
                    DatabaseList.DropDown.Add(FmainPassed.DbMaster.MstrDbDatabaseDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm.Length > 0)
            {
                if (!DatabaseList.DropDown.Contains(FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm))
                {
                    DatabaseList.DropDown.Add(FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm);
                }
            }
            if (!DatabaseList.DropDown.Contains(@"Database99")) { DatabaseList.DropDown.Add(@"Database99"); }
            //
            if (DatabaseList.Prev.Count > 0 && !DatabaseList.Prev.SequenceEqual(DatabaseList.Curr)) { DatabaseList.DropDown.AddRange(DatabaseList.Prev); }
            //
            FileState.ObjectListLoading = false;
            return DatabaseListLoadResult;
        }

        public StateIs DatabaseListCheckResult;
        /// <summary> 
        /// Check if the Database is in the Database List.
        /// </summary> 
        public StateIs DatabaseListCheck(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            DatabaseListCheckResult = StateIs.Started;
            if (DatabaseList.Main == null || DoClearTargetPassed) { DatabaseListCheckResult = DatabaseListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (DatabaseList.Main.Contains(FmainPassed.Fs.DatabaseName))
            {
                DatabaseListCheckResult = StateIs.DoesExist;
            }
            else
            {
                DatabaseListCheckResult = StateIs.DoesNotExist;
            }
            return DatabaseListCheckResult;
        }

        public StateIs DatabaseNameResult;
        /// <summary> 
        /// Get the default Database Name.
        /// </summary> 
        public virtual String DatabaseNameGetDefault(ref mFileMainDef FmainPassed)
        {
            DatabaseNameResult = StateIs.Started;
            String DatabaseName = null;
            if (!FileState.ObjectListLoading)
            {
                if (DatabaseList.Main == null) { DatabaseNameResult = DatabaseListLoad(ref FmainPassed, false, false); }
                DatabaseName = DatabaseList.Main.FirstOrDefault();
            }
            if (DatabaseName == null) { DatabaseName = FmainPassed.DbMaster.MstrDbDatabaseDefault; }
            if (DatabaseName.Length == 0) { DatabaseName = FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm; }
            if (DatabaseName.Length == 0) { DatabaseName = @"Database99"; }
            return DatabaseName;
        }

        /// <summary> 
        /// On Database Name changed
        /// Clear dependent lists.
        /// </summary> 
        public void DatabaseNameChanged()
        {
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        public StateIs DatabaseListGetResult;
        /// <summary> 
        /// Get the Database List.
        /// </summary> 
        public List<String> DatabaseListGetTo(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            DatabaseListGetResult = StateIs.Started;
            if (DatabaseList.Main == null || DoClearTargetPassed)
            {
                DatabaseListGetResult = DatabaseListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            }
            else
            {
                DatabaseListGetResult = StateIs.Successful;
            }
            if (DoGetUiVs)
            {
                return DatabaseList.DropDown;
            }
            else { return DatabaseList.Main; }
        }
        #endregion
        #region File Owner Lists & Default
        public StateIs FileOwnerListLoadResult;
        /// <summary> 
        /// Load the File Owner List.
        /// </summary> 
        public StateIs FileOwnerListLoad(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            FileOwnerListLoadResult = StateIs.Started;
            FileState.ObjectListLoading = true;
            if (FileOwnerList.Main == null)
            {
                FileOwnerList.Main = new List<String>();
            }
            else { FileOwnerList.PrepareNext(); }
            //            FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check Database Table List
            //
            String CommandCurrent = sEmpty;
            //CommandCurrent += "USE [" + "master" + "]";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT * FROM sys.sysusers WHERE ";
            CommandCurrent += "NAME = 'dbo' OR ";
            CommandCurrent += "NAME NOT IN ( ";
            CommandCurrent += "SELECT NAME FROM sys.sysusers WHERE ";
            CommandCurrent += "NAME LIKE 'db%' ";
            CommandCurrent += "OR NAME LIKE '##%' ";
            // HACK
            // CommandCurrent += "OR NAME = 'INFORMATION_SCHEMA' ";
            // CommandCurrent += "OR NAME = 'public' ";
            CommandCurrent += "OR NAME LIKE 'INFORMATION_SCHEMA' ";
            CommandCurrent += "OR NAME LIKE 'public' ";
            CommandCurrent += ")";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;
            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.SQL;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.Default;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Sql;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.Database;
            //
            FileOwnerListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            if ((FileOwnerListLoadResult & StateIs.MaskSuccessfulAll) > 0)
            {
                FileOwnerListLoadResult = ObjectListLoad(ref FmainPassed, ref FileOwnerList.Curr);
            }
            //
            if (FileOwnerList.Curr.Count == 0)
            {
                //
                // Check Text maintained Database Table List
                //
                FmainPassed.Fs.FileId.FileNameLine = StdBaseDef.DriveOs + "\\Data\\DatabaseFileOwner.ItemList";
                FmainPassed.Fs.FileId.FileNameSetFromLine(null);
                FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
                FmainPassed.FileStatus.bpDoKeepConn = false;
                FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
                FmainPassed.Fs.FileType = FileType_Is.TEXT;
                FmainPassed.Fs.FileSubType = FileType_SubTypeIs.ItemList;
                FmainPassed.Fs.Direction = FileAction_DirectionIs.Input;
                FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Line;
                FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.DiskFile;
                //
                FileOwnerListLoadResult = AsciiFileReadRecord(ref FmainPassed);
                if ((FileOwnerListLoadResult & StateIs.MaskSuccessfulAll) > 0)
                {
                    FileOwnerListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref FileOwnerList.Curr);
                }
            }
            //
            FileOwnerList.DropDown = FileOwnerList.Curr.ToList();
            FileOwnerList.Main = FileOwnerList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbOwnerDefault.Length > 0)
            {
                if (!FileOwnerList.DropDown.Contains(FmainPassed.DbMaster.MstrDbOwnerDefault))
                {
                    FileOwnerList.DropDown.Add(FmainPassed.DbMaster.MstrDbOwnerDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbOwnerDefaultMdm.Length > 0)
            {
                if (!FileOwnerList.DropDown.Contains(FmainPassed.DbMaster.MstrDbOwnerDefaultMdm))
                {
                    FileOwnerList.DropDown.Add(FmainPassed.DbMaster.MstrDbOwnerDefaultMdm);
                }
            }
            if (!FileOwnerList.DropDown.Contains(@"dbo")) { FileOwnerList.DropDown.Add(@"dbo"); }
            //
            if (FileOwnerList.Prev.Count > 0 && !FileOwnerList.Prev.SequenceEqual(FileOwnerList.Curr)) { FileOwnerList.DropDown.AddRange(FileOwnerList.Prev); }
            //
            FileState.ObjectListLoading = false;
            return FileOwnerListLoadResult;
        }

        public StateIs FileOwnerListCheckResult;
        /// <summary> 
        /// Check if the File Owner is in the File Owner List.
        /// </summary> 
        public StateIs FileOwnerListCheck(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            FileOwnerListCheckResult = StateIs.Started;
            if (FileOwnerList.Main == null || DoClearTargetPassed) { FileOwnerListCheckResult = FileOwnerListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (FileOwnerList.Main.Contains(FmainPassed.Fs.FileOwnerName))
            {
                FileOwnerListCheckResult = StateIs.DoesExist;
            }
            else
            {
                FileOwnerListCheckResult = StateIs.DoesNotExist;
            }
            return FileOwnerListCheckResult;
        }

        public StateIs FileOwnerResult;
        /// <summary> 
        /// Get the default File Owner.
        /// </summary> 
        public virtual String FileOwnerGetDefault(ref mFileMainDef FmainPassed)
        {
            FileOwnerResult = StateIs.Started;
            String FileOwner = null;
            if (!FileState.ObjectListLoading)
            {
                if (FileOwnerList.Main == null) { FileOwnerResult = FileOwnerListLoad(ref FmainPassed, false, false); }
                FileOwner = FileOwnerList.Main.FirstOrDefault();
            }
            if (FileOwner == null) { FileOwner = FmainPassed.DbMaster.MstrDbOwnerDefault; }
            if (FileOwner.Length == 0) { FileOwner = FmainPassed.DbMaster.MstrDbOwnerDefaultMdm; }
            if (FileOwner.Length == 0) { FileOwner = "dbo"; }
            return FileOwner;
        }

        public StateIs FileOwnerListGetResult;
        /// <summary> 
        /// Get the File Owner List.
        /// </summary> 
        public List<String> FileOwnerListGetTo(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            FileOwnerListGetResult = StateIs.Started;
            if (FileOwnerList.Main == null || DoClearTargetPassed)
            {
                FileOwnerListGetResult = FileOwnerListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            }
            else
            {
                FileOwnerListGetResult = StateIs.Successful;
            }
            if (DoGetUiVs)
            {
                return FileOwnerList.DropDown;
            }
            else { return FileOwnerList.Main; }
        }
        #endregion
        #region File Table Lists & Default
        // File Table (Database)
        public StateIs FileNameResult;
        /// <summary> 
        /// Get the default File Name.
        /// </summary> 
        public virtual String FileNameGetDefault(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            FileNameResult = StateIs.Started;
            return "File99.Txt";
        }
        #endregion
        #region Table Lists & Default
        public StateIs TableListLoadResult;
        /// <summary> 
        /// Load the Table List.
        /// </summary> 
        public StateIs TableListLoad(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            TableListLoadResult = StateIs.Started;
            FileState.ObjectListLoading = true;
            if (TableList.Main == null)
            {
                TableList.Main = new List<String>();
            }
            else { TableList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            if (FmainPassed.Fs.DatabaseName.Length == 0)
            {
                return (TableListLoadResult = StateIs.EmptyValue);
            }
            //
            if (FmainPassed.Fs.TableNameLine.Length == 0) { FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed); }
            //
            String CommandCurrent = sEmpty;
            //CommandCurrent += "USE " + FmainPassed.Fs.DatabaseName + sEmpty;
            //CommandCurrent += ";";
            CommandCurrent += "SELECT name FROM sys.tables";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;
            // FmainPassed.DbIo.CommandCurrent = "USE [" + FmainPassed.Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FmainPassed.Fs.FileId.FileName + "'";
            // FmainPassed.DbIo.CommandCurrent = "USE[" + FmainPassed.Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.FileNameFull + "'";
            // FmainPassed.DbIo.CommandCurrent = "USE[" + FmainPassed.Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.FileNameFull + "';";
            // FmainPassed.DbIo.CommandCurrent = "USE[" + FmainPassed.Fs.DatabaseName + "]; SELECT * FROM sys.objects WHERE name = " + "'" + FileId.FileNameFull + "';";
            // SQL = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=MyTable";
            // int result = this.ExecuteQuery("if exists(select * from sys.databases where name = {0}", DatabaseName) return 1 else 0");
            // \r\n"; FROM INFORMATION_SCHEMA.TABLES

            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.SQL;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.Default;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Sql;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.Database;
            TableListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            if ((TableListLoadResult & StateIs.MaskSuccessfulAll) > 0)
            {
                TableListLoadResult = ObjectListLoad(ref FmainPassed, ref TableList.Curr);
            }
            TableList.DropDown = TableList.Curr.ToList();
            TableList.Main = TableList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbTableDefault.Length > 0)
            {
                if (!TableList.DropDown.Contains(FmainPassed.DbMaster.MstrDbTableDefault))
                {
                    TableList.DropDown.Add(FmainPassed.DbMaster.MstrDbTableDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbTableDefaultMdm.Length > 0)
            {
                if (!TableList.DropDown.Contains(FmainPassed.DbMaster.MstrDbTableDefaultMdm))
                {
                    TableList.DropDown.Add(FmainPassed.DbMaster.MstrDbTableDefaultMdm);
                }
            }
            if (!TableList.DropDown.Contains(@"Table99")) { TableList.DropDown.Add(@"Table99"); }
            //
            if (TableList.Prev.Count > 0 && !TableList.Prev.SequenceEqual(TableList.Curr)) { TableList.DropDown.AddRange(TableList.Prev); }
            //
            FileState.ObjectListLoading = false;
            return TableListLoadResult;
        }

        public StateIs TableListCheckResult;
        /// <summary> 
        /// Check if the Table is in the Table List.
        /// </summary> 
        public StateIs TableListCheck(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            TableListCheckResult = StateIs.Started;
            if (FmainPassed.Fs.DatabaseName.Length == 0)
            {
                TableListCheckResult = StateIs.EmptyValue;
            }
            else
            {
                if (TableList.Main == null || DoClearTargetPassed) { TableListCheckResult = TableListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
                if (TableList.Main.Contains(FmainPassed.Fs.TableName))
                {
                    TableListCheckResult = StateIs.DoesExist;
                }
                else
                {
                    TableListCheckResult = StateIs.DoesNotExist;
                }
            }
            return TableListCheckResult;
        }

        public StateIs TableResult;
        /// <summary> 
        /// Get the default Table Name.
        /// </summary> 
        public String TableGetDefault(ref mFileMainDef FmainPassed)
        {
            TableResult = StateIs.Started;
            String Table = null;
            if (!FileState.ObjectListLoading)
            {
                if (TableList.Main == null) { TableResult = TableListLoad(ref FmainPassed, false, false); }
                Table = TableList.Main.FirstOrDefault();
            }
            if (Table == null) { Table = "Table99"; }
            return Table;
        }

        public StateIs TableListGetResult;
        /// <summary> 
        /// Get the Table List.
        /// </summary> 
        public List<String> TableListGetTo(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            TableListGetResult = StateIs.Started;
            if (TableList.Main == null || DoClearTargetPassed)
            {
                TableListGetResult = TableListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            }
            else
            {
                TableListGetResult = StateIs.Successful;
            }
            if (DoGetUiVs)
            {
                return TableList.DropDown;
            }
            else { return TableList.Main; }
        }
        #endregion

        #region FileGroup Lists & Default
        public StateIs FileGroupListLoadResult;
        /// <summary> 
        /// Load the File Group List.
        /// </summary> 
        public StateIs FileGroupListLoad(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            FileGroupListLoadResult = StateIs.Started;
            FileState.ObjectListLoading = true;
            if (FileGroupList.Main == null)
            {
                FileGroupList.Main = new List<String>();
            }
            else { FileGroupList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            if (FmainPassed.Fs.DatabaseName.Length == 0)
            {
                return (FileGroupListLoadResult = StateIs.EmptyValue);
            }
            if (FmainPassed.Fs.TableNameLine.Length == 0)
            {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
            }
            String CommandCurrent = sEmpty;
            CommandCurrent = sEmpty;
            CommandCurrent += "USE [" + FmainPassed.Fs.DatabaseName + "]";
            CommandCurrent += ";";
            CommandCurrent += "SELECT name FROM sys.filegroups ";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;

            FmainPassed.Fs.MetaLevelId = FileType_LevelIs.Data;
            FmainPassed.Fs.FileType = FileType_Is.SQL;
            FmainPassed.Fs.FileSubType = FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.Default;
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Sql;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.Database;
            //
            FileGroupListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            if ((FileGroupListLoadResult & StateIs.MaskSuccessfulAll) > 0)
            {
                FileGroupListLoadResult = ObjectListLoad(ref FmainPassed, ref FileGroupList.Curr);
            }
            FileGroupList.DropDown = FileGroupList.Curr.ToList();
            FileGroupList.Main = FileGroupList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbFileGroupDefault.Length > 0)
            {
                if (!FileGroupList.DropDown.Contains(FmainPassed.DbMaster.MstrDbFileGroupDefault))
                {
                    FileGroupList.DropDown.Add(FmainPassed.DbMaster.MstrDbFileGroupDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbFileGroupDefaultMdm.Length > 0)
            {
                if (!FileGroupList.DropDown.Contains(FmainPassed.DbMaster.MstrDbFileGroupDefaultMdm))
                {
                    FileGroupList.DropDown.Add(FmainPassed.DbMaster.MstrDbFileGroupDefaultMdm);
                }
            }
            if (!FileGroupList.DropDown.Contains(@"FileGroup99")) { FileGroupList.DropDown.Add(@"FileGroup99"); }
            //
            if (FileGroupList.Prev.Count > 0 && !FileGroupList.Prev.SequenceEqual(FileGroupList.Curr)) { FileGroupList.DropDown.AddRange(FileGroupList.Prev); }
            //
            FileState.ObjectListLoading = false;
            return FileGroupListLoadResult;
        }

        public StateIs FileGroupListCheckResult;
        /// <summary> 
        /// Check if the File Group is in the File Group List.
        /// </summary> 
        public StateIs FileGroupListCheck(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            FileGroupListCheckResult = StateIs.Started;
            if (FmainPassed.Fs.DatabaseName.Length == 0)
            {
                FileGroupListCheckResult = StateIs.EmptyValue;
            }
            else
            {
                if (FileGroupList.Main == null || DoClearTargetPassed) { FileGroupListCheckResult = FileGroupListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
                if (FileGroupList.Main.Contains(FmainPassed.Fs.FileGroupName))
                {
                    FileGroupListCheckResult = StateIs.DoesExist;
                }
                else
                {
                    FileGroupListCheckResult = StateIs.DoesNotExist;
                }
            }
            return FileGroupListCheckResult;
        }

        public StateIs FileGroupResult;
        /// <summary> 
        /// Get the default File Group.
        /// </summary> 
        public virtual String FileGroupGetDefault(ref mFileMainDef FmainPassed)
        {
            FileGroupResult = StateIs.Started;
            String FileGroupName = null;
            if (!FileState.ObjectListLoading)
            {
                if (FileGroupList.Main == null) { FileGroupResult = FileGroupListLoad(ref FmainPassed, false, false); }
                FileGroupName = FileGroupList.Main.FirstOrDefault();
            }
            if (FileGroupName == null) { FileGroupName = FmainPassed.DbMaster.MstrDbFileGroupDefault; }
            if (FileGroupName.Length == 0) { FileGroupName = FmainPassed.DbMaster.MstrDbFileGroupDefaultMdm; }
            // if (FileGroupName.Length == 0) { FileGroupName = "FileGroup99"; }
            // FileGroupName = @"localhost";
            return FileGroupName;
        }

        public StateIs FileGroupListGetResult;
        /// <summary> 
        /// Get the File Group List.
        /// </summary> 
        public List<String> FileGroupListGetTo(ref mFileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs)
        {
            FileGroupListGetResult = StateIs.Started;
            if (FileGroupList.Main == null || DoClearTargetPassed)
            {
                FileGroupListGetResult = FileGroupListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            }
            else
            {
                FileGroupListGetResult = StateIs.Successful;
            }
            if (DoGetUiVs)
            {
                return FileGroupList.DropDown;
            }
            else { return FileGroupList.Main; }
        }
        #endregion
        #endregion
    }
}
