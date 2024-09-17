using UnityEngine;

[CreateAssetMenu(fileName = "Combantant", menuName = "Can/Combantant", order = 0)]
public class Combantant_SO : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public bool IsUnlocked;
    public bool IsSelected;
}