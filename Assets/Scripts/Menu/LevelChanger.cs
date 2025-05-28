using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Update()
    {
        
    }

    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }
}
