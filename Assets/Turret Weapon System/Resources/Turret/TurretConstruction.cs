using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TurretBase))]
public class TurretConstruction : Editor
{
    private GameObject[] weapons;
    public string[] nameList;
    int index = 0;

    void OnEnable()
    {
        weapons = Resources.LoadAll<GameObject>("Weapons");
        nameList = new string[weapons.Length];
        foreach (var item in weapons)
        {
            nameList[index] = item.ToString();
            index++;
        }
        index = 0;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        index = EditorGUILayout.Popup(index, nameList);
        var turret = target as TurretBase;
        //TurretBase turretBase = (TurretBase)target;
        turret.testString = nameList[index];

        if(GUILayout.Button("Build Weapon 1"))
        {

            for (var i = turret.weaponAttachements[0].childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(turret.weaponAttachements[0].GetChild(i).gameObject);
            }
            Instantiate(weapons[index], turret.weaponAttachements[0]);
          
            turret.AttachWeapons();

        }

        if (GUILayout.Button("Build Weapon 2"))
        {
            //DestroyImmediate(turret.weaponAttachements[1].transform.GetChild(0).gameObject);
            
            for (var i = turret.weaponAttachements[1].childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(turret.weaponAttachements[1].GetChild(i).gameObject);
            }
            Instantiate(weapons[index], turret.weaponAttachements[1]);
            turret.AttachWeapons();

        }


    }
}
