using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int maxLevel = 4;

    public int Level
    {
        get => level;
        set => level = value;
    }

    public void LevelUp()
    {
        if(level <= maxLevel)
        {
            level++;
            ManageWeaponPlayer.Instance.upgradeWeapon = true;
        }
    }

    public void LevelUp(int amount)
    {
        if (level + amount <= maxLevel)
        {
            level += amount;
            ManageWeaponPlayer.Instance.upgradeWeapon = true;
        }
    }

    public void LevelDown()
    {
        if(level > 1)
        {
            level--;
            ManageWeaponPlayer.Instance.upgradeWeapon = true;
        }
    }

    public void LevelDown(int amount)
    {
        if (level - amount >= 1)
        {
            level -= amount;
            ManageWeaponPlayer.Instance.upgradeWeapon = true;
        }
    }


    public void ResetLevel()
    {
        level = 1;
    }

}
