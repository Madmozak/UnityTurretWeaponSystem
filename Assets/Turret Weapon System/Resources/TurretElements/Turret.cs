using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> weapons;


    public GameObject TurretBase;

    public GameObject TurretMainComponent;
    [HideInInspector]
    public int TurretBaseIndex { get; set; }
    [HideInInspector]
    public int TurretMainComponentIndex { get; set; }
    [HideInInspector]
    public int WeaponIndex1 { get; set; }
    [HideInInspector]
    public int WeaponIndex2 { get; set; }
    [HideInInspector] 
    public Transform[] weaponAttachements;
    public GameObject target;
    [HideInInspector]
    public float heightOfMainComponent;

    public bool randomize;
    public void Start()
    {
        
    }
    public void Update()
    {
        
    }

    public void AttachWeapons()
    {
        Debug.Log("Attached weapon");
    }
    private void TrackTarget()
    {
        TurretMainComponent.transform.LookAt(target.transform);
    }

    private async void FireWeapons()
    {
        //foreach (WeaponBase weapon in weapons)
        //{
        //    weapon.Shoot();
        //    await Task.Delay(shootDelayBetweenWeapons);
        //}

        
    }
}
