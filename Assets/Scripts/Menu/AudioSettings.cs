using UnityEngine;
using FMODUnity;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.VCA SfxVCA;
    FMOD.Studio.VCA MusicVCA;
    [SerializeField] float decibelLimit;

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
        if (dB > decibelLimit)
        {
            SfxVCA.setVolume(DecibelToLinear(dB));
        }
        else
        {
            SfxVCA.setVolume(DecibelToLinear(-100));
        }

    }

    public void Music(float dB)
    {

        if (dB > decibelLimit)
        {
            MusicVCA.setVolume(DecibelToLinear(dB));
        }
        else
        {
            MusicVCA.setVolume(DecibelToLinear(-100));
        }
       
    }
    
}
