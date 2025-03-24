using System;
using System.Linq;
using System.Collections.Generic;
using Contexto.Core.Models;

namespace Contexto.Core.Analysis
{
    public class StatsEngine
    {
        public FileRecord MostRecentFile { get; private set; }
        public Dictionary<string, int> FileTypeCounts { get; private set; }
        public long TotalSize { get; private set; }
        public int TotalFiles { get; private set; }
        public int TotalFolders { get; private set; }

        public void Process(FolderNode root)
        {
            FileTypeCounts = new Dictionary<string, int>();
            TotalFiles = 0;
            TotalFolders = 0;
            TotalSize = 0;

            ProcessNode(root);
        }

        private void ProcessNode(FolderNode node)
        {
            TotalFolders++;

            foreach (var file in node.Files)
            {
                TotalFiles++;
                TotalSize += file.Size;

                // Update file type counts
                var fileType = string.IsNullOrEmpty(file.FileType) ? "no-extension" : file.FileType;
                if (FileTypeCounts.ContainsKey(fileType))
                    FileTypeCounts[fileType]++;
                else
                    FileTypeCounts[fileType] = 1;

                // Update most recent file
                if (MostRecentFile == null || file.ModifiedUtc > MostRecentFile.ModifiedUtc)
                    MostRecentFile = file;
            }

            foreach (var subfolder in node.Subfolders)
            {
                ProcessNode(subfolder);
            }
        }
    }
} 