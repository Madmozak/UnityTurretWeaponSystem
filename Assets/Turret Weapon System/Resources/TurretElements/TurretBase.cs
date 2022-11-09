using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    public List<GameObject> weapons;
    
    //public string testString;
    [SerializeField] private GameObject turretMain;
    [SerializeField] private GameObject turretBase;

    [HideInInspector] public Transform[] weaponAttachements;
    public GameObject target;

    public void AttachWeapons()
    {
        Debug.Log("Attached weapon");
    }
    private void TrackTarget()
    {
        turretMain.transform.LookAt(target.transform);
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
