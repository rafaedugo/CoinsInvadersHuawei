using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static Data data = new Data();

    public static void Save()
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string directory = Path.Combine(Application.persistentDataPath, "itch.io");

        Directory.CreateDirectory(directory);

        string path = directory + "/saves.dat";

        FileStream file = File.Create(path);

        formatter.Serialize(file, data);

        file.Close();
    }

    public static void Load()
    {

        string directory = Path.Combine(Application.persistentDataPath, "itch.io");

        string path = directory + "/saves.dat";
        if (!File.Exists(path)) return;

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        data = (Data)formatter.Deserialize(file);

        file.Close();

        Debug.Log("Settings Loaded");
    }
}
