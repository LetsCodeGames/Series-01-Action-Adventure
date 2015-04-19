using UnityEngine;
using System.Collections;

public class CharacterAnimationListener : MonoBehaviour
{
    public CharacterMovementModel MovementModel;
    public CharacterMovementView MovementView;

    public void OnAttackStarted()
    {
        if( MovementModel != null )
        {
            MovementModel.OnAttackStarted();
        }

        if( MovementView != null )
        {
            MovementView.OnAttackStarted();
        }
    }

    public void OnAttackFinished()
    {
        if( MovementModel != null )
        {
            MovementModel.OnAttackFinished();
        }

        if( MovementView != null )
        {
            MovementView.OnAttackFinished();
        }
    }
}
