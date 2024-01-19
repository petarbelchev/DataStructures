using _03.MinHeap;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int minSweetness, int[] cookies)
        {
            MinHeap<int> minHeap = new MinHeap<int>();

            foreach (var cookie in cookies)
                minHeap.Add(cookie);

            int counter = 0;

            while (minHeap.Peek() < minSweetness)
            {
                if (minHeap.Count == 1)
                    return -1;

                int least = minHeap.ExtractMin();
                int secondLeast = minHeap.ExtractMin();
                int newCookie = least + 2 * secondLeast;
                minHeap.Add(newCookie);
                counter++;
            }

            return counter;
        }
    }
}
