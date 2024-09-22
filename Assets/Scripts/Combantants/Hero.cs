using System;
using System.Collections;
using UnityEngine;

public abstract class Hero : Combantant
{
   [SerializeField] private bool _isUnlocked;
   public HeroSelection HeroSelection { get; set; }
   public int Level { get; private set; } = 1;
   public int Experience { get; private set; }

   public bool IsUnlocked
   {
      get => _isUnlocked;
      set => _isUnlocked = value;
   }

   private void OnEnable()
   {
      ApplicationQuitOrPause.Add(SaveHeroData);
   }
   
   public virtual void Start()
   {
      LoadHeroData();
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
      var basicHealthController = GetComponent<IHealthController>(); //TODO:  bugged, the save system saves  this but if the  player leaves after getting to the hero  selection panel, it will give an missing reference exception
      ES3.Save($"hero_{CombantantConfig.Name} Health", basicHealthController.Health);
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

   private void SaveHeroData()
   {
      ES3.Save($"hero_{CombantantConfig.Name} Level", Level);
      ES3.Save($"hero_{CombantantConfig.Name} attackPower", AttackPower);
      ES3.Save($"hero_{CombantantConfig.Name} Is unlocked", _isUnlocked);
   }

   public void LoadHeroData()
   {
      Name = CombantantConfig.Name;
      Level = ES3.Load($"hero_{CombantantConfig.Name} Level", CombantantConfig.BaseLevel);
      AttackPower  =  ES3.Load($"hero_{CombantantConfig.Name} attackPower", CombantantConfig.BaseAttackPower);
      var basicHealthController = GetComponent<IHealthController>();
      basicHealthController.Health = ES3.Load($"hero_{CombantantConfig.Name} Health", CombantantConfig.MaxHealth);
      _isUnlocked = ES3.Load($"hero_{CombantantConfig.Name} Is unlocked", false);
   }
}

[System.Serializable]
public class HeroSelection
{
   [System.NonSerialized]  public bool IsSelected;  
}
