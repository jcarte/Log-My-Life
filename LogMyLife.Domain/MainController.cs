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
        //TODO remove for testing only
        //public static void DestroyDatabase()
        //{
        //    d.DatabaseController.Destroy();
        //}

        public static m.Category CreateCategory(string name, CategoryType type)
        {
            d.Category dCat = new d.Category();

            dCat.IsRecordDeleted = false;
            dCat.Name = name;
            dCat.RecordCreated = DateTime.Now;
            dCat.RecordModifed = DateTime.Now;
            dCat.Type = (int)type;

            d.DatabaseController.Create(dCat);//TODO check doesn't need an out

            return ConvertCategoryToModel(dCat);
        }

        public static List<m.Category> GetCategories()
        {
            //TODO Remove
            //d.DatabaseController.Init();


            List<m.Category> lis = new List<Model.Category>();
            d.DatabaseController.GetCategories().ToList().ForEach(c => lis.Add(ConvertCategoryToModel(c)));
            return lis;
        }

        public static void UpdateCategory(m.Category mCat)//TODO add protection - what if not in db
        {
            d.Category dCat = d.DatabaseController.GetCategory(mCat.CategoryID);//TODO should this get from db???Other way? hold the data obj inside the model?

            dCat.Name = mCat.Name;
            dCat.Type = (int)mCat.Type;

            Update(dCat);
        }

        //TODO IsRecordDeleted vs actual deletion
        public static void DeleteCategory(m.Category mCat) => DeleteCategory(mCat.CategoryID);//TODO needed?
        public static void DeleteCategory(int catID)
        {
            //Manual cascading delete
            DeleteRange(d.DatabaseController.GetRecords(catID));
            DeleteRange(d.DatabaseController.GetColumns(catID));
            DeleteRange(d.DatabaseController.GetCells(catID));
            Delete(d.DatabaseController.GetCategory(catID));
        }

        private static m.Category ConvertCategoryToModel(d.Category dCat)
        {
            m.Category mCat = new m.Category();
            mCat.CategoryID = dCat.CategoryID;
            mCat.Name = dCat.Name;

            try
            {
                mCat.Type = (CategoryType)dCat.Type;
            }
            catch (Exception)
            {
                mCat.Type = CategoryType.UserCreated;
            }

            return mCat;
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
            }

            return lis;
        }


        public static void UpdateEntry(m.Entry ent)
        {

            var cat = d.DatabaseController.GetCategory(ent.CategoryID);//Needed?
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
            s.RecordModifed = DateTime.Now;
            d.DatabaseController.Update(s);
        }










        //public static List<m.Column> GetColumns(int catID)
        //{

        //}


    }




}
