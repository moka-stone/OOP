using System.Text.Json;

namespace DocumentEditor.Settings
{
    public class EditorSettings
    {
        private static EditorSettings instance;
        private static readonly object lockObject = new object();

        public string Theme { get; set; } = "Light";
        public int FontSize { get; set; } = 12;

        private EditorSettings() { }

        public static EditorSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = LoadSettings();
                        }
                    }
                }
                return instance;
            }
        }

        private static EditorSettings LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                string json = File.ReadAllText("settings.json");
                return JsonSerializer.Deserialize<EditorSettings>(json) ?? new EditorSettings();
            }
            return new EditorSettings();
        }

        public void SaveSettings()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText("settings.json", json);
        }
    }
} 