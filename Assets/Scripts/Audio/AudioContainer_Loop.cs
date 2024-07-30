using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioContainer_Loop", order = 2)]
public class AudioContainer_Loop : AudioContainerBase
{
    public AudioClip Start;
    public AudioClip Loop;
    public AudioClip End;

    public override int GetNumSections()
    {
        AudioClip[] List = { Start, Loop, End };
        return List.Count(x => x != null);
    }

    public override AudioClip GetSection(int SectionIndex)
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

    public override bool IsSectionLooping(int SectionIndex)
    {
        return Loop != null && GetSection(SectionIndex) == Loop;
    }
}

