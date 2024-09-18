using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : Combantant
{
   [SerializeField] private Combantant_SO _combantantConfig;
   public Combantant_SO CombantantConfig => _combantantConfig;
   public int Level { get; protected set; } = 1;
   public int Experience { get; protected set; }

   private void Start()
   {
      Level = _combantantConfig.BaseLevel;
      AttackPower = _combantantConfig.BaseAttackPower;
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
      Health *= 1.1f;
      AttackPower = _combantantConfig.BaseAttackPower * (1 + (Level * 0.1f));  //10% bonus per level
      Debug.Log($"{Name } Leveled Up");
   }
}
