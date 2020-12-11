/*
using UnityEngine;

public class ReverbControl : MonoBehaviour
{
    private AudioReverbZone reverbZone;
    private AudioReverbPreset defaultPreset;

    public AudioReverbPreset reverbPreset;

    private void Awake()
    {
        reverbZone = GetComponentInParent<AudioReverbZone>();
        defaultPreset = AudioReverbPreset.StoneCorridor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            reverbZone.reverbPreset = reverbPreset;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            reverbZone.reverbPreset = defaultPreset;
        }
    }
}
*/
