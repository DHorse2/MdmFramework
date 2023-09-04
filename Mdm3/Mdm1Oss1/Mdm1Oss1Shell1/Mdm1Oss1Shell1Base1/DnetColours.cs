using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms; // only present for test function

namespace Mdm.Oss {
    public class DnetColours {
        public System.Array ColorsArray;
        public String[] ColorsArrayNames;
        public KnownColor[] AllColors;
        public DnetColours() {
            // Get all the values from the KnownColor enumeration.
            ColorsArray = Enum.GetValues(typeof(KnownColor));
            ColorsArrayNames = Enum.GetNames(typeof(KnownColor));
            AllColors = new KnownColor[ColorsArray.Length];

            Array.Copy(ColorsArray, AllColors, ColorsArray.Length);
        }

        public void TestColour(int ColourNumber, PaintEventArgs e, System.Windows.Forms.Form TestForm) {
            // Create a custom brush from the color and use it to draw
            // the brush's name.
            // SolidBrush ColourBrush =
                // new SolidBrush(Color.FromName(AllColors[ColourNumber].ToString()));

            SolidBrush ColourBrush =
                new SolidBrush(Color.FromKnownColor(AllColors[ColourNumber]));
            //
            // e.Graphics.DrawString(AllColors[ColourNumber].ToString(),
               // TestForm.Font, ColourBrush, 10, 10);

            e.Graphics.DrawString(
                ColorsArrayNames[ColourNumber],
                TestForm.Font, 
                ColourBrush, 
                10, 10);

            // Dispose of the custom brush.
            ColourBrush.Dispose();
        }
    }
}
