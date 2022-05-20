using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using System.Collections.Generic;
using System.Linq;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    public static class MetadataManager
    {
        public static List<EntityMetadata> GetEntitiesList(IOrganizationService service)
        {
            EntityQueryExpression entityQueryExpressionFull = new EntityQueryExpression
            {
                Properties = new MetadataPropertiesExpression
                {
                    AllProperties = false,
                    PropertyNames =
                    {
                        "LogicalName"
                    }
                }
            };

            RetrieveMetadataChangesRequest request = new RetrieveMetadataChangesRequest
            {
                Query = entityQueryExpressionFull,
                ClientVersionStamp = null
            };

            var fullResponse = (RetrieveMetadataChangesResponse)service.Execute(request);

            var logicalNames = fullResponse.EntityMetadata.Where(e => e.LogicalName.StartsWith("adx_") || e.LogicalName == "annotation").Select(e => e.LogicalName).ToList();

            entityQueryExpressionFull = new EntityQueryExpression
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
                        "Attributes",
                        "ObjectTypeCode"
                    }
                },
                Criteria = new MetadataFilterExpression
                {
                    Conditions =
                    {
                        new MetadataConditionExpression("LogicalName", MetadataConditionOperator.In, logicalNames.ToArray())
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

            request = new RetrieveMetadataChangesRequest
            {
                Query = entityQueryExpressionFull,
                ClientVersionStamp = null
            };

            fullResponse = (RetrieveMetadataChangesResponse)service.Execute(request);

            return fullResponse.EntityMetadata.ToList();
        }
    }
}