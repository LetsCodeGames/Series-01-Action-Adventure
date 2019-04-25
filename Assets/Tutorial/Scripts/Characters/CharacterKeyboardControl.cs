using UnityEngine;
using System.Collections;

public class CharacterKeyboardControl : CharacterBaseControl
{
    void Start()
    {
        SetDirection(new Vector2(0, -1));
    }

    void Update()
    {
        UpdateDirection();
        UpdateActions();
        UpdateAttack();
    }

    void UpdateAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnAttackPressed();
        }
    }

    void UpdateActions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnActionPressed();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnPlaceBombPressed();
        }
    }

    void UpdateDirection()
    {
        Vector2 newDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newDirection.y = 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newDirection.y = -1;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newDirection.x = -1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newDirection.x = 1;
        }

        SetDirection(newDirection);
    }
}