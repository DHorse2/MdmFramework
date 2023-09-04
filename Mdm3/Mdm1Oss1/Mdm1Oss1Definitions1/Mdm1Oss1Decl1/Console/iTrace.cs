using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mdm.Oss.Components;
using Mdm.Oss.Decl;
using Mdm.Oss.Std;

namespace Mdm.Oss.Console
{
    public interface iTrace : iClassFeatures
    {
        void OnTraceMdmDoImpl(object sender, ConsoleMessageEventArgs e);
        // I see no reasone to require these, but you might override them.
        // It seems the fire event is relevent.
        // These were written in the process of documenting the early code.
        // Only one class implements this. StdConsoleManagerDef.
        void TraceMdmDoBasic(String PassedTraceMessage);
        void TraceMdmDoBasic(String PassedTraceMessage, ConsoleFormUses iPassedConsoleType, int iPassedVerbosity);

        void TraceMdmDoDetailed(
            ConsoleFormUses iConsoleType,
            int iPassedVerbosity,
            ref Object Sender,
            bool PassedIsMessage,
            int CharMaxIndexPassed,
            int MethodAttributeMaxPassed,
            StateIs iPassed_MethodResult,
            bool PassedError,
            int iPassedErrorLevel,
            int iPassedErrorSource,
            bool PassedDisplay,
            int iPassedUserEntry,
            String PassedTraceMessage
        );
        void TraceMdmDoImpl(mMsgDetailsDef Message);
        void TraceMdmDoPrintMultiLine(ref Object Sender, String PassedTraceMessage, String PassedTracePrefix);
        void TraceMdmDoPrint(ref Object Sender);
        void TraceMdmDoCall();

    }
}
