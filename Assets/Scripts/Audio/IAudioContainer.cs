using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioContainer
{
    int GetNumSections();
    bool HasSection(int SectionIndex);
    bool IsSectionLooping(int SectionIndex);
    AudioClip GetSection(int SectionIndex);
}

public abstract class AudioContainerBase : ScriptableObject, IAudioContainer
{
    public abstract int GetNumSections();
    public abstract bool IsSectionLooping(int SectionIndex);
    public abstract AudioClip GetSection(int SectionIndex);

    public bool HasSection(int SectionIndex)
    {
        return SectionIndex >= GetNumSections();
    }
}