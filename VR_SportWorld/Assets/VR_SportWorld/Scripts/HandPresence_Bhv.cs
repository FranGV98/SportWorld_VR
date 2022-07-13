using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence_Bhv : MonoBehaviour
{
    private InputDevice targetDevice;
    public List<GameObject> controllerPrefabs;
    private GameObject spawnedController;
    void Start()
    {
        List<InputDevice> devices_list = new List<InputDevice>();
        InputDeviceCharacteristics R_Controller_Chars = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(R_Controller_Chars, devices_list);

        foreach (var i in devices_list)
        {
            Debug.Log(i.name + i.characteristics);
        }

        if(devices_list.Count > 0)
        {
            targetDevice = devices_list[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

            if(prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Not corresponding model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }
    }

    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
            Debug.Log("dandole duro ");

        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
            Debug.Log(triggerValue);

        //targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        //if (triggerValue > 0.1f)
        //    Debug.Log(triggerValue);
    }
}
