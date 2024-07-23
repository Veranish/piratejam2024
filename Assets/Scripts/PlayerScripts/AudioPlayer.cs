using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayHandle
{
    public float Interval { get; set; } = 0.0f;
    public bool Loop { get; set; } = true;
    public bool Stop { get; set; } = false;
    public bool IsValid { get; set; } = true;
}

public class AudioPlayer : MonoBehaviour
{
    private AudioSource AudioSource;

    public float VolumeScale = 1.0f;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        if (!AudioSource)
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public PlayHandle PlaySoundOneShot(IAudioContainer Container)
    {
        PlayHandle Handle = new PlayHandle();
        StartCoroutine(PlaySoundOneShot_Internal(Container, Handle));
        return Handle;
    }

    public PlayHandle PlaySoundAtInterval(IAudioContainer Container, float Interval)
    {
        if (Container.GetNumSections() > 1)
        {
            return new PlayHandle() { IsValid = false };
        }


        PlayHandle Handle = new PlayHandle() { Interval = Interval };
        StartCoroutine(PlaySoundAtInterval_Internal(Container, Handle));
        return Handle;
    }

    private IEnumerator PlaySoundOneShot_Internal(IAudioContainer Container, PlayHandle Handle)
    {
        int SectionIndex = 0;

        while (!Handle.Stop)
        {
            AudioClip Clip = Container.GetSection(SectionIndex);
            AudioSource.PlayOneShot(Clip);

            yield return new WaitForSeconds(Clip.length);
            
            if (!Container.IsSectionLooping(SectionIndex) || !Handle.Loop)
            {
                ++SectionIndex;
            }

            if (SectionIndex > Container.GetNumSections())
            {
                Handle.Stop = true;
            }
        }

        yield break;
    }

    private IEnumerator PlaySoundAtInterval_Internal(IAudioContainer Container, PlayHandle Handle)
    {
        while (!Handle.Stop)
        {
            AudioClip Clip = Container.GetSection(0);
            AudioSource.PlayOneShot(Clip);

            yield return new WaitForSeconds(Handle.Interval);
        }

        yield break;
    }
}