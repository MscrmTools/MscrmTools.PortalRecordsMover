using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class NoteManager
    {
        private IOrganizationService service;
        private Entity settings;

        public NoteManager(IOrganizationService service)
        {
            this.service = service;
            GetSettings();
        }

        public bool HasJsRestriction => settings.GetAttributeValue<string>("blockedattachments").Contains(";js");

        public void AddRestriction()
        {
            var blockedAtt = settings.GetAttributeValue<string>("blockedattachments");
            var list = blockedAtt.Split(';').ToList();
            list.Add("js");
            list.Sort();

            settings["blockedattachments"] = string.Join(";", list);

            service.Update(settings);
        }

        public void RemoveRestriction()
        {
            var blockedAtt = settings.GetAttributeValue<string>("blockedattachments");
            var list = blockedAtt.Split(';').ToList();
            list.Remove("js");
            list.Sort();

            settings["blockedattachments"] = string.Join(";", list);

            service.Update(settings);
        }

        private void GetSettings()
        {
            var query = new QueryExpression("organization")
            {
                ColumnSet = new ColumnSet("blockedattachments"),
            };

            settings = service.RetrieveMultiple(query).Entities.First();
        }
    }
}