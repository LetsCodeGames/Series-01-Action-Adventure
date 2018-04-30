using UnityEngine;
using System.Collections;

public class CharacterActionModel : MonoBehaviour 
{
    public GameObject BombPrefab;

    Character m_Character;

    private void Awake()
    {
        m_Character = GetComponent<Character>();
    }

    public void PlaceBomb()
    {
        if( m_Character.Inventory.GetItemCount( ItemType.Bomb ) > 0 )
        {
            Instantiate( BombPrefab, transform.position, Quaternion.identity );
            m_Character.Inventory.AddItem( ItemType.Bomb, -1, PickupType.None );
        }
    }
}
