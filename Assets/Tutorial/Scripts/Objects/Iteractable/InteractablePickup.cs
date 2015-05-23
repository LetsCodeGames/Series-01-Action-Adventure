using UnityEngine;
using System.Collections;

public class InteractablePickup : InteractableBase
{
    public float ThrowDistance = 5;
    public float ThrowSpeed = 3;

    Vector3 m_CharacterThrowPosition;
    Vector3 m_ThrowDirection;

    public override void OnInteract( Character character )
    {
        CharacterInteractionModel interactionModel = character.GetComponent<CharacterInteractionModel>();

        if( interactionModel == null )
        {
            return;
        }

        BroadcastMessage( "OnPickupObject", character, SendMessageOptions.DontRequireReceiver );

        interactionModel.PickupObject( this );
    }

    public void Throw( Character character )
    {
        StartCoroutine( ThrowRoutine( character.transform.position, character.Movement.GetFacingDirection() ) );
    }

    IEnumerator ThrowRoutine( Vector3 characterThrowPosition, Vector3 throwDirection )
    {
        transform.parent = null;

        Vector3 targetPosition = characterThrowPosition + throwDirection.normalized * ThrowDistance;

        while( transform.position != targetPosition )
        {
            transform.position = Vector3.MoveTowards( transform.position, targetPosition, ThrowSpeed * Time.deltaTime );
            yield return null;
        }

        BroadcastMessage( "OnObjectThrown", SendMessageOptions.DontRequireReceiver );
    }
}
