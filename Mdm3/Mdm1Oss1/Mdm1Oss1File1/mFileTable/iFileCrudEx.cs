using Mdm.Oss.Decl;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mdm.Oss.File
{
    public interface AppTransaction : IDisposable
    {
        int GetId();
        void SetId(int IdPassed);
        StateIs Update();
        StateIs Cancel();
        object LockItem(Object ItemId);
        object UnlockItem(Object ItemId);

    }
    public interface iFileCrudEx : IDisposable
    {
        void Create();
        void Read();
        bool Update();
        bool Delete();
        void Validate();
        void GetId();
        void SetId();
        void GetObject();
        void SetObject();
        void Clear();
        void Reset();
        void Open(string FileName);
        void Close(string FileName);
        object LockItem(Object ItemId);
        object UnlockItem(Object ItemId);
    }
}
