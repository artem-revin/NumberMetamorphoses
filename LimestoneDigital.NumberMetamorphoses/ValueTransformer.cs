using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LimestoneDigital.NumberMetamorphoses.Contracts;

namespace LimestoneDigital.NumberMetamorphoses
{
    public class ValueTransformer : IValueTransformer
    {
        public string Transform(string value)
        {
            
            #region Checking initial string

            Regex r = new Regex(@"[\d]");

            if (!r.IsMatch(value))
            {
                throw new ArgumentException();
            }
            
            if (value is null)
            {
                throw new ArgumentNullException();
            }
            
            if (value.Length > 7)
            {
                throw new ArgumentOutOfRangeException();
            }

            #endregion
            
            #region Prepearing for transformation

            char[] charsOfValue = value.ToCharArray();

            List<int> listOfNumbers = new List<int>();
            
            for (int i = 0; i < charsOfValue.Length; i++)
            {
                listOfNumbers.Add(Convert.ToInt32(charsOfValue[i].ToString()));
            }

            listOfNumbers.Sort();
            
            List<int> completeList = listOfNumbers.Distinct().ToList();

            #endregion
            
            #region Algorithm of transformation

            StringBuilder result = new StringBuilder();
            StringBuilder range = new StringBuilder();
            range.Append(completeList[0]);
            
            int checking = Convert.ToInt32(range.ToString()) + 1;
            
            string completeRange;
            
            for (int i = 1; i < completeList.Count; i++)
            {
                if (checking != completeList[i])
                {
                    completeRange = range.ToString();
                    if (completeRange.Length > 1)
                    {
                        result.Append($"{completeRange[0]}-{completeRange[completeRange.Length - 1]}, ");
                        range.Clear();
                        checking = completeList[i];
                    }
                    else
                    {
                        result.Append($"{completeRange}, ");
                        checking = completeList[i];
                        range.Clear();
                    }
                    range.Append(completeList[i]);
                }
                else
                {
                    range.Append(completeList[i]);
                }

                checking++;
            }
            
            completeRange = range.ToString();
            if (completeRange.Length > 1)
            {
                result.Append($"{completeRange[0]}-{completeRange[completeRange.Length - 1]}");
                range.Clear();
                range.Append(checking);
            }
            else 
            {
                result.Append($"{completeRange}"); 
                range.Clear();
            }

            return result.ToString();

            #endregion
        }
    }
}
