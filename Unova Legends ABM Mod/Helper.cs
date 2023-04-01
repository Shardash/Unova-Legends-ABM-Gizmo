using System;
using System.Globalization;
using UnityEngine;

namespace Unova_Legends_ABM_Gizmo
{
    public static class Helper
    {
        public static GameObject GetPlacementGhost()
        {
            var player = GetPlayer();
            if (!player.m_placementGhost) throw new InvalidOperationException("Not currently placing anything.");
            return player.m_placementGhost;
        }

        public static float ParseFloat(string value, float defaultValue = 0)
        {
            if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result)) return result;
            return defaultValue;
        }
        public static int ParseInt(string value, int defaultValue = 0)
        {
            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) return result;
            return defaultValue;
        }


        public static void AddError(Terminal context, string message, bool priority = true)
        {
            AddMessage(context, $"Error: {message}", priority);
        }
        public static void AddMessage(Terminal context, string message, bool priority = true)
        {           
            if (context == Console.instance )
                    context.AddString(message);
            var hud = MessageHud.instance;
            if (!hud) return;
            if (priority)
            {
                var items = hud.m_msgQeue.ToArray();
                hud.m_msgQeue.Clear();
                Player.m_localPlayer?.Message(MessageHud.MessageType.TopLeft, message);
                foreach (var item in items)
                    hud.m_msgQeue.Enqueue(item);
                hud.m_msgQueueTimer = 10f;
            }
            else
            {
                Player.m_localPlayer?.Message(MessageHud.MessageType.TopLeft, message);
            }
        }

        public static Player GetPlayer()
        {
            var player = Player.m_localPlayer;
            if (!player) throw new InvalidOperationException("No player.");
            return player;
        }
        
    }
}