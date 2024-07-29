using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

// Little wrapper that manages playback of composite SFX
public class AudioPlayHandle
{

    public AudioPlayHandle(AudioPlayer Player, IAudioContainer Clip, AudioSource Source, float VolumeScale, bool Loop)
    {
        m_Player = Player;
        m_Container = Clip;
        m_Source = Source;
        m_SectionIndex = 0;

        SetVolume(VolumeScale);
        m_ShouldLoop = Loop;
    }

    ~AudioPlayHandle()
    {
        GameObject.Destroy(m_Source);
    }

    public float Interval { get; set; } = 0.0f;

    public bool IsPlaying { get { return m_IsPlaying; } }
    public bool IsLooping { get { return m_Source ? m_Source.loop : false; } }

    public IEnumerator Begin()
    {
        m_IsPlaying = true;

        while (m_IsPlaying)
        {
            // End the play handle
            if (m_Container.HasSection(m_SectionIndex))
            {
                Stop();
                break;
            }

            AudioClip Clip = m_Container.GetSection(m_SectionIndex);
            if (Clip == null)
            {
                Stop();

                Debug.Log("Encountered null clip in container");
                break;
            }

            m_Source.clip = Clip;
            m_Source.Play();

            if (m_ShouldLoop && m_Container.IsSectionLooping(m_SectionIndex))
            {
                m_Source.loop = true;
                break;
            }
            else
            {
                m_Source.loop = false;
                yield return new WaitForSeconds(Clip.length);
            }

            ++m_SectionIndex;
        }

        yield break;
    }

    public IEnumerator BeginInterval()
    {
        m_IsPlaying = true;

        while (m_IsPlaying)
        {
            AudioClip Clip = m_Container.GetSection(0); // Always play section 0 (container may return different clips)
            if (Clip == null)
            {
                Stop();

                Debug.Log("Encountered null clip in container");
                yield break;
            }

            m_Source.clip = Clip;
            m_Source.Play();

            yield return new WaitForSeconds(Interval);
        }

        yield break;
    }

    public void Stop()
    {
        m_IsPlaying = false;
        GameObject.Destroy(m_Source);
    }

    public void StopImmediate()
    {
        m_IsPlaying = false;
        m_Source.Stop();

        GameObject.Destroy(m_Source);
    }

    public void EndLoop()
    {
        m_Source.loop = false;
        ++m_SectionIndex;

        m_Player.KickAsync(this);
    }

    public void SetVolume(float VolumeScale)
    {
        m_Source.volume = VolumeScale * m_Player.GlobalVolumeScale;
    }

    private IAudioContainer m_Container;
    private AudioSource     m_Source;
    private AudioPlayer     m_Player;
    private bool            m_ShouldLoop;
    private int             m_SectionIndex;
    private bool            m_IsPlaying;
}

public class AudioPlayer : MonoBehaviour
{
    public float GlobalVolumeScale = 1.0f;

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

        public bool HasSection(int SectionIndex)
        {
            return SectionIndex == 0;
        }
    }


    public AudioPlayHandle PlaySound(AudioClip Clip, float VolumeScale = 1.0f, bool Looping = false, float IntervalSec = 0.0f)
    {
        return PlaySound(new AudioContainer_Wrapper(Clip), VolumeScale, Looping);
    }

    public AudioPlayHandle PlaySound(IAudioContainer Container, float VolumeScale = 1.0f, bool Looping = false)
    {
        if (Container == null)
        {
            Debug.Log("Null audio audio container clip supplied by user");
        }

        var Source = gameObject.AddComponent<AudioSource>();

        var PlayHandle = new AudioPlayHandle(this, Container, Source, VolumeScale, Looping);
        StartCoroutine(PlayHandle.Begin());
        return PlayHandle;
    }

    public AudioPlayHandle PlaySoundAtInterval(AudioClip Clip, float Interval, float VolumeScale = 1.0f)
    {
        return PlaySoundAtInterval(new AudioContainer_Wrapper(Clip), Interval, VolumeScale);
    }

    public AudioPlayHandle PlaySoundAtInterval(IAudioContainer Container, float Interval, float VolumeScale = 1.0f)
    {
        if (Container == null)
        {
            Debug.Log("Null audio audio container clip supplied by user");
        }

        var Source = gameObject.AddComponent<AudioSource>();

        var PlayHandle = new AudioPlayHandle(this, Container, Source, VolumeScale, false);
        PlayHandle.Interval = Interval;

        StartCoroutine(PlayHandle.BeginInterval());
        return PlayHandle;
    }

    public void KickAsync(AudioPlayHandle Handle)
    {
        StartCoroutine(Handle.Begin());
    }
}