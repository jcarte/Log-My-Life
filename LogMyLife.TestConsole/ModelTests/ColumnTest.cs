using LogMyLife.Domain;
using LogMyLife.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static LogMyLife.Domain.Test.Logging;

namespace LogMyLife.TestConsole.ModelTests
{
    public class ColumnTest
    {
        static Category cat1;

        static Column cat1Col1;
        static Column cat1Col2;
        static Column cat1Col3;
        static Column cat1Col4;//Added in CreateColumn


        static Entry cat1Ent1;
        static Entry cat1Ent2;
        static Entry cat1Ent3;

        static Category cat2;
        static Column cat2Col1;
        static Column cat2Col3;

        static Category cat3;

        public static void RunTest()
        {
            
            CreateInitial();

            AddColumn();

            AddEntry();

            UpdateEntry();

            DeleteColumn();

            AddColumnBack();

            UpdateColumn();

            //DeleteCategory();

        }

        private static void UpdateColumn()
        {
            LogTitle("Add Column After Being Deleting Test");
            ListAll();

            cat1Col1.Name = "BOB (RENAME)";
            cat1Col1.Type = Column.ColumnType.Review;
            cat1Col1.Order = 500;
            MainController.UpdateColumn(cat1Col1);
            ListAll();

            
        }

        //private static void DeleteCategory()
        //{
        //    LogTitle("Add Column After Being Deleting Test");
        //    ListAll();

        //    MainController.DeleteCategory(cat1);

        //    ListAll();
        //}

        private static void AddColumnBack()
        {
            LogTitle("Add Column After Being Deleting Test");
            ListAll();

            cat1Col2 = MainController.CreateColumn("Column2 (Review)", cat1.CategoryID, Domain.Model.Column.ColumnType.Review);

            ListAll();

        }

        private static void DeleteColumn()
        {
            LogTitle("Delete Column Test");
            ListAll();

            MainController.DeleteColumn(cat1Col2);

            ListAll();
        }

        private static void UpdateEntry()
        {
            LogTitle("Update Entry Test");
            ListAll();


            cat1Ent2 = MainController.GetEntry(cat1Ent2.EntryID);
            cat1Ent2.EditColumnData("Column4 (Added)", "2-4");
            MainController.UpdateEntry(cat1Ent2);

            ListAll();
        }

        private static void AddEntry()
        {

            LogTitle("Create Entry Test");
            ListAll();

            var cat1Ent4 = MainController.CreateEntry(cat1.CategoryID);
            cat1Ent4.EditColumnData("Column1 (Title)", "4-1");
            cat1Ent4.EditColumnData("Column2 (Review)", "4-2");
            cat1Ent4.EditColumnData("Column3 (Normal)", "4-3");
            cat1Ent4.EditColumnData("Column4 (Added)", "4-4");
            MainController.UpdateEntry(cat1Ent4);

            ListAll();
        }

        private static void AddColumn()
        {
            LogTitle("Create Column Test");
            ListAll();

            cat1Col4 = MainController.CreateColumn("Column4 (Added)",cat1.CategoryID,Column.ColumnType.Normal);

            ListAll();
        }

        private static void CreateInitial()
        {
            LogTitle("Create Test");
            ListAll();
            
            cat1 =  MainController.CreateCategory("Test 1 (Enties / Cols)", Domain.Model.Category.CategoryType.UserCreated);

            cat1Col1 = MainController.CreateColumn("Column1 (Title)", cat1.CategoryID, Domain.Model.Column.ColumnType.Title);
            cat1Col2 = MainController.CreateColumn("Column2 (Review)", cat1.CategoryID, Domain.Model.Column.ColumnType.Review);
            cat1Col3 = MainController.CreateColumn("Column3 (Normal)", cat1.CategoryID, Domain.Model.Column.ColumnType.Normal);

            cat1Ent1 = MainController.CreateEntry(cat1.CategoryID);
            cat1Ent1.EditColumnData("Column1 (Title)", "1-1");
            cat1Ent1.EditColumnData("Column2 (Review)", "1-2");
            cat1Ent1.EditColumnData("Column3 (Normal)", "1-3");

            cat1Ent2 = MainController.CreateEntry(cat1.CategoryID);
            cat1Ent2.EditColumnData("Column1 (Title)", "2-1");
            cat1Ent2.EditColumnData("Column2 (Review)", "2-2");
            cat1Ent2.EditColumnData("Column3 (Normal)", "2-3");

            cat1Ent3 = MainController.CreateEntry(cat1.CategoryID);
            cat1Ent3.EditColumnData("Column1 (Title)", "3-1");
            cat1Ent3.EditColumnData("Column2 (Review)", "3-2");
            cat1Ent3.EditColumnData("Column3 (Normal)", "3-3");

            MainController.UpdateEntry(cat1Ent1);
            MainController.UpdateEntry(cat1Ent2);
            MainController.UpdateEntry(cat1Ent3);

            cat2 = MainController.CreateCategory("Test 2 (Cols)", Domain.Model.Category.CategoryType.UserCreated);
            cat2Col1 = MainController.CreateColumn("ColumnA (Title)", cat2.CategoryID, Domain.Model.Column.ColumnType.Title);
            cat2Col3 = MainController.CreateColumn("ColumnB (Normal)", cat2.CategoryID, Domain.Model.Column.ColumnType.Normal);

            
            cat3 = MainController.CreateCategory("Test 3 (None)", Domain.Model.Category.CategoryType.UserCreated);

            ListAll();
        }

        private static void ListAll()
        {
            Log("\n==List All==");
            foreach (var cat in MainController.GetCategories())
            {
                Log("\n"+cat.ToString(), 1);
                foreach (var col in MainController.GetAllColumns(cat.CategoryID))
                {
                    Log($"     {col.ToString()}");
                }
                foreach (var ent in MainController.GetEntries(cat.CategoryID))
                {
                    Log($"{ent.ToString()}",3);
                }
            }
            Log("\n==End Listing==");
        }
    }
}
