using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mdm.Oss.File;
using Mdm.Oss.File.RunControl;

namespace Mdm {
    namespace World {

        public class ActorActionDef {
            Int32 ActorId;
            Int32 ActorType;
            Int32 SystemId;
            Int32 ActingForId;
            public ActorActionDef() {
                ActorId = 0;
                ActorType = 0;
                SystemId = 0;
                ActingForId = 0;
            }
        }

        public class ListMgntDef {

            public class List {
                String ListName;
                ActorActionDef Actor;
                bool NodeUsed;
                NodeDef Node;
                // Please note that file summary records are not created
                // by default and must be instantiated.  They
                // should be instantiated in the owning class and
                // then pointers stored in the associated classes
                // such as Name.  An example is shown here...
                object Sender; // Orgin of event (sender). The Control or the app.
                FileSummaryDef Fs;
                mFileCommand FileCommand;
                //FileCommandDef FileCommand;

                public List() {
                    ListName = "";
                    Actor = new ActorActionDef();
                    NodeUsed = false;
                    Node = new NodeDef();
                    //Fs = new FileSummaryDef();
                    Fs = new FileSummaryDef(ref Sender);
                    FileCommand = new mFileCommand();
                    FileCommand.FileSummary = Fs;
                }
            }

            public class ListItemDef {
                // Id is an ascii delimited field that is built
                // according to the UniqueIdSequence found in the
                // Name field.
                //
                // In order for select lists and list retention to
                // work in a reproduceable way it must retain enough
                // fields in order to select the database and return
                // the item desired.
                Object Id;


                public ListItemDef() {

                }
            }

            public ListMgntDef() {
                // int i = 0;
            }

        }
    }
}
