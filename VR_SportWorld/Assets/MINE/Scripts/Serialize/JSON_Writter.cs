using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSON_Writter : MonoBehaviour
{
    // Start is called before the first frame update
    string directorypath;

    void Start()
    {
        //SportPlayer player_1 = new SportPlayer();
        //player_1.name = "User 1";
        //player_1.weight = 85.5F;
        //player_1.UID = 1;

        //SavePlayerToJson(player_1);

        //path = Application.persistentDataPath + "/MINE/SavedData/PlayerDataFile.json";
        directorypath = Application.persistentDataPath + "/MINE/SavedData";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerToJson(SportPlayer _player)
    {
        CreateDirectory(directorypath);

        string json = JsonUtility.ToJson(_player);
        //File.WriteAllText(path, json);

        using StreamWriter writer = new StreamWriter(directorypath + "/playerdata.json");
        writer.Write (json);
        writer.Flush();
        writer.Close();
    }

    public SportPlayer LoadPlayerFromJson()
    {
        using StreamReader reader = new StreamReader(directorypath + "/playerdata.json");
        print(directorypath);
        string json = reader.ReadToEnd();
        reader.Close();
        //string json = File.ReadAllText(path);
        SportPlayer _player = JsonUtility.FromJson<SportPlayer>(json);
        return _player;
    }

    public static DirectoryInfo CreateDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            return null;
        }
        return Directory.CreateDirectory(path);
    }
    
}
