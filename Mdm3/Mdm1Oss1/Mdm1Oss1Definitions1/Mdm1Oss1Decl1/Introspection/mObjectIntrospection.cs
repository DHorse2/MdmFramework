using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mdm.Oss.Std;

namespace Mdm.Oss.Decl
{
    class mObjectIntrospection
    {
        #region Class Hierarchy
        protected const string sUnknown = "unknown";
        protected const string sEmpty = "";
        protected const int iUnknown = 99999;
        #region System properties
        private int zMdmSystemId = iUnknown;
        public int MdmSystemId { get { return zMdmSystemId; } set { zMdmSystemId = value; } }
        private String zMdmSystemName = sUnknown;
        public String MdmSystemName { get { return zMdmSystemName; } set { zMdmSystemName = value; } }
        private String zMdmSystemTitle = sUnknown;
        public String MdmSystemTitle { get { return zMdmSystemTitle; } set { zMdmSystemTitle = value; } }
        private int zMdmSystemNumber = iUnknown;
        public int MdmSystemNumber { get { return zMdmSystemNumber; } set { zMdmSystemNumber = value; } }
        private StateIs zMdmSystemIntStatus = StateIs.NotSet;
        public StateIs MdmSystemIntStatus { get { return zMdmSystemIntStatus; } set { zMdmSystemIntStatus = value; } }
        private String zMdmSystemStatusText = sUnknown;
        public String MdmSystemStatusText { get { return zMdmSystemStatusText; } set { zMdmSystemStatusText = value; } }
        private StateIs zMdmSystemIntResult = StateIs.NotSet;
        public StateIs MdmSystemIntResult { get { return zMdmSystemIntResult; } set { zMdmSystemIntResult = value; } }
        private bool zMdmSystemResult = false;
        public bool MdmSystemResult { get { return zMdmSystemResult; } set { zMdmSystemResult = value; } }
        private bool zMdmServerResult = false;
        public bool MdmServerResult { get { return zMdmServerResult; } set { zMdmServerResult = value; } }
        #endregion
        private StdProcessDef StdProcess;
        #region Class properties
        private int zMdmClassId = iUnknown;
        public int MdmClassId { get { return zMdmClassId; } set { zMdmClassId = value; } }
        private String zMdmClassName = sUnknown;
        public String MdmClassName { get { return zMdmClassName; } set { zMdmClassName = value; } }
        private String zMdmClassTitle = sUnknown;
        public String MdmClassTitle { get { return zMdmClassTitle; } set { zMdmClassTitle = value; } }
        private int zMdmClassNumber = iUnknown;
        public int MdmClassNumber { get { return zMdmClassNumber; } set { zMdmClassNumber = value; } }
        private StateIs zMdmClassIntStatus = StateIs.NotSet;
        public StateIs MdmClassIntStatus { get { return zMdmClassIntStatus; } set { zMdmClassIntStatus = value; } }
        private String zMdmClassStatusText = sUnknown;
        public String MdmClassStatusText { get { return zMdmClassStatusText; } set { zMdmClassStatusText = value; } }
        private StateIs zMdmClassResult = StateIs.NotSet;
        public StateIs MdmClassResult { get { return zMdmClassResult; } set { zMdmClassResult = value; } }
        private bool zMdmClassBoolResult = false;
        public bool MdmClassBoolResult { get { return zMdmClassBoolResult; } set { zMdmClassBoolResult = value; } }
        #endregion
        #region Method properties
        private int zMdmMethodId = iUnknown;
        public int MdmMethodId { get { return zMdmMethodId; } set { zMdmMethodId = value; } }
        private String zMdmMethodName = sUnknown;
        public String MdmMethodName { get { return zMdmMethodName; } set { zMdmMethodName = value; } }
        private String zMdmMethodTitle = sUnknown;
        public String MdmMethodTitle { get { return zMdmMethodTitle; } set { zMdmMethodTitle = value; } }
        private int zMdmMethodNumber = iUnknown;
        public int MdmMethodNumber { get { return zMdmMethodNumber; } set { zMdmMethodNumber = value; } }
        private StateIs zMdmMethodStatus = StateIs.NotSet;
        public StateIs MdmMethodStatus { get { return zMdmMethodStatus; } set { zMdmMethodStatus = value; } }
        private String zMdmMethodStatusText = sUnknown;
        public String MdmMethodStatusText { get { return zMdmMethodStatusText; } set { zMdmMethodStatusText = value; } }
        private StateIs zMdmMethodResult = StateIs.NotSet;
        public StateIs MdmMethodResult { get { return zMdmMethodResult; } set { zMdmMethodResult = value; } }
        private bool zMdmMethodBoolResult = false;
        public bool MdmMethodBoolResult { get { return zMdmMethodBoolResult; } set { zMdmMethodBoolResult = value; } }
        #endregion
        #region Attribute properties
        private int zMdmAttributeId = iUnknown;
        public int MdmAttributeId { get { return zMdmAttributeId; } set { zMdmAttributeId = value; } }
        private String zMdmAttributeName = sUnknown;
        public String MdmAttributeName { get { return zMdmAttributeName; } set { zMdmAttributeName = value; } }
        private String zMdmAttributeTitle = sUnknown;
        public String MdmAttributeTitle { get { return zMdmAttributeTitle; } set { zMdmAttributeTitle = value; } }
        private int zMdmAttributeNumber = iUnknown;
        public int MdmAttributeNumber { get { return zMdmAttributeNumber; } set { zMdmAttributeNumber = value; } }
        private StateIs zMdmAttributeStatus = StateIs.NotSet;
        public StateIs MdmAttributeStatus { get { return zMdmAttributeStatus; } set { zMdmAttributeStatus = value; } }
        private String zMdmAttributeStatusText = sUnknown;
        public String MdmAttributeStatusText { get { return zMdmAttributeStatusText; } set { zMdmAttributeStatusText = value; } }
        private StateIs zMdmAttributeIntResult = StateIs.NotSet;
        public StateIs MdmAttributeIntResult { get { return zMdmAttributeIntResult; } set { zMdmAttributeIntResult = value; } }
        private bool zMdmAttributeBoolResult = false;
        public bool MdmAttributeBoolResult { get { return zMdmAttributeBoolResult; } set { zMdmAttributeBoolResult = value; } }
        #endregion
        #region Parameter properties
        private int zMdmParameterId = iUnknown;
        public int MdmAuthorParameterId { get { return zMdmParameterId; } set { zMdmParameterId = value; } }
        private String zMdmParameterName = sUnknown;
        public String MdmParameterName { get { return zMdmParameterName; } set { zMdmParameterName = value; } }
        private int zMdmParameterNumber = iUnknown;
        public int MdmParameterNumber { get { return zMdmParameterNumber; } set { zMdmParameterNumber = value; } }
        private String zMdmParameterTitle = sUnknown;
        public String MdmParameterTitle { get { return zMdmParameterTitle; } set { zMdmParameterTitle = value; } }
        private StateIs zMdmParameterStatus = StateIs.NotSet;
        public StateIs MdmParameterStatus { get { return zMdmParameterStatus; } set { zMdmParameterStatus = value; } }
        private String zMdmParameterStatusText = sUnknown;
        public String MdmParameterStatusText { get { return zMdmParameterStatusText; } set { zMdmParameterStatusText = value; } }
        private StateIs zMdmParameterIntResult = StateIs.NotSet;
        public StateIs MdmParameterIntResult { get { return zMdmParameterIntResult; } set { zMdmParameterIntResult = value; } }
        private bool zMdmParameterBoolResult = false;
        public bool MdmParameterBoolResult { get { return zMdmParameterBoolResult; } set { zMdmParameterBoolResult = value; } }
        #endregion
        #region Property properties
        private int zMdmPropertyId = iUnknown;
        public int MdmAuthorPropertyId { get { return zMdmPropertyId; } set { zMdmPropertyId = value; } }
        private String zMdmPropertyName = sUnknown;
        public String MdmPropertyName { get { return zMdmPropertyName; } set { zMdmPropertyName = value; } }
        private int zMdmPropertyNumber = iUnknown;
        public int MdmPropertyNumber { get { return zMdmPropertyNumber; } set { zMdmPropertyNumber = value; } }
        private String zMdmPropertyTitle = sUnknown;
        public String MdmPropertyTitle { get { return zMdmPropertyTitle; } set { zMdmPropertyTitle = value; } }
        private StateIs zMdmPropertyStatus = StateIs.NotSet;
        public StateIs MdmPropertyStatus { get { return zMdmPropertyStatus; } set { zMdmPropertyStatus = value; } }
        private String zMdmPropertyStatusText = sUnknown;
        public String MdmPropertyStatusText { get { return zMdmPropertyStatusText; } set { zMdmPropertyStatusText = value; } }
        private StateIs zMdmPropertyIntResult = StateIs.NotSet;
        public StateIs MdmPropertyIntResult { get { return zMdmPropertyIntResult; } set { zMdmPropertyIntResult = value; } }
        private bool zMdmPropertyBoolResult = false;
        public bool MdmPropertyBoolResult { get { return zMdmPropertyBoolResult; } set { zMdmPropertyBoolResult = value; } }
        #endregion
        #endregion

    }
}
