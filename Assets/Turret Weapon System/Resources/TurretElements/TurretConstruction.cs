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
    

    //public Object[][] testGameOjbjectList;

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

        GenerateList(weaponList1, weapons);
        GenerateList(weaponList2, weapons);
        GenerateList(turretMainComponentsList, turretMainComponents);
        GenerateList(turretBasesList, turretBases);


        
        if (turretBaseScript.randomize)
        {
            GenerateRandomTurret();

        }

    }

    private void GenerateRandomTurret()
    {
        EditorGUI.BeginChangeCheck();
        weaponList1Index = EditorGUILayout.Popup("Weapon1", Random.Range(0, weaponList1.Length - 1), weaponList1);
        weaponList2Index = EditorGUILayout.Popup("Weapon2", Random.Range(0, weaponList2.Length - 1), weaponList2);
        turretBasesIndex = EditorGUILayout.Popup(Random.Range(0, turretMainComponentsList.Length - 1), turretBasesList);
        turretMainComponentsIndex = EditorGUILayout.Popup(Random.Range(0, turretMainComponentsList.Length-1), turretMainComponentsList);
        EditorGUI.EndChangeCheck();
    }

    private void GenerateList(string[] slist, GameObject[] olist)
    {
        for (int i = 0; i < olist.Length; i++)
        {
            slist[i] = olist[i].ToString();
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
        weaponList1Index = EditorGUILayout.Popup("Weapon1", weaponList1Index, weaponList1);
        weaponList2Index = EditorGUILayout.Popup("Weapon2", weaponList2Index, weaponList2);

        if (EditorGUI.EndChangeCheck())
        {
            if (turretBaseScript.TurretMainComponent is not null)
            {
                turretBaseScript.weapons.Clear();
                foreach (var item in turretBaseScript.TurretMainComponent.GetComponent<TurretMain>().weaponMountTransforms)
                {
                    for (var i = item.childCount - 1; i >= 0; i--)
                    {
                        DestroyImmediate(item.GetChild(i).gameObject);
                    }
                }
                turretBaseScript.weapons.Add(Instantiate(weapons[weaponList1Index], 
                    turretBaseScript.TurretMainComponent.GetComponent<TurretMain>().weaponMountTransforms[0].transform));

                if(turretBaseScript.TurretMainComponent.GetComponent<TurretMain>().weaponMountTransforms.Count > 1)
                {
                    turretBaseScript.weapons.Add(Instantiate(weapons[weaponList2Index],
                     turretBaseScript.TurretMainComponent.GetComponent<TurretMain>().weaponMountTransforms[1].transform));
                }
            }
            
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
        
        turretMainComponentsIndex = EditorGUILayout.Popup(turretMainComponentsIndex, turretMainComponentsList);
        

        if (EditorGUI.EndChangeCheck())
        {
            if (turretBaseScript.TurretBase is not null)
            {
                DestroyImmediate(turretBaseScript.TurretMainComponent);

                turretBaseScript.TurretMainComponent = Instantiate(turretMainComponents[turretMainComponentsIndex],
                    turretBaseScript.TurretBase.GetComponent<TurretBase>().turretBaseMainBodyMount.transform.position,
                    Quaternion.identity,
                    turretBaseScript.transform);
            }
            

        }

    }
}
