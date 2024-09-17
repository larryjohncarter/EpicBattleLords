using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthController
{
    void Die();
    void TakeDamage(int damage);
    bool IsAlive();
}
