using System.Collections;
using UnityEngine;

namespace Architecture
{
    public class PlayerInteractor : Interactor
    {
        public Player player {  get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            var goPlayer = new GameObject("Player");
            player = goPlayer.AddComponent<Player>();
        }
    }
}