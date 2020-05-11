////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using System.Collections;

public enum WalkType
{
    DIRT,
    GRASS,
    RUBBLE,
    SAND,
    STONE,
    WATER,
    WOOD
};

public class SoundMaterial : MonoBehaviour
{
    //public AK.Wwise.Switch material
    public WalkType walkType;
}
