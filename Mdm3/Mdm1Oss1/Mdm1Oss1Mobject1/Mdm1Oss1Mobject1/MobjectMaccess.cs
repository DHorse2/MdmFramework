using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Oss.Components
{
    // nMapplicationStore.cs
    // Persistence Class for Mobjects
    //
    using System;

    // A set of classes for handling an ApplicationStore:
    namespace MapplicationStore {
        using System.Collections;

        // Describes an ocPassedMapplication in the cPassedMapplication List lMapplication:
        public struct csMapplication {
            public String sTitle;               // sTitle of the oPassedMapplication.
            public String sAuthor;              // sAuthor of the oPassedMapplication.
            public decimal dPagesPrinted;       // dPagesPrinted by the oPassedMapplication.
            public bool UsesPrinter;           // UsesPrinter does this Mapplication Print stuff.
            // struct have constructors you idiot
            public csMapplication(String PassedTitle, String PassedAuthor, decimal PassedPagesPrinted, bool PassedUsesPrinter) {
                sTitle = PassedTitle;
                sAuthor = PassedAuthor;
                dPagesPrinted = PassedPagesPrinted;
                UsesPrinter = PassedUsesPrinter;
            }
        }

        // Declare a delegate type for processing an oPassedMapplication:
        // cdProcessMapplicationObjectDelegate
        public delegate void cdProcessMapplicationObjectDelegate(csMapplication ocPassedMapplication);

        // Maintains a oPassedMapplication List.
        public class cMapplicationDb {
            // List of all Mapplication(s) in the List:
            ArrayList alMapplicationList = new ArrayList();

            // Add a oPassedMapplication to the List:
            public void mAddMapplication(String PassedTitle, String PassedAuthor, decimal PassedPagesPrinted, bool PassedPaperBack) {
                alMapplicationList.Add(new csMapplication(PassedTitle, PassedAuthor, PassedPagesPrinted, PassedPaperBack));
            }

            // Call a passed-in delegate on each UsesPrinter oPassedMapplication to process it: 
            public void mProcessUsesPrinterMapplications(cdProcessMapplicationObjectDelegate cPassedProcessMapplication) {
                foreach (csMapplication oalMapplicationInstance in alMapplicationList) {
                    if (oalMapplicationInstance.UsesPrinter)
                        // Calling the delegate:
                        cPassedProcessMapplication(oalMapplicationInstance);
                }
            }
        }
    }

    // Using the nMapplicationStore classes:
    namespace MapplicationTestClient {
        using MapplicationStore;

        // Class to total and average number of printed pages of Mapplication(s):
        class cPageTotaller {
            int iCountMapplications = 0;
            decimal dTotalPrintedPages = 0;
            decimal dPagesPrinted = 0.0m;

            internal void mAddMapplicationTotal(csMapplication oPassedMapplication) {
                iCountMapplications += 1;
                dTotalPrintedPages += oPassedMapplication.dPagesPrinted;
                dPagesPrinted += oPassedMapplication.dPagesPrinted;
            }

            internal decimal mAveragePagesPrinted() {
                return dPagesPrinted / iCountMapplications;
            }
        }

        // Class to test the oPassedMapplication List:
        class cTest {
            // Print the PassedTitle of the oPassedMapplication.
            static void mPrintTitle(csMapplication oPassedMapplication) {
                System.Diagnostics.Debug.WriteLine("   ", oPassedMapplication.sTitle);
            }

            // Execution starts here.
            static void mMain() {
                cMapplicationDb osMapplicationDb = new cMapplicationDb();

                // Initialize the List with some Mapplication(s):
                mAddMapplications(osMapplicationDb);

                // Print all the titles of paperbacks:
                System.Diagnostics.Debug.WriteLine("UsesPrinter Mapplication Titles:");
                // Create a new delegate Object associated with the static 
                // method cTest.mPrintTitle:
                osMapplicationDb.mProcessUsesPrinterMapplications(new cdProcessMapplicationObjectDelegate(mPrintTitle));

                // Get the average PassedPagesPrinted of a paperback by using
                // a cPriceTotaller object:
                cPageTotaller ocPageTotaller = new cPageTotaller();
                // Create a new delegate Object associated with the nonstatic 
                // method mAddMapplicationTotal on the Object oTotaller:
                osMapplicationDb.mProcessUsesPrinterMapplications(new cdProcessMapplicationObjectDelegate(ocPageTotaller.mAddMapplicationTotal));
                System.Diagnostics.Debug.WriteLine("Average UsesPrinter Mapplication dPagesPrinted: " + ocPageTotaller.mAveragePagesPrinted());
            }

            // Initialize the oPassedMapplication List with some test Mapplication(s):
            static void mAddMapplications(cMapplicationDb boOkDb) {
                boOkDb.mAddMapplication("The C Programming Language",
                   "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
                boOkDb.mAddMapplication("The Unicode Standard 2.0",
                   "The Unicode Consortium", 39.95m, true);
                boOkDb.mAddMapplication("The MS-DOS Encyclopedia",
                   "Ray Duncan", 129.95m, false);
                boOkDb.mAddMapplication("Dogbert's Clues for the Clueless",
                   "Scott Adams", 12.00m, true);
            }
        }
    }

    /// <summary>
    /// <para> Ignore This</para>
    /// </summary>
    class MobjectMaccess {

    }
}
