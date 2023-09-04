using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mdm.World;
using Mdm.World.User;
using Mdm.World.Temporal;

using Mdm.Oss.Sys;

namespace Mdm.World
{

    internal class WorldClass
    {

        protected internal SystemInfoClass SystemInfo;
        //
        // File System IO Area
        //

        public WorldClass()
        {
            // Task Create Instance of WorldProcessMain Class
            WorldAgentClass WorldAgent = new WorldAgentClass(this);
            // Task Load This System From MdmSystem directory
            SystemInfo = new SystemInfoClass(this);
            //
            // Task Load Last System Action from System Log
            // SystemLog SystemLog
            //
            // Task Load Current User From MdmSystem
            // UserInfo UserInfoInstance = WorldAgentInstance.UserInfoGet(this);
            //
            // No Current User:
            // Task Prompt for Current User
            //
            // Task Load Current Contexts for User
            // Task Load Current Task for User

        }
        public WorldClass(Object Sender)
            : this()
        {
        }


    }

    class WorldAgentClass
    {

        public WorldAgentClass()
        {
        }
        public WorldAgentClass(Object Sender)
            : this()
        {
        }
    }

}