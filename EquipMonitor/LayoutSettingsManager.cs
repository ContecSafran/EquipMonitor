using System;
using System.IO;
using System.Text.Json;
using EquipMonitor.dto;

namespace EquipMonitor
{
    public static class LayoutSettingsManager
    {
        private static readonly string SettingsFilePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "layout_settings.json");
        public static LayoutSettings Settings { get; private set; } = new LayoutSettings();

        public static void Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    var deserialized = JsonSerializer.Deserialize<LayoutSettings>(json);
                    if (deserialized != null)
                    {
                        Settings = deserialized;
                    }
                }
            }
            catch (Exception)
            {
                Settings = new LayoutSettings();
            }
            finally
            {
                if (Settings.EquipmentSplitterDistances == null)
                {
                    Settings.EquipmentSplitterDistances = new System.Collections.Generic.Dictionary<string, int>();
                }
            }
        }

        public static void Save()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(Settings, options);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch (Exception)
            {
                // Ignored to avoid runtime crashes upon standard I/O failures
            }
        }
    }
}
