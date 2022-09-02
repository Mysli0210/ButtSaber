﻿using LovenseBSControl.Configuration;
using System;
using System.Linq;
using UnityEngine;

namespace LovenseBSControl
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class LovenseBSControlController : MonoBehaviour
    {
        public static LovenseBSControlController Instance { get; private set; }

        ScoreController scoreController;
        private NoteController noteController;
        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            // For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
            //   and destroy any that are created while one already exists.
            if (Instance != null)
            {
                Plugin.Log?.Warn($"Instance of {GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            Instance = this;
            Plugin.Log?.Debug($"{name}: Awake()");
        }
        /// <summary>
        /// Only ever called once on the first frame the script is Enabled. Start is called after any other script's Awake() and before Update().
        /// </summary>
        private void Start()
        {

        }

        /// <summary>
        /// Called every frame if the script is enabled.
        /// </summary>
        private void Update()
        {

        }

        /// <summary>
        /// Called every frame after every other enabled script's Update().
        /// </summary>
        private void LateUpdate()
        {

        }

        /// <summary>
        /// Called when the script becomes enabled and active
        /// </summary>
        private void OnEnable()
        {

        }

        /// <summary>
        /// Called when the script becomes disabled or when it is being destroyed.
        /// </summary>
        private void OnDisable()
        {

        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            if (Instance == this)
                Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

        }
        #endregion

        public void GetControllers()
        {
            scoreController = Resources.FindObjectsOfTypeAll<ScoreController>().LastOrDefault();

            if (scoreController != null)
            {
                // noteController.noteWasCutEvent += NoteHit;
                //scoreController.noteWasMissedEvent += NoteMiss; // replaced into harmony patches
            }
        }

        public void HandleWasCutBySaber() { 
        }
       
            private void NoteHit(NoteData data, in NoteCutInfo info, int multiplier)
        {
            if (PluginConfig.Instance.Enabled)
            {
               
                Plugin.Control.HandleCut(info.saberType == SaberType.SaberA, info.allIsOK, info);
            }
        }

        private void NoteMiss(NoteData data, int score)
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.HandleCut(data.colorType.ToString().Equals("ColorA"), false);
            }
        }
    }
}
