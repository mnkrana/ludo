using System;
using Ludo.Data;

namespace Ludo.Events
{
    public static class LudoEvents
    {
        public static Action<Player> OnTurnChange;
    }
}