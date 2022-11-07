using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponFireRate;
    [SerializeField] private float weaponMaxSpread;
    private float fireTime;
    [SerializeField] private GameObject bulletModel;
    [SerializeField] private Transform[] muzzleTransforms;

    private void Update()
    {
        fireTime += Time.deltaTime;
    }
    public void Shoot()
    {
        if(fireTime > weaponFireRate)
        {
            foreach (Transform muzzleTransform in muzzleTransforms)
            {
                Instantiate(bulletModel, muzzleTransform.position, Quaternion.Euler(muzzleTransform.rotation.eulerAngles + new Vector3(Random.Range(-weaponMaxSpread, weaponMaxSpread),
                                                                                                                                Random.Range(-weaponMaxSpread, weaponMaxSpread), 0)));
            }
            
            fireTime = 0;
        }
            
    }


}
