using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mdm.Oss.Decl;
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;

namespace Mdm.Oss.Decl
{
    #region Class InternalId properties
    /// <summary>
    /// Class to contain identification (introspection) information for an object.
    /// Identification is viewed in terms of Internal, External and Local scope.
    /// </summary> 
    public class InternalIdDef
    {
        private int ipId;
        public int Id { get { return ipId; } set { ipId = value; } }
        private String spName;
        public String Name { get { return spName; } set { spName = value; } }
        private String spTitle;
        public String Title { get { return spTitle; } set { spTitle = value; } }
        private int ipNumber;
        public int Number { get { return ipNumber; } set { ipNumber = value; } }
        private int ipStatus;
        public int Status { get { return ipStatus; } set { ipStatus = value; } }
        private String spStatusText;
        public String StatusText { get { return spStatusText; } set { spStatusText = value; } }
        private int ipIntResult;
        public int IntResult { get { return ipIntResult; } set { ipIntResult = value; } }
        private bool bpBoolResult;
        public bool BoolResult { get { return bpBoolResult; } set { bpBoolResult = value; } }
        protected const string sUnknown = "unknown";
        protected const string sEmpty = "";
        protected const int iUnknown = 99999;
        public InternalIdDef()
        {
            ipId = iUnknown;
            spName = sUnknown;
            spTitle = sUnknown;
            ipNumber = iUnknown;
            ipStatus = iUnknown;
            spStatusText = sUnknown;
            ipIntResult = iUnknown;
            bpBoolResult = false;
        }
    }
    #endregion
    #region Class ExternalId properties
    /// <summary>
    /// Class to contain identification (introspection) information for an object.
    /// Identification is viewed in terms of Internal, External and Local scope.
    /// </summary> 
    public class ExternalIdDef
    {
        private int ipId;
        public int Id { get { return ipId; } set { ipId = value; } }
        private String spName;
        public String Name { get { return spName; } set { spName = value; } }
        private String spTitle;
        public String Title { get { return spTitle; } set { spTitle = value; } }
        private int ipNumber;
        public int Number { get { return ipNumber; } set { ipNumber = value; } }
        private int ipStatus;
        public int Status { get { return ipStatus; } set { ipStatus = value; } }
        private String spStatusText;
        public String StatusText { get { return spStatusText; } set { spStatusText = value; } }
        private int ipIntResult;
        public int IntResult { get { return ipIntResult; } set { ipIntResult = value; } }
        private bool bpBoolResult;
        public bool BoolResult { get { return bpBoolResult; } set { bpBoolResult = value; } }
        protected const string sUnknown = "unknown";
        protected const string sEmpty = "";
        protected const int iUnknown = 99999;
        public ExternalIdDef()
        {
            ipId = iUnknown;
            spName = sUnknown;
            spTitle = sUnknown;
            ipNumber = iUnknown;
            ipStatus = iUnknown;
            spStatusText = sUnknown;
            ipIntResult = iUnknown;
            bpBoolResult = false;
        }
    }
    #endregion

    #region Class LocalId Result properties
    /// <summary>
    /// Class to contain identification (introspection) information for an object.
    /// Identification is viewed in terms of Internal, External and Local scope.
    /// </summary> 
    public class LocalIdDef
    {
        // Initialization
        public bool Started;
        public bool Running;
        // <Area Id = "CallResults">
        private String spProcessName;
        public String ProcessName { get { return spProcessName; } set { spProcessName = value; } }
        private String spClassName;
        public String ClassName { get { return spClassName; } set { spClassName = value; } }
        private String spPatternName;
        public String PatternName { get { return spPatternName; } set { spPatternName = value; } }
        // Area is refers to area within coding patern
        private String spAreaName; // such as init, main, loop, dispose, open, close, display
        public String AreaName { get { return spAreaName; } set { spAreaName = value; } }
        private String spMethodName;
        public String MethodName { get { return spMethodName; } set { spMethodName = value; } }
        private int ipIntResult;
        public int IntResult { get { return ipIntResult; } set { ipIntResult = value; } }
        private StateIs ipLongResult;
        public StateIs LongResult { get { return ipLongResult; } set { ipLongResult = value; } }
        private String isStringResult;
        public String StringResult { get { return isStringResult; } set { isStringResult = value; } }
        private bool bpBoolResult;
        public bool BoolResult { get { return bpBoolResult; } set { bpBoolResult = value; } }
        private Object opObjectResult;
        public Object ObjectResult { get { return opObjectResult; } set { opObjectResult = (Object)value; } }
        private bool bpObjectDoesExist;
        public bool ObjectDoesExist { get { return bpObjectDoesExist; } set { bpObjectDoesExist = value; } }
        public LocalIdDef() { }
    }
    #endregion



}
