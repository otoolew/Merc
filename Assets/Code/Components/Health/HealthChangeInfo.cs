using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HealthChangeInfo
{
	public HealthComponent HealthComp;

	public float OldHealth;

	public float NewHealth;

	public float HealthDifference => NewHealth - OldHealth;

	public float AbsHealthDifference => Mathf.Abs(HealthDifference);
}
