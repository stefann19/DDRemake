using System.IO;
using Newtonsoft.Json;

namespace DDRemakeProject.SerializingHelpers
{
    public class JsonSerializer
    {
        public static void WriteToFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            string json = JsonConvert.SerializeObject(objectToWrite,Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static string GetGeneratorPath(string mapName) => $"Maps\\{mapName}\\Generator.json";
        public static string GetMinimapPath(string mapName) => $"Maps\\{mapName}\\Minimap.json";
        public static string GetPlayerPath(string mapName) => $"Maps\\{mapName}\\Player.json";

        public static string GetMapInfoPath(string mapName) => $"Maps\\{mapName}\\Info.json";

    }
}
