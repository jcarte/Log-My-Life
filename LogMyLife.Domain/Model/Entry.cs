using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using d= LogMyLife.Domain.Data;

namespace LogMyLife.Domain.Model
{
    public class Entry
    {
        //TODO restrict set


        /// <summary>
        /// Unique Identifier for the Entry
        /// </summary>
        public int EntryID { get; internal set; }

        /// <summary>
        /// Unique Identifier for the category
        /// </summary>
        public int CategoryID { get; internal set; }


        //TODO complete
        //public string DisplayValue1 { get; set; }
        //public string DisplayValue2 { get; set; }
        //public string DisplayValue3 { get; set; }

        //public Bitmap Image { get; set; }

        public int StarRating { get; internal set; }//TODO open up, special setter

        //TODO how to handle updating?
        public Dictionary<string,string> AllData { get; internal set; }
        public Dictionary<string, string> KeyData { get; internal set; }

        public bool IsArchived { get; set; }


        public void EditColumnData(string colName, string data)
        {
            if(AllData.ContainsKey(colName))
                AllData[colName] = data;
            else
            {
                //TODO throw error as doesn't exist or return bool
            }

            if (KeyData.ContainsKey(colName))
                KeyData[colName] = data;
        }

        //public Entry(d.Category cat, d.Record rec, List<d.Column> cols, List<d.Cell> cells)
        //{
        //    var dic = new Dictionary<d.Column, string>();

        //    foreach (d.Column col in cols.OrderBy(c => c.Order))
        //    {
        //        //TODO test if cell doesn't exist for this column
        //        string data = cells.FirstOrDefault(c => c.ColumnID == col.ColumnID)?.Data ?? string.Empty;
        //        dic.Add(col, data);
        //    }

        //    AllData = dic.ToDictionary(d => d.Key.Name, d => d.Value);
        //    KeyData = dic.Where(k => k.Key.IsKey).ToDictionary(d => d.Key.Name, d => d.Value);
        //    //TODO fill out the rest of these if it works


        //    if (KeyData.Count >= 1)
        //        DisplayValue1 = KeyData.ToList()[0].Value;
        //    if (KeyData.Count >= 2)
        //        DisplayValue2 = KeyData.ToList()[1].Value;
        //    if (KeyData.Count >= 3)
        //        DisplayValue3 = KeyData.ToList()[2].Value;

        //    //Try to get the star rating column, then try to convert the string into an int, assign the int if between 0 and 10
        //    int srInt;
        //    string srStr;
        //    if (!AllData.TryGetValue("Star Rating", out srStr) || !Int32.TryParse(srStr, out srInt) || srInt < 0 || srInt > 10)//TODO Test
        //        StarRating = 0;
        //    else
        //        StarRating = srInt;
        //}





        internal Entry()
        {

        }


        public override string ToString()
        {
            string st = $"ID = {EntryID}, CategoryID = {CategoryID}, IsArchived = {IsArchived}\nValues:";
            AllData.ToList().ForEach(a => st += $"\n{a.Key} : {a.Value}");
            return st;
        }

    }
}
