using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
    public static GameCamera Instance;

    public RoomParent CurrentRoom;
    public float MovementSpeedOnRoomChange;

    void Awake() 
    {
        Instance = this;
    }

    void Update() 
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if( CurrentRoom == null )
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards( transform.position, targetPosition, Time.deltaTime * MovementSpeedOnRoomChange );
    }

    Vector3 GetCameraTargetPosition()
    {
        if( CurrentRoom == null )
        {
            return Vector3.zero;
        }

        Vector3 targetPosition = CurrentRoom.GetRoomCenter();
        targetPosition.z = transform.position.z;

        return targetPosition;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals( GetCameraTargetPosition() ) == false;
    }
}
