using System;
using System.Xml.Linq;
using System.Linq;
using Contexto.Core.Models;
using Contexto.Core.Analysis;

namespace Contexto.Output.XmlWriters
{
    public class CompleteXmlWriter : BaseXmlWriter
    {
        private readonly StatsEngine _stats;

        public CompleteXmlWriter(StatsEngine stats) : base("complete.xml")
        {
            _stats = stats;
        }

        public void Generate(FolderNode root)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("contexto",
                    new XAttribute("generated", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                    CreateSummaryElement(),
                    CreateStructureElement(root)
                )
            );

            SaveXml(doc);
        }

        private XElement CreateSummaryElement()
        {
            return new XElement("summary",
                CreateElement("totalFiles", _stats.TotalFiles),
                CreateElement("totalFolders", _stats.TotalFolders),
                CreateElement("totalSize", _stats.TotalSize),
                CreateElement("mostRecentFile", _stats.MostRecentFile?.FullPath ?? "No files found"),
                CreateElement("mostRecentFileModified", 
                    _stats.MostRecentFile?.ModifiedUtc.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? "N/A")
            );
        }

        private XElement CreateStructureElement(FolderNode node)
        {
            var element = CreateElementWithAttributes("folder",
                ("name", node.Name),
                ("path", node.FullPath),
                ("fileCount", node.FileCount.ToString()),
                ("totalFileCount", node.TotalFileCount.ToString())
            );

            // Add README content if available
            if (!string.IsNullOrEmpty(node.ReadmeContent))
            {
                element.Add(new XElement("readme", node.ReadmeContent));
            }

            // Add files in this folder
            if (node.Files.Any())
            {
                element.Add(new XElement("files",
                    node.Files.Select(f => CreateFileElement(f))
                ));
            }

            // Add subfolders
            foreach (var subfolder in node.Subfolders)
            {
                element.Add(CreateStructureElement(subfolder));
            }

            return element;
        }

        private XElement CreateFileElement(FileRecord file)
        {
            return CreateElementWithAttributes("file",
                ("name", file.Name),
                ("path", file.FullPath),
                ("type", file.FileType),
                ("size", file.Size.ToString()),
                ("created", file.CreatedUtc.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                ("modified", file.ModifiedUtc.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                ("accessed", file.LastAccessedUtc.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            );
        }
    }
} 