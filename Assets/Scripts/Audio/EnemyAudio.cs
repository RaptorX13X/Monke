using UnityEngine;
using FMODUnity;

public class EnemyAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance EnemyAttackSound;
    FMOD.Studio.EventInstance EnemyDamageSound;

    [SerializeField] private EventReference enemyAttackEvent;
    [SerializeField] private EventReference enemyDamageEvent;
    public void PlayAttack()
    {
        EnemyAttackSound = FMODUnity.RuntimeManager.CreateInstance(enemyAttackEvent);
        EnemyAttackSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        EnemyAttackSound.start();
        EnemyAttackSound.release();
    }

    public void PlayDamage()
    {
        EnemyDamageSound = FMODUnity.RuntimeManager.CreateInstance(enemyDamageEvent);
        EnemyDamageSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        EnemyDamageSound.start();
        EnemyDamageSound.release();
    }
}
