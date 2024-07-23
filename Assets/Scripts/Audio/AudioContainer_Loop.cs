using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioContainer_Loop", order = 2)]
public class AudioContainer_Loop : ScriptableObject, IAudioContainer
{
    public AudioClip Start;
    public AudioClip Loop;
    public AudioClip End;

    public int GetNumSections()
    {
        return (Start ? 1 : 0) + (Loop ? 1 : 0) + (End ? 1 : 0);
    }

    public AudioClip GetSection(int SectionIndex)
    {
        if (SectionIndex > 2)
            return null;

        AudioClip[] List = { Start, Loop, End };

        int Counter = 0;
        foreach (var Clip in List)
        {
            if (Clip != null)
            {
                if (Counter == SectionIndex)
                {
                    return Clip;
                }

                ++Counter;
            }
        }

        return null;
    }

    public bool IsSectionLooping(int SectionIndex)
    {
        return Loop != null && GetSection(SectionIndex) == Loop;
    }
}

