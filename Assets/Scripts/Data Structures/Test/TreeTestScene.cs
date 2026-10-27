
using UnityEngine;
using ED262C;

public class TreeTestScene : MonoBehaviour
{
    void Start()
    {
        int[] values = { 10, 20, 30, 40, 50, 25 };

        BST<int> bst = new BST<int>();
        AVL<int> avl = new AVL<int>();

        // Insert the same values into both trees.
        foreach (int value in values)
        {
            bst.Insert(value);
            avl.Insert(value);
        }

        // Test the normal BST.
        Debug.Log("===== NORMAL BST =====");
        Debug.Log("Count: " + bst.Count);
        Debug.Log("Height: " + bst.Height);
        Debug.Log("Root balance factor: " + bst.BalanceFactor);
        Debug.Log("Contains 25: " + bst.Contains(25));
        Debug.Log("In-order traversal:");
        bst.InOrder();

        // Test the AVL tree.
        Debug.Log("===== AVL TREE =====");
        Debug.Log("Count: " + avl.Count);
        Debug.Log("Height: " + avl.Height);
        Debug.Log("Root balance factor: " + avl.BalanceFactor);
        Debug.Log("Contains 25: " + avl.Contains(25));
        Debug.Log("In-order traversal:");
        avl.InOrder();

        // Test removal from both trees.
        bst.Remove(30);
        avl.Remove(30);

        Debug.Log("===== AFTER REMOVING 30 =====");
        Debug.Log("BST still contains 30: " + bst.Contains(30));
        Debug.Log("AVL still contains 30: " + avl.Contains(30));
        Debug.Log("BST count: " + bst.Count);
        Debug.Log("AVL count: " + avl.Count);
        Debug.Log("AVL height after removal: " + avl.Height);
        Debug.Log("AVL in-order traversal after removal:");
        avl.InOrder();
    }
}
