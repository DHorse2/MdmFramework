using System;
using System.Windows.Forms;

namespace Mdm.Oss.Components
{
    #region Console and Status Message Box MessageBoxPadding
    /// <summary> 
    /// This is a basic general structure for containing
    /// Margins or Padding for user interface objects.
    /// </summary> 
    public struct MessageBoxPadding
    {
        public double dLeft;
        public double dTop;
        public double dRight;
        public double dBottom;
        //
        public MessageBoxPadding(
            double dL,
            double dT,
            double dR,
            double dB)
        {
            dLeft = dL;
            dRight = dR;
            dTop = dT;
            dBottom = dB;
        }
    }
    #endregion
    #region TextBoxDim structure
    /// <summary> 
    /// A basic general purpose for managing the
    /// size of text, combo or other UI elements that is
    /// used internally by page size adjustments methods.
    /// </summary> 
    public class TextBoxDim
    {
        // Max and Min per WPF
        public double Max; // Maximum Size as per WPF
        public double Min; // Minimum Size as per WPF
                           // Desired is calculated and frequently overriden in the code.
        public double Desired; // Desired size ignoring maximums & minimums
        public double Actual; // Actual size in presentation container
        public double Current; // Working variable for calculations
                               // Current is being used for actual at this time.
        public double High; // Maximum width in order to widen box
        public double Low; // Maximum width in order to narrow box
                           //
        public class TextBoxEncl
        {
            public double Inner; // Inside padding
            public double Outer; // Outside padding
            public double Border; // Border width
        }
        //
        public TextBoxEncl EnclBefore; // Left / Top
        public TextBoxEncl EnclAfter; // Right / Bottom
        public string BorderSytle; // Border pattern name
                                   //
    }
    #endregion
    #region TextBoxManageDef structure
    /// <summary>
    /// Text, combo or general purpose UI element management class.
    /// </summary> 
    /// <remarks></remarks> 
    public class TextBoxManageDef
    {
        public Object Sender;
        public TextBoxManageDef TextBoxManageObject;
        public TextBox BoxObject;
        // Text Displayed so far.
        public double DisplayCount;
        public double DisplayAdjustCount;
        public double DisplayAdjustCountMax;
        //
        // various control properties
        // ToDo TextBoxManageDef TextBoxManageDef partially implemented:
        public double DisplayMaxChars;
        public double DisplayMaxCharsToKeep;
        public double DisplayMaxLines;
        public double DisplayMaxLinesToKeep;
        //
        public bool DisplayAddToTop;
        // Flag to scroll to bottom or top
        public bool ScrollDo;
        // Font
        // ToDo TextBoxManageDef FONT HERE OR...
        // ToDo public Object StyleObject;
        // ToDo public Object TextBoxObject;
        //
        // Box Padding
        // NOTE TextBoxManageDef This should be loaded from WPF
        public double BoxWidthCurrent; // ToDo TextBoxManageDef replace with Actual below ?
        public double BoxPosX;
        public double BoxPosY;
        public MessageBoxPadding BoxPadding;
        public MessageBoxPadding BoxPaddingAdditional;
        // public TextBoxDim BoxWidth;
        // public TextBoxDim BoxHeight;
        // Width
        public double WidthMax; // Maximum Size as per WPF
        public double WidthMin; // Minimum Size as per WPF
                                // Desired is calculated and frequently overriden in the code.
        public double WidthDesired; // Desired size ignoring maximums & minimums
        public double WidthActual; // Actual size in presentation container
        public double WidthCurrent; // Working variable for calculations
                                    // NOTE TextBoxManageDef Current is being used for actual at this time.
        public double WidthHigh; // Maximum width in order to widen box
        public double WidthLow; // Maximum width in order to narrow box
                                // Height
        // ToDo TextBoxManageDef This is a presentation object properties structure:
        public double HeightMax;
        public double HeightMin; // Minimum Size as per WPF
        public double HeightDesired;
        public double HeightActual;
        public double HeightCurrent;
        public double HeightHigh;
        public double HeightLow;
        //
        /// <summary>
        /// Standard data clear method.
        /// </summary> 
        public void DataClear()
        {
            BoxObject = null;
            //
            DisplayCount = 0;
            DisplayAdjustCount = 0;
            DisplayAdjustCountMax = 15;
            DisplayAddToTop = false;
            //
            DisplayMaxChars = 10000;
            DisplayMaxCharsToKeep = 9000;
            DisplayMaxLines = 500;
            DisplayMaxLinesToKeep = 400;
            //
            ScrollDo = true;
            //
            BoxWidthCurrent = 0;
            // ToDo TextBoxManageDef Currently not loading from WPF:
            BoxPadding = new MessageBoxPadding(5, 0, 0, 0); // ToDo 
            BoxPaddingAdditional = new MessageBoxPadding(0, 0, 10, 0);
            //
            WidthCurrent = 0;
            WidthHigh = 0;
            WidthLow = 0;
        }
        /// <summary>
        /// Constructor
        /// </summary> 
        public TextBoxManageDef()
        {
            Sender = this;
            TextBoxManageObject = this;
            DataClear();
        }
        /// <summary>
        /// Constructor creates a box management object for the pass UI element.
        /// </summary> 
        public TextBoxManageDef(TextBox BoxObjectPassed)
            : this()
        {
            BoxObject = BoxObjectPassed;
        }
    }
    #endregion
}
