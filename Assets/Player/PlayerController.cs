using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public ElevatorHealth elevator;
    public GameObject heldWeapon;
    public SteamVR_TrackedObject trackedObject;
    void Start()
    {
        elevator = GameObject.FindGameObjectWithTag("Elevator").GetComponent<ElevatorHealth>();
    }
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);
        if (heldWeapon != null && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)&&elevator.health>=0)
        {
            FireArm arm = heldWeapon.GetComponent<FireArm>();
            arm.Fire();
            device.TriggerHapticPulse(arm.PulseDuration());
        }
    }
}
