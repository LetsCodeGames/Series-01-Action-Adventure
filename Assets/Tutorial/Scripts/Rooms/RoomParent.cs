using UnityEngine;
using System.Collections;

public class RoomParent : MonoBehaviour 
{
    public int Width;
    public int Height;
    public int X;
    public int Y;

    void Start()
    {
        if( RoomManager.Instance == null )
        {
            Debug.LogWarning( "Pressed play in a room and not in the main scene" );
            return;
        }

        RoomManager.Instance.RegisterRoom( this );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube( transform.position, new Vector3( Width, Height, 0 ) );
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3( X * Width, Y * Height );
    }

    void OnTriggerEnter2D( Collider2D collider )
    {
        if( collider.tag == "Player" )
        {
            RoomManager.Instance.OnPlayerEnterRoom( this );
        }
    }
}
