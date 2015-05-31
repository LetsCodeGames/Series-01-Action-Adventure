using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomData
{
    public string Name;
    public int X;
    public int Y;
}
    
public class RoomManager : MonoBehaviour 
{
    public static RoomManager Instance;

    string m_CurrentWorldName = "Overworld";

    RoomData m_CurrentLoadRoomData;
    Queue<RoomData> m_LoadRoomQueue = new Queue<RoomData>();
    List<RoomParent> m_LoadedRooms = new List<RoomParent>();

    bool m_IsLoadingRoom = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadRoom( "Start", 0, 0 );
        LoadRoom( "End", 1, -2 );
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if( m_IsLoadingRoom == true )
        {
            return;
        }

        if( m_LoadRoomQueue.Count == 0 )
        {
            return;
        }

        m_CurrentLoadRoomData = m_LoadRoomQueue.Dequeue();
        m_IsLoadingRoom = true;

        //Debug.Log( "Loading new room: " + m_CurrentLoadRoomData.Name + " at " + m_CurrentLoadRoomData.X + ", " + m_CurrentLoadRoomData.Y );

        StartCoroutine( LoadRoomRoutine( m_CurrentLoadRoomData ) );
    }

    void LoadRoom( string name, int x, int y )
    {
        if( DoesRoomExist( x, y ) == true )
        {
            return;
        }

        RoomData newRoomData = new RoomData();
        newRoomData.Name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        m_LoadRoomQueue.Enqueue( newRoomData );
    }

    IEnumerator LoadRoomRoutine( RoomData data )
    {
        string levelName = m_CurrentWorldName + data.Name;

        AsyncOperation loadLevel = Application.LoadLevelAdditiveAsync( levelName );

        while( loadLevel.isDone == false )
        {
            //Debug.Log( "Loading " + levelName + ": " + Mathf.Round( loadLevel.progress * 100 ) + "%" );
            yield return null;
        }   
    }

    public void RegisterRoom( RoomParent roomParent )
    {
        roomParent.transform.position = new Vector3( 
            m_CurrentLoadRoomData.X * roomParent.Width,
            m_CurrentLoadRoomData.Y * roomParent.Height, 0 );

        roomParent.X = m_CurrentLoadRoomData.X;
        roomParent.Y = m_CurrentLoadRoomData.Y;
        roomParent.name = m_CurrentWorldName + "-" + m_CurrentLoadRoomData.Name + " " + roomParent.X + ", " + roomParent.Y;
        roomParent.transform.parent = transform;

        m_IsLoadingRoom = false;

        if( m_LoadedRooms.Count == 0 )
        {
            GameCamera.Instance.CurrentRoom = roomParent;
        }

        m_LoadedRooms.Add( roomParent );
    }

    bool DoesRoomExist( int x, int y )
    {
        return m_LoadedRooms.Find( item => item.X == x && item.Y == y ) != null;
    }

    string GetRandomRegularRoomName()
    {
        string[] possibleRooms = new string[] { 
            "Empty", 
            "Regular00", 
            "Regular01", 
            "Regular02", 
            "Regular03", 
            "Regular04",
            "Puzzle01", 
        };

        return possibleRooms[ Random.Range( 0, possibleRooms.Length ) ];
    }

    public void OnPlayerEnterRoom( RoomParent roomParent )
    {
        GameCamera.Instance.CurrentRoom = roomParent;

        LoadRoom( GetRandomRegularRoomName(), roomParent.X + 1, roomParent.Y );
        LoadRoom( GetRandomRegularRoomName(), roomParent.X - 1, roomParent.Y );
        LoadRoom( GetRandomRegularRoomName(), roomParent.X, roomParent.Y + 1 );
        LoadRoom( GetRandomRegularRoomName(), roomParent.X, roomParent.Y - 1 );
    }
}
