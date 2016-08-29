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
        static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),DB_FILE_NAME);
        private static SQLiteConnection db;





        //TODO Replace
        static DatabaseController()
        //public static void Init()
        {
            File.Delete(dbPath);//todo remove for testing only

            //TODO setup for multi threading https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/ (bottom of page)
            //TODO need to install  Install-Package sqlite-net-pcl  to front end (better way)??
            db = new SQLiteConnection(dbPath);//TODO put in folder
            
            db.CreateTable<Category>();
            db.CreateTable<Column>();
            db.CreateTable<Record>();
            db.CreateTable<Cell>();

            //UserCreated = 0,
            //Music = 1,
            //Film = 2,
            //Book = 3,
            //Wine = 4

            List<Category> cats = new List<Category>();

            cats.Add(new Category()
            {
                Name = "Music",
                Type = 1,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });

            cats.Add(new Category()
            {
                Name = "Film",
                Type = 2,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });

            cats.Add(new Category()
            {
                Name = "Book",
                Type = 3,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });

            cats.Add(new Category()
            {
                Name = "Wine",
                Type = 4,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });

            cats.Add(new Category()
            {
                Name = "Your List",
                Type = 0,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });

            CreateRange(cats);



            //===Create cols====
            Category musicCat = cats[0];

            List<Column> cols = new List<Column>();
            cols.Add(new Column()
            {
                Name = "Artist",
                CategoryID = musicCat.CategoryID,
                IsActive = true,
                IsKey = true,
                IsReview = false,
                Order = 1,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cols.Add(new Column()
            {
                Name = "Album",
                CategoryID = musicCat.CategoryID,
                IsActive = true,
                IsKey = true,
                IsReview = false,
                Order = 2,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cols.Add(new Column()
            {
                Name = "Track",
                CategoryID = musicCat.CategoryID,
                IsActive = true,
                IsKey = true,
                IsReview = false,
                Order = 3,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cols.Add(new Column()
            {
                Name = "Star Rating",
                CategoryID = musicCat.CategoryID,
                IsActive = true,
                IsKey = false,
                IsReview = true,
                Order = 4,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cols.Add(new Column()
            {
                Name = "Comment",
                CategoryID = musicCat.CategoryID,
                IsActive = true,
                IsKey = false,
                IsReview = true,
                Order = 5,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cols.Add(new Column()
            {
                Name = "Would James Approve?",
                CategoryID = musicCat.CategoryID,
                IsActive = true,
                IsKey = false,
                IsReview = false,
                Order = 6,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });

            CreateRange(cols);

            

            
            //====Create Record====

            Record r1 = new Record()
            {
                CategoryID = musicCat.CategoryID,
                IsArchived = false,
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            };

            Create(r1);





            //====Create Cells===

            List<Cell> cells = new List<Cell>();

            cells.Add(new Cell()
            {
                RecordID = r1.RecordID,
                ColumnID = cols[0].ColumnID,//Artist
                Data = "Black Sabbath",
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cells.Add(new Cell()
            {
                RecordID = r1.RecordID,
                ColumnID = cols[1].ColumnID,//Album
                Data = "Sabotage",
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cells.Add(new Cell()
            {
                RecordID = r1.RecordID,
                ColumnID = cols[2].ColumnID,//Track
                Data = "Hole in the Sky",
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cells.Add(new Cell()
            {
                RecordID = r1.RecordID,
                ColumnID = cols[3].ColumnID,//Star Rating
                Data = "10",
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cells.Add(new Cell()
            {
                RecordID = r1.RecordID,
                ColumnID = cols[4].ColumnID,//Comment
                Data = "Totally Badass",
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });
            cells.Add(new Cell()
            {
                RecordID = r1.RecordID,
                ColumnID = cols[5].ColumnID,//WJA
                Data = "I think he might, yes",
                IsRecordDeleted = false,
                RecordCreated = DateTime.Now,
                RecordModifed = DateTime.Now
            });


            //TODO add more default records / refactor / Move this logic to the main controller
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
