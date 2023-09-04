using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.World {
    public class StatusDef {

    }

    public enum ItemStatusIs : int {
        NotStarted = 0x0000001,
        Pending = 0x0000002,
        Hold = 0x0000004,
        Started = 0x0000008,
        InProgress = 0x0000010,
        Cancelled = 0x0000020,
        Completed = 0x0000040,
        UseAppStatus = 0x8000000
    }

    public class ItemStatusDef {
        public bool NotStarted;
        public bool Pending;
        public bool Hold;
        public bool Started;
        public bool InProgress;
        public bool Cancelled;
        public bool Completed;
        public bool UseAppStatus;

        public ItemStatusDef() {
            NotStarted = false;
            Pending = false;
            Hold = false;
            Started = false;
            InProgress = false;
            Cancelled = false;
            Completed = false;
            UseAppStatus = false;
        }
    }

}
