using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject defeat;

    public bool canMove = true;

    public void WinGame()
    {
        canMove = false;
        victory.SetActive(true);
    }

    public void LoseGame()
    {
        canMove = false;
        defeat.SetActive(true);
    }
}