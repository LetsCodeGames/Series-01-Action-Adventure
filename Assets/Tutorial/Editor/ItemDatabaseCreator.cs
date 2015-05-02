using UnityEngine;
using UnityEditor;
using System.Collections;

public class ItemDatabaseCreator : MonoBehaviour 
{
    [MenuItem( "Let's Code Games/Databases/Create Item Database" )]
    public static void CreateItemDatabase()
    {
        ItemDatabase newDatabase = ScriptableObject.CreateInstance<ItemDatabase>();
        AssetDatabase.CreateAsset( newDatabase, "Assets/ItemDatabase.asset" );
        AssetDatabase.Refresh();
    }
}
