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
    int weaponList1Index;
    int weaponList2Index;
    int turretBasesIndex;
    int turretMainComponentsIndex;
    public float heightOfMainComponent;

    void OnEnable()
    {
        var turretBaseScript = target as Turret;

        weapons = Resources.LoadAll<GameObject>("Weapons");
        turretBases = Resources.LoadAll<GameObject>("TurretElements/TurretBases");
        turretMainComponents = Resources.LoadAll<GameObject>("TurretElements/TurretMains");
        weaponList1 = new string[weapons.Length];
        weaponList2 = new string[weapons.Length];
        turretBasesList = new string[turretBases.Length];
        turretMainComponentsList = new string[turretMainComponents.Length];
        weaponList1Index = turretBaseScript.WeaponIndex1;
        weaponList2Index = turretBaseScript.WeaponIndex2;
        turretBasesIndex = turretBaseScript.TurretBaseIndex;
        turretMainComponentsIndex = turretBaseScript.TurretMainComponentIndex;
        heightOfMainComponent = turretBaseScript.heightOfMainComponent;

        for (int i = 0; i < weapons.Length; i++)
        {
            weaponList1[i] = weapons[i].ToString();
            weaponList2[i] = weapons[i].ToString();
        }

        for (int i = 0; i < turretBases.Length; i++)
        {
            turretBasesList[i] = turretBases[i].ToString();
            
        }

        for (int i = 0; i < turretMainComponents.Length; i++)
        {
            turretMainComponentsList[i] = turretMainComponents[i].ToString();
        }
        
        
    }

    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();
        
        GUILayout.Space(20);
        GUILayout.Label("CONSTRUCTION SCRIPT", EditorStyles.boldLabel);

        ChangedTurretBase((Turret)target);
        ChangedTurretMainComponent((Turret)target);
        ChangedWeapons((Turret)target);

    }

    private void ChangedWeapons(Turret turretBaseScript)
    {
        EditorGUI.BeginChangeCheck();
        turretBaseScript.WeaponIndex1 = weaponList1Index;
        turretBaseScript.WeaponIndex2 = weaponList2Index;
        GUILayout.Label("Weapon1");
        weaponList1Index = EditorGUILayout.Popup(weaponList1Index, weaponList1);
        GUILayout.Label("Weapon2");
        weaponList2Index = EditorGUILayout.Popup(weaponList2Index, weaponList2);

        if (EditorGUI.EndChangeCheck())
        {
            turretBaseScript.weapons.Clear();
            foreach (var item in turretBaseScript.weaponAttachements)
            {
                for (var i = item.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(item.GetChild(i).gameObject);
                }

                
            }
            turretBaseScript.weapons.Add(Instantiate(weapons[weaponList1Index], turretBaseScript.weaponAttachements[0]));
            turretBaseScript.weapons.Add(Instantiate(weapons[weaponList2Index], turretBaseScript.weaponAttachements[1]));
        }
    }

    private void ChangedTurretBase(Turret turretBaseScript)
    {
        turretBaseScript.TurretBaseIndex = turretBasesIndex;
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("Turret Base");
        turretBasesIndex = EditorGUILayout.Popup(turretBasesIndex, turretBasesList);

        if (EditorGUI.EndChangeCheck())
        {
            DestroyImmediate(turretBaseScript.TurretBase); 
            turretBaseScript.TurretBase = null;
            turretBaseScript.TurretBase = Instantiate(turretBases[turretBasesIndex], turretBaseScript.transform);
        }
            
    }

    private void ChangedTurretMainComponent(Turret turretBaseScript)
    {
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("Turret Main Component");
        turretBaseScript.TurretMainComponentIndex = turretMainComponentsIndex;
        turretBaseScript.heightOfMainComponent = heightOfMainComponent;
        turretMainComponentsIndex = EditorGUILayout.Popup(turretMainComponentsIndex, turretMainComponentsList);
        heightOfMainComponent = EditorGUILayout.Slider("Height",heightOfMainComponent, -10.0f, 10.0f);

        if (EditorGUI.EndChangeCheck())
        {
            DestroyImmediate(turretBaseScript.TurretMainComponent);
            turretBaseScript.TurretMainComponent = null;
            turretBaseScript.TurretMainComponent = Instantiate(turretMainComponents[turretMainComponentsIndex], 
                turretBaseScript.transform.position + new Vector3(0,heightOfMainComponent,0), Quaternion.identity, turretBaseScript.transform);
        }

    }
}
