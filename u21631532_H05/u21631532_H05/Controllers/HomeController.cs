using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.SqlClient;
using u21631532_H05.Service;
using u21631532_H05.Models;

namespace u21631532_H05.Controllers
{
    public class HomeController : Controller
    {

        private DataService services = DataService.GetDataService();
        
        public ActionResult Searched(string name)
        {
            TypesController.myBooks bookFound = services.getSearched(name);

            return View("Index", TypesController.myBooks);
        }

        public ActionResult Index()
        {

            List<books> books = services.getBooks();
            List<borrows> borrows = services.getBookDetails();
            books withStateBooks = new books();
            booksVM booksVM = new booksVM();
            booksVM.books = new List<books>();
            for(int i = 0; i < books.Count(); i++)
            {
                withStateBooks = books.ElementAt(i);

                withStateBooks.status = status(books, borrows, i);
                booksVM.books.Add(withStateBooks);
            }
            return View(booksVM.books);
        }

        

        borrows getBorrowedBook (List<borrows> borrows, int id)
        {
            borrows borrows1 = null;

            foreach(var item in borrows)
            {
                borrows1 = item;
                if(item.bookId == id)
                {
                    return borrows1;
                }
            }
            return borrows1;
        }

        string status(List<books> books, List<borrows> borrows,int counter)
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

        //public ActionResult searching(string s)
        //{
        //    try
        //    {
        //        myConnection.Open();
        //        SqlCommand sql = new SqlCommand("Select ")
        //    }
        //}


        public ActionResult Students()
        {
            List<students> stu = services.getStudents();


            return View(stu);
        }

       
        public ActionResult Borrows()
        {
            List<borrows> myBorrows = services.getBookDetails();
            return View(myBorrows);
        }
    }
}