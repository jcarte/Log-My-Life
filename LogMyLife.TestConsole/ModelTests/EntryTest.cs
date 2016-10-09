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
    public static class EntryTest
    {
        public static void RunTest()
        {


            //BasicAdd();
            //BasicGetEntries();
            BasicGetEntry();
            //BasicUpdate();
            //KeyUpdate();
            //StarUpdate();
            //BasicDelete();
        }
        private static void BasicDelete()
        {
            LogTitle("Basic Delete");
            ListAll();

            Entry last = null;

            Log("\n\nDelete all defaults");
            foreach (var cat in MainController.GetCategories())
            {
                foreach (var ent in MainController.GetEntries(cat.CategoryID))
                {
                    last = ent;
                    MainController.DeleteEntry(ent.EntryID);
                }
            }
            ListAll();


            Log("\n\nTry delete non existent entry *FAIL EXPECTED*");
            try
            {
                MainController.DeleteEntry(999);
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            ListAll();


            Log("\n\nTry delete null entry *FAIL EXPECTED*");
            try
            {
                MainController.DeleteEntry(null);
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            ListAll();



            Log("\n\nTry delete already deleted entry *FAIL EXPECTED*");
            try
            {
                MainController.DeleteEntry(last);
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            ListAll();

        }
        private static void StarUpdate()
        {
            LogTitle("Star Field Updates");
            Log("\n\nCreate Blank");
            var upd1 = MainController.CreateEntry(1);
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nGet Star from int before update: {upd1.StarRating}");

            Log("\n\n1 rating through int");
            upd1.StarRating = 1f;
            MainController.UpdateEntry(upd1);
            ListAll(true);
            Log($"\n\nGet Star from int: {upd1.StarRating}");


            Log("\n\n2.75 rating through int");
            upd1.StarRating = 2.75f;
            MainController.UpdateEntry(upd1);
            ListAll(true);
            Log($"\n\nGet Star from int: {upd1.StarRating}");


            Log("\n\n0 rating through int");
            upd1.StarRating = 0f;
            MainController.UpdateEntry(upd1);
            ListAll(true);
            Log($"\n\nGet Star from int: {upd1.StarRating}");


            Log("\n\nTry put -1 rating *FAIL EXPECTED*");
            try
            {
                upd1.StarRating = -1;
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);

            Log("\n\nTry put 11 rating *FAIL EXPECTED*");
            try
            {
                upd1.StarRating = 11;
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);

            Log("\n\nTry put int min rating *FAIL EXPECTED*");
            try
            {
                upd1.StarRating = int.MinValue;
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);

            Log("\n\nTry put int max rating *FAIL EXPECTED*");
            try
            {
                upd1.StarRating = int.MaxValue;
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);








            Log("\n\n10 rating through string");
            upd1.EditColumnData("Star Rating", "10");
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);

            Log("\n\n2 rating through string");
            upd1.EditColumnData("Star Rating", "2");
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);

            Log("\n\n'005' rating through string");
            upd1.EditColumnData("Star Rating", "005");
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);

            Log("\n\nTry put string -11");
            try
            {
                upd1.EditColumnData("Star Rating", "-11");
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);

            Log("\n\nTry put string 111");
            try
            {
                upd1.EditColumnData("Star Rating", "111");
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            MainController.UpdateEntry(upd1);
            Log($"Get Star from int: {upd1.StarRating}");
            ListAll(true);

            Log("\n\nTry put string 'BOB' *FAIL EXPECTED*");
            try
            {
                upd1.EditColumnData("Star Rating", "BOB");
                Log($"Get Star from int: {upd1.StarRating}");
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            MainController.UpdateEntry(upd1);
            ListAll(true);



            Log("\n\nStar rating for cat which doesn't have that column");
            var upd2 = MainController.CreateEntry(3);
            try
            {
                upd2.StarRating = 5;
            }
            catch (Exception x)
            {Log($"[Exception] message: {x.Message}", 3);}
            try
            {
                Log($"Get Star from int: {upd2.StarRating}");
            }
            catch (Exception x)
            { Log($"[Exception] message: {x.Message}", 3); }
            MainController.UpdateEntry(upd2);
            ListAll(true);
        }
        private static void KeyUpdate()
        {
            LogTitle("Key Field Updates");
            Log("\n\nCreate Blank on cat with no key fields");
            var upd1 = MainController.CreateEntry(4);
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log("\n\nTry to update all 3 display fields");
            upd1.DisplayValue1 = "Display1";
            upd1.DisplayValue2 = "Display2";
            upd1.DisplayValue3 = "Display3";
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nTry to get 3 display fields: 1 = {upd1.DisplayValue1}, 2 = {upd1.DisplayValue2}, 3 = {upd1.DisplayValue3}");
        }

        private static void BasicUpdate()
        {
            LogTitle("Basic Update standard fields");
            Log("\n\nCreate and incrementally update");
            Log($"\n\nBlank");
            var upd1 = MainController.CreateEntry(1);
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nDisplay1");
            upd1.DisplayValue1 = "Display 1";
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nDisplay2");
            upd1.DisplayValue2 = "Display 2";
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nDisplay3");
            upd1.DisplayValue3 = "Display 3";
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nStar Rating");
            upd1.StarRating = 5;
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nArchived");
            upd1.IsArchived = true;
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nTry to get 3 display fields: 1 = {upd1.DisplayValue1}, 2 = {upd1.DisplayValue2}, 3 = {upd1.DisplayValue3}");

            Log($"\n\nClear off Display2");
            upd1.DisplayValue2 = string.Empty;
            MainController.UpdateEntry(upd1);
            ListAll(true);

            Log($"\n\nClear off Display1,3");
            upd1.DisplayValue1 = string.Empty;
            upd1.DisplayValue3 = string.Empty;
            MainController.UpdateEntry(upd1);
            ListAll(true);

            //Log($"\n\nDelete Entry");
            //MainController.DeleteEntry(upd1);

        }

        private static void BasicGetEntries()
        {
            LogTitle("Basic Get Entries");
            ListAll();

            var all = MainController.GetEntries(1);
            var arc = MainController.GetArchivedEntries(1);
            var cur = MainController.GetCurrentEntries(1);

            Log("\n\nGet For Cat With Both archive and current");
            Log("\nGet All Entries for Cat 1");
            all.ForEach(e => Log(e.ToString(),1));
            Log("\nGet Archived Entries for Cat 1");
            arc.ForEach(e => Log(e.ToString(), 1));
            Log("\nGet Current Entries for Cat 1");
            cur.ForEach(e => Log(e.ToString(), 1));

            

            Log("\n\nGet For Cat With all current");
            Log("\nGet All Entries for Cat 2");
            MainController.GetEntries(2).ForEach(e => Log(e.ToString(), 1));
            Log("\nGet Archived Entries for Cat 2");
            MainController.GetArchivedEntries(2).ForEach(e => Log(e.ToString(), 1));
            Log("\nGet Current Entries for Cat 2");
            MainController.GetCurrentEntries(2).ForEach(e => Log(e.ToString(), 1));


            Log("\n\nGet For Cat With all archive");
            Log("\nGet All Entries for Cat 3");
            MainController.GetEntries(3).ForEach(e => Log(e.ToString(), 1));
            Log("\nGet Archived Entries for Cat 3");
            MainController.GetArchivedEntries(3).ForEach(e => Log(e.ToString(), 1));
            Log("\nGet Current Entries for Cat 3");
            MainController.GetCurrentEntries(3).ForEach(e => Log(e.ToString(), 1));


            Log("\n\nGet For Cat Which doesn't exist");
            Log("\nGet All Entries for Cat 99");
            MainController.GetEntries(99).ForEach(e => Log(e.ToString(), 1));
            Log("\nGet Archived Entries for Cat 99");
            MainController.GetArchivedEntries(99).ForEach(e => Log(e.ToString(), 1));
            Log("\nGet Current Entries for Cat 99");
            MainController.GetCurrentEntries(99).ForEach(e => Log(e.ToString(), 1));


        }


        private static void BasicGetEntry()
        {
            LogTitle("Basic Get Entry");
            ListAll();

            Log("\n\nGet Entry 1");
            Log(MainController.GetEntry(1).ToString(), 1);


            Log("\n\nGet Entry 9998 (Doesn't exist) Null Expected");

            Entry e = MainController.GetEntry(9998);
            if (e == null)
                Log("Success, Entry Null");
            else
                Log("Fail, Entry not Null: " + e.ToString());
            


        }


        private static void BasicAdd()
        {
            LogTitle("Basic Add");
            ListAll();

            Log("\n\n\nAdd new");
            MainController.CreateEntry(1);
            ListAll();

            Log("\n\n\nAdd to Cat with no entries");
            MainController.CreateEntry(3);
            ListAll();


            Log("\n\n\nAdd to Cat that doesn't exist  (id 969) *FAIL EXPECTED*");
            try
            {
                var newEnt3 = MainController.CreateEntry(969);
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            ListAll();


            Log("\n\n\nAdd multiple to cat");
            MainController.CreateEntry(1);
            MainController.CreateEntry(1);
            MainController.CreateEntry(1);
            ListAll();


            Log("\n\n\nAdd new to every list");
            MainController.CreateEntry(1);
            MainController.CreateEntry(2);
            MainController.CreateEntry(3);
            MainController.CreateEntry(4);
            ListAll();
        }


        private static void ListAll(bool showDictionaries = false)
        {
            //Log("\n==============================List All==");
            //foreach (var cat in MainController.GetCategories())
            //{
            //    Log($"==CATEGORY {cat.ToString()}");
            //    foreach (var ent in MainController.GetEntries(cat.CategoryID))
            //    {
            //        Log("\n" + ent.ToString(), 5);
            //        if(showDictionaries)
            //        {
            //            StringBuilder sb = new StringBuilder();
                        
            //            sb.Append("Dictionary All Data: ");
            //            ent.AllData.ToList().ForEach(kvp => sb.Append($"[{kvp.Key}, {kvp.Value}] "));
            //            sb.Append("\nDictionary Visible: ");
            //            ent.VisibleData.ToList().ForEach(kvp => sb.Append($"[{kvp.Key}, {kvp.Value}] "));
            //            sb.Append("\nDictionary Hidden: ");
            //            ent.HiddenData.ToList().ForEach(kvp => sb.Append($"[{kvp.Key}, {kvp.Value}] "));
            //            sb.Append("\nDictionary Title: ");
            //            ent.TitleData.ToList().ForEach(kvp => sb.Append($"[{kvp.Key}, {kvp.Value}] "));
            //            sb.Append("\nDictionary Review: ");
            //            ent.ReviewData.ToList().ForEach(kvp => sb.Append($"[{kvp.Key}, {kvp.Value}] "));
            //            sb.Append("\nDictionary Other: ");
            //            ent.OtherData.ToList().ForEach(kvp => sb.Append($"[{kvp.Key}, {kvp.Value}] "));
            //            Log(sb.ToString(), 4);
            //        }
            //    }
            //}
            //Log("================================End Listing==");

        }
    }
}
