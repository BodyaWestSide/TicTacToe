using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

    public string Player;
    public BoardManager BoardManager;
    public ClientManager ClientManager;
    
    private void Start()
    {
        ClientManager = GameObject.FindGameObjectWithTag("ClientManager").GetComponent<ClientManager>();
        BoardManager = ClientManager.BoardManager;

        if (isServer)
            Player = "X";
        else
            Player = "O";

        ClientManager.Setup(this);
    }
    public void OnRestartButtonPressed()
    {
        Cmd_OnRestartButtonPressed();
    }
    public void OnBoardButtonPressed(int index)
    {
        BoardManager.SetPlayerOnBoardCell(Player, index);
        Cmd_ButtonPressed(Player, index);
    }
    public void ClientReady()
    {
        Cmd_ClientReady();
    }
    [Command]
    public void Cmd_ButtonPressed(string CurrentPlayer, int index)
    {
        ClientManager.Rpc_ButtonPressed(CurrentPlayer, index);
    }
    [Command]
    public void Cmd_ClientReady()
    {
        ClientManager.Rpc_ClientsReady();
    }
    [Command]
    public void Cmd_OnRestartButtonPressed()
    {
        ClientManager.Rpc_OnRestartButtonPressed();
    }
}
