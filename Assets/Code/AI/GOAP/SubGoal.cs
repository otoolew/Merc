using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGoal
{
    // Dictionary to store our goals
    public Dictionary<string, int> sGoals;
    // Bool to store if goal should be removed after it has been achieved
    public bool remove;

    // Constructor
    public SubGoal(string s, int i, bool r) {

        sGoals = new Dictionary<string, int> {{s, i}};
        remove = r;
    }
}
