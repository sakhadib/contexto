using System;
using System.IO;
using Contexto.Core.Models;
using Contexto.Core.Scanner;
using Contexto.Core.Analysis;
using Contexto.Output.XmlWriters;

namespace Contexto.CLI
{
    public class CommandRouter
    {
        private readonly DirectoryCrawler _crawler;
        private readonly StatsEngine _stats;
        private readonly FoldersXmlWriter _foldersWriter;
        private readonly FilesXmlWriter _filesWriter;
        private readonly StatXmlWriter _statWriter;
        private readonly CompleteXmlWriter _completeWriter;

        public CommandRouter()
        {
            _crawler = new DirectoryCrawler();
            _stats = new StatsEngine();
            _foldersWriter = new FoldersXmlWriter();
            _filesWriter = new FilesXmlWriter();
            _statWriter = new StatXmlWriter();
            _completeWriter = new CompleteXmlWriter(_stats);
        }

        public void Execute(string command)
        {
            try
            {
                var currentDir = Environment.CurrentDirectory;
                var root = _crawler.ScanDirectory(currentDir);
                _stats.Process(root);

                switch (command.ToLower())
                {
                    case "run":
                        GenerateAll(root);
                        break;
                    case "folders":
                        _foldersWriter.Generate(root);
                        break;
                    case "files":
                        _filesWriter.Generate(root);
                        break;
                    case "stat":
                        _statWriter.Generate(_stats);
                        break;
                    case "complete":
                        _completeWriter.Generate(root);
                        break;
                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        Console.WriteLine("Available commands: run, folders, files, stat, complete");
                        break;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("contexto.errors.log",
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error executing command '{command}': {ex.Message}\n");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void GenerateAll(FolderNode root)
        {
            _foldersWriter.Generate(root);
            _filesWriter.Generate(root);
            _statWriter.Generate(_stats);
            _completeWriter.Generate(root);
        }
    }
} 