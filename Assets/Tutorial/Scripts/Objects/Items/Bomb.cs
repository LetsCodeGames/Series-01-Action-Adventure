using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
    public float TimeUntilExplosion;
    public float ExplosionRadius;
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
            DestroyDestructablesInRadius();
            OnExplode();
        }
    }

    void DestroyDestructablesInRadius()
    {
        Collider2D[] collidersInRadius = Physics2D.OverlapCircleAll( transform.position, ExplosionRadius );

        for( int i = 0; i < collidersInRadius.Length; ++i )
        {
            DestructableByBomb destructable = collidersInRadius[ i ].GetComponent<DestructableByBomb>();
            
            if( destructable != null )
            {
                destructable.OnDestroyedByBomb();
            }
        }
    }

    void OnExplode()
    {
        Destroy( gameObject );
        Instantiate( ExplosionPrefab, transform.position, Quaternion.identity );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere( transform.position, ExplosionRadius );
    }
}
