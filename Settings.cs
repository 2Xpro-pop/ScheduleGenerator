using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace ScheduleGenerator
{
    public class Settings
    {
        private readonly Dictionary<string, object> _settings;

        public Settings()
        {
            if(File.Exists("settings.json"))
            {
                _settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText("settings.json"));
            }
            _settings = _settings ?? new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get => _settings[key];
            set
            {
                if(_settings.ContainsKey(key))
                {
                    _settings[key] = value;
                }
                else
                {
                    _settings.Add(key, value);
                }
            }
        }

        public bool ContainsKey(string key) => _settings.ContainsKey(key);
        public bool ContainsValue(object value) => _settings.ContainsValue(value);

        public void Save()
        {
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(_settings, Formatting.Indented));
        }

        ~Settings()
        {
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(_settings, Formatting.Indented));
        }

    }
}