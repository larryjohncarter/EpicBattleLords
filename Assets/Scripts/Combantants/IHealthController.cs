using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthController
{
    void Die();
    void TakeDamage(int damage);
    float Health { get; set; }
    float MaxHealth { get; set; }
    bool IsAlive();
}
