using HarmonyLib;

namespace Unova_Legends_ABM_Gizmo
{
    public class Restrictions
    {

        ///<summary>Disables the resource check.</summary>
        [HarmonyPatch(typeof(Player), nameof(Player.HaveRequirements), new[] { typeof(Piece), typeof(Player.RequirementMode) })]
        public class HaveRequirements
        {
            public static bool Prefix(ref bool __result)
            {
                return true;
            }
        }
        ///<summary>Removes resource usage.</summary>
        [HarmonyPatch(typeof(Player), nameof(Player.ConsumeResources))]
        public class ConsumeResources
        {
            static bool Prefix() => true;

        }

        [HarmonyPatch(typeof(Player), nameof(Player.PlacePiece))]
        public class PostProcessToolOnPlace
        {
            public static void Prefix(Player __instance, ref float __state)
            {
              
            }
            static void Postfix(Player __instance, ref bool __result)
            {
                
            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.UpdatePlacementGhost))]
        public class UnlockPlacement
        {
            public static bool allow;
            public static void TogglePlacement(bool toggle)
            {
                allow = toggle;
            }

            public static void Prefix(Player __instance)
            {
                if (!__instance.m_placementGhost) return;
                //var piece = __instance.m_placementGhost.GetComponent<Piece>();
                //piece.m_allowedInDungeons = true;
            }
            public static void Postfix(Player __instance)
            {
                if (!__instance.m_placementGhost) return;
                if (!__instance.m_placementGhost.activeSelf) return;
                // These three are handled by other settings in other places.
                var status = __instance.m_placementStatus;
                if (status == Player.PlacementStatus.NoBuildZone) return;
                if (status == Player.PlacementStatus.NotInDungeon) return;
                if (status == Player.PlacementStatus.PrivateZone) return;
                if (!allow) return;

                if (allow)
                {    
                    __instance.m_placementStatus = Player.PlacementStatus.Valid;
                    __instance.SetPlacementGhostValid(true);
                }
            }
        }
        [HarmonyPatch(typeof(Location), nameof(Location.IsInsideNoBuildLocation))]
        public class IsInsideNoBuildLocation
        {
            public static bool Prefix(ref bool __result)
            {
                return true;
            }
        }
        [HarmonyPatch(typeof(PrivateArea), nameof(PrivateArea.CheckAccess))]
        public class CheckAccess
        {
            public static bool Prefix(ref bool __result)
            {
                return true;
            }
        }
        [HarmonyPatch(typeof(Player), nameof(Player.CheckCanRemovePiece))]
        public class CheckCanRemovePiece
        {
            public static bool Prefix(ref bool __result)
            {
                return true;
            }
        }
    }
}
