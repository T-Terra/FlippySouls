using UnityEngine;
using UnityEngine.TextCore.Text;

public static class UtilsFunc
{
    public static void TakeDamage(GameObject target, float damage)
    {
        Stats stats = null;

        if (target.CompareTag("Player"))
        {
            stats = target.GetComponent<PlayerMovement>().stats;
        }
        else
        {
            stats = target.GetComponent<Enemies>().stats;
        }

        Debug.Log($"Target: {target.name}, Damage: {damage}, hp: {stats.hp}");

        if (stats != null)
        {
            stats.hp -= damage;
            Debug.Log($"New HP: {stats.hp}");
        }
        else
        {
            Debug.LogWarning("Stats component not found on target.");
        }
    }

}
