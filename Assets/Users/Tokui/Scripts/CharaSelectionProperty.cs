using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSelectionProperty : SingletonMonoBehaviour<CharaSelectionProperty>
{
    [SerializeField]
    private ScrollSnapSelector snapSelector;

    public static ScrollSnapSelector SnapSelector => Instance.snapSelector;
    
    [SerializeField]
    private ScrollSnapSelector snapSelector2;

    public static ScrollSnapSelector SnapSelector2 => Instance.snapSelector2;
}