using System;
using System.Xml.Linq;
using Contexto.Core.Models;

namespace Contexto.Output.XmlWriters
{
    public class FoldersXmlWriter : BaseXmlWriter
    {
        public FoldersXmlWriter() : base("Folders.xml") { }

        public void Generate(FolderNode root)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("folders",
                    new XAttribute("generated", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                    CreateFolderElement(root)
                )
            );

            SaveXml(doc);
        }

        private XElement CreateFolderElement(FolderNode folder)
        {
            var element = CreateElementWithAttributes("folder",
                ("name", folder.Name),
                ("path", folder.FullPath),
                ("fileCount", folder.FileCount.ToString()),
                ("totalFileCount", folder.TotalFileCount.ToString())
            );

            if (!string.IsNullOrEmpty(folder.ReadmeContent))
            {
                element.Add(new XElement("readme", folder.ReadmeContent));
            }

            foreach (var subfolder in folder.Subfolders)
            {
                element.Add(CreateFolderElement(subfolder));
            }

            return element;
        }
    }
} 