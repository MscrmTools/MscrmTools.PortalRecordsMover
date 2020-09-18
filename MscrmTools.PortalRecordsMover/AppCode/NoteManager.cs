using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;
using System.ServiceModel;
using System.Threading;

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

        public void TestRestriction()
        {
            bool restricted = true;

            do
            {
                try
                {
                    var id = service.Create(new Entity("annotation")
                    {
                        Attributes =
                        {
                            {"filename", "testrestriction.js"},
                            {"mimetype", "application/javascript"},
                            {"documentbody", "ZnVuY3Rpb24gdGVzdCgpe30="},
                        }
                    });

                    service.Delete("annotation", id);

                    restricted = false;
                }
                catch (FaultException<OrganizationServiceFault> error)
                {
                    // Wait only if error because file restriction is still there
                    if (error.Detail.ErrorCode == -2147205623)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        throw;
                    }
                }
            } while (restricted);
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