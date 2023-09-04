using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Oss.Mobj {
    // nMapplicationStore.cs
    // Persistence Class for Mobjects
    //
    using System;

    // A set of classes for handling an ApplicationStore:
    namespace MapplicationStore {
        using System.Collections;

        // Describes an ocsPassedMapplication in the csPassedMapplication List lMapplication:
        public struct csMapplication {
            public string sTitle;               // sTitle of the osPassedMapplication.
            public string sAuthor;              // sAuthor of the osPassedMapplication.
            public decimal dPagesPrinted;       // dPagesPrinted by the osPassedMapplication.
            public bool bUsesPrinter;           // bUsesPrinter does this Mapplication Print stuff.
            // struct have constructors you idiot
            public csMapplication(string sPassedTitle, string sPassedAuthor, decimal dPassedPagesPrinted, bool bPassedUsesPrinter) {
                sTitle = sPassedTitle;
                sAuthor = sPassedAuthor;
                dPagesPrinted = dPassedPagesPrinted;
                bUsesPrinter = bPassedUsesPrinter;
            }
        }

        // Declare a delegate type for processing an osPassedMapplication:
        // cdProcessMapplicationObjectDelegate
        public delegate void cdProcessMapplicationObjectDelegate(csMapplication ocsPassedMapplication);

        // Maintains a osPassedMapplication List.
        public class cMapplicationDb {
            // List of all Mapplication(s) in the List:
            ArrayList alMapplicationList = new ArrayList();

            // Add a osPassedMapplication to the List:
            public void mAddMapplication(string sPassedTitle, string sPassedAuthor, decimal dPassedPagesPrinted, bool bPassedPaperBack) {
                alMapplicationList.Add(new csMapplication(sPassedTitle, sPassedAuthor, dPassedPagesPrinted, bPassedPaperBack));
            }

            // Call a passed-in delegate on each UsesPrinter osPassedMapplication to process it: 
            public void mProcessUsesPrinterMapplications(cdProcessMapplicationObjectDelegate cdPassedProcessMapplication) {
                foreach (csMapplication oalMapplicationInstance in alMapplicationList) {
                    if (oalMapplicationInstance.bUsesPrinter)
                        // Calling the delegate:
                        cdPassedProcessMapplication(oalMapplicationInstance);
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

            internal void mAddMapplicationTotal(csMapplication osPassedMapplication) {
                iCountMapplications += 1;
                dTotalPrintedPages += osPassedMapplication.dPagesPrinted;
                dPagesPrinted += osPassedMapplication.dPagesPrinted;
            }

            internal decimal mAveragePagesPrinted() {
                return dPagesPrinted / iCountMapplications;
            }
        }

        // Class to test the osPassedMapplication List:
        class cTest {
            // Print the sPassedTitle of the osPassedMapplication.
            static void mPrintTitle(csMapplication osPassedMapplication) {
                System.Diagnostics.Debug.WriteLine("   ", osPassedMapplication.sTitle);
            }

            // Execution starts here.
            static void mMain() {
                cMapplicationDb osMapplicationDb = new cMapplicationDb();

                // Initialize the List with some Mapplication(s):
                mAddMapplications(osMapplicationDb);

                // Print all the titles of paperbacks:
                System.Diagnostics.Debug.WriteLine("bUsesPrinter Mapplication Titles:");
                // Create a new delegate object associated with the static 
                // method cTest.mPrintTitle:
                osMapplicationDb.mProcessUsesPrinterMapplications(new cdProcessMapplicationObjectDelegate(mPrintTitle));

                // Get the average dPassedPagesPrinted of a paperback by using
                // a cPriceTotaller object:
                cPageTotaller ocPageTotaller = new cPageTotaller();
                // Create a new delegate object associated with the nonstatic 
                // method mAddMapplicationTotal on the object oTotaller:
                osMapplicationDb.mProcessUsesPrinterMapplications(new cdProcessMapplicationObjectDelegate(ocPageTotaller.mAddMapplicationTotal));
                System.Diagnostics.Debug.WriteLine("Average bUsesPrinter Mapplication dPagesPrinted: " + ocPageTotaller.mAveragePagesPrinted());
            }

            // Initialize the osPassedMapplication List with some test Mapplication(s):
            static void mAddMapplications(cMapplicationDb bookDb) {
                bookDb.mAddMapplication("The C Programming Language",
                   "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
                bookDb.mAddMapplication("The Unicode Standard 2.0",
                   "The Unicode Consortium", 39.95m, true);
                bookDb.mAddMapplication("The MS-DOS Encyclopedia",
                   "Ray Duncan", 129.95m, false);
                bookDb.mAddMapplication("Dogbert's Clues for the Clueless",
                   "Scott Adams", 12.00m, true);
            }
        }
    }
    class MobjectMaccess {

    }
}
