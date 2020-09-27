using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace fianl_q1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region READING TEXT FILE and WRITE DUPLICATE INDEX
            StreamReader reader = new StreamReader(File.OpenRead(@"numbers.txt"));
            List<int> numbers = new List<int>();
            int i = 0; // to count the rows of the file
            Stopwatch sw1 = new Stopwatch();  // for benchmark test
            Stopwatch sw2 = new Stopwatch();  // for benchmark test
            long[] times_1 = new long[100];  // to store the timings for the 1st method
            long[] times_2 = new long[100];  // to store the timings for the 2nd method

            while (!reader.EndOfStream)
            {
                string lineString = reader.ReadLine();
                string[] numbersString = lineString.Split(' ');
                int len = numbersString.Length;
                int[] arr = new int[len];
                for (int j = 0; j < len; j++)
                {
                    arr[j] = Convert.ToInt32(numbersString[j]);
                }

                Console.WriteLine("\n------------- Finding Duplication through findDuplicate -------------\n");
                sw1.Start();  // stopwatch starts
                int a = findDuplicate(arr);
                sw1.Stop();   // stopwatch stops
                times_1[i] = sw1.ElapsedTicks;
                Console.WriteLine($"Index: {a}\tNumber: {arr[a]}\tTiming: {sw1.Elapsed}");

                Console.WriteLine("\n------------- Finding Duplication through findDuplicate_flags -------------\n");
                sw2.Start();
                a = findDuplicate_flags(arr);
                sw2.Stop();
                times_2[i] = sw2.ElapsedTicks;
                Console.WriteLine($"Index: {a}\tNumber: {arr[a]}\tTiming: {sw2.Elapsed}");

                i++;
            }
            #endregion

            #region CALCULATION OF THE AVG TIMINGS
            long avgTiming_1 = 0, avgTiming_2 = 0;
            for (int m = 0; m < 100; m++)
            {
                avgTiming_1 += times_1[m];
                avgTiming_2 += times_2[m];
            }
            avgTiming_1 = avgTiming_1 / 100;
            avgTiming_2 = avgTiming_2 / 100;
            #endregion

            #region TEXT FILE GENERATOR
            StreamWriter txtFile = new StreamWriter("Timing_Performances.txt");

            txtFile.Write("Average timing of the given method (# of clock cycles, ticks): {0}  \n", avgTiming_1);
            txtFile.Write("Average timing of the created method (# of clock cycles, ticks): {0} \n\n", avgTiming_2);
            txtFile.Write("The timing data for all iterations for the given method (# of clock cycles, ticks): \n");
            for (int k = 0; k < 100; k++){ txtFile.WriteLine(times_1[k]); }
            txtFile.Write("\nThe timing data for all iterations for the created method (# of clock cycles, ticks): \n");
            for (int k = 0; k < 100; k++) { txtFile.WriteLine(times_2[k]); }
            txtFile.Close();
            #endregion


            #region findDuplicate Method  
            int findDuplicate(int[] arr)  // Linear Search for every value has O(n^2) time complexity, which is costly.
                                          // it returns the first index of the duplicated number
            {
                int n;
                n = arr.Length;

                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (arr[i] == arr[j])
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }
            #endregion  

            #region findDuplicate_flags     returns the second index of the duplicated number
            int findDuplicate_flags(int[] arr)    // Finding the duplication via an array of numbers. Because the number domain (-1000,1000)
                                                  // it is costly in terms of memory yet it has better time performance
                                                  // it reutrns the second index of the duplicated number.
                                                  // Time Complexity = O(n) while the first method has O(n^2)
            {
                int domain = 2001;
                int[] arrFlag = new int[domain];  // because the values are scattered between -1000 and 1000, the domain is 2001
                                                  // and we dont need to assign zeros to the array as it is initialized with zeros by default.
                int n;
                n = arr.Length;

                for (int i = 0; i < n; i++)
                {
                    if (arrFlag[arr[i]+1000] == 0)  // normalization, because index cannot be negative
                    {
                        arrFlag[arr[i]+1000] = 1;   // normalization, because index cannot be negative
                    }
                    else
                    {
                        return i;
                    }
                }
                return -1;
            }
            #endregion
        }
    }
}
