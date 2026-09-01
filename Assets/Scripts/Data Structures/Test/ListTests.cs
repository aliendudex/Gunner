using UnityEngine;
namespace ED262C
{
    public class ListTests : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            TestListOperations(new SimpleArrayList<int>());
            TestListOperations(new SimpleLinkedList<int>());
        }

        void TestListOperations(ISimpleList<int> testList)
        {
            // Agregamos varios elementos
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            // Removemos en el medio
            testList.RemoveAt(1);
            // Removemos un valor que existe y uno que no existe
            Debug.Log(testList.Remove(1));
            Debug.Log(testList.Remove(10));
            // Insertamos al principio y en el medio
            testList.Insert(0, 4);
            testList.Insert(1, 7);

            // AddRange
            testList.AddRange(new int[] { 2, 10, 20 });

            // Hasta aca vamos 4, 7, 3, 2, 10, 20

            // RemoveRange
            testList.RemoveRange(1, 3);

            // Hasta aca vamos 4, 10, 20
            Debug.Log(testList.Contains(10));
            Debug.Log(testList.Contains(30));
            // Testeamos indexer
            Debug.Log(testList[1]);
            testList[1] = 5;
            Debug.Log(testList[1]);

            testList.Clear();
        }
        void PrintList(ISimpleList<int> testList)
        {
            string line = "";
            int[] arrayCast = testList.ToArray();

            for(int i = 0; i < arrayCast.Length; i++)
            {
                line += arrayCast;
            }
        }
    }
}
