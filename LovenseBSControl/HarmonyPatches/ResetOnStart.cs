﻿using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{
    [HarmonyPatch(typeof(ScoreController), "Start")]
    class Start
    {
        static void Prefix()
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.ResetCounter();
            }
        }
    }
}
