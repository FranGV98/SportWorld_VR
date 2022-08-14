using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubScript : MonoBehaviour
{
    public JSON_Writter json;
    public SportPlayer tempPlayer;

    public InputField PlayerName_InputField;
    public Text weight_text;
    // Start is called before the first frame update
    void Start()
    {
        json = gameObject.GetComponent<JSON_Writter>();
        tempPlayer.weight = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWeight()
    {
        tempPlayer.weight += 1;
        weight_text.text = tempPlayer.weight + " Kg";
    }

    public void LoseWeight()
    {
        tempPlayer.weight -= 1;
        weight_text.text = tempPlayer.weight + " Kg";
    }

    public void SetPlayer()
    {
        tempPlayer.name = PlayerName_InputField.text;
        json.SavePlayerToJson(tempPlayer);
    }
}
