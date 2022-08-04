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
    void Start()
    {
        TryInitialize();
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
            }
        }      
    }

    void OpenPauseMenu()
    {
        //go_pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void DebugControllers()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
            Debug.Log("dandole duro ");

        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
            Debug.Log(triggerValue);

        targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonValue);
        if (menuButtonValue)
            OpenPauseMenu();
        //targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        //if (triggerValue > 0.1f)
        //    Debug.Log(triggerValue);
    }
}
