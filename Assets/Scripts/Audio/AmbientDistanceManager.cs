using FMODUnity;
using UnityEngine;
using FMOD.Studio;

public class AmbientDistanceManager : MonoBehaviour
{
    public StudioEventEmitter jungleEmitter;  // Przypisz Jungle Event Emitter
    public StudioEventEmitter caveEmitter;    // Przypisz Cave Event Emitter

    public Transform player;       // Przypisz gracza
    public Transform caveEntrance; // Punkt wejœcia do jaskini
    public Transform caveDeep;     // G³êbszy punkt w jaskini

    public float entranceRadius = 5f; // Dystans, w którym jungle ambient przechodzi na 3D
    public float caveRadius = 3f;     // Dystans, po którym odpalamy cave ambient

    private bool isInCave = false;

    void Start()
    {
        jungleEmitter.Play(); // Jungle ambient startuje jako 2D
    }

    void Update()
    {
        float distanceToEntrance = Vector3.Distance(player.position, caveEntrance.position);
        float distanceToDeep = Vector3.Distance(player.position, caveDeep.position);

        if (!isInCave)
        {
            if (distanceToEntrance < entranceRadius)
            {
                // Jungle ambient przechodzi na 3D
                jungleEmitter.EventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(player.position));
                
            }
            else
            {
                // Jungle ambient pozostaje 2D
                jungleEmitter.EventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(Camera.main.transform));
            }

            if (distanceToDeep < caveRadius)
            {
                EnterCave();
            }
        }
        else
        {
            if (distanceToDeep > caveRadius)
            {
                ExitCave();
            }
        }
    }

    void EnterCave()
    {
        isInCave = true;
        jungleEmitter.Stop();
        caveEmitter.Play();
    }

    void ExitCave()
    {
        isInCave = false;
        caveEmitter.Stop();
        jungleEmitter.Play();
    }
}
