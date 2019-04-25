using UnityEngine;
using System.Collections;

public class DestroyParticlesWhenDone : MonoBehaviour 
{
	ParticleSystem m_Particles;

	// Use this for initialization
	void Awake() 
	{
		m_Particles = GetComponent<ParticleSystem> ();
        SoundFile = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (m_Particles.IsAlive() == false) 
        {
			Destroy(gameObject);
		}
	}

    public void PlayDeathSound()
    {
        if (!SoundFile) return;
        SoundFile.Play();
    }
}
