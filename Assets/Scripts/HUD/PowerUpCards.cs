using UnityEngine;

[CreateAssetMenu( fileName = "powerUpCards",menuName = "PowerUp Card/New Card")]
public class PowerUpCards : ScriptableObject
{
    public int ID;
    public Sprite spriteRender;
    public int bibleTotal;
    public float hadoukenTime;
    public float darkPowerArea;
    public float timeShield;
    public float ShieldInterval;
}
