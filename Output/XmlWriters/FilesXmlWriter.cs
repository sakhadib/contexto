using System;
using System.Xml.Linq;
using System.Linq;
using Contexto.Core.Models;

namespace Contexto.Output.XmlWriters
{
    public class FilesXmlWriter : BaseXmlWriter
    {
        public FilesXmlWriter() : base("files.xml") { }

        public void Generate(FolderNode root)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("files",
                    new XAttribute("generated", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                    GetAllFiles(root)
                )
            );

            SaveXml(doc);
        }

        private XElement[] GetAllFiles(FolderNode node)
        {
            var files = node.Files.Select(CreateFileElement).ToList();

            foreach (var subfolder in node.Subfolders)
            {
                files.AddRange(GetAllFiles(subfolder));
            }

            return files.ToArray();
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
                ("accessed", file.LastAccessedUtc.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                ("parent", file.ParentFolder)
            );
        }
    }
} 