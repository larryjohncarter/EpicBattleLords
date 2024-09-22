using System.Collections;
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
      Name = CombantantConfig.Name;
      Level = CombantantConfig.BaseLevel;
      AttackPower = CombantantConfig.BaseAttackPower;
   }
   public void GainXp(int xp, bool isAlive)
   {
      if (!isAlive) return;
      Experience += xp;
      XpGainText(xp);
      if (Experience >= 5)
      {
         LevelUp();
         Experience = 0;
      }
   }

   private void XpGainText(int amount)
   {
      var camera = Locator.Instance.MainCamera;
      var spawnPos = camera.WorldToScreenPoint(transform.position);
      spawnPos.y += transform.position.y + 3;
      var floatingTextObj = ObjectPool.Instance.SpawnFromPool(2, spawnPos, Quaternion.identity);
      var floatingText = floatingTextObj.GetComponent<FloatingTextController>();
      floatingText.SetXpGainedText(amount);
   }

   private void LevelUp()
   {
      var basicHealthController = GetComponent<IHealthController>();
      Level++;
      basicHealthController.MaxHealth *= 1.1f;
      AttackPower = CombantantConfig.BaseAttackPower * (1 + (Level * 0.1f));  //10% bonus per level
   }
}

[System.Serializable]
public class HeroSelection
{
   [System.NonSerialized]  public bool IsSelected;  
}
