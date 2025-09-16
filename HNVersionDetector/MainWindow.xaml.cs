using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Linq;

namespace HelloNeighborVersionDetector
{
    public partial class MainWindow : Window
    {
        private string? _detectedTimestamp;
        private string? _detectedMemorySize;
        private string? _detectedProcess;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        public MainWindow()
        {
            InitializeComponent();
            _detectedTimestamp = string.Empty;
            _detectedMemorySize = string.Empty;
            _detectedProcess = string.Empty;

            this.StateChanged += (sender, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    PlayRestoreAnimation();
                }
            };
        }

        private void PlayRestoreAnimation()
        {
            var restoreAnimation = (Storyboard)FindResource("RestoreAnimation");
            restoreAnimation.Begin(this);
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            var minimizeAnimation = (Storyboard)FindResource("MinimizeAnimation");
            minimizeAnimation.Completed += (s, args) =>
            {
                this.WindowState = WindowState.Minimized;
            };
            minimizeAnimation.Begin(this);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var closeAnimation = (Storyboard)FindResource("CloseAnimation");
            closeAnimation.Completed += (s, args) => this.Close();
            closeAnimation.Begin(this);
        }

        private void DetectButton_Click(object sender, RoutedEventArgs e)
        {
            ResultText.Text = "Detecting...";
            DetectVersion();
        }

