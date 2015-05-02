using UnityEngine;
using System.Collections;

public class CharacterKeyboardControl : CharacterBaseControl 
{
    void Start() 
    {
        SetDirection( new Vector2( 0, -1 ) );
    }

    void Update() 
    {
        UpdateDirection();
        UpdateAction();
        UpdateAttack();
    }

    void UpdateAttack()
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            OnAttackPressed();
        }
    }

    void UpdateAction()
    {
        if( Input.GetKeyDown( KeyCode.E ) )
        {
            OnActionPressed();
        }
    }

    void UpdateDirection()
    {
        Vector2 newDirection = Vector2.zero;

        if( Input.GetKey( KeyCode.W ) )
        {
            newDirection.y = 1;
        }

        if( Input.GetKey( KeyCode.S ) )
        {
            newDirection.y = -1;
        }

        if( Input.GetKey( KeyCode.A ) )
        {
            newDirection.x = -1;
        }

        if( Input.GetKey( KeyCode.D ) )
        {
            newDirection.x = 1;
        }

        SetDirection( newDirection );
    }
}
