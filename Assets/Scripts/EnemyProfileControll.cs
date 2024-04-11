using UnityEngine;

[System.Serializable]
public class EnemyProfile
{
    public int attackValue;
    public int defensiveValue;
    public int idleValue;
    public int lowAttackProbability;
    public int midAttackProbability;
    public int highAttackProbability;
    public int lowDefenseProbability;
    public int midDefenseProbability;
    public int highDefenseProbability;
}

public class EnemyProfileControll : MonoBehaviour
{
    public EnemyProfile eProfile = new EnemyProfile();
}
