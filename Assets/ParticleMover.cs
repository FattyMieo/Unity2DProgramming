using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleMover : MonoBehaviour
{
    public ParticleSystem part;
    public Transform target;
    ParticleSystem.Particle[] partArray = new ParticleSystem.Particle[1000];
    public float frequency = 1.0f;
    public float amplitude = 1.0f;
    public float decreasingFactor = 10.0f;

	void LateUpdate ()
    {
        int partCount = part.GetParticles(partArray);

        for(int i = 0; i < partCount; i++)
        {
            ParticleSystem.Particle p = partArray[i];

            // Move particle to target
            float time = (p.startLifetime - p.remainingLifetime) / p.startLifetime;
            p.position = Vector3.Lerp(p.position, target.position, time);

            Vector3 offset = Vector3.zero;
            offset.x = Mathf.Sin(Time.time * frequency * (1.0f - time) / decreasingFactor) * amplitude;
            p.position += offset;

            partArray[i] = p;
        }

        part.SetParticles(partArray, partCount);
	}
}
