using UnityEngine;

public class Enemy
{
    public string enemyName;

    public Enemy(string enemyName)
    {
        this.enemyName = enemyName;
    }

    public override string ToString()
    {
        return enemyName;
    }

}
