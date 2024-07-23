using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioContainer_Simple", order = 2)]
public class AudioContainer_Simple : ScriptableObject, IAudioContainer
{
    public AudioClip Clip;

    public int GetNumSections()
    {
        return 1;
    }

    public AudioClip GetSection(int SectionIndex)
    {
        if (SectionIndex > 1)
            return null;

        return Clip;
    }

    public bool IsSectionLooping(int SectionIndex)
    {
        return false;
    }
}

