using UnityEngine;
using System.Collections;

public class CharacterMovementKeyboardControl : CharacterMovementBaseControl 
{
    void Start() 
    {
        
    }

    void Update() 
    {
        UpdateDirection();
        
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
