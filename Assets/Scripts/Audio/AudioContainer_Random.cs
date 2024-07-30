using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioContainer_Random", order = 1)]
public class AudioContainer_Random : AudioContainerBase
{
    public List<AudioClip> Variants = new();

    public override int GetNumSections()
    {
        return 1;
    }

    public override AudioClip GetSection(int SectionIndex)
    {
        if (Variants?.Count == 0)
            return null;

        int index = (int)(Random.value * Variants.Count);
        return Variants[index];
    }

    public override bool IsSectionLooping(int SectionIndex)
    {
        return false;
    }
}

