using Architecture;
using System;
using UnityEngine;

public class ArchTester : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        Game.Run();
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _player = playerInteractor.player;
    }

    private void Update()
    {
        if (!Bank.IsInitialized)
            return;

        if (_player == null)
            return;

        Debug.Log($"Player position: {_player.transform.position}");

        if (Input.GetKeyDown(KeyCode.W))
        {
            Bank.AddCoins(this, 5);
            Debug.Log($"Coins added (5), {Bank.Coins}");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Bank.Spend(this, 10);
            Debug.Log($"Coins spent (10), {Bank.Coins}");
        }
    }
}