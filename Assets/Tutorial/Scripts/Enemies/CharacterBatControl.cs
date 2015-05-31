using UnityEngine;
using System.Collections;

public class CharacterBatControl : CharacterBaseControl 
{
    public float PushStrength;
    public float PushTime;
    public AttackableEnemy AttackableEnemy;
    public float AttackDamage;

    GameObject m_CharacterInRange;

    void Update()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        Vector2 direction = Vector2.zero;

        if( m_CharacterInRange != null )
        {
            direction = m_CharacterInRange.transform.position - transform.position;
            direction.Normalize();
        }

        if( AttackableEnemy != null && AttackableEnemy.GetHealth() <= 0 )
        {
            direction = Vector2.zero;
        }

        SetDirection( direction );
    }

    public void SetCharacterInRange( GameObject characterInRange )
    {
        m_CharacterInRange = characterInRange;
    }

    public void OnHitCharacter( Character character )
    {
        Vector2 direction = character.transform.position - transform.position;
        direction.Normalize();

        m_CharacterInRange = null;
        character.Movement.PushCharacter( direction * PushStrength, PushTime );

        character.Health.DealDamage( AttackDamage );
    }
}
