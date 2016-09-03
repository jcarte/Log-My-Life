using LogMyLife.Domain;
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
            BasicUpdate();


        }

        private static void BasicUpdate()
        {
            LogTitle("Basic Update");
            ListAll();

            Log("\n\nCreate and update");
            var upd1 = MainController.CreateEntry(1);
            upd1.DisplayValue1 = "Display 1";
            upd1.DisplayValue2 = "Display 2";
            upd1.DisplayValue3 = "Display 3";
            upd1.StarRating = 5;
            upd1.IsArchived = true;
            MainController.UpdateEntry(upd1);
            ListAll();

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
        }


        private static void ListAll()
        {
            Log("\n==============================List All==");
            foreach (var cat in MainController.GetCategories())
            {
                Log($"==CATEGORY {cat.ToString()}");
                foreach (var ent in MainController.GetEntries(cat.CategoryID))
                {
                    Log("\n" + ent.ToString(), 5);
                }
            }
            Log("================================End Listing==");

        }
    }
}
