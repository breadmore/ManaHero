using UnityEngine;

public class HealthTest : MonoBehaviour
{
    public void AddHealth()
    {
        PlayerHealth.instance.AddHealth();
    }

    public void Heal(float health)
    {
        PlayerHealth.instance.RestoreHealth(health);
    }

    public void Hurt(float dmg)
    {
        PlayerHealth.instance.OnDamage(dmg);
    }
}
