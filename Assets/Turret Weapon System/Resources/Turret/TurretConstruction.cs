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

        if(GUILayout.Button("Build Object"))
        {
            turret.AttachWeapons();
            foreach (var item in weapons)
            {
                Debug.Log(item.ToString());
            }
        }
     
        
    }
}
