using UnityEngine;
using System.Collections;

public class AttackableBush : AttackableBase
{
    public Sprite DestroyedSprite;
    public GameObject DestroyEffect;

    private SpriteRenderer m_Renderer;

    void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnHit( ItemType item )
    {
        m_Renderer.sprite = DestroyedSprite;

        if( GetComponent<Collider2D>() != null )
        {
            GetComponent<Collider2D>().enabled = false;
        }

        if( DestroyEffect != null )
        {
            GameObject destroyEffect = (GameObject)Instantiate( DestroyEffect );
            destroyEffect.transform.position = transform.position;
        }
    }
}
