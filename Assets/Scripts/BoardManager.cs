using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardCells
{
    public PlayerManager Player;
    public int ButtonIndex;

    public void OnButtonPressed()
    {
        Player.OnBoardButtonPressed(ButtonIndex);
    }
}
public class BoardManager : MonoBehaviour
{
    public List<Button> BoardCellButtons;
    public List<BoardCells> BoardCellList = new List<BoardCells>();
    public PlayerManager Player;
    public Button RestartButton;
    public Text ServerWins;
    public Text ClientWins;

    public int serverWinsCount = 0;
    public int clientWinsCount = 0;

    public void Settings()
    {
        RestartButton.onClick.RemoveAllListeners();
        RestartButton.onClick.AddListener(Player.OnRestartButtonPressed);
        for (int i = 0; i < BoardCellButtons.Count; i++)
        {
            BoardCellList.Add(new BoardCells() { ButtonIndex = i, Player = Player });
            BoardCellButtons[i].onClick.RemoveAllListeners();
            BoardCellButtons[i].onClick.AddListener(BoardCellList[BoardCellList.Count - 1].OnButtonPressed);
            BoardCellButtons[i].GetComponent<Image>().color = Color.white;
            BoardCellButtons[i].GetComponentInChildren<Text>().text = "";
            BoardCellButtons[i].interactable = true;
        }
    }
    public void SetBoard(bool isInteractable)
    {
        if (isInteractable)
        {
            for (int i = 0; i < BoardCellButtons.Count; i++)
            {
                if (GetBoardCellText(BoardCellButtons[i]) == "")
                {
                    BoardCellButtons[i].interactable = isInteractable;
                }
            }
        }
        else
            BoardCellButtons.ForEach(x => x.interactable = isInteractable);
    }
    public void SetPlayerOnBoardCell(string Player, int BoardCellIndex)
    {
        BoardCellButtons[BoardCellIndex].GetComponentInChildren<Text>().text = Player;
    }
    public void SetWinner(string Player)
    {
        if(Player == "X")
        {
            serverWinsCount += 1;
            ServerWins.text = serverWinsCount.ToString();
        }
        else
        {
            clientWinsCount += 1;
            ClientWins.text = clientWinsCount.ToString();
        }
    }

    public bool Winner(string Player)
    {
        for (int i = 0; i < 7; i += 3)
        {
            if (GetBoardCellText(BoardCellButtons[0 + i]) == Player && GetBoardCellText(BoardCellButtons[1 + i]) == Player && GetBoardCellText(BoardCellButtons[2 + i]) == Player)
            {
                return true;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (GetBoardCellText(BoardCellButtons[0 + i]) == Player && GetBoardCellText(BoardCellButtons[3 + i]) == Player && GetBoardCellText(BoardCellButtons[6 + i]) == Player)
            {
                return true;
            }
        }
        if (GetBoardCellText(BoardCellButtons[0]) == Player && GetBoardCellText(BoardCellButtons[4]) == Player && GetBoardCellText(BoardCellButtons[8]) == Player)
        {
            return true;
        }
        if (GetBoardCellText(BoardCellButtons[2]) == Player && GetBoardCellText(BoardCellButtons[4]) == Player && GetBoardCellText(BoardCellButtons[6]) == Player)
        {
            return true;
        }
        return false;
    }
    string GetBoardCellText(Button BoardCell)
    {
        return BoardCell.GetComponentInChildren<Text>().text;
    }

}
