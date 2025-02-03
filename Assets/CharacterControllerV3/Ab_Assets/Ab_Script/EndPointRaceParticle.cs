using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointRaceParticle : MonoBehaviour
{
    public ParticleSystem raceEndPointParticel_1;
    public ParticleSystem raceEndPointParticel_2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("RacePlayer"))
        {
            if(other.gameObject.GetComponentInParent<RacePlayer>())
            {
                raceEndPointParticel_1.Play();
                raceEndPointParticel_2.Play();
            }
        }
    }
}
