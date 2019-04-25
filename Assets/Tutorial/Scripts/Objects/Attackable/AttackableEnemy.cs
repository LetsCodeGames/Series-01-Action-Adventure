using UnityEngine;
using System.Collections;

public class AttackableEnemy : AttackableBase 
{
    public float MaxHealth;
    public GameObject DestroyObjectOnDeath;
    public float DestroyDelayOnDeath;
    public CharacterMovementModel MovementModel;
    public float HitPushStrength;
    public float HitPushDuration;
    public GameObject DeathFX;
    public float DelayDeathFX;
    Character attackableCharacter;

    float m_Health;

    AudioSource deathSound;

    void Awake()
    {
        m_Health = MaxHealth;
        deathSound = GetComponent(typeof(AudioSource)) as AudioSource;
        attackableCharacter = GetComponentInParent<Character>();
    }

    public float GetHealth()
    {
        return m_Health;
    }

    public override void OnHit( Collider2D hitCollider, ItemType item )
    {
        if (attackableCharacter.isDead) return;
        float damage = 10;

        m_Health -= damage;
        UIDamageNumbers.Instance.ShowDamageNumber( damage, transform.position );

        if( MovementModel != null )
        {
            Vector3 pushDirection = transform.position - hitCollider.gameObject.transform.position;
            pushDirection = pushDirection.normalized * HitPushStrength;

            MovementModel.PushCharacter( pushDirection, HitPushDuration );
        }

        if( m_Health <= 0 )
        {
            StartCoroutine( DestroyRoutine( DestroyDelayOnDeath ) );

            if( DeathFX != null )
            {
                StartCoroutine( CreateDeathFXRoutine( DelayDeathFX ) );
            }
        }
    }

    IEnumerator DestroyRoutine( float delay )
    {
        yield return new WaitForSeconds( delay );

        BroadcastMessage( "OnLootDrop", SendMessageOptions.DontRequireReceiver );
        attackableCharacter.isDead = true;
        attackableCharacter.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3);
        Destroy( DestroyObjectOnDeath );
    }

    IEnumerator CreateDeathFXRoutine( float delay )
    {
        yield return new WaitForSeconds( delay );

        Instantiate( DeathFX, transform.position, Quaternion.identity );

        if (deathSound) {
            deathSound.Play();
        }
        else {
            Debug.Log("No sound Available!");
        }
    }
}
