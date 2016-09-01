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
            ListAll();

            Log("\n\nAdd new");
            var newEnt = MainController.CreateEntry(1);


            ListAll();
        }

        private static void ListAll()
        {
            Log("\n==List All==");
            foreach (var cat in MainController.GetCategories())
            {
                Log($"=={cat.ToString()}");
                foreach (var ent in MainController.GetEntries(cat.CategoryID))
                {
                    Log("\n" + ent.ToString(), 1);
                }
            }
            Log("==End Listing==");

        }
    }
}
