using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIItemCounter : MonoBehaviour 
{
    public CharacterInventoryModel Inventory;
    public ItemType ItemType;
    public string NumberFormat;

    Text m_Text;

    void Awake() 
    {
        m_Text = GetComponent<Text>();
    }

    void Update() 
    {
        if( Inventory == null )
        {
            return;
        }

        m_Text.text = Inventory.GetItemCount( ItemType ).ToString( NumberFormat );
    }
}
