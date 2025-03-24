using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Contexto.Core.Models;

namespace Contexto.Core.Scanner
{
    public class DirectoryCrawler
    {
        private readonly HashSet<string> _excludedDirs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "node_modules", "vendor", "venv",
            ".git", ".vscode", ".idea", "bin", "obj"
        };

        public FolderNode ScanDirectory(string path)
        {
            var dir = new DirectoryInfo(path);
            return CrawlDirectory(dir);
        }

        private FolderNode CrawlDirectory(DirectoryInfo dir)
        {
            var node = new FolderNode(dir.Name, dir.FullName);

            try
            {
                // Process subdirectories
                foreach (var subdir in dir.GetDirectories()
                                        .Where(d => !IsExcluded(d)))
                {
                    node.Subfolders.Add(CrawlDirectory(subdir));
                }

                // Process files
                foreach (var file in dir.GetFiles())
                {
                    try
                    {
                        node.Files.Add(new FileRecord(file));
                    }
                    catch (Exception)
                    {
                        // Skip files that can't be accessed
                        continue;
                    }
                }

                // Check for README.md
                var readme = dir.GetFiles()
                               .FirstOrDefault(f => f.Name.Equals("README.md",
                                                   StringComparison.OrdinalIgnoreCase));
                if (readme != null)
                {
                    try
                    {
                        node.ReadmeContent = File.ReadAllText(readme.FullName);
                    }
                    catch (Exception)
                    {
                        // Skip if README can't be read
                    }
                }

                node.UpdateCounts();
            }
            catch (Exception)
            {
                // Skip directories that can't be accessed
            }

            return node;
        }

        private bool IsExcluded(DirectoryInfo dir)
        {
            return _excludedDirs.Contains(dir.Name);
        }
    }
} 