using System;
using Ludo.Data;

namespace Ludo.Events
{
    public static class LudoEvents
    {
        public static Action<Player> OnTurnChange;
        public static Action<int, Player> OnRoll;
        public static Action<int, Player> OnMove;
    }
}