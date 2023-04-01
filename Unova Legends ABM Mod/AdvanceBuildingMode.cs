using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using HarmonyLib;
using static Unova_Legends_ABM_Gizmo.Restrictions;

namespace Unova_Legends_ABM_Gizmo
{

    [HarmonyPatch(typeof(Player), nameof(Player.PieceRayTest))]
    public class FreezePlacementMarker
    {
        static Vector3 CurrentNormal = Vector3.up;
        static void Postfix(ref Vector3 point, ref Vector3 normal, ref Piece piece, ref Heightmap heightmap, ref Collider waterSurface, ref bool __result)
        {
            if (Position.Override.HasValue)
            {
                point = Position.Override.Value;
                normal = CurrentNormal;
                __result = true;
                piece = null;
                heightmap = null;
                waterSurface = null;
            }
            else
            {
                CurrentNormal = normal;
            }
        }
    }

    [HarmonyPatch(typeof(Player), nameof(Player.UpdatePlacementGhost))]
    public class OverridePlacementGhost
    {
        ///<summary>Then override snapping and other modifications for the final result (and some rules are checked too).</summary>
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                  .MatchForward(
                      useEnd: false,
                      new CodeMatch(
                          OpCodes.Call,
                          AccessTools.Method(typeof(Location), nameof(Location.IsInsideNoBuildLocation))))
                  .Advance(-2)
                  // If-branches require using ops from the IsInsideBuildLocation so just duplicate the used ops afterwards.
                  .Insert(new CodeInstruction(OpCodes.Call, Transpilers.EmitDelegate<Action<GameObject>>(
                         (GameObject ghost) => ghost.transform.position = Position.Apply(ghost.transform.position)).operand),
                        new CodeInstruction(OpCodes.Ldarg_0),
                        new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Player), nameof(Player.m_placementGhost)))
                  )
                  .InstructionEnumeration();
        }
    }
    public static class Position
    {

        public static Vector3? Override = null;
        public static Vector3 Offset = Vector3.zero;
        public static void ToggleFreeze()
        {
            if (Override.HasValue)
                Unfreeze();
            else
                Freeze();
        }
        public static void Freeze(Vector3 position)
        {
            Override = position;
        }
        public static void Freeze()
        {
            var player = Helper.GetPlayer();
            var ghost = player.m_placementGhost;
            Override = ghost ? Deapply(ghost.transform.position) : player.transform.position;
        }
        public static void Unfreeze()
        {
            Override = null;
            Offset = Vector3.zero;
        }

        public static Vector3 Apply(Vector3 point)
        {
            var ghost = Helper.GetPlayer().m_placementGhost;
            if (!ghost) return point;
            if (Override.HasValue)
                point = Override.Value;
            var rotation = ghost.transform.rotation;
            point += rotation * Vector3.right * Offset.x;
            point += rotation * Vector3.up * Offset.y;
            point += rotation * Vector3.forward * Offset.z;
            return point;
        }
        public static Vector3 Deapply(Vector3 point)
        {
            var ghost = Helper.GetPlayer().m_placementGhost;
            if (!ghost) return point;
            if (Override.HasValue)
                point = Override.Value;
            var rotation = ghost.transform.rotation;
            point -= rotation * Vector3.right * Offset.x;
            point -= rotation * Vector3.up * Offset.y;
            point -= rotation * Vector3.forward * Offset.z;
            return point;
        }

        public static void MoveLeft(float value)
        {
            Offset.x -= value;
        }
        public static void MoveRight(float value)
        {
            Offset.x += value;
        }
        public static void MoveDown(float value)
        {
            Offset.y -= value;
        }
        public static void MoveUp(float value)
        {
            Offset.y += value;
        }
        public static void MoveBackward(float value)
        {
            Offset.z -= value;
        }
        public static void MoveForward(float value)
        {
            Offset.z += value;
        }
        public static void Reset()
        {
            Offset = Vector3.zero;
        }
    }
}