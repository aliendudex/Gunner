using UnityEngine;

public class RecursionExercises : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Factorial: " + Factorial(4));
    }

    public int Factorial(int n)
    {
        // Caso base
        if (n == 0 || n == 1)
        {
            return 1;
        }
        else
        {
            return n * Factorial(n - 1);
        }
    }
    //public int ExlusiveSum(int n)
    //{

    //}
}
