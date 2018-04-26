using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

[CustomEditor( typeof( CharacterInventoryModel ))]
public class CharacterInventoryModelEditor : Editor 
{
    CharacterInventoryModel m_Target;

    public override void OnInspectorGUI()
    {
        m_Target = (CharacterInventoryModel)target;

        DrawDefaultInspector();

        if( Application.isPlaying == true )
        {
            var itemTypeValues = (ItemType[])Enum.GetValues( typeof( ItemType ) );

            for( int i = 0; i < itemTypeValues.Length; ++i )
            {
                int itemCount = m_Target.GetItemCount( itemTypeValues[ i ] );

                if( itemCount > 0 )
                {
                    EditorGUILayout.LabelField( itemTypeValues[ i ].ToString(), itemCount.ToString() );
                }
            }
        }
    }
}
