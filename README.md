# Contexto

Contexto is a powerful command-line tool that analyzes directory structures and generates detailed XML reports about your codebase. It provides insights into your project's structure, file statistics, and documentation.

## Features

- **Directory Analysis**: Recursively scans directories while intelligently excluding common development folders (node_modules, vendor, .git, etc.)
- **File Statistics**: Collects comprehensive information about files including:
  - File types and counts
  - Total size
  - Creation and modification dates
  - Last accessed times
- **README Integration**: Automatically detects and includes README.md content in the analysis
- **Multiple Report Formats**: Generates different XML reports for various aspects of your codebase:
  - `folders.xml`: Directory structure and hierarchy
  - `files.xml`: Detailed file information
  - `stat.xml`: Statistical analysis of the codebase
  - `complete.xml`: Comprehensive report combining all information

## Prerequisites

- .NET 6.0 SDK or later
- Windows, Linux, or macOS operating system

## Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/contexto.git
cd contexto
```

2. Build the project:
```bash
dotnet build
```

3. The executable will be available in the `bin/Debug/net6.0` directory.

## Usage

Contexto is a command-line tool that can be run with different commands to generate various reports:

```bash
# Generate all reports (default command)
dotnet run

# Generate specific reports
dotnet run folders    # Generate only folders.xml
dotnet run files      # Generate only files.xml
dotnet run stat       # Generate only stat.xml
dotnet run complete   # Generate only complete.xml
```

### Output Files

- `folders.xml`: Contains the directory structure and README content
- `files.xml`: Lists all files with their properties
- `stat.xml`: Provides statistical analysis of the codebase
- `complete.xml`: Combines all information into a single comprehensive report
- `contexto.errors.log`: Contains any errors that occur during execution

## License

This project is licensed under the Creative Commons Attribution 4.0 International License (CC BY 4.0). This means you are free to:

- Share: Copy and redistribute the material in any medium or format
- Adapt: Remix, transform, and build upon the material

Under the following terms:

- **Attribution**: You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
- **No additional restrictions**: You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.

For the full license text, see [LICENSE](LICENSE) file.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Error Handling

The tool includes robust error handling:
- Skips inaccessible files and directories gracefully
- Logs errors to `contexto.errors.log` for debugging
- Continues processing even if individual files or directories fail

## Support

If you encounter any issues or have questions, please open an issue in the GitHub repository. 