using UnityEngine;
using System.Collections;

public class CharacterGamepadControl : CharacterBaseControl 
{
    void Start() 
    {
        
    }

    void Update() 
    {
        UpdateDirection();
        UpdateAction();
        UpdateAttack();
    }

    void UpdateAttack()
    {
        if( Input.GetButtonDown( "Attack" ) )
        {
            OnAttackPressed();
        }
    }

    void UpdateAction()
    {
        if( Input.GetButtonDown( "Interact" ) )
        {
            OnActionPressed();
        }
    }

    void UpdateDirection()
    {
        Vector2 newDirection = new Vector2(
            Input.GetAxisRaw( "Horizontal" ),
            Input.GetAxisRaw( "Vertical" ) );

        float threshold = 0.4f;

        if( Mathf.Abs( newDirection.x ) < threshold )
        {
            newDirection.x = 0;
        }
        else
        {
            newDirection.x = Mathf.Sign( newDirection.x );
        }

        if( Mathf.Abs( newDirection.y ) < threshold )
        {
            newDirection.y = 0;
        }
        else
        {
            newDirection.y = Mathf.Sign( newDirection.y );
        }

        SetDirection( newDirection );
    }
}
