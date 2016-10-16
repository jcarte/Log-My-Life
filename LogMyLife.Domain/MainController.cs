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
            d.DatabaseController.Init();
            if (!d.DatabaseController.HasDefaults)//have the default records been created yet?
                CreateDefaultTables(true,true);
        }

        private static void CreateDefaultTables(bool createCols = true,bool createRecs = true)
        {
            var musicCat = CreateCategory("Music", CategoryType.Music);
            var bookCat = CreateCategory("Books", CategoryType.Book);
            var filmCat = CreateCategory("Films", CategoryType.Film);
            var wineCat = CreateCategory("Wine", CategoryType.Wine);
            var tvCat = CreateCategory("TV", CategoryType.TV);

            if (createCols)
            {
                //MUSIC Columns = 3 title, 2 review, 2 normal (1 of which is hidden)
                CreateColumn("Artist", musicCat.CategoryID, m.Column.ColumnType.Title);
                CreateColumn("Album", musicCat.CategoryID, m.Column.ColumnType.Title);
                CreateColumn("Track", musicCat.CategoryID, m.Column.ColumnType.Title);
                CreateColumn("Star Rating", musicCat.CategoryID, m.Column.ColumnType.Review);
                CreateColumn("Comment", musicCat.CategoryID, m.Column.ColumnType.Review);
                CreateColumn("Year", musicCat.CategoryID, m.Column.ColumnType.Normal);
                CreateColumn("Yeara", musicCat.CategoryID, m.Column.ColumnType.Normal);
                CreateColumn("Yearb", musicCat.CategoryID, m.Column.ColumnType.Normal);
                CreateColumn("Yearc", musicCat.CategoryID, m.Column.ColumnType.Normal);
                CreateColumn("Yeard", musicCat.CategoryID, m.Column.ColumnType.Normal);
                CreateColumn("Yeare", musicCat.CategoryID, m.Column.ColumnType.Normal);
                CreateColumn("Yearf", musicCat.CategoryID, m.Column.ColumnType.Normal);
                CreateColumn("Yearg", musicCat.CategoryID, m.Column.ColumnType.Normal);
                CreateColumn("Yearh", musicCat.CategoryID, m.Column.ColumnType.Normal);


                //BOOK Columns = 2 title, 2 review, 1 normal (all hidden)
                CreateColumn("Title", bookCat.CategoryID, Model.Column.ColumnType.Title, true);
                CreateColumn("Author", bookCat.CategoryID, Model.Column.ColumnType.Title, true);
                CreateColumn("Star Rating", bookCat.CategoryID, m.Column.ColumnType.Review, true);
                CreateColumn("Comment", bookCat.CategoryID, m.Column.ColumnType.Review, true);
                CreateColumn("Publisher", bookCat.CategoryID, m.Column.ColumnType.Normal, true);

                //FILM Columns = 1 Title, 1 Review, 0 normal, no start rating
                CreateColumn("Title", filmCat.CategoryID, Model.Column.ColumnType.Title);
                CreateColumn("Comment", filmCat.CategoryID, Model.Column.ColumnType.Review);

                //WINE Columns = 0 ,0, 3 Normal
                CreateColumn("Vintage", wineCat.CategoryID, Model.Column.ColumnType.Normal);
                CreateColumn("Year", wineCat.CategoryID, Model.Column.ColumnType.Normal);
                CreateColumn("Region", wineCat.CategoryID, Model.Column.ColumnType.Normal);

                //TV Columns = 1 ,2, 1 Normal
                CreateColumn("Title", tvCat.CategoryID, Model.Column.ColumnType.Title);
                CreateColumn("Star Rating", tvCat.CategoryID, m.Column.ColumnType.Review);
                CreateColumn("Comment", tvCat.CategoryID, m.Column.ColumnType.Review);
                CreateColumn("Year", tvCat.CategoryID, Model.Column.ColumnType.Normal);
            }

            if (createRecs)
            {
                //music records
                var hits = CreateEntry(musicCat.CategoryID);
                hits.EditColumnData("Artist", "Black Sabbath");
                hits.EditColumnData("Album", "Sabotage");
                hits.EditColumnData("Track", "The Writ");
                hits.EditColumnData("Star Rating", "5");
                hits.EditColumnData("Comment", "This song is freaking awesome, actually I'm going to put it on right now");
                hits.EditColumnData("Year", "1975 (I think)");
                UpdateEntry(hits);

                var cp = CreateEntry(musicCat.CategoryID);
                cp.EditColumnData("Artist", "Coldplay");
                cp.EditColumnData("Album", "I don't know, XY, is that an album?");
                cp.EditColumnData("Track", "Clocks...pretty sure that's one of theirs");
                cp.EditColumnData("Star Rating", "0");
                cp.EditColumnData("Comment", "I'd barely call this music");
                cp.EditColumnData("Year", "???");
                cp.IsArchived = true;
                UpdateEntry(cp);

                var es = CreateEntry(musicCat.CategoryID);
                es.EditColumnData("Artist", "Metallica");
                es.EditColumnData("Album", "The Black Album");
                es.EditColumnData("Track", "Enter Sandman");
                es.EditColumnData("Star Rating", "4");
                es.EditColumnData("Comment", "Pretty good");
                es.EditColumnData("Year", "1990");
                UpdateEntry(es);

                var im = CreateEntry(musicCat.CategoryID);
                im.EditColumnData("Artist", "Iron Maiden");
                im.EditColumnData("Album", "Triller");
                im.EditColumnData("Track", "Prowler");
                im.EditColumnData("Star Rating", "4.5");
                im.EditColumnData("Comment", "Pretty damn good");
                im.EditColumnData("Year", "1981");
                im.IsArchived = true;
                UpdateEntry(im);

                //book records
                var hp = CreateEntry(bookCat.CategoryID);
                hp.EditColumnData("Title", "Harry Potter");
                hp.EditColumnData("Author", "JK Rowling");
                hp.EditColumnData("Star Rating", "3.5");
                hp.EditColumnData("Comment", "Not bad from what I remember...wow I really don't know many books");
                UpdateEntry(hp);

                var oz = CreateEntry(bookCat.CategoryID);
                oz.EditColumnData("Title", "Ozzys Autobiography");
                oz.EditColumnData("Author", "Ozzys Ghost writer");
                oz.EditColumnData("Star Rating", "4");
                oz.EditColumnData("Comment", "Also pretty good");
                UpdateEntry(oz);

                //Film records
                var sf = CreateEntry(filmCat.CategoryID);
                sf.EditColumnData("Title", "Scarface");
                sf.EditColumnData("Comment", "Really awesome, need to see this again soon");
                sf.IsArchived = true;
                UpdateEntry(sf);

                var bj = CreateEntry(filmCat.CategoryID);
                bj.EditColumnData("Title", "Bridget Joan's Diary");
                bj.EditColumnData("Comment", "Who would ever watch this???");
                bj.IsArchived = true;
                UpdateEntry(bj);



                //TV Records
                var bb = CreateEntry(tvCat.CategoryID);
                bb.EditColumnData("Title", "Breaking Bad");
                bb.EditColumnData("Star Rating", "4.5");
                bb.EditColumnData("Comment", "Properly Awesome");
                bb.EditColumnData("Year", "2012");
                bb.IsArchived = true;
                UpdateEntry(bb);

                var got = CreateEntry(tvCat.CategoryID);
                got.EditColumnData("Title", "Game of Thrones");
                got.EditColumnData("Star Rating", "5");
                got.EditColumnData("Comment", "Long and awesome");
                got.EditColumnData("Year", "2011");
                got.IsArchived = false;
                UpdateEntry(got);

                var hoc = CreateEntry(tvCat.CategoryID);
                hoc.EditColumnData("Title", "House of Cards");
                hoc.EditColumnData("Star Rating", "4");
                hoc.EditColumnData("Comment", "Spacey is a bit of a god");
                hoc.EditColumnData("Year", "2012");
                hoc.IsArchived = false;
                UpdateEntry(hoc);

                var na = CreateEntry(tvCat.CategoryID);
                na.EditColumnData("Title", "Narcos");
                na.EditColumnData("Star Rating", "3.5");
                na.EditColumnData("Comment", "I wish I was pablo");
                na.EditColumnData("Year", "2014");
                na.IsArchived = false;
                UpdateEntry(na);

                var bh = CreateEntry(tvCat.CategoryID);
                bh.EditColumnData("Title", "Bojack Horseman");
                bh.EditColumnData("Star Rating", "4");
                bh.EditColumnData("Comment", "I don't think this one is for ninks");
                bh.EditColumnData("Year", "2014");
                bh.IsArchived = false;
                UpdateEntry(bh);

                var xf = CreateEntry(tvCat.CategoryID);
                xf.EditColumnData("Title", "X Factor");
                xf.EditColumnData("Star Rating", "0");
                xf.EditColumnData("Comment", "I do believe the x factor can do one...who would watch this?");
                xf.EditColumnData("Year", "2009");
                xf.IsArchived = true;
                UpdateEntry(xf);
            }
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

        /// <summary>
        /// Convert from a category data object (ORM) to category model
        /// </summary>
        /// <param name="dCat">Data Object</param>
        /// <returns>Category Model</returns>
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















        /// <summary>
        /// Create a new entry and store it in the database
        /// </summary>
        /// <param name="catID">ID of the category to create the new entry in</param>
        /// <returns>Newly created Entry Model</returns>
        public static m.Entry CreateEntry(int catID)
        {
            d.Category cat = d.DatabaseController.GetCategory(catID);

            if (cat == null)
                throw new Exception($"Can't create entry because category ({catID}) doesn't exist");

            d.Record rec = new d.Record();
            rec.CategoryID = catID;
            rec.IsArchived = false;//entrys are not archived when newly created
            Create(rec);

            var cols = d.DatabaseController.GetColumns(catID).ToList();//get existing cols for category

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

            return ConvertEntryToModel(rec, cols, cells);
        }





        /// <summary>
        /// Get a single entry from any category if the ID is known
        /// </summary>
        /// <param name="entryID">Unique ID of the entry</param>
        /// <returns>Entry object, null if doesn't exist</returns>
        public static m.Entry GetEntry(int entryID)
        {
            d.Record rec = d.DatabaseController.GetRecord(entryID);

            if (rec == null)
                return null;

            List<d.Column> cols = d.DatabaseController.GetColumns(rec.CategoryID).ToList();//get columns in category
            List<d.Cell> cells = d.DatabaseController.GetCells(rec.RecordID).ToList();
            m.Entry e = ConvertEntryToModel(rec, cols, cells);
            return e;
        }

        /// <summary>
        /// Get all entries for a category (both current and archived)
        /// </summary>
        /// <param name="catID">ID of the category to get the entries from</param>
        /// <returns>List of all entries in the category</returns>
        public static List<m.Entry> GetEntries(int catID) => GetEntries(catID, true, true);

        /// <summary>
        /// Get all non archived (i.e. current) entries in a category
        /// </summary>
        /// <param name="catID">ID of the category to get the entries from</param>
        /// <returns>List of all non archived entries in categroy</returns>
        public static List<m.Entry> GetCurrentEntries(int catID) => GetEntries(catID, true, false);

        /// <summary>
        /// Get all archived (i.e. not current) entries in a category
        /// </summary>
        /// <param name="catID">ID of the category to get the entries from</param>
        /// <returns>List of all archived entries in categroy</returns>
        public static List<m.Entry> GetArchivedEntries(int catID) => GetEntries(catID, false, true);

        /// <summary>
        /// Get list of entries from a category
        /// </summary>
        /// <param name="catID">ID of the category to get the entries from</param>
        /// <param name="getCurrent">Should items that are not currently archived be returned?</param>
        /// <param name="getArchived">Should archived items be returned?</param>
        /// <returns>List of all entries matching given params</returns>
        private static List<m.Entry> GetEntries(int catID, bool getCurrent, bool getArchived)
        {
            List<m.Entry> lis = new List<Model.Entry>();//obj to return

            d.Category cat = d.DatabaseController.GetCategory(catID);
            List<d.Column> cols = d.DatabaseController.GetColumns(catID).ToList();//get columns in category
            List<d.Record> recs = d.DatabaseController.GetRecords(catID).Where(r => (getCurrent && !r.IsArchived) || (getArchived && r.IsArchived)).ToList();//get records which match params

            foreach (d.Record rec in recs)//for all records returned, get all of their cells and convert the lot to a entry model, add to running list
            {
                List<d.Cell> cells = d.DatabaseController.GetCells(rec.RecordID).ToList();
                m.Entry e = ConvertEntryToModel(rec, cols, cells);
                lis.Add(e);
            }

            return lis;
        }

        /// <summary>
        /// Update an entry in the database
        /// </summary>
        /// <param name="ent">The entry to be updated with updated values</param>
        public static void UpdateEntry(m.Entry ent)
        {
            if (ent == null)
                throw new Exception($"Can't update a null entry");

            var rec = d.DatabaseController.GetRecord(ent.EntryID);
            if (rec == null)
                throw new Exception($"Can't update Entry {ent.EntryID} because it doesn't exist");
            
            var cols = d.DatabaseController.GetColumns(ent.CategoryID);
            var cells = d.DatabaseController.GetCells(ent.EntryID);

            rec.IsArchived = ent.IsArchived;
            Update(rec);//update modified date

            foreach (var col in cols)//for each col in db, find its equivalent in model and map over to cells for updating
            {
                m.Column mCol = ent.Data.Keys.FirstOrDefault(c => c.Name == col.Name);
                if(mCol != null)//does the data col have a model col equivalent in the entry?
                {
                    d.Cell tCell = cells.FirstOrDefault(c => c.ColumnID == col.ColumnID);//get the data cell
                    if (tCell != null)
                    {
                        tCell.Data = ent.Data[mCol];//fill the cell with the new data from the model
                        Update(tCell);//TODO conditional update only if changed?
                    }
                }
            }
        }

        /// <summary>
        /// Delete an entry in the database
        /// </summary>
        /// <param name="mEnt">The entry to be deleted</param>
        public static void DeleteEntry(m.Entry mEnt)
        {
            if (mEnt == null)
                throw new Exception($"Can't delete a null entry");
            DeleteEntry(mEnt.EntryID);
        } 

        /// <summary>
        /// Delete an entry in the database
        /// </summary>
        /// <param name="entID">The ID of the entry to delete in the database</param>
        public static void DeleteEntry(int entID)
        {
            var r = d.DatabaseController.GetRecord(entID);
            if (r == null)
                throw new Exception($"Can't delete Entry {entID} because it doesn't exist");
            
            //Manual Cascading Delete
            DeleteRange(d.DatabaseController.GetCells(entID));
            Delete(d.DatabaseController.GetRecord(entID));
        }

        /// <summary>
        /// Converts a series of data objects (ORMs) into a single Entry Model object
        /// </summary>
        /// <param name="rec">The data record object</param>
        /// <param name="cols">A list of column data objects</param>
        /// <param name="cells">A list of cell data objects</param>
        /// <returns>Single Entry model</returns>
        private static m.Entry ConvertEntryToModel(d.Record rec, List<d.Column> cols, List<d.Cell> cells)
        {
            m.Entry ent = new m.Entry();

            ent.EntryID = rec.RecordID;
            ent.CategoryID = rec.CategoryID;
            ent.IsArchived = rec.IsArchived;

            //Create dictionary of columns and their value for the entry (pulled from the cell)
            var dic = new Dictionary<m.Column, string>();
            foreach (d.Column col in cols.OrderBy(c => c.Order))
            {
                string data = cells.FirstOrDefault(c => c.ColumnID == col.ColumnID)?.Data ?? string.Empty;//if no cell then empty string
                dic.Add(ConvertColumnToModel(col), data);
            }
            ent.Data = dic;

            return ent;
        }








        //TODO complete Column CRUD operations
        //public static List<m.Column> GetAllColumns(int catID)
        //{

        //}
        //public static List<m.Column> GetVisibleColumns(int catID)
        //{

        //}
        public static m.Column CreateColumn(string name, int catID, m.Column.ColumnType type, bool isHidden = false)
        {
            d.Column dCol = new Data.Column();

            d.Category cat = d.DatabaseController.GetCategory(catID);
            if (cat == null)
                throw new Exception($"Can't create entry because category ({catID}) doesn't exist");

            dCol.CategoryID = catID;
            dCol.Name = name;

            //Column order is 1 if first to be created or one more than the greatest previous order otherwise
            var existingCols = d.DatabaseController.GetColumns(catID);
            if (existingCols.Count() == 0)
                dCol.Order = 1;
            else
                dCol.Order = d.DatabaseController.GetColumns(catID).Max(c => c.Order) + 1;

            switch (type)
            {
                case m.Column.ColumnType.Title:
                    dCol.IsKey = true;
                    dCol.IsReview = false;
                    break;
                case m.Column.ColumnType.Review:
                    dCol.IsKey = false;
                    dCol.IsReview = true;
                    break;
                case m.Column.ColumnType.Normal:
                    dCol.IsKey = false;
                    dCol.IsReview = false;
                    break;
            }

            dCol.IsActive = !isHidden;

            Create(dCol);

            return ConvertColumnToModel(dCol);
        }
        public static void UpdateColumn(m.Column col)
        {
            if (col == null)
                throw new Exception($"Can't update Column because it is null");

            d.Column dCol = d.DatabaseController.GetColumn(col.ColumnID);
            if (dCol == null)
                throw new Exception($"Can't update Column {col.ColumnID} because it doesn't exist");

            dCol.Name = col.Name;
            dCol.Order = col.Order;//TODO what if order changes? knock on effect?

            switch (col.Type)
            {
                case m.Column.ColumnType.Title:
                    dCol.IsKey = true;
                    dCol.IsReview = false;
                    break;
                case m.Column.ColumnType.Review:
                    dCol.IsKey = false;
                    dCol.IsReview = true;
                    break;
                case m.Column.ColumnType.Normal:
                    dCol.IsKey = false;
                    dCol.IsReview = false;
                    break;
            }

            dCol.IsActive = !col.IsHidden;

            Update(dCol);
        }
        //public static void DeleteColumn(int colID)
        //{

        //}
        //public static void DeleteColumn(m.Column col)
        //{

        //}

        private static m.Column ConvertColumnToModel(d.Column dCol)
        {
            m.Column mCol = new m.Column();

            mCol.ColumnID = dCol.ColumnID;
            mCol.CategoryID = dCol.ColumnID;
            mCol.Name = dCol.Name;
            mCol.Order = dCol.Order;

            if (dCol.IsKey)
                mCol.Type = m.Column.ColumnType.Title;
            else if (dCol.IsReview)
                mCol.Type = m.Column.ColumnType.Review;
            else
                mCol.Type = m.Column.ColumnType.Normal;

            mCol.IsHidden = !dCol.IsActive;

            return mCol;
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









    }




}
