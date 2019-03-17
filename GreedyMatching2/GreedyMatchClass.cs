using System;
using System.Collections.Generic;
using System.Text;

namespace GreedyMatching2
{
    public struct PositionInfo
    {
        public int FirstArrayIndex;
        public int FirstArrayMatchingPosition;
        public int SecArrayIndex;
        public int SecArrayMatchingPosition;
        public int MaxMatchingCount;

        public PositionInfo(int firstArrayIndex, int firstArrayMatchingPosition, int secArrayIndex, int secArrayMatchingPosition)
        {
            this.FirstArrayIndex = firstArrayIndex;
            this.FirstArrayMatchingPosition = firstArrayMatchingPosition;
            this.SecArrayIndex = secArrayIndex;
            this.SecArrayMatchingPosition = secArrayMatchingPosition;
            this.MaxMatchingCount = 0;
        }

        public void PositionInfoReset()
        {
            this.FirstArrayIndex = 0;
            this.FirstArrayMatchingPosition = 0;
            this.SecArrayIndex = 0;
            this.SecArrayMatchingPosition = 0;
            this.MaxMatchingCount = 0;
        }
    }

    public class GreedyMatchClass
    {
        public string GreedyMatching(List<string> inputStringList)
        {
            PositionInfo tempPositionInfo1 = new PositionInfo(0, 0, 1, 0);
            PositionInfo tempPositionInfo2 = new PositionInfo(0, 0, 1, 0);
            PositionInfo maxPositionInfo = new PositionInfo(0, 0, 1, 0);

            for (int i = 0; i < inputStringList.Count; i++)
            {
                for (int j = i + 1; j < inputStringList.Count; j++)
                {
                    if (inputStringList[i].ToUpper().Contains(inputStringList[j].ToUpper()))
                    {
                        inputStringList.Remove(inputStringList[j]);
                        j--;
                    }
                    else if (inputStringList[j].ToUpper().Contains(inputStringList[i].ToUpper()))
                    {
                        inputStringList.Remove(inputStringList[i]);
                        i--;
                    }
                }
            }
            List<char[]> charArrayList = new List<char[]>();
            foreach (string inputString in inputStringList)
            {
                charArrayList.Add(inputString.ToUpper().ToCharArray());
            }

            while (charArrayList.Count > 1)
            {
                maxPositionInfo.PositionInfoReset();
                for (int i = 0; i < charArrayList.Count; i++)
                {
                    for (int j = i + 1; j < charArrayList.Count; j++)
                    {
                        tempPositionInfo1.PositionInfoReset();
                        tempPositionInfo2.PositionInfoReset();
                        StringBuilder sb1 = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();

                        StringBuilder sb3 = new StringBuilder();
                        StringBuilder sb4 = new StringBuilder();
                        //compare from first letter of the first array to the last letter of the second array
                        //then the first two letters of the first array to the last two letters of the second array, etc.
                        for (int k = 0; k < charArrayList[i].Length && k < charArrayList[j].Length; k++)
                        {
                            sb1.Append(charArrayList[i][k]);
                            sb2.Insert(0, charArrayList[j][charArrayList[j].Length - k - 1]);

                            //has two sets, compare the other way around at the same time
                            sb3.Append(charArrayList[j][k]);
                            sb4.Insert(0, charArrayList[i][charArrayList[i].Length - k - 1]);

                            if (sb1.Equals(sb2))
                            {
                                tempPositionInfo1.FirstArrayIndex = i;
                                tempPositionInfo1.SecArrayIndex = j;
                                tempPositionInfo1.FirstArrayMatchingPosition = 0;
                                tempPositionInfo1.SecArrayMatchingPosition = charArrayList[j].Length - k - 1;
                                tempPositionInfo1.MaxMatchingCount = k+1;

                                if (tempPositionInfo1.MaxMatchingCount > maxPositionInfo.MaxMatchingCount)
                                {
                                    maxPositionInfo = tempPositionInfo1;
                                }
                            }
                            else
                            {
                                tempPositionInfo1.PositionInfoReset();
                            }

                            if (sb3.Equals(sb4))
                            {
                                tempPositionInfo2.FirstArrayIndex = j;
                                tempPositionInfo2.SecArrayIndex = i;
                                tempPositionInfo2.FirstArrayMatchingPosition = 0;
                                tempPositionInfo2.SecArrayMatchingPosition = charArrayList[i].Length - k - 1;
                                tempPositionInfo2.MaxMatchingCount = k+1;

                                if (tempPositionInfo2.MaxMatchingCount > maxPositionInfo.MaxMatchingCount)
                                {
                                    maxPositionInfo = tempPositionInfo2;
                                }
                            }
                            else
                            {
                                tempPositionInfo2.PositionInfoReset();
                            }
                        }
                    }

                }

                //after found the max match, merge
                List<char> newChar = new List<char>();

                //if no matching is found, simply put all the strings together
                if (maxPositionInfo.MaxMatchingCount == 0)
                {
                    StringBuilder sb5 = new StringBuilder();
                    foreach (char[] element in charArrayList)
                    {
                        foreach(char ele in element)
                        {
                            sb5.Append(ele);
                        }                       
                    }
                    return sb5.ToString();
                }

                if (maxPositionInfo.FirstArrayMatchingPosition >= maxPositionInfo.SecArrayMatchingPosition)
                {
                    //first step: add everything from the first array before the matching point
                    for (int i = 0; i < maxPositionInfo.FirstArrayMatchingPosition; i++)
                    {
                        newChar.Add(charArrayList[maxPositionInfo.FirstArrayIndex][i]);
                    }
                    //second step: add everything from the second array
                    foreach (char element in charArrayList[maxPositionInfo.SecArrayIndex])
                    {
                        newChar.Add(element);
                    }
                }
                else
                {
                    for (int i = 0; i < maxPositionInfo.SecArrayMatchingPosition; i++)
                    {
                        newChar.Add(charArrayList[maxPositionInfo.SecArrayIndex][i]);
                    }
                    //second step: add everything from the second array
                    foreach (char element in charArrayList[maxPositionInfo.FirstArrayIndex])
                    {
                        newChar.Add(element);
                    }
                }
                char[] newCharArray = newChar.ToArray();

                //remove the previously merged arrays
                if(maxPositionInfo.FirstArrayIndex>maxPositionInfo.SecArrayIndex)
                {
                    charArrayList.Remove(charArrayList[maxPositionInfo.FirstArrayIndex]);
                    charArrayList.Remove(charArrayList[maxPositionInfo.SecArrayIndex]);
                }
                else
                {
                    charArrayList.Remove(charArrayList[maxPositionInfo.SecArrayIndex]);
                    charArrayList.Remove(charArrayList[maxPositionInfo.FirstArrayIndex]);
                }
                
                //add the new array
                charArrayList.Add(newCharArray);                
            }
            return new String(charArrayList[0]);
        }
    }
}
