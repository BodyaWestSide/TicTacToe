using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientManager : NetworkBehaviour {

    public BoardManager BoardManager;
    public NetworkManager Manager;
    public PlayerManager CurrentPlayer;
    public PlayerManager OpponentPlayer;
    int TurnCount = 0;

    public void Setup(PlayerManager Player)
    {
        TurnCount = 0;
        if (Player.isLocalPlayer)
        {
            CurrentPlayer = Player;
            BoardManager.Player = Player;
            if (!isServer)
                Player.ClientReady();
        }
        else
        {
            OpponentPlayer = Player;
        }
    }
    public void ChangePlayer(bool PlayerOne)
    {
        if (PlayerOne)
            BoardManager.SetBoard(true);
        else
            BoardManager.SetBoard(false);
    }
    public void SetupBoard()
    {
        TurnCount = 0;
        BoardManager.Settings();
        ChangePlayer(isServer);
    }
    [ClientRpc]
    public void Rpc_ButtonPressed(string CurrentPlayer, int index)
    {
        BoardManager.SetPlayerOnBoardCell(CurrentPlayer, index);
        TurnCount++;
        if (TurnCount < 10)
        {
            if (BoardManager.Winner(CurrentPlayer))
            {
                BoardManager.SetWinner(CurrentPlayer);
                BoardManager.SetBoard(false);
            }
            else
            {
                if (CurrentPlayer == "X")
                    ChangePlayer(!isServer);
                else
                    ChangePlayer(isServer);
            }
        }
    }
    [ClientRpc]
    public void Rpc_ClientsReady()
    {
        SetupBoard();
    }
    [ClientRpc]
    public void Rpc_OnRestartButtonPressed()
    {
        SetupBoard();
    }
}
