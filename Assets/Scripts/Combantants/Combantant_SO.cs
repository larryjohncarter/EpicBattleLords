using UnityEngine;

[CreateAssetMenu(fileName = "Combantant", menuName = "Can/Combantant", order = 0)]
public class Combantant_SO : ScriptableObject
{
    public string Name;
    [Header("Stats")]
    public float MaxHealth;
    public float BaseAttackPower;
    public int BaseLevel;
    [Header("Selections")]
    public bool IsUnlocked;
    public bool IsSelected;
}