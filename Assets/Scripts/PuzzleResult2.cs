using UnityEngine;

public class PuzzleResult2 : MonoBehaviour
{
    [SerializeField] private PressurePlateCheat plate;
    [SerializeField] private PressurePlateCheat plate2;
    private bool happened = false;
    [SerializeField] private Vector3 move;

    private void Update()
    {
        if ((plate.isSet || plate2.isSet) && !happened)
        {
            MoveDoor();
            happened = true;
        }
    }

    private void MoveDoor()
    {
        transform.position += move;
    }
}
