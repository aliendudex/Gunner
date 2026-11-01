using UnityEngine;

public class Enemy
{
    public string enemyName;
    public string Id;

    public Enemy(string enemyName)
    {
        this.enemyName = enemyName;
    }

    public override string ToString()
    {
        return enemyName;
    }

}
