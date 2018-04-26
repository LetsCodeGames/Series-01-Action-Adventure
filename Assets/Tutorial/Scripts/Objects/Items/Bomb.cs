using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
    public float TimeUntilExplosion;
    public GameObject ExplosionPrefab;

    float m_CreationTime;

    void Start() 
    {
        m_CreationTime = Time.time;
    }
    
    void Update() 
    {
        float elapsedTime = Time.time - m_CreationTime;

        if( elapsedTime >= TimeUntilExplosion )
        {
            Destroy( gameObject );
            Instantiate( ExplosionPrefab, transform.position, Quaternion.identity );
        }
    }
}
