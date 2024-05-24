namespace Arcade.RPG.Lib;

using Arcade.RPG.Lib.Models;

using Newtonsoft.Json;

using System;
using System.IO;

public abstract class AtlasMapLoader {
    public static Atlas LoadFromFile(string relativePath) {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(baseDirectory, relativePath);

        string json = File.ReadAllText(filePath);

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new ColorConverter());

        return JsonConvert.DeserializeObject<Atlas>(json, settings);
    }
}