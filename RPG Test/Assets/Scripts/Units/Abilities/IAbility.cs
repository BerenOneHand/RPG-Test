using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    float GetCooldown();

    void OnActivate();

    void OnLevelUp();

    string GetTooltip();

    void GetImage(); // Not sure of format
    
}