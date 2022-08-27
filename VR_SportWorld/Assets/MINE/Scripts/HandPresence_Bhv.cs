using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence_Bhv : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    public InGame_PlayerScore _player;

    void Start()
    {
        TryInitialize();

        _player = GameObject.Find("CommonVRPlayer").GetComponent<InGame_PlayerScore>();
    }

    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void TryInitialize()
    {
        List<InputDevice> devices_list = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices_list);

        foreach (var i in devices_list)
        {
            Debug.Log(i.name + i.characteristics);
        }

        if (devices_list.Count > 0)
        {
            targetDevice = devices_list[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Not corresponding model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if(!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();

                if(_player != null)
                {
                    targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonValue);
                    if (menuButtonValue)
                    {
                        _player.PauseGame();
                    }

                    switch (_player.currentMinigame)
                    {
                        case InGame_PlayerScore.Minigame.LegGame:
                            break;

                        case InGame_PlayerScore.Minigame.RacketGame:
                            TennisGameFeatures();
                            break;
                    }
                }
                //else { print("There's no player"); }
            }
        }      
    }

    void DebugControllers()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
            Debug.Log("dandole duro ");

        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
            Debug.Log(triggerValue);

        //    targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonValue);
        //if (menuButtonValue)
        //    OpenPauseMenu();
        //targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        //if (triggerValue > 0.1f)
        //    Debug.Log(triggerValue);
    }

    void TennisGameFeatures()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
        {
            GameObject _go_Racket;

            if (targetDevice.name == "Oculus Touch Controller - Left")
            {
                _go_Racket = GameObject.Find("RedRacket");
            }
            else
            {
                _go_Racket = GameObject.Find("BlueRacket");
            }

            Vector3 handPos = transform.position;
            _go_Racket.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            //RedRacket.transform.LookAt(handPos);
            _go_Racket.transform.position = Vector3.MoveTowards(_go_Racket.transform.position, handPos, triggerValue * 10 * Time.deltaTime);
        }


        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
        {
            GameObject Ball = GameObject.Find("RedSphere");
            Ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Ball.transform.position = Vector3.MoveTowards(Ball.transform.position, transform.position,  10 * Time.deltaTime);
        }
    }
}
