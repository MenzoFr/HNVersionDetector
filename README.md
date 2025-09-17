# HNVersionDetector

HNVersionDetector is a small program that you can use to detect on which version you are playing Hello Neighbor on.

It can be pretty useful for speedrunners and/or developers who want a consistent way to find the game version.

This can also be used to know if a build is new or not without having to search for in game differences.

Feel free to make Pull Requests to the VersionDatabase.cs if you want to add new builds.

---

## Features 
- **Accurate Detection:** Detects the exact version of Hello Neighbor that you are running
- **Extensible Database:** Uses a version database (`VersionDatabase.cs`) that can easily be updated when people find more stuff to add
- **Open Source:** Full source code available for contributions and transparency
- **Fast & Lightweight:** Instant results and low size
- **Zero Setup:** Portable executable available

---

## 📥 Download & Installation 
- [Latest Release](../../releases/latest)  
You can download the pre-compiled version of the program here if you don't want to do it yourself. 

### Build from Source
1. Clone the repository: ```git clone https://github.com/MenzoFr/HNVersionDetector.git```
2. Open in Visual Studio 2022 (another version should be fine)
3. Build the solution (Ctrl+Shift+B)
4. Find the executable in `bin/Release/` or `bin/Debug/`

## Contributing

I highly encourage contributions to make it better. Here's how you can help:

### Adding New Versions
1. Fork the repository
2. Update `VersionDatabase.cs` with the new build
3. Test your changes if you want to
4. Submit a pull request with:
   - Version number/name
   - Detection stuff (Module memory size and raw timestamp)
   - Any relevant notes about the build

### Reporting Issues
- Found a version that isn't detected though it should? [Open an issue](../../issues/new)
- Please include the game version, any error messages and any other relevant informations.

## Requirements

- **OS**: Windows 7/8/10/11 (64-bit)
- **Framework**: .NET Framework 4.7.2 or higher
- **Permissions**: Standard user permissions (no admin required)
- **Dependencies**: None (self-contained executable)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Development
- **Language**: C# with WPF framework
- **Interface**: GUI-based for better user interaction
- **Development**: UI components partially designed with modern tooling assistance
