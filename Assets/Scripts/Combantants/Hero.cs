using UnityEngine;

public abstract class Hero : Combantant
{
   public HeroSelection HeroSelection { get; set; }
   public int Level { get; private set; } = 1;
   public int Experience { get; private set; }
   
   public virtual void Start()
   {
      Initialize();
   }
   
   public void Initialize()
   {
      Level = CombantantConfig.BaseLevel;
      AttackPower = CombantantConfig.BaseAttackPower;
   }
   public void GainXp(int xp)
   {
      Experience += xp;
      if (Experience >= 5)
      {
         LevelUp();
         Experience = 0;
      }
   }

   private void LevelUp()
   {
      Level++;
    //  Health *= 1.1f;
      AttackPower = CombantantConfig.BaseAttackPower * (1 + (Level * 0.1f));  //10% bonus per level
      Debug.Log($"{Name } Leveled Up");
   }
}

[System.Serializable]
public class HeroSelection
{
   [System.NonSerialized]  public bool IsSelected;  
}
