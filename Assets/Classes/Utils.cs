using UnityEngine;
using UnityEngine.TextCore.Text;

public static class UtilsFunc
{
    public static void TakeDamage(GameObject target, float damage)
    {
        Stats stats = target.GetComponent<Stats>();
        if (stats != null)
        {
            stats.hp -= damage;
        }
    }

}
