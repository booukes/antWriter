using System;
using System.IO;
using System.Text.Json;
using System.Windows.Media;

public class FontSettings
{
    public String Family { get; set; } = "Courier New";
    public double Size { get; set; } = 24;
}

public class EditorSettings
{
    public bool AutoSave { get; set; } = true;
    public int AutoSaveInterval { get; set; } = 60;
    public bool SpellCheck { get; set; } = false;
    public string Theme { get; set; } = "Light";
}

public class AppSettings
{
    public string WindowTitle { get; set; } = "Kaszepad";
    public bool StartMaximized { get; set; } = true;
}

public class AppConfig
{
    public FontSettings Font { get; set; } = new();
    public EditorSettings Editor { get; set; } = new();
    public AppSettings App { get; set; } = new();
}

public static class ConfigManager
{
    private const string ConfigFile = "config.json";

    public static AppConfig Config { get; private set; } = new AppConfig();

    public static void Load()
    {
        try
        {
            if (File.Exists(ConfigFile))
            {
                string json = File.ReadAllText(ConfigFile);
                Config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
            }
            else
            {
                Save();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to load config: " + ex.Message);
            Config = new AppConfig();
        }
    }

    public static void Save()
    {
        try
        {
            string json = JsonSerializer.Serialize(Config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFile, json);
            Console.WriteLine($"Config saved successfully to {Path.GetFullPath(ConfigFile)}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Access denied while saving config file: " + ex.Message);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Directory not found while saving config file: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("IO error while saving config file: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error while saving config file: " + ex.Message);
        }
    }
}
