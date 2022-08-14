using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSON_Writter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SportPlayer player_1 = new SportPlayer();
        player_1.name = "User 1";
        player_1.weight = 85.5F;
        player_1.UID = 1;

        SavePlayerToJson(player_1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerToJson(SportPlayer _player)
    {
        string json = JsonUtility.ToJson(_player);
        File.WriteAllText(Application.dataPath + "/MINE/SavedData/PlayerDataFile.json", json);
    }

    public void LoadPlayerFromJson(SportPlayer _player)
    {
        string json = File.ReadAllText(Application.dataPath + "/MINE/SavedData/PlayerDataFile.json");
        _player = JsonUtility.FromJson<SportPlayer>(json);
    }
}
