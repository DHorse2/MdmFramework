#region System
using System;
#endregion
using Mdm.Oss.Thread;
namespace Mdm.Oss.File.Db.Data
{
    public interface iDbTask
    {
        void OnTaskDataEvent(object sender, object TaskDataEventObject);
        void OnTaskStatusChanged(object sender, object TaskDataEventObject);
        void OnTaskProgressChanged(object sender, CalculationEventArgs e);
    }
}
