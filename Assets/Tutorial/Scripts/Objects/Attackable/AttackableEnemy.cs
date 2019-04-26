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
    CharacterBatControl m_Control;


    float m_Health;

    AudioSource deathSound;

    void Awake()
    {
        m_Health = MaxHealth;
        deathSound = GetComponent(typeof(AudioSource)) as AudioSource;
        m_Control = GetComponentInParent<CharacterBatControl>();
    }

    public float GetHealth()
    {
        return m_Health;
    }

    public override void OnHit( Collider2D hitCollider, ItemType item )
    {
        float damage = 10;

        m_Health -= damage;
        if (UIDamageNumbers.Instance == null)
        {
            Debug.Log("No Instance!");
        }
        else
        {
            UIDamageNumbers.Instance.ShowDamageNumber(damage, transform.position);
        }

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
        yield return new WaitForSeconds(deathSound.clip.length);
        Destroy( DestroyObjectOnDeath );
    }

    IEnumerator CreateDeathFXRoutine( float delay )
    {
        yield return new WaitForSeconds( delay );

        Instantiate( DeathFX, transform.position, Quaternion.identity );

        var c_all = m_Control.gameObject.GetComponentsInChildren<Collider2D>();
        foreach (var c in c_all)
        {
            DestroyImmediate(c);
        }

        if (deathSound) {
            deathSound.Play();
        }
        else {
            Debug.Log("No sound Available!");
        }
    }
}
