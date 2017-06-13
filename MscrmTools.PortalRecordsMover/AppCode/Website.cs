using Microsoft.Xrm.Sdk;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    public class Website
    {
        public Website(Entity record)
        {
            Record = record;
        }

        public Entity Record { get; }

        public override string ToString()
        {
            return $"{Record.GetAttributeValue<string>("adx_name")} ({Record.Id})";
        }
    }
}