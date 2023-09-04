using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
// using System.Threading.Tasks; // Not in Net35

namespace Mdm.Oss.Std
{
    public class StdResult
    {
        // StateIs is an enum.
        // How does that work?
        public StateIs StateCurrent;
        public String StdResultMessage;
        public StdResult()
        {
            StateCurrent = StateIs.NotSet;
            StdResultMessage = "Not Set";

        }
        public StdResult(StateIs StateIsPassed)
        {
            StateCurrent = StateIsPassed;
            // StdResultMessage = "Not Set";
        }
        public StdResult(String StateMessagePassed)
        {
            // StateCurrent = StateIs.NotSet;
            StdResultMessage = StateMessagePassed;
        }
    }
    public class ObjectResultDef
    {
        public StateIs StdResultLong;
        public int Id;
        public string ItemId;
        public object ObjectResult;
        public bool NewRecord;

        public ObjectResultDef()
        {
            StdResultLong = StateIs.NotSet;
            Id = -1;
            ItemId = "";
            NewRecord = false;
        }
    }

}
