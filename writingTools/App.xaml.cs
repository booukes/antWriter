using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace antWriter
{
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        Process currentProcess = Process.GetCurrentProcess();

        public void Github_Event()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "https://github.com/booukes/antWriter",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            string logFileName = $"logs\\app-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", theme: SystemConsoleTheme.Colored)
                .WriteTo.File(logFileName, rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            AllocConsole();
            ConfigManager.Load();
            Resources["AppChosenLogo"] = ConfigManager.Config.Editor.Logo;
            Resources["FontSize"] = (double)ConfigManager.Config.Font.Size;
            Resources["AppEditorFont"] = new System.Windows.Media.FontFamily(ConfigManager.Config.Font.Family);
            Resources["Username"] = ConfigManager.Config.Editor.Username;
            Log.Debug($"Font Family: {ConfigManager.Config.Font.Family}");
            Log.Debug($"Font Size: {ConfigManager.Config.Font.Size}");
            Log.Debug($"Logo: {ConfigManager.Config.Editor.Logo}");
            Log.Debug("Logo:" + Application.Current.Resources["AppChosenLogo"]);
            Log.Debug($"Username: {ConfigManager.Config.Editor.Username}");
            Log.Information("Console attached. Debugging started.");
            Log.Information("Current Directory: {Directory}", Environment.CurrentDirectory);
            Log.Information("Executable Path: {ExePath}", AppContext.BaseDirectory);
            Log.Information("Config File Path: {ConfigPath}", Path.GetFullPath("config.json"));
            Log.Information("Machine Name: {MachineName}", Environment.MachineName);
            Log.Information("User Name: {UserName}", Environment.UserName);
            Log.Information("OS Version: {OSVersion}", Environment.OSVersion);
            Log.Information(".NET Version: {DotNetVersion}", Environment.Version);
            Log.Information("64-bit OS: {Is64BitOS}", Environment.Is64BitOperatingSystem);
            Log.Information("64-bit Process: {Is64BitProcess}", Environment.Is64BitProcess);
            Log.Information("CPU Cores: {ProcessorCount}", Environment.ProcessorCount);
            Log.Information("System Uptime (s): {Uptime}", Environment.TickCount64 / 1000);
            Log.Information("=== Resource Usage ===");
            Log.Information("Physical Memory Usage: {0} MB",
                Math.Round(currentProcess.WorkingSet64 / 1024.0 / 1024.0, 2));
            Log.Information("Private Memory Size: {0} MB",
                Math.Round(currentProcess.PrivateMemorySize64 / 1024.0 / 1024.0, 2));
            Log.Information("Virtual Memory Size: {0} MB",
                Math.Round(currentProcess.VirtualMemorySize64 / 1024.0 / 1024.0, 2));
            Log.Information("Paged System Memory Size: {0} MB",
                Math.Round(currentProcess.PagedSystemMemorySize64 / 1024.0 / 1024.0, 2));
            Log.Information("CPU Time (User + Kernel): {0} ms",
                (currentProcess.UserProcessorTime + currentProcess.PrivilegedProcessorTime).TotalMilliseconds);


            base.OnStartup(e);
        }
    }
}