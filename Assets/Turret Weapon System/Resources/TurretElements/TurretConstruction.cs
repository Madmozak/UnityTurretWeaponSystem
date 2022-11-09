using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TurretBase))]
public class TurretConstruction : Editor
{
    private GameObject[] weapons;
    private string[] weaponList1;
    private string[] weaponList2;
    int indexWeaponList1 = 0;
    int indexWeaponList2 = 0;

    void OnEnable()
    {
        weapons = Resources.LoadAll<GameObject>("Weapons");
        weaponList1 = new string[weapons.Length];
        weaponList2 = new string[weapons.Length];
        
        foreach (var item in weapons)
        {
            weaponList1[indexWeaponList1] = item.ToString();
            weaponList2[indexWeaponList2] = item.ToString();
            indexWeaponList1++;
            indexWeaponList2++;
        }
        indexWeaponList1 = 0;
        indexWeaponList2 = 0;
    }

    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();

        GUILayout.Space(20);
        GUILayout.Label("CONSTRUCTION SCRIPT", EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label("Weapon1 List");
        indexWeaponList1 = EditorGUILayout.Popup(indexWeaponList1, weaponList1);
        GUILayout.Label("Weapon2 List");
        indexWeaponList2 = EditorGUILayout.Popup(indexWeaponList2, weaponList2);
        var turretBaseScript = target as TurretBase;

        
        GUILayout.Label("Weapons");
        if(GUILayout.Button("Build Weapons", GUILayout.Width(100)))
        {
            turretBaseScript.weapons.Clear();
            foreach (var item in turretBaseScript.weaponAttachements)
            {
                for (var i = item.childCount - 1; i >= 0; i--)
                {
                    Object.DestroyImmediate(item.GetChild(i).gameObject);
                }

                turretBaseScript.AttachWeapons();
            }

            turretBaseScript.weapons.Add(Instantiate(weapons[indexWeaponList1], turretBaseScript.weaponAttachements[0]));
            turretBaseScript.weapons.Add(Instantiate(weapons[indexWeaponList2], turretBaseScript.weaponAttachements[1]));

        }


    }
}
