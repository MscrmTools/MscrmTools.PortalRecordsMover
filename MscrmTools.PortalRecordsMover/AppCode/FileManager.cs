using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class FileManager
    {
        public static EntityCollection GetRecordsFromDisk(string path)
        {
            EntityCollection ec = null;
            var tempPath = Path.Combine(Path.GetTempPath(), "PortalRecordsMoverTemp");

            var extension = Path.GetExtension(path);
            switch (extension)
            {
                case ".xml":
                    ec = DeserializeFullFile(path);
                    break;

                case ".zip":
                    if (Directory.Exists(tempPath))
                    {
                        Directory.Delete(tempPath, true);
                    }

                    ZipFile.ExtractToDirectory(path, tempPath);
                    ec = GetRecordsFromFolder(tempPath);
                    break;

                default:
                    ec = GetRecordsFromFolder(path);
                    break;
            }

            return ec;
        }

        private static EntityCollection DeserializeFullFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var serializer = new DataContractSerializer(typeof(EntityCollection), new List<Type> { typeof(Entity) });
                return (EntityCollection)serializer.ReadObject(reader.BaseStream);
            }
        }

        private static EntityCollection GetRecordsFromFolder(string path)
        {
            var ec = new EntityCollection();
            var serializer = new DataContractSerializer(typeof(Entity));

            foreach (var file in Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories))
            {
                using (var reader = new StreamReader(file))
                {
                    var entity = (Entity)serializer.ReadObject(reader.BaseStream);
                    ec.Entities.Add(entity);
                }
            }

            return ec;
        }
    }
}