using UnityEngine;
using UnityEngine.TextCore.Text;

public static class UtilsFunc
{
    public static void TakeDamage(GameObject target, float damage)
    {
        Stats stats = target.CompareTag("Player") ? target.GetComponent<PlayerMovement>().stats : target.GetComponent<Enemies>().stats;

        if (stats != null)
        {
            stats.HpRemove(damage);
        }

        if (target.tag != "Player")
        {
            target.GetComponent<Enemies>().ChangeToWhiteTemporarily(0.1f);
        }
        else
        {
            target.GetComponent<PlayerMovement>().ChangeToWhiteTemporarily(0.1f);
        }

    }
}