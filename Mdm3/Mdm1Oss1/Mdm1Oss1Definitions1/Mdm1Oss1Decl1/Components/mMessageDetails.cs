using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mdm;
using Mdm.Oss.Decl;
using Mdm.Oss.Std;

namespace Mdm.Oss.Components
{
    #region MessageDetailsDef
    /// <summary>
    /// <para> CLASS MDM MESSAGE</para> 
    /// <para> The message object common to the Trace, Logging, 
    /// Error and Messaging components.  This single object
    /// design is intended for single, multi-threaded, or event
    /// based implementation and might be passed as an Args
    /// object.</para>   
    /// <para> . </para>
    /// <para> This object includes features for user input, there
    /// are plans to implement (console) green screen features.</para> 
    /// <para> . </para>
    /// <para> It currently supports the prefixed message type
    /// marshalling used in multi-threaded user interface / 
    /// background worker scenarios and not all messages are
    /// bound for the user interface.</para> 
    /// </summary> 
    /// <remarks>
    /// ToDo mMsgDetailsDef InProgress Message Class: 
    /// ToDo implement this soon and eliminate verbosity via defaulting strategy.
    /// ToDo not implemented yet 202101 Dgh ToDo I think it is now implemented?
    /// </remarks> 
    public class mMsgDetailsDef
    {
        #region Declarations
        // Current Trace Parameters:
        public object Sender;
        //
        public long MessageId;

        public int Verbosity;
        public ConsoleFormUses ConsoleFormUse;

        public long ErrorId;
        public long ErrorIdStd;
        public long ResponseId;
        public StateIs ErrorIdStdState;
        //
        // Errors:
        public int Level;
        public int Source;
        // I assume this is a return result, HRESULT, exception
        // or legacy OS error code.
        public long ErrorCode;
        public double ErrorInnerExceptionId;
        //
        // Externally Set Control Fields: 
        // (Callback routing) (Embedded Fields in Text)
        public String StatusAction;
        public int StatusTarget;
        public int StatusSubTarget;
        public String UserState; // from StatusLineMessage.
        // ToDo mMsgDetailsDef StatusLine Handling will not be
        // ToDo combined with Trace Messaging. / Hacks go here?
        //
        // Application Details: Numeric Location from app 
        public string Location1;
        public string Location2;
        // Application Details: Return result
        public StateIs MethodResult;
        // Basic Message type:
        public bool IsMessage;
        public bool IsError;
        // Type of response to ask for
        // (if any):
        public long ResponseFlags;
        // Logging vs. display:
        public bool DoDisplay;
        public int DisplayFlags;
        // Message Text:
        public String Text;
        #endregion
        /// <summary>
        /// Create a message object for use in the Trace, 
        /// Logging, Error handling and multi-threaded
        /// messaging components.  Includes Status Line, 
        /// Console and Completion Progress messages.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public mMsgDetailsDef()
        {
            Verbosity = (int)ConsoleFormUses.DebugLog;
            ConsoleFormUse = ConsoleFormUses.ErrorLog;
            MessageId = 0;
            ErrorId = 0;
            ResponseId = 0;
            //
            ErrorCode = 0;
            ErrorInnerExceptionId = 0;
            // Current Trace Parameters:
            Sender = null;
            Location1 = "";
            Location2 = "";
            MethodResult = 0;
            IsError = false;
            Level = 0;
            Source = 0;
            DoDisplay = false;
            DisplayFlags = 0;
            Text = "";
            //
        }
    }
    #endregion
    #region Message Mdd To Page Arguments
    /// <summary>
    /// <para> This argument object is used to pass
    /// messages to the user interface.  It is not
    /// yet implemented and might be combined with
    /// the TraceMdm Message object.</para>
    /// <para> .</para>
    /// <para> Note that where possible, the
    /// method signature (Sender, Arguments)
    /// or (Sender, Object) will be employed.
    /// Arguments is either an aggregation of
    /// objects or a limitied part of the
    /// source object that the called method
    /// acts upon.</para>
    /// </summary>
    public class mMsgSendToPageArgsDef : EventArgs
    {
        public mMsgSendToPageArgsDef(String PassedMessageToPage)
        {
            MessageToPage = PassedMessageToPage;
        }
        #region Declarations
        private String spMessageToPage;
        public String MessageToPage
        {
            get { return spMessageToPage; }
            set { spMessageToPage = value; }
        }
        private bool spNewLine;
        public bool NewLine
        {
            get { return spNewLine; }
            set { spNewLine = value; }
        }
        private String spMessageFrom;
        public String MessageFrom
        {
            get { return spMessageFrom; }
            set { spMessageFrom = value; }
        }
        private String spMessageTo;
        public String MessageTo
        {
            get { return spMessageTo; }
            set { spMessageTo = value; }
        }
        private int spIndent;
        public int Indent
        {
            get { return spIndent; }
            set { spIndent = value; }
        }
        private int spErrorLevel;
        public int ErrorLevel
        {
            get { return spErrorLevel; }
            set { spErrorLevel = value; }
        }
        private String spRunAction;
        public String RunAction
        {
            get { return spRunAction; }
            set { spRunAction = value; }
        }
        private int spLine;
        public int Line
        {
            get { return spLine; }
            set { spLine = value; }
        }
        private int spColumn;
        public int Column
        {
            get { return spColumn; }
            set { spColumn = value; }
        }
        private String spEtc;
        public String Etc
        {
            get { return spEtc; }
            set { spEtc = value; }
        }
    }
    #endregion
    #endregion
    #region Class Local Messages
    /// <summary> 
    /// A class to contain a hierarchy or set of ten messages.
    /// It's more of a temporary skelton/place holder.
    /// </summary> 
    /// <remarks>
    /// This might be expanded (include) to be a standard list type
    /// in that the messages can be working values, a stack, each
    /// message belonging to a specific location or function, tied to
    /// a purporse, etc.
    /// </remarks> 
    public class LocalMsgDef
    {
        private const string emptyString = "";

        // Local Messages
        public volatile String Msg;
        public volatile String Msg0;
        public volatile String Msg1;
        public volatile String Msg2;
        public volatile String Msg3;
        public volatile String Msg4;
        public volatile String Msg5;
        public volatile String Msg6;
        public volatile String Msg7;
        public volatile String Msg8;
        public volatile String Msg9;
        // ToDo make these properties, ?? possibly routing ??
        // ToDo 202101 Routing and logfiles? Yes.
        public volatile String Header;
        public volatile String LogEntry;
        public volatile String ErrorMsg;
        public LocalMsgDef()
        {
            ErrorMsg = emptyString;
            Msg = emptyString;
            Msg0 = emptyString;
            Msg1 = emptyString;
            Msg2 = emptyString;
            Msg3 = emptyString;
            Msg4 = emptyString;
            Msg5 = emptyString;
            Msg6 = emptyString;
            Msg7 = emptyString;
            Msg8 = emptyString;
            Msg9 = emptyString;
        }
    }
    #endregion
}
