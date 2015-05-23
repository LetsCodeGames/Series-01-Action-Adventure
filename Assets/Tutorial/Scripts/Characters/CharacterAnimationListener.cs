using UnityEngine;
using System.Collections;

public class CharacterAnimationListener : MonoBehaviour
{
    public CharacterMovementModel MovementModel;
    public CharacterMovementView MovementView;

    public void OnAttackStarted( AnimationEvent animationEvent )
    {
        if( MovementModel != null )
        {
            MovementModel.OnAttackStarted();
        }

        if( MovementView != null )
        {
            MovementView.OnAttackStarted();
        }

        ShowWeapon();
        SetSortingOrderOfWeapon( animationEvent.intParameter );
        SetShieldDirection( animationEvent.stringParameter );
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
            MovementView.ReleaseShieldDirection();
        }

        HideWeapon();
    }

    public void ShowWeapon()
    {
        if( MovementView != null )
        {
            MovementView.ShowWeapon();
        }
    }

    public void HideWeapon()
    {
        if( MovementView != null )
        {
            MovementView.HideWeapon();
        }
    }

    public void SetSortingOrderOfWeapon( int sortingOrder )
    {
        if( MovementView != null )
        {
            MovementView.SetSortingOrderOfWeapon( sortingOrder );
        }
    }

    public void SetSortingOrderOfPickupItem( int sortingOrder )
    {
        if( MovementView != null )
        {
            MovementView.SetSortingOrderOfPickupItem( sortingOrder );
        }
    }

    public void SetShieldDirection( string direction )
    {
        if( MovementView == null || direction == "" )
        {
            return;
        }

        switch( direction )
        {
        default:
            Debug.LogWarning( "Shield direction '" + direction + "' does not exist." );
            break;
        case "Front":
            MovementView.ForceShieldDirection( CharacterMovementView.ShieldDirection.Front );
            break;
        case "Back":
            MovementView.ForceShieldDirection( CharacterMovementView.ShieldDirection.Back );
            break;
        case "Left":
            MovementView.ForceShieldDirection( CharacterMovementView.ShieldDirection.Left );
            break;
        case "Right":
            MovementView.ForceShieldDirection( CharacterMovementView.ShieldDirection.Right );
            break;
        case "FrontHalf":
            MovementView.ForceShieldDirection( CharacterMovementView.ShieldDirection.FrontHalf );
            break;
        case "BackHalf":
            MovementView.ForceShieldDirection( CharacterMovementView.ShieldDirection.BackHalf );
            break;
        }

    }
}
