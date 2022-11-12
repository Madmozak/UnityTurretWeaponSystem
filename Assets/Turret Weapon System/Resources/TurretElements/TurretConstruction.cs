using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Turret))]
public class TurretConstruction : Editor
{
    private GameObject[] weapons;
    private GameObject[] turretBases;
    private GameObject[] turretMainComponents;
    [HideInInspector]
    [SerializeField]
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
            //weaponList1[indexWeaponList1] = item.ToString();
            weaponList2[indexWeaponList2] = item.ToString();
            //indexWeaponList1++;
            indexWeaponList2++;
        }
        for (int i = 0; i < weapons.Length; i++)
        {
            weaponList1[i] = weapons[i].ToString();
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
        
        GUILayout.Label("Turet Main Component");
        turretMainComponentsIndex = EditorGUILayout.Popup(turretMainComponentsIndex, turretMainComponentsList);

        
        var turretBaseScript = target as Turret;

        ChangedTurretBase();
        ChangedWeapons((Turret)target);
        

        GUILayout.Label("Weapons");
        


    }

    private void ChangedWeapons(Turret turretBaseScript)
    {
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("Weapon1");
        indexWeaponList1 = EditorGUILayout.Popup(indexWeaponList1, weaponList1);
        GUILayout.Label("Weapon2");
        indexWeaponList2 = EditorGUILayout.Popup(indexWeaponList2, weaponList2);

        if (EditorGUI.EndChangeCheck())
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
            turretBaseScript.weapons.Add(Instantiate(weapons[indexWeaponList2], turretBaseScript.weaponAttachements[1]));
        }
    }

    private void ChangedTurretBase()
    {
        EditorGUI.BeginChangeCheck();
        GUILayout.Space(10);
        GUILayout.Label("Turret Base");
        turretBasesIndex = EditorGUILayout.Popup(turretBasesIndex, turretBasesList);
        if(EditorGUI.EndChangeCheck())
            Debug.Log("changed turret base");


    }
}
