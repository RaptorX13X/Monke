using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private int lookX;
    [SerializeField] private int lookZ;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 lookPos = player.transform.position - transform.position;
        lookPos.y = 0f;
        //lookPos.z = lookZ;
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
