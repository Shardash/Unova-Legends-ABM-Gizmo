using BepInEx.Configuration;
using UnityEngine;

namespace Unova_Legends_ABM_Gizmo
{
    public static class PluginConfig
    {
        public static ConfigEntry<int> SnapDivisions { get; private set; }

        public static ConfigEntry<KeyboardShortcut> MoveLeft;
        public static ConfigEntry<KeyboardShortcut> MoveRight;
        public static ConfigEntry<KeyboardShortcut> MoveDown;
        public static ConfigEntry<KeyboardShortcut> MoveUp;
        public static ConfigEntry<KeyboardShortcut> MoveBackward;
        public static ConfigEntry<KeyboardShortcut> MoveForward;
        public static ConfigEntry<KeyboardShortcut> AdvancedBuildingMode;
        public static ConfigEntry<KeyboardShortcut> CopyObjectRotation;
        public static ConfigEntry<KeyboardShortcut> PasteObjectRotation;
        public static ConfigEntry<KeyboardShortcut> IncreaseScrollSpeed;
        public static ConfigEntry<KeyboardShortcut> DecreaseScrollSpeed;
        public static ConfigEntry<KeyboardShortcut> ToggleFreeze;
        //public static ConfigEntry<KeyboardShortcut> Reset;
        public static void BindConfig(ConfigFile config)
        {            
            MoveLeft =
              config.Bind(
                  "Keys",
                  "moveLeftKey",
                  new KeyboardShortcut(KeyCode.LeftArrow),
                  "Press this key to move left by 0.1.");

            MoveRight =
              config.Bind(
                  "Keys",
                  "moveRightKey",
                  new KeyboardShortcut(KeyCode.RightArrow),
                  "Press this key to move right by 0.1.");

            MoveDown =
              config.Bind(
                  "Keys",
                  "moveDownKey",
                  new KeyboardShortcut(KeyCode.PageDown),
                  "Press this key to move down by 0.1.");

            MoveUp =
                config.Bind(
                    "Keys",
                    "moveUpKey",
                    new KeyboardShortcut(KeyCode.PageUp),
                    "Press this key to move up by 0.1.");

            MoveBackward =
               config.Bind(
                   "Keys",
                   "moveBackwardKey",
                   new KeyboardShortcut(KeyCode.DownArrow),
                   "Press this key to move backward by 0.1.");

            MoveForward =
              config.Bind(
                  "Keys",
                  "moveForwardKey",
                  new KeyboardShortcut(KeyCode.UpArrow),
                  "Press this key to move forward by 0.1.");

            AdvancedBuildingMode =
                config.Bind(
                    "Keys",
                    "enterAdvancedBuildingMode",
                    new KeyboardShortcut(KeyCode.F6),
                    "Toggle the advanced building mode.");

            IncreaseScrollSpeed =
                config.Bind(
                    "Keys",
                    "increaseScrollSpeed",
                    new KeyboardShortcut(KeyCode.Insert),
                    "Increases the amount an object moves.");

            DecreaseScrollSpeed =
                config.Bind(
                    "Keys",
                    "decreaseScrollSpeed",
                    new KeyboardShortcut(KeyCode.Delete),
                    "Decreases the amount an object moves.");

            ToggleFreeze =
               config.Bind(
                   "Keys",
                   "toggleFreeze",
                   new KeyboardShortcut(KeyCode.Semicolon),
                   "Toggle the freezing of the piece."); 
            
        
        }
    }
}
