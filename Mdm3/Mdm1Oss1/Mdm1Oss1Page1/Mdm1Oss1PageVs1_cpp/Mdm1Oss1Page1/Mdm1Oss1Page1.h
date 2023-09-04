// Mdm1Oss1Page1.h

#pragma once

using namespace System;
using namespace Mdm::Oss::Decl;
using namespace Mdm::Pick;

namespace Mdm {
    namespace Oss {
        namespace Wpf {
            generic <typename PageType>
            public ref class PageFunctionMdm : System::Windows::Navigation::PageFunction<PageType>
            {
            public: 
                typedef class Mdm::Oss::Decl::DefStdBaseRunFileConsole Dsbrfc;
                PageFunctionMdm() {
                    Dsbrfc a = gcnew Dsbrfc();
                    a.bNO = true;
                    bNo = true;
                    Dsbrfc.bNo = true;
                }

                // TODO: Add your methods for this class here.
                // System.Windows.Navigation
            };
            public ref class PageFunctionMdmInst
            {
            public: 
                int i;
                PageFunctionMdm<int> Bob;

                void MainProc() {
                    PageFunctionMdm<int> Bob = gcnew PageFunctionMdm<int>();
                    Bob.bNo = true;
                }
            };
        }
    }
}
