using UnityEngine;
using FMODUnity;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.VCA SfxVCA;
    FMOD.Studio.VCA MusicVCA;

    private float DecibelToLinear(float dB) 
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SfxVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
        MusicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");


    }

    public void SFX(float dB)
    {
        SfxVCA.setVolume(DecibelToLinear(dB));
    }

    public void Music(float dB)
    {
        MusicVCA.setVolume(DecibelToLinear(dB));
    }
    
}
