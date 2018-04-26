using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
        /*for (int i = 0; i < 21; ++i) {
            Debug.Log ("Button " + i + ": " + Input.GetKey( KeyCode.JoystickButton0 + i ) );
        }*/

        if (Input.GetKeyDown (KeyCode.JoystickButton10)) {
            Time.timeScale = 1;
            SceneManager.LoadScene( "Game");
        }

        if( Input.GetButtonDown( "Attack" ) || Input.GetKeyDown( KeyCode.JoystickButton16 ) )
        {
            OnAttackPressed();
        }
    }

    void UpdateAction()
    {
        if( Input.GetButtonDown( "Interact" ) || Input.GetKeyDown( KeyCode.JoystickButton17 ) )
        {
            OnActionPressed();
        }
    }

    void UpdateDirection()
    {
        Vector2 newDirection = new Vector2(
            Input.GetAxisRaw( "Horizontal" ),
            Input.GetAxisRaw( "Vertical" ) );

        SetDirection( GetDiagonalizedDirection( newDirection, 0.4f ) );
    }
}
