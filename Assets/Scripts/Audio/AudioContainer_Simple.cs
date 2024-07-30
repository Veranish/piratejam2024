using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioContainer_Simple", order = 2)]
public class AudioContainer_Simple : AudioContainerBase
{
    public AudioClip Clip;

    public override int GetNumSections()
    {
        return 1;
    }

    public override AudioClip GetSection(int SectionIndex)
    {
        if (SectionIndex > 1)
            return null;

        return Clip;
    }

    public override bool IsSectionLooping(int SectionIndex)
    {
        return false;
    }
}

