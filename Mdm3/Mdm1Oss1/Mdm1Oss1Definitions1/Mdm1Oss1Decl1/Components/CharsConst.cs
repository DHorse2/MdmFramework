#region Dependencies
using System;
using System.Windows.Forms;
#region  Mdm File Types
using Mdm.Oss.File.Type;
using Mdm.Oss.File.Control;
#endregion
using Mdm.Oss.Std;
#endregion

namespace Mdm.Oss.Decl
{
    /// <summary> 
    /// This set of characters constitutes the special characters
    /// that are in use within all implemented classes.  The mainly
    /// consist of formating, file data separators and punctuation.
    /// There are three instances of delimiter character sets that
    /// are the three most commonly used delimited sets.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public class CharTable : StdDef
    {
        // Pick Delimeters
        public static String Sm = ((char)255).ToString(); // Segment
        public static String Fm = ((char)28).ToString(); // File Separator
        public static String Gm = ((char)29).ToString(); // Group Separator
        public static String Rm = ((char)30).ToString(); // Record Separator
        public static String Am = ((char)254).ToString(); // Attribute / Field / Column
        public static String Vm = ((char)253).ToString(); // Multivalue
        public static String Svm = ((char)252).ToString(); // Subvalue
        public static String Lvm = "*"; // MultiField Level 1
        public static String Lsvm = "@"; // MultiField Level 2

        public static String Ass = ((char)255).ToString(); // File Separator
        public static String Afs = ((char)28).ToString(); // File Separator
        public static String Ags = ((char)29).ToString(); // Group Separator
        public static String Ars = ((char)30).ToString(); // Record Separator
        public static String Aus = ((char)31).ToString(); // Unit Separtor / Column / Field - Level 1
        public static String Auss = ((char)256).ToString(); // Unit Separtor multivalue = Level 2
        public static String Ausss = "*"; // MultiField Level 3
        public static String Aussss = "@"; // MultiField Level 4

        public static String Trm = sEmpty; // Trim Character

        public static StdDelimDef DelPickGet()
        {
            return new StdDelimDef(Sm, Fm, Rm, Am, Vm, Svm, Trm);
        }
        public static StdDelimDef DelStdGet()
        {
            return new StdDelimDef();
        }

        public static StdDelimDef DelAsciiGet()
        {
            Cr = ((char)13).ToString();
            Lf = ((char)10).ToString();
            return new StdDelimDef(Sm, Fm, Gm, Lf, ",", "|", Cr);
        }
    }
}
