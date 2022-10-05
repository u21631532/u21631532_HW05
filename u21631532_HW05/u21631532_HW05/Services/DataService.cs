using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using u21631532_HW05.Models;

namespace u21631532_HW05
{
    public class DataService
    {
        private SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
        private SqlConnection conn;
        private static DataService instance;
        public static DataService GetDataService()
        {
            if (instance == null)
            {
                instance = new DataService();
            }
            return instance;
        }


        public string getConnection()
        {

            stringBuilder["Data Source"] = "DESKTOP-P9OEU6S\\SQLEXPRESS";
            stringBuilder["Integrated Security"] = "True";
            stringBuilder["Initial Catalog"] = "Library";

            return stringBuilder.ToString();
        }

        public bool openConnection()
        {
            bool state = true;

            try
            {
                string connstring = getConnection();

                conn = new SqlConnection(connstring);
                conn.Open();
            }
            catch (Exception er)
            {
                state = false;
            }

            return state;
        }

        public bool closeConnection()
        {
            if (conn != null)
            {
                conn.Close();
            }
            return true;
        }

        public List<types> getType()
        {
            List<types> bookTypes = new List<types>();

            openConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM types", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    types types = new types();
                    types.typeId = Convert.ToInt32(reader["typeId"]);
                    types.name = reader["name"].ToString();

                    bookTypes.Add(types);
                }
            }

            return bookTypes;
        }

        public List<books> getBooks()
        {
            List<books> myBooks = new List<books>();
            types myTypes = new types();

            openConnection();
            SqlCommand cmd = new SqlCommand("select books.bookId, books.name, books.pagecount, books.point, authors.surname ,types.name AS typeName" + "\n" +
                "from books, types, authors" + "\n" +
                "where books.typeId = types.typeId and authors.authorId = books.authorId", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    books Books = new books();
                    Books.bookId = Convert.ToInt32(reader["bookId"]);
                    Books.name = reader["name"].ToString();
                    Books.pagecount = Convert.ToInt32(reader["pagecount"]);
                    Books.point = Convert.ToInt32(reader["point"]);

                    Books.typeId = reader["typeName"].ToString();
                    Books.authorId = Convert.ToString(reader["surname"]);

                    myBooks.Add(Books);
                }
            }
            closeConnection();

            return myBooks;
        }

        public List<borrows> getBookDetails()
        {
            //Uses books table only still needs things from other tables
            List<borrows> borrowsTable = new List<borrows>();

            openConnection();
            SqlCommand cmd = new SqlCommand("select borrows.borrowId, borrows.bookId ,borrows.takenDate, borrows.broughtDate, students.name, students.surname" + "\n" +
             "from borrows, students" + "\n" +
             "where students.studentId = borrows.studentId", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    borrows Books = new borrows();
                    Books.borrowId = Convert.ToInt32(reader["borrowId"]);
                    Books.studentId = reader["name"].ToString() + " " + reader["surname"].ToString();
                    Books.takenDate = Convert.ToDateTime(reader["takenDate"]);
                    if (reader["broughtDate"] != DBNull.Value)
                    {
                        Books.broughtDate = Convert.ToDateTime(reader["broughtDate"]);
                    }
                    Books.bookId = Convert.ToInt32(reader["bookId"]);

                    borrowsTable.Add(Books);
                }
            }
            closeConnection();

            return borrowsTable;
        }



        public List<students> getStudents()
        {
            //Uses books table only still needs things from other tables
            List<students> students = new List<students>();

            openConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM students", conn);

            using (SqlDataReader reads = cmd.ExecuteReader())
            {
                while (reads.Read())
                {
                    students Books = new students();
                    Books.studentId = Convert.ToInt32(reads["studentId"]);
                    Books.name = Convert.ToString(reads["name"]);
                    Books.surname = Convert.ToString(reads["surname"]);
                    Books.Class = reads["class"].ToString();
                    Books.point = Convert.ToInt32(reads["point"]);

                    students.Add(Books);
                }
            }
            closeConnection();

            return students;
        }

        public List<authors> getAuthors()
        {
            List<authors> authors = new List<authors>();

            openConnection();
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT authors.authorId, authors.name,authors.surname FROM authors", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    authors author = new authors();
                    author.authorId = Convert.ToInt32(reader["authorId"]);
                    author.name = reader["name"].ToString();
                    author.surname = reader["surname"].ToString();

                    authors.Add(author);
                }
            }

            return authors;
        }

        public List<books> getSearched(string name, string types, string authors)
        {
            List<books> searched = new List<books>();

            openConnection();
            string checkType = getType().FirstOrDefault(x => x.name == types).name;
            string checkAuthor = getAuthors().FirstOrDefault(x => x.surname == authors).surname;
            SqlCommand cmd = new SqlCommand("select books.bookId, books.name, books.pagecount, books.point, authors.surname ,types.name AS typeName " + "\n" +
                "from books, types, authors " + "\n" +
                "where books.name LIKE '" + name + "%' and types.name LIKE'" + types + "%' and authors.surname LIKE '" + authors + "%'", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    books found = new books();
                    found.bookId = Convert.ToInt32(reader["bookId"]);
                    found.name = reader["name"].ToString();
                    found.pagecount = Convert.ToInt32(reader["pagecount"]);
                    found.point = Convert.ToInt32(reader["point"]);
                    found.typeId = checkType;
                    found.authorId = checkAuthor;

                    searched.Add(found);
                }
            }
            closeConnection();
            return searched;
        }

        public List<borrows> getBybookId(int id)
        {
            //Uses books table only still needs things from other tables
            List<borrows> borrowsTable = new List<borrows>();

            openConnection();
            SqlCommand cmd = new SqlCommand("select borrows.borrowId, borrows.bookId ,borrows.takenDate, borrows.broughtDate, students.name, students.surname" + "\n" +
                "from borrows, students" + "\n" +
                "where students.studentId = borrows.studentId and borrows.bookId = " + id, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    borrows vmBooks = new borrows();
                    vmBooks.borrowId = Convert.ToInt32(reader["borrowId"]);
                    vmBooks.studentId = reader["name"].ToString() + " " + reader["surname"].ToString();
                    vmBooks.takenDate = Convert.ToDateTime(reader["takenDate"]);
                    if (reader["broughtDate"] != DBNull.Value)
                    {
                        vmBooks.broughtDate = Convert.ToDateTime(reader["broughtDate"]);
                    }

                    vmBooks.bookId = Convert.ToInt32(reader["bookId"]);

                    borrowsTable.Add(vmBooks);
                }
            }
            closeConnection();

            return borrowsTable;
        }

        public borrows getUserId(int id)
        {

            borrows bookFound = new borrows();

            openConnection();
            SqlCommand cmd = new SqlCommand("select DISTINCT books.name from borrows, books" + "\n" +
                "where books.bookId = " + id, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    bookFound.borrowName = reader["name"].ToString();
                }
            }

            closeConnection();

            return bookFound;
        }

        public int getNumberofBooks(int id)
        {
            int counter = 0;
            openConnection();
            SqlCommand cmd = new SqlCommand("select count(*) AS books from borrows" + "\n" +
                "where borrows.bookId = " + id, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    counter = Convert.ToInt32(reader["books"]);
                }
            }
            return counter;
        }
    }
}