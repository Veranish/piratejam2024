using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioContainer
{
    int GetNumSections();
    bool IsSectionLooping(int SectionIndex);
    AudioClip GetSection(int SectionIndex);
}

