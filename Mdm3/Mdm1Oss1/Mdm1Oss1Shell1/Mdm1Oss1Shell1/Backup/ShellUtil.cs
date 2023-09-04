using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mdm.Oss.WinUtil;

namespace Mdm.Oss.WinUtil {
    namespace BaseUtil {

        public class ShellBaseFunc {
            WinUtil.Win32ShellDef Win32Shell = new Mdm.Oss.WinUtil.Win32ShellDef();
            WinUtil.Win32MsgDef Win32Msg = new Mdm.Oss.WinUtil.Win32MsgDef();
            WinUtil.Win32ClipDef Win32Clip = new Mdm.Oss.WinUtil.Win32ClipDef();
            WinUtil.Win32FileDef Win32File = new Mdm.Oss.WinUtil.Win32FileDef();
            WinUtil.Win32UrlDef Win32Url = new Mdm.Oss.WinUtil.Win32UrlDef();
            WinUtil.Win32TimeDef Win32Time = new Mdm.Oss.WinUtil.Win32TimeDef();
        }
    }

    namespace FileUtil {
        public class FileUtilDummy {
            int i = 0;
        }

    }
}
