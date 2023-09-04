using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mdm.Oss.File.Db.Data
{
    public interface iDbRunData
    {
        // static void GetAppendedFieldNameList(ref List<string> FieldList, ref string FieldListText);
        object GetFieldData(String FieldNamePassed, object DataOject);
        string GetFieldName(Object FieldObject);
        string GetFieldValue(string FieldName, object DataOject);

    }
}
