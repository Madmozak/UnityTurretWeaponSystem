using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [SerializeField] private WeaponBase[] weapons;
    [SerializeField] private GameObject turretMain;
    // in miliseconds
    [SerializeField] private int shootDelayBetweenWeapons;

    public GameObject target;

    private void TrackTarget()
    {
        turretMain.transform.LookAt(target.transform);
    }

    private async void FireWeapons()
    {
        foreach (WeaponBase weapon in weapons)
        {
            weapon.Shoot();
            await Task.Delay(shootDelayBetweenWeapons);
        }

        
    }
}
