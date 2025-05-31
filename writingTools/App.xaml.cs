using Serilog;
using Serilog.Enrichers.WithCaller;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace antWriter
{
    public partial class App : Application
    {
        Stopwatch sw = new Stopwatch();
        Stopwatch swDLL = new Stopwatch();

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        private readonly Lazy<Process> _currentProcess = new Lazy<Process>(() => Process.GetCurrentProcess());
        public Process CurrentProcess => _currentProcess.Value;

        private static Lazy<AppConfig> _config = new Lazy<AppConfig>(() =>
        {
            ConfigManager.Load();
            return ConfigManager.Config;
        });
        public static AppConfig Config => _config.Value;

        /// <summary>
        /// Runs an Ollama prompt using the phi3 model, prepending a system prompt and adding temperature and max-tokens flags.
        /// </summary>
        /// <param name="prompt">User prompt text</param>
        /// <returns>Response from Ollama model</returns>
        public async Task<string> RunOllamaPromptAsync(string prompt)
        {
            string systemPrompt = "You are A-Tec, a helpful writer's assistant. Please talk in english, or polish depending on what the user starts with. Respond briefly and clearly, but be most human like and friendly as possible.";
            // Escape any quotes in user prompt to safely pass in CLI args
            string promptEscaped = prompt.Replace("\"", "\\\"");

            // Combine system prompt and user prompt, separated by newline
            string combinedPrompt = $"{systemPrompt}\\n{promptEscaped}";

            var psi = new ProcessStartInfo
            {
                FileName = "ollama",
                Arguments = $"run DuckingtonLabs/tinyStories \"{combinedPrompt}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
                throw new Exception("Failed to start Ollama process.");

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception($"Ollama error: {error}");

            return output.Trim();
        }


protected override async void OnStartup(StartupEventArgs e)
        {
            sw.Start();
            string logFileName = $"logs\\app-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithCaller()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] [{Caller}] {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    theme: SystemConsoleTheme.Colored)
                .WriteTo.File(
                    logFileName,
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] [{Caller}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            swDLL.Start();
            AllocConsole();
            swDLL.Stop();
            Log.Information($"Init sequence started. Console allocation took: {swDLL.ElapsedMilliseconds}ms.");
            Resources["AppChosenLogo"] = App.Config.Editor.Logo;
            Resources["FontSize"] = (double)App.Config.Font.Size;
            Resources["AppEditorFont"] = new System.Windows.Media.FontFamily(App.Config.Font.Family);
            Resources["Username"] = App.Config.Editor.Username;
            Log.Debug($"Font Family: {App.Config.Font.Family}");
            Log.Debug($"Font Size: {App.Config.Font.Size}");
            Log.Debug($"Logo: {App.Config.Editor.Logo}");
            Log.Debug("Logo:" + Application.Current.Resources["AppChosenLogo"]);
            Log.Debug($"Username: {App.Config.Editor.Username}");
            Log.Verbose("Console attached. Debugging started.");
            Log.Verbose("Current Directory: {Directory}", Environment.CurrentDirectory);
            Log.Verbose("Executable Path: {ExePath}", AppContext.BaseDirectory);
            Log.Verbose("Config File Path: {ConfigPath}", Path.GetFullPath("config.json"));
            Log.Verbose("Machine Name: {MachineName}", Environment.MachineName);
            Log.Verbose("User Name: {UserName}", Environment.UserName);
            Log.Verbose("OS Version: {OSVersion}", Environment.OSVersion);
            Log.Verbose(".NET Version: {DotNetVersion}", Environment.Version);
            Log.Verbose("64-bit OS: {Is64BitOS}", Environment.Is64BitOperatingSystem);
            Log.Verbose("64-bit Process: {Is64BitProcess}", Environment.Is64BitProcess);
            Log.Verbose("CPU Cores: {ProcessorCount}", Environment.ProcessorCount);
            Log.Verbose("System Uptime (s): {Uptime}", Environment.TickCount64 / 1000);
            Log.Verbose("=== Resource Usage ===");
            Log.Verbose("Physical Memory Usage: {0} MB",
                Math.Round(CurrentProcess.WorkingSet64 / 1024.0 / 1024.0, 2));
            Log.Verbose("Private Memory Size: {0} MB",
                Math.Round(CurrentProcess.PrivateMemorySize64 / 1024.0 / 1024.0, 2));
            Log.Verbose("Virtual Memory Size: {0} MB",
                Math.Round(CurrentProcess.VirtualMemorySize64 / 1024.0 / 1024.0, 2));
            Log.Verbose("Paged System Memory Size: {0} MB",
                Math.Round(CurrentProcess.PagedSystemMemorySize64 / 1024.0 / 1024.0, 2));
            Log.Verbose("CPU Time (User + Kernel): {0} ms",
                (CurrentProcess.UserProcessorTime + CurrentProcess.PrivilegedProcessorTime).TotalMilliseconds);

            base.OnStartup(e);
            sw.Stop();
            Log.Information($"SysInit took: {sw.ElapsedMilliseconds}ms");
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}