using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    public static class MetadataManager
    {
        public static List<EntityMetadata> GetEntitiesList(IOrganizationService service, List<string> logicalNames = null)
        {
            EntityQueryExpression entityQueryExpressionFull = new EntityQueryExpression
            {
                Properties = new MetadataPropertiesExpression
                {
                    AllProperties = false,
                    PropertyNames =
                    {
                        "DisplayName",
                        "LogicalName",
                        "SchemaName",
                        "ManyToManyRelationships",
                        "ManyToOneRelationships",
                        "IsIntersect",
                        "PrimaryNameAttribute",
                        "Attributes"
                    }
                },
                AttributeQuery = new AttributeQueryExpression
                {
                    Properties = new MetadataPropertiesExpression
                    {
                        AllProperties = false,
                        PropertyNames = { "IsValidForCreate", "IsValidForUpdate", "LogicalName", "Targets", "OptionSet", "DisplayName" }
                    }
                }
            };

            if (logicalNames != null)
            {
                entityQueryExpressionFull.Criteria = new MetadataFilterExpression
                {
                    Conditions =
                    {
                        new MetadataConditionExpression("LogicalName", MetadataConditionOperator.In, logicalNames.ToArray())
                    }
                };
            }

            RetrieveMetadataChangesRequest request = new RetrieveMetadataChangesRequest
            {
                Query = entityQueryExpressionFull,
                ClientVersionStamp = null
            };

            var fullResponse = (RetrieveMetadataChangesResponse)service.Execute(request);

            return fullResponse.EntityMetadata.Where(e => e.LogicalName.StartsWith("adx_") || e.LogicalName == "annotation").ToList();
        }
    }
}