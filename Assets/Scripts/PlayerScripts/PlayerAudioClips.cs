using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerAudioClips", order = 1)]
public class PlayerAudioClips : ScriptableObject
{
    public AudioContainerBase RunningClip;
    public AudioContainerBase StaffFire;
    
}
