using UnityEngine;

public class PushingBigSexyBox : MonoBehaviour


{
    [SerializeField] private PlayerAudio playerAudio;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PuzzleBlock"))
        {
            playerAudio.PlayPushingObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PuzzleBlock"))
        {
            playerAudio.StopPushingObject();
        }
    }
}
