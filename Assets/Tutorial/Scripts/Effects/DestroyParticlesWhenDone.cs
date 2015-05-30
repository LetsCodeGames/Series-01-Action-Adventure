using UnityEngine;
using System.Collections;

public class DestroyParticlesWhenDone : MonoBehaviour 
{
	ParticleSystem m_Particles;

	// Use this for initialization
	void Awake() 
	{
		m_Particles = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (m_Particles.IsAlive() == false) 
        {
			Destroy(gameObject);
		}
	}
}
