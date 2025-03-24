using System;
using System.Xml.Linq;
using System.IO;

namespace Contexto.Output.XmlWriters
{
    public abstract class BaseXmlWriter
    {
        protected readonly string OutputPath;

        protected BaseXmlWriter(string outputPath)
        {
            OutputPath = outputPath;
        }

        protected void SaveXml(XDocument doc)
        {
            try
            {
                doc.Save(OutputPath);
            }
            catch (Exception ex)
            {
                File.AppendAllText("contexto.errors.log", 
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error saving {OutputPath}: {ex.Message}\n");
            }
        }

        protected XElement CreateElement(string name, object value)
        {
            return new XElement(name, value?.ToString() ?? string.Empty);
        }

        protected XElement CreateElementWithAttributes(string name, params (string name, string value)[] attributes)
        {
            var element = new XElement(name);
            foreach (var (attrName, attrValue) in attributes)
            {
                element.Add(new XAttribute(attrName, attrValue));
            }
            return element;
        }
    }
} 