using System;
using System.Collections;
using System.Collections.Generic;
using VRTK;
using UnityEngine;

public class FlashLight : MonoBehaviour, FireArm {

    public VRTK_InteractableObject linkedObject;
    [SerializeField] private float degradeDelta = 1f;
    [SerializeField] private Light spotlight;
    [SerializeField] private float maxBattery = 100f;
    private ushort pulse = 500;

    [SerializeField] private float maxIntensity = 10f;
    [SerializeField] private float battery = 100f;
    private bool lightOn;

    public void Start()
    {
        battery = maxBattery;
        spotlight.intensity = maxIntensity;
        lightOn = true;
    }

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
        }
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
        }
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        Fire();
    }

    public void Fire()
    {
        GetComponent<AudioSource>().Play();
        if (lightOn)
        {
            lightOn = false;
            ChangeIntensity(0);
        }
        else
        {
            lightOn = true;
            int currentBattery = (int)battery;
            ChangeIntensity(currentBattery);
        }
    }

    public void Reload(int ammo)
    {
        battery = ammo;
        ChangeIntensity(ammo);
    }



    // Update is called once per frame
    void Update () {
        int currentBattery = (int)battery;
        float delta = Time.deltaTime;
        if (lightOn) { 
            if (currentBattery % 25 == 0)
                ChangeIntensity(currentBattery);
            battery = Mathf.Clamp(battery - (delta * degradeDelta), 0f, maxBattery);
        }

    }

    private void ChangeIntensity(int currentBattery)
    {
        if (currentBattery != 0)
        {
            spotlight.intensity = maxIntensity - ((maxBattery-battery) / maxBattery);
        }
        else
        {
            spotlight.intensity = 0f;
        }
    }

    public ushort PulseDuration()
    {
        return (ushort)pulse;
    }
    
}
