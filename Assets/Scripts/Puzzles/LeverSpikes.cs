using System.Collections;
using UnityEngine;

public class LeverSpikes : MonoBehaviour
{
    public bool isSet;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject spikes;
    private bool onlyOnce;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent += Interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent -= Interact;
        }
    }

    public void Interact()
    {
        Debug.Log("interacted");
        animator.SetTrigger("interact");
        isSet = !isSet;
        if (!onlyOnce)
        {
            StartCoroutine(Spikes());
            onlyOnce = true;
        }
    }

    private IEnumerator Spikes()
    {
        spikes.transform.Translate(new Vector3(0, -5, 0), Space.Self);
        yield return new WaitForSeconds(3f);
        Destroy(spikes);
    }
}
