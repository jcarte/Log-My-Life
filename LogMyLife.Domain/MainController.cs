using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using m = LogMyLife.Domain.Model;
using d = LogMyLife.Domain.Data;
using static LogMyLife.Domain.Model.Category;

namespace LogMyLife.Domain
{
    public static class MainController//TODO better name
    {

        static MainController()
        {
            //TODO check if db exists yet if it doesn't create and insert with default data
        }




        /// <summary>
        /// Creates a new category and stores it in the database
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <param name="type">The type of data held in the category</param>
        /// <returns>The new category with generated ID</returns>
        public static m.Category CreateCategory(string name, CategoryType type)
        {
            d.Category dCat = new d.Category();

            dCat.Name = name;
            dCat.Type = (int)type;

            Create(dCat);

            return ConvertCategoryToModel(dCat);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of all categories currently in database</returns>
        public static List<m.Category> GetCategories()
        {
            List<m.Category> lis = new List<Model.Category>();
            d.DatabaseController.GetCategories().ToList().ForEach(c => lis.Add(ConvertCategoryToModel(c)));
            return lis;
        }

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="cat">The category to be updated complete with updated values</param>
        public static void UpdateCategory(m.Category cat)
        {
            if (cat == null)
                throw new Exception($"Can't update Category because it is null");

            d.Category dCat = d.DatabaseController.GetCategory(cat.CategoryID);
            if (dCat == null)
                throw new Exception($"Can't update Category {cat.CategoryID} because it doesn't exist");

            dCat.Name = cat.Name;
            dCat.Type = (int)cat.Type;

            Update(dCat);
        }

        /// <summary>
        /// Delete a category and all records inside it
        /// </summary>
        /// <param name="cat">The category to be deleted</param>
        public static void DeleteCategory(m.Category cat)
        {
            if (cat == null)
                throw new Exception($"Can't delete Category because it is null");
            DeleteCategory(cat.CategoryID);
        }
        
        /// <summary>
        /// Delete a category and all records inside it
        /// </summary>
        /// <param name="catID">The CategoryID of the category to be deleted</param>
        public static void DeleteCategory(int catID)
        {
            var c = d.DatabaseController.GetCategory(catID);
            if (c == null)
                throw new Exception($"Can't delete Category {catID} because it doesn't exist");

            //Manual cascading delete
            DeleteRange(d.DatabaseController.GetRecords(catID));
            DeleteRange(d.DatabaseController.GetColumns(catID));
            DeleteRange(d.DatabaseController.GetCells(catID));
            Delete(c);
        }

        private static m.Category ConvertCategoryToModel(d.Category dCat)
        {
            m.Category mCat = new m.Category();
            mCat.CategoryID = dCat.CategoryID;
            mCat.Name = dCat.Name;

            try
            { mCat.Type = (CategoryType)dCat.Type; }
            catch (Exception)
            { mCat.Type = CategoryType.UserCreated; }//unknown type, make a custom type

            return mCat;
        }






        public static List<m.Column> GetAllColumns(int catID)
        {

        }
        public static List<m.Column> GetVisibleColumns(int catID)
        {

        }
        public static m.Column CreateColumn(string name, int catID, m.Column.ColumnType type)
        {

        }
        public static void UpdateColumn(m.Column col)
        {

        }
        public static void DeleteColumn(int colID)
        {

        }
        public static void DeleteColumn(m.Column col)
        {

        }

        private static m.Column ConvertColumnToModel(d.Column col)
        {

        }






        public static m.Entry CreateEntry(int catID)
        {
            d.Category cat = d.DatabaseController.GetCategory(catID);

            d.Record rec = new d.Record();
            rec.CategoryID = catID;
            rec.IsArchived = false;
            Create(rec);//TODO Needs out??

            var cols = d.DatabaseController.GetColumns(catID).ToList();//get existing cols

            var cells = new List<d.Cell>();

            foreach (var col in cols)//create a blank cell for each col
            {
                d.Cell cell = new d.Cell();
                cell.ColumnID = col.ColumnID;
                cell.Data = string.Empty;
                cell.RecordID = rec.RecordID;
                Create(cell);
                cells.Add(cell);
            }

            return ConvertEntryToModel(cat, rec, cols, cells);

            //m.Entry ent = new m.Entry();
            //ent.EntryID = rec.RecordID;
            //ent.CategoryID = catID;
            //ent.AllData = allData;
            //ent.KeyData = keyData;
            //ent.StarRating = starRating;

            //return ent;

        }


        //TODO Test
        public static List<m.Entry> GetEntries(int catID) => GetEntries(catID, true, true);
        public static List<m.Entry> GetCurrentEntries(int catID) => GetEntries(catID, true, false);
        public static List<m.Entry> GetArchivedEntries(int catID) => GetEntries(catID, false, true);

        private static List<m.Entry> GetEntries(int catID, bool getCurrent, bool getArchived)
        {
            List<m.Entry> lis = new List<Model.Entry>();//to return

            d.Category cat = d.DatabaseController.GetCategory(catID);
            List<d.Column> cols = d.DatabaseController.GetColumns(catID).ToList();
            List<d.Record> recs = d.DatabaseController.GetRecords(catID).Where(r => (getCurrent && !r.IsArchived) || (getArchived && r.IsArchived)).ToList();

            foreach (d.Record rec in recs)
            {
                List<d.Cell> cells = d.DatabaseController.GetCells(rec.RecordID).ToList();
                m.Entry e = ConvertEntryToModel(cat, rec, cols, cells);
                lis.Add(e);
            }

            return lis;
        }


        public static void UpdateEntry(m.Entry ent)
        {

            //var cat = d.DatabaseController.GetCategory(ent.CategoryID);//Needed?
            var rec = d.DatabaseController.GetRecord(ent.EntryID);
            var cols = d.DatabaseController.GetColumns(ent.CategoryID);
            var cells = d.DatabaseController.GetCells(ent.EntryID);

            rec.IsArchived = ent.IsArchived;
            Update(rec);//update modified date


            foreach (var col in cols)
            {
                string tData;
                if(ent.AllData.TryGetValue(col.Name,out tData))
                {//could find col data in list, update cell
                    var tCell = cells.FirstOrDefault(c => c.ColumnID == col.ColumnID);
                    if(tCell != null)
                    {
                        tCell.Data = tData;
                        Update(tCell);
                    }
                        
                }
            }
        }

        //TODO IsRecordDeleted vs actual deletion
        public static void DeleteEntry(m.Entry mEnt) => DeleteEntry(mEnt.EntryID);
        public static void DeleteEntry(int entID)
        {
            DeleteRange(d.DatabaseController.GetCells(entID));
            Delete(d.DatabaseController.GetRecord(entID));
        }


        //TODO messy as, clean up - is the old code
        private static m.Entry ConvertEntryToModel(d.Category cat, d.Record rec, List<d.Column> cols, List<d.Cell> cells)
        {
            m.Entry ent = new m.Entry();

            ent.EntryID = rec.RecordID;
            ent.CategoryID = cat.CategoryID;
            ent.IsArchived = rec.IsArchived;

            var dic = new Dictionary<d.Column, string>();

            foreach (d.Column col in cols.OrderBy(c => c.Order))
            {
                //TODO test if cell doesn't exist for this column
                string data = cells.FirstOrDefault(c => c.ColumnID == col.ColumnID)?.Data ?? string.Empty;
                dic.Add(col, data);
            }

            ent.AllData = dic.ToDictionary(d => d.Key.Name, d => d.Value);
            ent.KeyData = dic.Where(k => k.Key.IsKey).ToDictionary(d => d.Key.Name, d => d.Value);
            //TODO fill out the rest of these if it works


            //TODO reinstate
            //if (ent.KeyData.Count >= 1)
            //    ent.DisplayValue1 = ent.KeyData.ToList()[0].Value;
            //if (ent.KeyData.Count >= 2)
            //    ent.DisplayValue2 = ent.KeyData.ToList()[1].Value;
            //if (ent.KeyData.Count >= 3)
            //    ent.DisplayValue3 = ent.KeyData.ToList()[2].Value;


            //Try to get the star rating column, then try to convert the string into an int, assign the int if between 0 and 10
            int srInt;
            string srStr;
            if (!ent.AllData.TryGetValue("Star Rating", out srStr) || !Int32.TryParse(srStr, out srInt) || srInt < 0 || srInt > 10)//TODO Test
                ent.StarRating = 0;
            else
                ent.StarRating = srInt;


            return ent;
        }


























        private static void DeleteRange(IEnumerable<d.IStorable> s) => s.ToList().ForEach(ss => Delete(ss));
        private static void Delete(d.IStorable s)
        {
            //s.IsRecordDeleted = true;
            //s.RecordModifed = DateTime.Now;
            //DatabaseController.Update(s);

            //OR
            d.DatabaseController.Delete(s);
        }

        private static void UpdateRange(IEnumerable<d.IStorable> s) => s.ToList().ForEach(ss => Update(ss));
        private static void Update(d.IStorable s)
        {
            s.RecordModifed = DateTime.Now;
            d.DatabaseController.Update(s);
        }


        private static void CreateRange(IEnumerable<d.IStorable> s) => s.ToList().ForEach(ss => Update(ss));
        private static void Create(d.IStorable s)
        {
            s.IsRecordDeleted = false;
            s.RecordCreated = DateTime.Now;
            s.RecordModifed = DateTime.Now;
            d.DatabaseController.Create(s);
        }










        //public static List<m.Column> GetColumns(int catID)
        //{

        //}


    }




}
