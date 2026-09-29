using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int currencyAmount;
    public SerializableDick<string, bool> powersUnlocked;
    public GameData()
    {
        this.currencyAmount = 0;
        powersUnlocked = new SerializableDick<string, bool>();
    }
}
