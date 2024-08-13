using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private GameLogic gameLogic;
    [SerializeField] private GameObject playerObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            gameLogic.LoseGame();
            AudioManager.instance.PlaySFX(AudioManager.instance.loseSound);
        }
    }
}