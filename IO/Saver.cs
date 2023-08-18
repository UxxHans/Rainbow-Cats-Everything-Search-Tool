using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using EverythingSearch.Model;
using System.Windows;

namespace EverythingSearch.IO
{
    public static class Saver
    {
        public static Config ProgramConfig { get; set; } = new();

        public static string Version => 
            "V" + Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown Version";

        public static string LocalDirectory => 
            Path.GetDirectoryName(LocalPath) ?? "";

        public static string LocalPath => 
            Process.GetCurrentProcess().MainModule?.FileName ?? "";

        public static string ConfigPath =>
            DocumentDirectory.Trim() != "" ? Path.Combine(DocumentDirectory, "Everything-Search\\Config.pd") : "";


        public static string DocumentDirectory
        {
            get
            {
                try
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                catch(Exception)
                {
                    return LocalDirectory;
                }
            }
        }

        public static void StartService()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(LocalDirectory, "Everything.exe"),
                    Arguments = "-admin -first-instance -minimized -nodb -close",
                    UseShellExecute = false,
                };

                Process process = new();
                process.StartInfo = startInfo;
                process.Start();

                ChildProcessTracker.AddProcess(process);
            }
            catch (Exception ex)
            {
                Console.WriteLine("打开服务失败: " + ex.Message);
            }
        }

        public static void SaveConfig() => WriteJson(ConfigPath, ProgramConfig);
        public static void LoadConfig() => ProgramConfig = ReadJson(ConfigPath, new Config());

        public static Config GenerateConfig() => new();

        public static T ReadJson<T>(string filePath, T defaultValue)
        {
            try
            {
                if (!File.Exists(filePath))
                    WriteJson(filePath, defaultValue);

                string jsonString = File.ReadAllText(filePath);
                T? jsonObject = JsonSerializer.Deserialize<T>(jsonString);

                if (jsonObject == null)
                {
                    WriteJson(filePath, defaultValue);
                    return defaultValue;
                }
                else
                {
                    return jsonObject;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"读取设置失败: {e.Message}", "通知", MessageBoxButton.OK, MessageBoxImage.Warning);
                return defaultValue;
            }
        }

        public static void WriteJson<T>(string filePath, T content)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(content);

                FileInfo file = new FileInfo(filePath);
                file.Directory?.Create();

                File.WriteAllText(file.FullName, jsonString);
            }
            catch (Exception e) 
            {
                MessageBox.Show($"保存设置失败: {e.Message}", "通知", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
