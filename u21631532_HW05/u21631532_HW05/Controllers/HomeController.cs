using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21631532_HW05.Models;


namespace u21631532_HW05.Controllers
{
    public class HomeController : Controller
    {
        private DataService services = DataService.GetDataService();

        public ActionResult Index()
        {
            List<books> books = services.getBooks();
            List<borrows> borrows = services.getBookDetails();
            
            booksVM booksVM1 = new booksVM();
            booksVM1.books = new List<books>();
            books withStateBooks = new books();
            for (int i = 0; i < books.Count(); i++)
            {
                
                withStateBooks = books.ElementAt(i);

                withStateBooks.status = status(books, borrows, i);
                booksVM1.books.Add(withStateBooks);
            }
            return View(booksVM1.books);
        }

        public ActionResult Search(string name, string typeName, string authors)
        {
            //TypesController.myBooks.Clear();
            TypesController.myBooks = services.getSearched(name, typeName, authors);

            return View("Index", TypesController.myBooks);
        }

        string status(List<books> books, List<borrows> borrows, int counter)
        {
            string state = "";
            books books1 = new books();
            borrows borrows1 = new borrows();

            books1 = books.ElementAt(counter);

            if (getBorrowedBook(borrows, books1.bookId) != null)
            {
                borrows borrows2 = getBorrowedBook(borrows, books1.bookId);
                if (borrows2.takenDate.CompareTo(borrows2.broughtDate) > 0 || borrows2.broughtDate == null)
                {
                    state = "Out";
                }
                else
                {
                    state = "Available";
                }
            }

            return state;
        }

        borrows getBorrowedBook(List<borrows> borrows, int id)
        {
            borrows borrows1 = null;

            foreach (var item in borrows)
            {
                borrows1 = item;
                if (item.bookId == id)
                {
                    return borrows1;
                }
            }
            return borrows1;
        }

        public ActionResult Students()
        {
            List<students> stu = services.getStudents();

            return View(stu);
        }

        public ActionResult Borrows()
        {
            List<borrows> borrows = services.getBookDetails();


            return View(borrows);
        }

        [HttpGet]
        public ActionResult Borrows(int id, string state)
        {
            List<borrows> borrows = services.getBybookId(id);
            borrows borrows1 = services.getUserId(id);
            ViewBag.Bookname = borrows1.borrowName;
            ViewBag.Status = state;
            ViewBag.numBooks = services.getNumberofBooks(id);
            return View("Borrows", borrows);
        }
    }
}