using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using System.Reflection.Emit;
using static Unova_Legends_ABM_Gizmo.PluginConfig;
using static Unova_Legends_ABM_Gizmo.Restrictions;

namespace Unova_Legends_ABM_Gizmo
{

    [BepInPlugin(GUID, NAME, VERSION)]
    //Created by Jere - Thank you for all of your help!
    public class UnovaLegendsABMMod : BaseUnityPlugin
    {
        public const string GUID = "Unova_Legends_ABM_Mod";
        public const string NAME = "Unova Legends ABM Mod";
        public const string VERSION = "1.2";
        public static bool StructureTweaks = false;
        public static ManualLogSource Log;
        public static bool ConfigExists = false;
        private float speed = 0.1F;

        Harmony _harmony;
        public void Awake()
        {
            Log = Logger;
            new Harmony(GUID).PatchAll();
            BindConfig(Config);         
        }
         
        public void Update()
        {

            if (Input.GetKeyDown(MoveLeft.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                    Position.MoveLeft(speed);
            }
            else if (Input.GetKeyDown(MoveRight.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                    Position.MoveRight(speed);
            }
            else if (Input.GetKeyDown(MoveDown.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                    Position.MoveDown(speed);
            }
            else if (Input.GetKeyDown(MoveUp.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                    Position.MoveUp(speed);
            }
            else if (Input.GetKeyDown(MoveBackward.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                    Position.MoveBackward(speed);
            }
            else if (Input.GetKeyDown(MoveForward.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                    Position.MoveForward(speed);
            }
            else if (Input.GetKeyDown(IncreaseScrollSpeed.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                {
                    if (speed == 1.9F)
                        speed = 2.1F;
                    else if (speed == 1.7F)
                        speed = 1.9F;
                    else if (speed == 1.5F)
                        speed = 1.7F;
                    else if (speed == 1.3F)
                        speed = 1.5F;
                    else if (speed == 1.1F)
                        speed = 1.3F;
                    else if (speed == 0.9F)
                        speed = 1.1F;
                    else if (speed == 0.7F)
                        speed = 0.9F;
                    else if (speed == 0.5F)
                        speed = 0.7F;
                    else if (speed == 0.3F)
                        speed = 0.5F;
                    else if (speed == 0.1F)
                        speed = 0.3F;
                    else if (speed == 0.05F)
                        speed = 0.1F;
                    else if (speed == 0.01F)
                        speed = 0.05F;

                    Player.m_localPlayer?.Message(MessageHud.MessageType.TopLeft, speed.ToString());
                }
            }
            else if (Input.GetKeyDown(DecreaseScrollSpeed.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                {
                    if (speed == 0.05F)
                        speed = 0.01F; 
                    else if (speed == 0.1F)
                        speed = 0.05F;
                    else if (speed == 0.3F)
                        speed = 0.1F;
                    else if (speed == 0.5F)
                        speed = 0.3F;
                    else if (speed == 0.7F)
                        speed = 0.5F;
                    else if (speed == 0.9F)
                        speed = 0.7F;
                    else if (speed == 1.1F)
                        speed = 0.9F;
                    else if (speed == 1.3F)
                        speed = 1.1F;
                    else if (speed == 1.5F)
                        speed = 1.3F;
                    else if (speed == 1.7F)
                        speed = 1.5F;
                    else if (speed == 1.9F)
                        speed = 1.7F;
                    else if (speed == 2.1F)
                        speed = 1.9F;

                    Player.m_localPlayer?.Message(MessageHud.MessageType.TopLeft, speed.ToString());
                }
            }
            else if (Input.GetKeyDown(AdvancedBuildingMode.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                {
                    Player.m_localPlayer?.Message(MessageHud.MessageType.Center, "Exiting Advanced Building Mode.");
                    UnlockPlacement.TogglePlacement(false);
                    Position.Unfreeze();
                }
                else
                {
                    Player.m_localPlayer?.Message(MessageHud.MessageType.Center, "Entering Advanced Building Mode");
                    UnlockPlacement.TogglePlacement(true);
                    Position.Freeze();
                }
            }
            else if (Input.GetKeyDown(ToggleFreeze.Value.MainKey))
            {
                if (UnlockPlacement.allow)
                    Position.ToggleFreeze();
            }
            //else if (Input.GetKeyDown(Reset.Value.MainKey))
            //{
            //    if (UnlockPlacement.allow)
            //        Position.Reset();
            //}
        }

        public void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }

        
    }
}



