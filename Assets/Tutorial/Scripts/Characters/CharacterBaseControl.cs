using UnityEngine;
using System.Collections;

public class CharacterBaseControl : MonoBehaviour
{
    protected Character m_Character;

    void Awake()
    {
        m_Character = GetComponent<Character>();
    }

    protected Vector2 GetDiagonalizedDirection( Vector2 direction, float threshold )
    {
        if( Mathf.Abs( direction.x ) < threshold )
        {
            direction.x = 0;
        }
        else
        {
            direction.x = Mathf.Sign( direction.x );
        }

        if( Mathf.Abs( direction.y ) < threshold )
        {
            direction.y = 0;
        }
        else
        {
            direction.y = Mathf.Sign( direction.y );
        }

        return direction;
    }

    protected void SetDirection( Vector2 direction )
    {
        if( m_Character.Movement == null )
        {
            return;
        }

        m_Character.Movement.SetDirection( direction );
    }

    protected void OnActionPressed()
    {
        if( m_Character.Interaction == null )
        {
            return;
        }

        m_Character.Interaction.OnInteract();
    }

    protected void OnAttackPressed()
    {
        if( m_Character.Movement == null )
        {
            return;
        }

        if( m_Character.Movement.CanAttack() == false )
        {
            return;
        }

        m_Character.Movement.DoAttack();
        m_Character.MovementView.DoAttack();
    }

    protected void OnPlaceBombPressed()
    {
        if( m_Character.Action == null )
        {
            return;
        }

        m_Character.Action.PlaceBomb();
    }
}
