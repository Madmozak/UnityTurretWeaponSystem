using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TurretBase))]
public class TurretConstruction : Editor
{
    private GameObject[] weapons;
    private GameObject[] turretBases;
    private GameObject[] turretMainComponents;
    private string[] weaponList1;
    private string[] weaponList2;
    private string[] turretBasesList;
    private string[] turretMainComponentsList;
    int indexWeaponList1 = 0;
    int indexWeaponList2 = 0;
    int turretBasesIndex = 0;
    int turretMainComponentsIndex = 0;

    void OnEnable()
    {
        weapons = Resources.LoadAll<GameObject>("Weapons");
        turretBases = Resources.LoadAll<GameObject>("TurretElements/TurretBases");
        turretMainComponents = Resources.LoadAll<GameObject>("TuretMains");
        weaponList1 = new string[weapons.Length];
        weaponList2 = new string[weapons.Length];
        turretBasesList = new string[turretBases.Length];
        turretMainComponentsList = new string[turretMainComponents.Length];
        
        foreach (var item in weapons)
        {
            weaponList1[indexWeaponList1] = item.ToString();
            weaponList2[indexWeaponList2] = item.ToString();
            indexWeaponList1++;
            indexWeaponList2++;
        }

        foreach (var item in turretBases)
        {
            turretBasesList[turretBasesIndex] = item.ToString();
            turretBasesIndex++;
        }

        foreach (var item in turretMainComponents)
        {
            turretMainComponentsList[turretMainComponentsIndex] = item.ToString();
            turretMainComponentsIndex++;
        }

        
    }

    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();
        
        GUILayout.Space(20);
        GUILayout.Label("CONSTRUCTION SCRIPT", EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label("Turret Base");
        turretBasesIndex = EditorGUILayout.Popup(turretBasesIndex, turretBasesList);
        GUILayout.Label("Turet Main Component");
        turretMainComponentsIndex = EditorGUILayout.Popup(turretMainComponentsIndex, turretMainComponentsList);
        GUILayout.Label("Weapon1");
        indexWeaponList1 = EditorGUILayout.Popup(indexWeaponList1, weaponList1);
        GUILayout.Label("Weapon2");
        indexWeaponList2 = EditorGUILayout.Popup(indexWeaponList2, weaponList2);
        var turretBaseScript = target as TurretBase;

        if (GUI.changed)
        {
            turretBaseScript.weapons.Clear();
            foreach (var item in turretBaseScript.weaponAttachements)
            {
                for (var i = item.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(item.GetChild(i).gameObject);
                }

                turretBaseScript.AttachWeapons();
            }
            turretBaseScript.weapons.Add(Instantiate(weapons[indexWeaponList1], turretBaseScript.weaponAttachements[0]));
        }

        GUILayout.Label("Weapons");
        if(GUILayout.Button("Build Weapons", GUILayout.Width(100)))
        {
            turretBaseScript.weapons.Clear();
            foreach (var item in turretBaseScript.weaponAttachements)
            {
                for (var i = item.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(item.GetChild(i).gameObject);
                }

                turretBaseScript.AttachWeapons();
            }

            if(!(indexWeaponList1 > weaponList1.Length) || !(indexWeaponList2 > weaponList2.Length))
            {
                turretBaseScript.weapons.Add(Instantiate(weapons[indexWeaponList1], turretBaseScript.weaponAttachements[0]));
                turretBaseScript.weapons.Add(Instantiate(weapons[indexWeaponList2], turretBaseScript.weaponAttachements[1]));
            }
            

        }


    }
}
