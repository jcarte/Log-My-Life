using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using d= LogMyLife.Domain.Data;

namespace LogMyLife.Domain.Model
{
    /// <summary>
    /// A single "Row" of data in a category, can we seen as a single object inside the category
    /// holds data inside columns to describe the object.
    /// </summary>
    public class Entry
    {
        /// <summary>
        /// Unique Identifier for the Entry
        /// </summary>
        public int EntryID { get; internal set; }

        /// <summary>
        /// Unique Identifier for the category
        /// </summary>
        public int CategoryID { get; internal set; }

        /// <summary>
        /// The first bit of data which describes the entry, used to summarise the entry
        /// </summary>
        public string DisplayValue1
        {
            get
            {
                if (TitleData.Count > 0)
                    return TitleData.Values.Skip(0).First();
                else
                    return string.Empty;
            }     
            set
            {
                if (TitleData.Count > 0)
                    EditColumnData(TitleData.Keys.Skip(0).First(),value);
            }
        }

        /// <summary>
        /// The second bit of data which describes the entry, used to summarise the entry
        /// </summary>
        public string DisplayValue2
        {
            get
            {
                if (TitleData.Count > 1)
                    return TitleData.Values.Skip(1).First();
                else
                    return string.Empty;
            }
            set
            {
                if (TitleData.Count > 1)
                    EditColumnData(TitleData.Keys.Skip(1).First(), value);
            }
        }

        /// <summary>
        /// The third bit of data which descrubes the entry, used to summarise the entry
        /// </summary>
        public string DisplayValue3
        {
            get
            {
                if (TitleData.Count > 2)
                    return TitleData.Values.Skip(2).First();
                else
                    return string.Empty;
            }
            set
            {
                if (TitleData.Count > 2)
                    EditColumnData(TitleData.Keys.Skip(2).First(), value);
            }
        }

        /// <summary>
        /// Rating that the user gave this entry between 0 and 10 inclusive
        /// </summary>
        public float StarRating
        {
            get
            {
                float star;
                string colData = Data[GetColumn("Star Rating")];
                if (colData == string.Empty)
                    return 0;//not yet set
                if (!float.TryParse(colData, out star))
                    throw new Exception($"Star Rating of {colData} could not be converted to a number");
                return star;
            }
            set
            {
                if (value < 0 || value > 5)
                    throw new Exception($"Star rating must be between 0 and 5 inclusive, {value} is not in that range");
                EditColumnData("Star Rating", value.ToString());
            }
        }

        /// <summary>
        /// The user's notes about this entry
        /// </summary>
        public string Comment 
        { 
            get 
            {
                return Data[GetColumn("Comment")];
            } 
            set
            {
                EditColumnData("Comment", value);
            } 
        }

        /// <summary>
        /// Every column of data for this entry, given in form: (Column Name, Data Value)
        /// </summary>
        public Dictionary<string, string> AllData { get { return Data.ToDictionary(d => d.Key.Name, d => d.Value); } }

        /// <summary>
        /// Only columns which are not hidden, given in form: (Column Name, Data Value)
        /// </summary>
        public Dictionary<string, string> VisibleData { get { return Data.Where(da=>da.Key.IsHidden == false).ToDictionary(d => d.Key.Name, d => d.Value); } }
        /// <summary>
        /// Only hidden columns, given in form: (Column Name, Data Value)
        /// </summary>
        public Dictionary<string, string> HiddenData { get { return Data.Where(da => da.Key.IsHidden == true).ToDictionary(d => d.Key.Name, d => d.Value); } }

        /// <summary>
        /// All visible columns which are marked as "Title" columns, given in form: (Column Name, Data Value)
        /// </summary>
        public Dictionary<string, string> TitleData { get { return Data.Where(da => da.Key.IsHidden == false && da.Key.Type == Column.ColumnType.Title).ToDictionary(d => d.Key.Name, d => d.Value); } }
        /// <summary>
        /// All visible columns which are marked as "Review" columns, given in form: (Column Name, Data Value)
        /// </summary>
        public Dictionary<string, string> ReviewData { get { return Data.Where(da => da.Key.IsHidden == false && da.Key.Type == Column.ColumnType.Review).ToDictionary(d => d.Key.Name, d => d.Value); } }
        /// <summary>
        /// All visible columns which are not marked as either "Title" or "Review" columns, given in form: (Column Name, Data Value)
        /// </summary>
        public Dictionary<string, string> OtherData { get { return Data.Where(da => da.Key.IsHidden == false && da.Key.Type == Column.ColumnType.Normal).ToDictionary(d => d.Key.Name, d => d.Value); } }

        /// <summary>
        /// Has this entry been archived?
        /// </summary>
        public bool IsArchived { get; set; }


        /// <summary>
        /// Edit the data held in columns, throws column not found exception if invalid column name supplied.
        /// </summary>
        /// <param name="colName">Name of the column to change the data of</param>
        /// <param name="data">The data to store in that column</param>
        public void EditColumnData(string colName, string data)
        {
            Data[GetColumn(colName)] = data;
        }


        //Column retriever convenience method, throws not found exception
        private Column GetColumn(string colName)
        {
            Column col = Data.Keys.FirstOrDefault(c => c.Name == colName);

            if (col == null)
                throw new Exception($"Column {colName} could not be found in the column list for this entry.");

            return col;
        }

        /// <summary>
        /// Main data storage for the entry, all getters and setters reference this
        /// </summary>
        internal Dictionary<Column, string> Data { get; set; }

        /// <summary>
        /// For interal creation
        /// </summary>
        internal Entry()
        {
        }


        public override string ToString()
        {
            string st = $"ID = {EntryID}, CategoryID = {CategoryID}, IsArchived = {IsArchived}\nValues:";
            //Data.ToList().ForEach(a => st += $"\n[{a.Key.Name}, {a.Value}]               {a.Key.ToString()}");
            Data.ToList().ForEach(a => st += $"[{a.Key.Name}, {a.Value}], ");
            return st;
        }

    }
}
