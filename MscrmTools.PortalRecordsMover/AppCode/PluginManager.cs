using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class PluginManager
    {
        private IOrganizationService service;

        private List<Entity> steps;

        public PluginManager(IOrganizationService service)
        {
            this.service = service;
            steps = new List<Entity>();
            GetWebpagePluginsSteps();
        }

        public void ActivateWebpagePlugins()
        {
            foreach (var step in steps)
            {
                step["statecode"] = new OptionSetValue(0);
                step["statuscode"] = new OptionSetValue(-1);
                service.Update(step);
            }
        }

        public void DeactivateWebpagePlugins()
        {
            foreach (var step in steps)
            {
                step["statecode"] = new OptionSetValue(1);
                step["statuscode"] = new OptionSetValue(-1);
                service.Update(step);
            }
        }

        private void GetWebpagePluginsSteps()
        {
            var query = new QueryExpression("sdkmessageprocessingstep")
            {
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("stage", ConditionOperator.In, new []{10,20,40})
                    }
                },
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = "sdkmessageprocessingstep",
                        LinkFromAttributeName = "sdkmessagefilterid",
                        LinkToAttributeName = "sdkmessagefilterid",
                        LinkToEntityName = "sdkmessagefilter",
                        LinkCriteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("primaryobjecttypecode", ConditionOperator.Equal, "adx_webpage")
                            }
                        }
                    }
                }
            };

            steps = service.RetrieveMultiple(query).Entities.ToList();
        }
    }
}