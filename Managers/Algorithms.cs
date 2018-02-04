using System;
using System.Collections.Generic;

namespace BenLearns.Managers
{
    public class Algorithms
    {

#region "Internal Classes"
        internal double ComputeMedian(List<int> nums, int Count)
        {
            double Median;
            if (Count % 2 == 0)
            {
                //mean the middle
                //1 2 3 4 5 6 7 8 9 10

                var middle = Count / 2;
                double sumMiddle = nums[middle - 1] + nums[middle];

                Median = sumMiddle / 2.0;


            }
            else
            {
                int middle = Count / 2;

                Median = nums[middle - 1];
            }


            return Median;
        }


        internal double ComputeMean(List<int> nums, int Count)
        {
            double Sum = new double();
            foreach(int num in nums)
            {
                Sum += num;
            }

            return Sum / Count;
        }

        internal int ComputeMode(List<int> nums, int Count)
        {
            var Counts = BuildCounts(nums);
            int Max = int.MinValue;
            int Mode = int.MinValue;

            foreach(int key in Counts.Keys)
            {
                if(Counts[key] > Max)
                {
                    Max = Counts[key];
                    Mode = key;
                }
            }

            return Mode;
        }

        internal double ComputeStandardDeviation(List<int> nums, int count)
        {
            var Mean = ComputeMean(nums, count);

            double SumMeans = new double();

            foreach(int num in nums)
            {
                SumMeans += Math.Pow((num - Mean), 2);
            }

            return Math.Pow((SumMeans / count), .5);

        }


        #endregion




        #region "Private Methods"


        //Builds a dictionary with number as key, and its count in the array as the value.
        private Dictionary<int, int> BuildCounts(List<int> items)
        {
            Dictionary<int, int> counts = new Dictionary<int, int>();

            foreach(int item in items)
            {
                
                if (counts.ContainsKey(item))
                {
                    counts[item] = counts[item] + 1;
                }
                else
                {
                    counts[item] = 1;
                }

            }

            return counts;
        }

#endregion

        public Algorithms()
        {
        }

    }
}
