using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleController : MonoBehaviour
{
    public ParticleSystem part;
    List<ParticleCollisionEvent> collEvents = new List<ParticleCollisionEvent>();
    List<ParticleSystem.Particle> enterParts = new List<ParticleSystem.Particle>();

    void OnParticleCollision(GameObject other)
    {
        int colCount = part.GetCollisionEvents(other, collEvents);

        for(int i = 0; i < colCount; i++)
        {
            Debug.DrawRay(collEvents[i].intersection, collEvents[i].normal * 3.0f);
        }
    }

    void OnParticleTrigger()
    {
        int partCount = part.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParts);

        for(int i = 0; i < partCount; i++)
        {
            ParticleSystem.Particle p = enterParts[i];

            //Randomize the complete color spectrum Color(Rand, Rand, Rand, 1)
            p.startColor = new Color(Random.value, Random.value, Random.value, 1.0f);

            enterParts[i] = p;
        }

        part.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParts);
    }
}
