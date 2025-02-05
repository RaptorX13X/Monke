using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    private GameObject player;
    private Camera cam;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 lookPos = cam.transform.position - transform.position;
        lookPos.y = 0f;
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
