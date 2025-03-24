using System;
using System.Collections.Generic;
using System.Linq;

namespace Contexto.Core.Models
{
    public class FolderNode
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string ReadmeContent { get; set; }
        public List<FolderNode> Subfolders { get; set; }
        public List<FileRecord> Files { get; set; }
        public int FileCount { get; set; }
        public int TotalFileCount { get; set; }

        public FolderNode()
        {
            Subfolders = new List<FolderNode>();
            Files = new List<FileRecord>();
        }

        public FolderNode(string name, string fullPath)
        {
            Name = name;
            FullPath = fullPath;
            Subfolders = new List<FolderNode>();
            Files = new List<FileRecord>();
        }

        public void UpdateCounts()
        {
            FileCount = Files.Count;
            TotalFileCount = FileCount + Subfolders.Sum(f => f.TotalFileCount);
        }
    }
} 