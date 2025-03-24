using System;
using System.Xml.Linq;
using System.Linq;
using Contexto.Core.Models;
using Contexto.Core.Analysis;

namespace Contexto.Output.XmlWriters
{
    public class StatXmlWriter : BaseXmlWriter
    {
        public StatXmlWriter() : base("stat.xml") { }

        public void Generate(StatsEngine stats)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("statistics",
                    new XAttribute("generated", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                    CreateSummaryElement(stats),
                    CreateFileTypesElement(stats),
                    CreateMostRecentElement(stats)
                )
            );

            SaveXml(doc);
        }

        private XElement CreateSummaryElement(StatsEngine stats)
        {
            return new XElement("summary",
                CreateElement("totalFiles", stats.TotalFiles),
                CreateElement("totalFolders", stats.TotalFolders),
                CreateElement("totalSize", stats.TotalSize)
            );
        }

        private XElement CreateFileTypesElement(StatsEngine stats)
        {
            return new XElement("fileTypes",
                stats.FileTypeCounts.Select(kvp =>
                    CreateElementWithAttributes("type",
                        ("extension", kvp.Key),
                        ("count", kvp.Value.ToString())
                    )
                )
            );
        }

        private XElement CreateMostRecentElement(StatsEngine stats)
        {
            if (stats.MostRecentFile == null)
                return new XElement("mostRecent", "No files found");

            return new XElement("mostRecent",
                CreateElementWithAttributes("file",
                    ("name", stats.MostRecentFile.Name),
                    ("path", stats.MostRecentFile.FullPath),
                    ("modified", stats.MostRecentFile.ModifiedUtc.ToString("yyyy-MM-ddTHH:mm:ssZ"))
                )
            );
        }
    }
} 