        private void DetectVersion()
        {
            foreach (var process in Process.GetProcesses())
            {
                foreach (var version in VersionDatabase.Versions)
                {
                    if (process.ProcessName.Equals(version.ProcessName.Replace(".exe", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        if (TryGetProcessInfo(process, version.Timestamp, version.ModuleSize, out uint actualTimestamp, out uint actualModuleSize))
                        {
                            bool timestampMatches = actualTimestamp == version.Timestamp;
                            bool moduleMatches = actualModuleSize == version.ModuleSize;

                            if (timestampMatches && moduleMatches)
                            {
                                ResultText.Text = $"Detected Version: {version.Version}";
                                _detectedTimestamp = $"0x{actualTimestamp:X8}";
                                _detectedMemorySize = $"0x{actualModuleSize:X8}";
                                _detectedProcess = version.ProcessName;
                                DetailsButton.IsEnabled = true;
                                return;
                            }
                        }
                    }
                }
            }

            foreach (var process in Process.GetProcesses())
            {
                foreach (var version in VersionDatabase.Versions)
                {
                    if (process.ProcessName.Equals(version.ProcessName.Replace(".exe", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        if (TryGetProcessInfo(process, version.Timestamp, version.ModuleSize, out uint actualTimestamp, out uint actualModuleSize))
                        {
                            bool timestampMatches = actualTimestamp == version.Timestamp;
                            bool moduleMatches = actualModuleSize == version.ModuleSize;

                            if (timestampMatches || moduleMatches)
                            {
                                var matchingVersions = VersionDatabase.Versions
                                    .Where(v => v.ProcessName.Equals($"{process.ProcessName}.exe", StringComparison.OrdinalIgnoreCase))
                                    .ToList();

                                var closestVersion = moduleMatches
                                    ? matchingVersions.FirstOrDefault(v => v.ModuleSize == actualModuleSize)
                                    : matchingVersions.FirstOrDefault(v => v.Timestamp == actualTimestamp);

                                if (closestVersion != default)
                                {
                                    string matchType = timestampMatches ? "timestamp" : "module size";
                                    ResultText.Text = $"Partial match: {closestVersion.Version} ({matchType} matches)";
                                    _detectedTimestamp = $"0x{actualTimestamp:X8}";
                                    _detectedMemorySize = $"0x{actualModuleSize:X8}";
                                    _detectedProcess = closestVersion.ProcessName;
                                    DetailsButton.IsEnabled = true;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            foreach (var process in Process.GetProcesses())
            {
                foreach (var version in VersionDatabase.Versions)
                {
                    if (process.ProcessName.Equals(version.ProcessName.Replace(".exe", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        if (TryGetProcessInfo(process, version.Timestamp, version.ModuleSize, out uint actualTimestamp, out uint actualModuleSize))
                        {
                            bool timestampMatches = actualTimestamp == version.Timestamp;
                            bool moduleMatches = actualModuleSize == version.ModuleSize;

                            if (!timestampMatches && !moduleMatches)
                            {
                                ResultText.Text = "No version found but the process was detected. Check details, it's most likely a new build. Consider merging it to github if it's a new one!";
                                _detectedTimestamp = $"0x{actualTimestamp:X8}";
                                _detectedMemorySize = $"0x{actualModuleSize:X8}";
                                _detectedProcess = version.ProcessName;
                                DetailsButton.IsEnabled = true;
                                return;
                            }
                        }
                    }
                }
            }

            foreach (var process in Process.GetProcesses())
            {
                string processName = process.ProcessName;
                if (processName.Contains("Hello", StringComparison.OrdinalIgnoreCase) &&
                    processName.Contains("Win64", StringComparison.OrdinalIgnoreCase))
                {
                    if (TryGetProcessInfo(process, 0, 0, out uint timestamp, out uint moduleSize))
                    {
                        ResultText.Text = "No version found but a process that could be HN was detected. Check details, it could be a new build. Consider merging it to github if it's a new one!";
                        _detectedTimestamp = $"0x{timestamp:X8}";
                        _detectedMemorySize = $"0x{moduleSize:X8}";
                        _detectedProcess = $"{process.ProcessName}.exe";
                        DetailsButton.IsEnabled = true;
                        return;
                    }
                }
            }

            foreach (var process in Process.GetProcesses())
            {
                string processName = process.ProcessName;
                if (processName.Contains("Hello", StringComparison.OrdinalIgnoreCase) &&
                    processName.Contains("Win32", StringComparison.OrdinalIgnoreCase))
                {
                    if (TryGetProcessInfo(process, 0, 0, out uint timestamp, out uint moduleSize))
                    {
                        ResultText.Text = "No version found but a process that could be HN was detected. Check details, it could be a new build. Consider merging it to github if it's a new one!";
                        _detectedTimestamp = $"0x{timestamp:X8}";
                        _detectedMemorySize = $"0x{moduleSize:X8}";
                        _detectedProcess = $"{process.ProcessName}.exe";
                        DetailsButton.IsEnabled = true;
                        return;
                    }
                }
            }

            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains("Hello", StringComparison.OrdinalIgnoreCase))
                {
                    if (TryGetProcessInfo(process, 0, 0, out uint timestamp, out uint moduleSize))
                    {
                        ResultText.Text = "No version found but a process that could be HN was detected. Check details, it could be a new build. Consider merging it to github if it's a new one!";
                        _detectedTimestamp = $"0x{timestamp:X8}";
                        _detectedMemorySize = $"0x{moduleSize:X8}";
                        _detectedProcess = $"{process.ProcessName}.exe";
                        DetailsButton.IsEnabled = true;
                        return;
                    }
                }
            }

            ResultText.Text = "Nothing found.";
            DetailsButton.IsEnabled = false;
        }

        private bool TryGetProcessInfo(Process process, uint expectedTimestamp, uint expectedModuleSize, out uint timestamp, out uint moduleSize)
        {
            timestamp = 0;
            moduleSize = 0;
            try
            {
                var module = process.MainModule;
                if (module == null) return false;
                var baseAddress = module.BaseAddress;
                moduleSize = (uint)module.ModuleMemorySize;
                byte[] buffer = new byte[4];

                IntPtr peHeaderAddress = IntPtr.Add(baseAddress, 0x3C);
                if (!ReadProcessMemory(process.Handle, peHeaderAddress, buffer, 4, out _))
                    return false;
                uint peOffset = BitConverter.ToUInt32(buffer, 0);

                IntPtr timestampAddress = IntPtr.Add(baseAddress, (int)peOffset + 0x8);
                if (!ReadProcessMemory(process.Handle, timestampAddress, buffer, 4, out _))
                    return false;
                timestamp = BitConverter.ToUInt32(buffer, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsWindow.ShowWindow(
                _detectedTimestamp ?? string.Empty,
                _detectedMemorySize ?? string.Empty,
                _detectedProcess ?? string.Empty,
                this
            );
        }
    }
}