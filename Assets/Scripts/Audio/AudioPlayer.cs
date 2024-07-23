using System.Collections;
using UnityEngine;

public class AudioPlayHandle
{
    // Hook to dynamically modify interval after specification
    public float Interval { get; set; } = 0.0f;

    // Hook to dynamically change volume scale
    public float VolumeScale { get; set; } = 1.0f;

    // Hook to allow user to end loop
    public bool Loop { get; set; } = true;

    // Hook to allow the user to stop the audio playback 
    public bool Stop { get; set; } = false;

    // Specifies the handle is invalid
    public bool IsValid { get; set; } = true;
}

public class AudioPlayer : MonoBehaviour
{
    public float GlobalVolumeScale = 1.0f;

    private AudioSource AudioSource;

    // Small wrapper to facilitate convenient interface while leveraging single implementation
    class AudioContainer_Wrapper : IAudioContainer
    {
        private AudioClip Clip;

        public AudioContainer_Wrapper(AudioClip Clip_) { Clip = Clip_; }

        public int GetNumSections() { return 1; }
        public bool IsSectionLooping(int SectionIndex) { return false; }
        public AudioClip GetSection(int SectionIndex)
        {
            if (SectionIndex > 1)
                return null;

            return Clip;
        }
    }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        // Need an audio source to play sounds
        if (!AudioSource)
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public AudioPlayHandle PlaySoundOneShot(AudioClip Clip, float VolumeScale = 1.0f, bool Looping = false)
    {
        return PlaySoundOneShot(new AudioContainer_Wrapper(Clip), VolumeScale);
    }

    public AudioPlayHandle PlaySoundOneShot(IAudioContainer Container, float VolumeScale = 1.0f, bool Looping = false)
    {
        AudioPlayHandle Handle = new AudioPlayHandle() { VolumeScale = GlobalVolumeScale * VolumeScale, Loop = Looping };
        StartCoroutine(PlaySoundOneShot_Internal(Container, Handle));
        return Handle;
    }

    public AudioPlayHandle PlaySoundAtInterval(AudioClip Clip, float Interval, float VolumeScale = 1.0f)
    {
        return PlaySoundAtInterval(new AudioContainer_Wrapper(Clip), Interval, VolumeScale);
    }

    public AudioPlayHandle PlaySoundAtInterval(IAudioContainer Container, float Interval, float VolumeScale = 1.0f)
    {
        if (Container.GetNumSections() > 1)
        {
            Debug.Log("PlaySoundAtInterval doesn't support composite AudioContainers.");
            return new AudioPlayHandle() { IsValid = false };
        }

        AudioPlayHandle Handle = new AudioPlayHandle() { Interval = Interval, VolumeScale = GlobalVolumeScale * VolumeScale };
        StartCoroutine(PlaySoundAtInterval_Internal(Container, Handle));
        return Handle;
    }

    private IEnumerator PlaySoundOneShot_Internal(IAudioContainer Container, AudioPlayHandle Handle)
    {
        int SectionIndex = 0;

        while (!Handle.Stop)
        {
            AudioClip Clip = Container.GetSection(SectionIndex);
            AudioSource.PlayOneShot(Clip, Handle.VolumeScale);

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

        Handle.IsValid = false;
        yield break;
    }

    private IEnumerator PlaySoundAtInterval_Internal(IAudioContainer Container, AudioPlayHandle Handle)
    {
        while (!Handle.Stop)
        {
            AudioClip Clip = Container.GetSection(0); // Always play section 0 (container may return different sounds)
            AudioSource.PlayOneShot(Clip, Handle.VolumeScale);

            yield return new WaitForSeconds(Handle.Interval);
        }

        Handle.IsValid = false;
        yield break;
    }
}