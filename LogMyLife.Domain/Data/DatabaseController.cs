using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using System.IO;
//using SQLitePCL;
//using SQLite.Net;

namespace LogMyLife.Domain.Data
{
    internal static class DatabaseController
    {
        private const string DB_FILE_NAME = "lmldb2.db3";//TODO change name
        private static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),DB_FILE_NAME);
        private static SQLiteConnection db;

        public static bool HasRecords {
            get
            {
                //Not sure what was trying to do here
                //FileInfo fi = new FileInfo(dbPath);
                //bool recentlyCreated = DateTime.Now - fi.CreationTime < new TimeSpan(0, 0, 10);//created in the last 10 secs
                //bool hasCategories = GetCategories().Count() > 0;
                //return recentlyCreated || hasCategories;
                return GetCategories().Count() > 0;
            }
        }



        


        //static DatabaseController()
        public static void Init()
        {
            //TODO remove for testing only
            //File.Delete(dbPath);

            //TODO setup for multi threading https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/ (bottom of page)
            //TODO need to install  Install-Package sqlite-net-pcl  to front end (better way)??
            db = new SQLiteConnection(dbPath);//TODO put in folder

            //Init all tables, doesn't do anything if they already exist
            db.CreateTable<Category>();
            db.CreateTable<Category>();
            db.CreateTable<Column>();
            db.CreateTable<Record>();
            db.CreateTable<Cell>();

        }


        //internal static void CreateCategory(Category c) => db.Insert(c);
        //internal static void CreateColumn(Column c) => db.Insert(c);
        //internal static void CreateRecord(Record c) => db.Insert(c);
        //internal static void CreateCell(Cell c) => db.Insert(c);


        internal static void Create(IStorable s) => db.Insert(s);
        internal static void CreateRange(IEnumerable<IStorable> s) => db.InsertAll(s);

        internal static void Update(IStorable s) => db.Update(s);
        //internal static void UpdateRange(IEnumerable<IStorable> s) => s.ToList().ForEach(ss=>db.Update(ss));

        internal static void Delete(IStorable s) => db.Delete(s);
        //internal static void DeleteRange(IEnumerable<IStorable> s) => s.ToList().ForEach(ss => db.Delete(ss));

        //internal static void UpdateCategory(Category c) => db.Update(c);
        //internal static void UpdateColumn(Column c) => db.Update(c);
        //internal static void UpdateRecord(Record c) => db.Update(c);
        //internal static void UpdateCell(Cell c) => db.Update(c);

        //internal static void UpdateCategories(List<Category> c) => db.UpdateAll(c);
        //internal static void UpdateColumns(List<Column> c) => db.UpdateAll(c);
        //internal static void UpdateRecords(List<Record> c) => db.UpdateAll(c);
        //internal static void UpdateCells(List<Cell> c) => db.UpdateAll(c);



        //internal static void DeleteCategory(Category c) => db.Delete(c);
        //internal static void DeleteColumn(Column c) => db.Delete(c);
        //internal static void DeleteRecord(Record c) => db.Delete(c);
        //internal static void DeleteCell(Cell c) => db.Delete(c);

        //internal static void DeleteCategories(List<Category> c) => c.ForEach(cc=> db.Delete(c));
        //internal static void DeleteColumns(List<Column> c) => c.ForEach(cc => db.Delete(c));
        //internal static void DeleteRecords(List<Record> c) => c.ForEach(cc => db.Delete(c));
        //internal static void DeleteCells(List<Cell> c) => c.ForEach(cc => db.Delete(c));




        internal static IEnumerable<Category> GetCategories()
        {
            return db.Table<Category>();//.Where(c => c.IsRecordDeleted == false);//.ToList();
        }

        internal static Category GetCategory(int catID)
        {
            try
            {
                return db.Get<Category>(catID);
            }
            catch (Exception)
            {
                return null;
            }
            
        }







        internal static IEnumerable<Column> GetColumns(int catID)
        {
            return db.Table<Column>().Where(c => c.CategoryID == catID);//.ToList();
        }

        internal static Column GetColumn(int colID)
        {
            try
            {
                return db.Get<Column>(colID);
            }
            catch (Exception)
            {
                return null;
            }
            
        }



        internal static IEnumerable<Record> GetRecords(int catID)
        {
            return db.Table<Record>().Where(c => c.CategoryID == catID);//.ToList();
        }

        internal static Record GetRecord(int recordID)
        {
            try
            {
                return db.Get<Record>(recordID);
            }
            catch (Exception)
            {
                return null;
            }
            
        }



        internal static IEnumerable<Cell> GetCells(int recordID)
        {
            return db.Table<Cell>().Where(c => c.RecordID == recordID);//.ToList();
        }

        internal static Cell GetCell(int recordID, int columnID)
        {
            try
            {
                return db.Get<Cell>(c => c.RecordID == recordID && c.ColumnID == columnID);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        
    }
}
