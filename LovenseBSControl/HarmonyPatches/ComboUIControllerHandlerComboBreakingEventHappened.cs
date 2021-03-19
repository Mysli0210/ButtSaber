﻿using HarmonyLib;
using LovenseBSControl.Configuration;
using UnityEngine;

namespace LovenseBSControl
{
    [HarmonyPatch(typeof(BombNoteController), "HandleWasCutBySaber")]
    class HandleWasCutBySaber
    {
        static void Prefix(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
        {
            //Plugin.Log.Notice(saber.saberType.ToString());
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.handleBomb();
            }
        }
    }

}
