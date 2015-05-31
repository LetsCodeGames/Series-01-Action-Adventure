using UnityEngine;
using System.Collections;

public class RoomDoorBlockingObject : MonoBehaviour 
{
    public float DestroyProbability;

    void Start() 
    {
        RoomParent roomParent = GetComponentInParent<RoomParent>();

        float randomValue = Random.Range( 0f, 1f );

        if( randomValue < DestroyProbability ||
            ( roomParent.X == 0 && roomParent.Y == 0 ) ||
            ( roomParent.X == 0 && roomParent.Y == 1 ) ||
            ( roomParent.X == 0 && roomParent.Y == -1 ) ||
            ( roomParent.X == 1 && roomParent.Y == -1 ) ||
            ( roomParent.X == 1 && roomParent.Y == -2 ) )
        {
            Destroy( gameObject );
        }
    }

    
}
