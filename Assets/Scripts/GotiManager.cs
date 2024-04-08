using System.Collections.Generic;
using UnityEngine;

namespace ludo
{
    public class GotiManager : MonoBehaviour
    {
        [SerializeField] private GameObject gotiPrefab;
        [SerializeField] private List<PlayerData> players;    

        private LudoManager _ludoManager;

        private void Awake()
        {
            _ludoManager = GetComponent<LudoManager>();
        }    

        public void CreateGoties()
        {
            foreach(var player in players)
            {
                for(var count = 4; count > 0; --count)
                {
                    var goti = Instantiate(gotiPrefab).GetComponent<Goti>();
                    goti.SetData(player, _ludoManager);
                }
            }
        }
    }
}