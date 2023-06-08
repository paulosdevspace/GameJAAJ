using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;


    private void Awake()
    {

    }
    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);

        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        healthSystem.OnDied += HealthSystem_OnDied;
    }



    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        //SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        //SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        Destroy(gameObject);
    }
}
