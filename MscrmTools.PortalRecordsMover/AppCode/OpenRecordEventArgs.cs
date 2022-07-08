using Microsoft.Xrm.Sdk;
using System;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    public class OpenRecordEventArgs : EventArgs
    {
        public OpenRecordEventArgs(Entity record)
        {
            Record = record;
        }

        public Entity Record { get; private set; }
    }
}