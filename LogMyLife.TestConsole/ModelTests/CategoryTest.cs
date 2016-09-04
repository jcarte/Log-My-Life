using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static LogMyLife.Domain.Test.Logging;

namespace LogMyLife.Domain.Test.ModelTests
{
    public class CategoryTest
    {
        public static void RunTest()
        {
            ListAll();

            Log("\n\nAdd new");
            var newCat = MainController.CreateCategory("Test New Name", Model.Category.CategoryType.UserCreated);
            ListAll();

            Log("\n\nChange Name");
            newCat.Name = "New Name Changed";
            MainController.UpdateCategory(newCat);
            ListAll();

            Log("\n\nChange Type");
            newCat.Type = Model.Category.CategoryType.Book;
            MainController.UpdateCategory(newCat);
            ListAll();

            Log("\n\nDelete (by entering object)");
            MainController.DeleteCategory(newCat);
            ListAll();

            Log("\n\nAdd a crap load of them");
            var a = MainController.CreateCategory("new 1", Model.Category.CategoryType.Book);
            var b = MainController.CreateCategory("new 2", Model.Category.CategoryType.Book);
            var c = MainController.CreateCategory("new 3", Model.Category.CategoryType.Book);
            var d = MainController.CreateCategory("new 4", Model.Category.CategoryType.Book);
            var e = MainController.CreateCategory("new 5", Model.Category.CategoryType.Book);
            var f = MainController.CreateCategory("new 6", Model.Category.CategoryType.Book);
            var g = MainController.CreateCategory("new 7", Model.Category.CategoryType.Music);
            ListAll();

            Log("\n\nDelete (by entering id)");
            MainController.DeleteCategory(a.CategoryID);
            MainController.DeleteCategory(b.CategoryID);
            MainController.DeleteCategory(c.CategoryID);
            MainController.DeleteCategory(d.CategoryID);
            MainController.DeleteCategory(e.CategoryID);
            MainController.DeleteCategory(f.CategoryID);
            MainController.DeleteCategory(g.CategoryID);
            ListAll();


            Log("\n\nDelete in middle and add a new one");
            MainController.DeleteCategory(2);
            var h = MainController.CreateCategory("new87", Model.Category.CategoryType.Music);
            ListAll();


            Log("\n\nDelete Item that doesn't exist (id 999) *FAIL EXPECTED*");
            try
            {
                MainController.DeleteCategory(999);
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}",3);
            }
            ListAll();




            Log("\n\nAdd a blank name");
            var i = MainController.CreateCategory("", Model.Category.CategoryType.UserCreated);
            ListAll();




            Log("\n\nDelete an item then update it *FAIL EXPECTED*");
            MainController.DeleteCategory(i.CategoryID);
            i.Name = "This has been deleted";
            try
            {
                MainController.UpdateCategory(i);
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            ListAll();




            Log("\n\nDelete all then add another item");
            foreach (var ca in MainController.GetCategories())
            {
                MainController.DeleteCategory(ca);
            }
            MainController.CreateCategory("First new item", Model.Category.CategoryType.Film);
            ListAll();






            Log("\n\nAdd 4 with the same name");
            MainController.CreateCategory("Same Name", Model.Category.CategoryType.Film);
            MainController.CreateCategory("Same Name", Model.Category.CategoryType.Film);
            MainController.CreateCategory("Same Name", Model.Category.CategoryType.Film);
            MainController.CreateCategory("Same Name", Model.Category.CategoryType.Film);
            ListAll();




            Log("\n\nDelete an Item multiple times *FAIL EXPECTED*");
            var toDel = MainController.GetCategories()[0];
            try
            {
                MainController.DeleteCategory(toDel);
                MainController.DeleteCategory(toDel);
                MainController.DeleteCategory(toDel);
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            ListAll();



            Log("\n\nUpdate a null *FAIL EXPECTED*");
            try
            {
                MainController.UpdateCategory(null);
            }
            catch (Exception x)
            {
                Log($"[Exception] message: {x.Message}", 3);
            }
            ListAll();



        }
        private static void Add()
        {
            
        }

        private static void ListAll()
        {
            Log("\n==List All==");
            foreach (var cat in MainController.GetCategories())
            {
                Log(cat.ToString(),1);
            }
            Log("\n==End Listing==");
        }
    }
}
