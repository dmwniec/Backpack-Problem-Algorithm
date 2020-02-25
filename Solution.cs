using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemPlecakowy
{
    class Solution
    {
        public struct Result
        {
            public int BackpackValue { get; set; }
            public ArrayList ItemList;
        }
        public static int Greedy(Matrix matrix, int maxWeight)
        {
            int itemsCount = matrix.Value.GetLength(0);
            double[] avgArray = new double[itemsCount];
            for (int i = 0; i < itemsCount; i++)
            {
                avgArray[i] = matrix.Value[i, 0] / matrix.Value[i, 1];
            }
            double maxValue = 0;
            int maxIndex = 0;
            Matrix sortedMatrix = new Matrix(itemsCount);
            for (int i = 0; i < itemsCount; i++)
            {
                maxValue = avgArray.Max();
                maxIndex = avgArray.ToList().IndexOf(maxValue);
                sortedMatrix.Value[i, 0] = matrix.Value[maxIndex, 0];
                sortedMatrix.Value[i, 1] = matrix.Value[maxIndex, 1];
                avgArray[maxIndex] = -1;
            }
            
            int eq = 0;
            int amount = 0;
            
            for (int i=0; i<itemsCount;i++)
            {
                amount = maxWeight / sortedMatrix.Value[i,1];
                maxWeight -= amount* sortedMatrix.Value[i, 1];
                eq += amount * sortedMatrix.Value[i, 0];
            }


            return eq;


        }
        public static Result Dynamic(Matrix matrix, int maxWeight)
        {
            
            
            int itemsCount = matrix.Value.GetLength(0);
            int[,] P = new int[itemsCount+1, maxWeight+1];
            int[,] Q = new int[itemsCount+1, maxWeight+1];
            
            for (int i = 0; i<itemsCount; i++)
            {
                P[i, 0] = 0;
                Q[i, 0] = 0;               
            }
            for (int j = 0; j < maxWeight; j++)
            {
                P[0, j] = 0;
                Q[0, j] = 0;
            }
            for (int i = 1; i <= itemsCount; i++)
            {               
                for (int j = 1; j <= maxWeight; j++)
                {
                    if ((j >= matrix.Value[i -1 , 1]) && (P[i - 1, j] < P[i, j - matrix.Value[i -1 , 1]] + matrix.Value[i -1 , 0]))
                    {                                               
                            P[i, j] = P[i, j - matrix.Value[i -1 , 1]] + matrix.Value[i-1 , 0];
                            Q[i, j] = i;
                    }
                    else
                    {
                            P[i, j] = P[i - 1, j];
                        
                            Q[i, j] = Q[i - 1, j];
                    }
                    
                }
            }
            Matrix newmatrix = new Matrix(Q);
            Matrix.PrintMatrix(newmatrix);
            Result result = new Result();
            result.BackpackValue = P[itemsCount, maxWeight];
          
            int maximum = maxWeight;
            int[] arrayofitems = new int[maxWeight];
            int[] path = Enumerable.Range(0, Q.GetLength(1)).Select(x => Q[Q.GetLength(0)-1, x]).ToArray();
            int itemNumber = path.GetLength(0) -1;
            int itemWeight = 0;
            int lastindex = path.GetLength(0) - 1;
            for (int i = 0; i<maxWeight; i++)
            {
                itemNumber = path[lastindex];
                if (itemNumber !=0 ) itemWeight = matrix.Value[itemNumber -1 , 1];
                lastindex = Math.Abs(lastindex - itemWeight);
                arrayofitems[i] = itemNumber;
                
            }
            ArrayList list = new ArrayList();
            
            for (int i = 0; i < arrayofitems.GetLength(0)-1; i++)
            {
                if (arrayofitems[i]!=0) {
                    itemWeight = matrix.Value[arrayofitems[i] - 1, 1];
                    if ((maximum - itemWeight) < 0) break;
                    maximum -= itemWeight;
                    list.Add(arrayofitems[i]);
                }
            }
            result.ItemList = list;
            
            

            return result ;
        }
        


    }
}
