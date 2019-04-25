using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHealthModel : MonoBehaviour 
{
    public float StartingHealth;
    
    private float m_MaximumHealth;
    private float m_Health;

    private List<ArmorModel> m_ArmorModels = new List<ArmorModel>();

    void Start()
    {
        m_Health = StartingHealth;
        m_MaximumHealth = StartingHealth;
    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.T ) )
        {
            DealDamage( 10 );
        }
    }

    public float GetHealth()
    {
        return m_Health;
    }

    public float GetMaximumHealth()
    {
        return m_MaximumHealth;
    }

    public float GetHealthPercentage()
    {
        return m_Health / m_MaximumHealth;
    }

    public float GetTotalArmor()
    {
        float totalArmor = 0;

        for( int i = 0; i < m_ArmorModels.Count; ++i )
        {
            totalArmor = m_ArmorModels[ i ].GetArmor();
        }

        return totalArmor;
    }

    public float GetTotalMaximumArmor()
    {
        float totalArmor = 0;

        for( int i = 0; i < m_ArmorModels.Count; ++i )
        {
            totalArmor = m_ArmorModels[ i ].GetMaximumArmor();
        }

        return totalArmor;
    }

    public float GetTotalArmorPercentage()
    {
        return GetTotalArmor() / GetTotalMaximumArmor();
    }

    public void DealDamage( float damage )
    {
        if( m_Health <= 0 )
        {
            return;
        }

        UIDamageNumbers.Instance.ShowDamageNumber( damage, transform.position );

        float healthDamage = damage;
        float damageAbsorbedByArmor = 0;
        float totalDamageToAbsorb = damage * 0.5f;

        m_ArmorModels.Sort( delegate( ArmorModel armor1, ArmorModel armor2 )
        {
            return armor1.GetArmor().CompareTo( armor2.GetArmor() );
        } );
        
        for( int i = 0; i < m_ArmorModels.Count; ++i )
        {
            float damageToAbsorb = totalDamageToAbsorb / m_ArmorModels.Count;

            if( damageToAbsorb > m_ArmorModels[ i ].GetArmor() )
            {
                damageToAbsorb = m_ArmorModels[ i ].GetArmor();
            }

            m_ArmorModels[ i ].DealDamage( damageToAbsorb );

            damageAbsorbedByArmor += damageToAbsorb;
        }

        healthDamage -= damageAbsorbedByArmor;
        m_Health -= healthDamage;

        if( m_Health <= 0 )
        {
            m_Health = 0;
            Debug.Log( "We died!" );
        }
    }

    public void RegisterArmor( ArmorModel armor )
    {
        m_ArmorModels.Add( armor );
    }

    public void UnregisterArmor( ArmorModel armor )
    {
        m_ArmorModels.Remove( armor );
    }
}
