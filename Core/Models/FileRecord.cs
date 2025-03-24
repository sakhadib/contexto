using System;

namespace Contexto.Core.Models
{
    public class FileRecord
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public long Size { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public DateTime LastAccessedUtc { get; set; }
        public string ParentFolder { get; set; }

        public FileRecord()
        {
        }

        public FileRecord(System.IO.FileInfo fileInfo)
        {
            Name = fileInfo.Name;
            FullPath = fileInfo.FullName;
            FileType = fileInfo.Extension.ToLowerInvariant();
            Size = fileInfo.Length;
            CreatedUtc = fileInfo.CreationTimeUtc;
            ModifiedUtc = fileInfo.LastWriteTimeUtc;
            LastAccessedUtc = fileInfo.LastAccessTimeUtc;
            ParentFolder = fileInfo.DirectoryName;
        }
    }
} 