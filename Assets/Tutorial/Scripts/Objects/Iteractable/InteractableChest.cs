using UnityEngine;
using System.Collections;

public class InteractableChest : InteractableBase
{
    public Sprite OpenChestSprite;
    public ItemType ItemInChest;
    public int Amount;

    private bool m_IsOpen;
    private SpriteRenderer m_Renderer;

    void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnInteract( Character character )
    {
        if( m_IsOpen == true )
        {
            return;
        }

        character.Inventory.AddItem( ItemInChest, Amount, PickupType.FromChest );
        m_Renderer.sprite = OpenChestSprite;
        m_IsOpen = true;
    }
}
