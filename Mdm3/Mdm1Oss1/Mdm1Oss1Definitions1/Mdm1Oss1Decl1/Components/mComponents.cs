using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Oss
{
    #region Mdm Color
    /// <summary> 
    /// Classes used to manage colors for ellipses and other
    /// user interface elements.  This will be expanded to be 
    /// used by any color scheme and style setters in WPF
    /// </summary> 
    public static class MdmEllipseColor
    {
        public static void ControlGetTo(ref Object Sender, ref System.Windows.Shapes.Ellipse EllipsePassed, int ColorOfBarPassed)
        {
            EllipsePassed.Fill = MdmColorDef.ControlGet(ColorOfBarPassed);
            EllipsePassed.InvalidateVisual();
            ((System.Windows.Controls.Page)Sender).InvalidateVisual();
        }
    }

    /// <summary> 
    /// Current list of basic colors.
    /// </summary> 
    public static class MdmColorDef
    {
        /// <summary> 
        /// Current list of basic colors used in the application.
        /// </summary> 
        [Flags]
        public enum Is : int
        {
            Green = 1,
            Blue = 2,
            Red = 3,
            Yellow = 4,
            LightBlue = 5,
            White = 6,
        }
        //
        /// <summary> 
        /// Uses the passed color to return a media brush color.
        /// </summary> 
        /// <param name="ColorOfBarPassed">Interger enumeration value indicating desired color.</param> 
        /// <returns>
        /// System.Windows.Media.Brush color.
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public static System.Windows.Media.Brush ControlGet(int ColorOfBarPassed)
        {
            switch (ColorOfBarPassed)
            {
                case ((int)Is.Green):
                    return System.Windows.Media.Brushes.Green;
                case ((int)Is.Blue):
                    return System.Windows.Media.Brushes.Blue;
                case ((int)Is.Red):
                    return System.Windows.Media.Brushes.Red;
                case ((int)Is.Yellow):
                    return System.Windows.Media.Brushes.Yellow;
                case ((int)Is.White):
                    return System.Windows.Media.Brushes.White;
                default:
                case ((int)Is.LightBlue):
                    return System.Windows.Media.Brushes.LightBlue;
            }
            // return System.Windows.Media.Brushes.LightBlue;
        }
    }
    #endregion
}

namespace Mdm.Oss.Components
{
    public enum ObjectVisibilityIs : int
    {
        NotSet = 0,
        Hiden = 1,
        Visible = 2,
        VisibleDisabled = 3,
        VisibleActive = 4,
        VisibleAlert = 5
    }
    class mControl
    {
        // See ???
    }
    public static class GenericObject
    {
        #region Generic Objects
        public static object GetNewObject(Type t)
        {
            try
            {
                return t.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return null;
            }
        }
        // Here is the same approach, contained in a generic method:
        public static T GetNewObject<T>()
        {
            try
            {
                return (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return default;
            }
        }
        #endregion
    }
}