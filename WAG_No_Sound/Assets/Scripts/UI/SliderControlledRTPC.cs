////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using UnityEngine.Audio;
public class SliderControlledRTPC : MonoBehaviour
{
    public AudioMixer master;
    public string channel;
   // public AK.Wwise.RTPC RTPC;

    public void SetRTPC(float value)
    {
        if (Menu.isOpen)
        {
            master.SetFloat(channel, value);
            //RTPC.SetGlobalValue(value);
        }
    }

}
