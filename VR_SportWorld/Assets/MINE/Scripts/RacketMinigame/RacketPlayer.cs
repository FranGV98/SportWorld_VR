using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class RacketPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> LifesImg_List;
    public int num_Lifes;
    public AudioSource DamageSFX;
    void Start()
    {
        num_Lifes = 3;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RemoveLife()
    {
        DamageSFX.Play();

        num_Lifes--;
        if(num_Lifes > 0)
        {
            LifesImg_List[num_Lifes].SetActive(false);
        }
        else
        {
            this.gameObject.GetComponent<InGame_PlayerScore>().EndGame();
        }
    }
}
