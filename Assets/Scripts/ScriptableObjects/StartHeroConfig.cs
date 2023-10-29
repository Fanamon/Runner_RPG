using UnityEngine;

[CreateAssetMenu(fileName = "New start hero config", menuName = "StartHeroConfig/Create new start hero config", 
    order = 51)]
public class StartHeroConfig : ScriptableObject
{
    public int StartHeroNumber { get; private set; }

    public void SetStartHeroNumber(int startHeroNumber)
    {
        StartHeroNumber = startHeroNumber;
    }
}
