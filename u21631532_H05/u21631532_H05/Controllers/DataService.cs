using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using u21686875HW05.Models;

namespace u21686875HW05.Services
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

            stringBuilder["Data Source"] = "LAPTOP-HB0SEDTT\\SQLEXPRESS";
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
            if(conn != null)
            {
                conn.Close();
            }
            return true;
        }

        public List<Types> getType()
        {
            List<Types> bookType = new List<Types>();

            openConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM types", conn);
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Types types = new Types();
                    types.typeId = Convert.ToInt32(reader["typeId"]);
                    types.name = reader["name"].ToString();

                    bookType.Add(types);
                }
            }

            return bookType;
        }


        public List<BookInfo> getBooks()
        {

            //Uses books table only still needs things from other tables
            List<BookInfo> books = new List<BookInfo>();
            Types types = new Types();

            openConnection();
            SqlCommand cmd = new SqlCommand("select books.bookId, books.name, books.pagecount, books.point, authors.surname ,types.name AS typeName" + "\n" +
                "from books, types, authors" + "\n" +
                "where books.typeId = types.typeId and authors.authorId = books.authorId", conn);
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM types", conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    BookInfo vmBooks = new BookInfo();
                    vmBooks.BookID = Convert.ToInt32(reader["bookId"]);
                    vmBooks.BookName = reader["name"].ToString();
                    vmBooks.PageCount = Convert.ToInt32(reader["pagecount"]);
                    vmBooks.Points = Convert.ToInt32(reader["point"]);
                    
                    vmBooks.Type = reader["typeName"].ToString();
                    vmBooks.Author = Convert.ToString(reader["surname"]);

                    books.Add(vmBooks);
                }
            }
            closeConnection();

            return books;
        }

        

        public List<BookTrades> getBDetails()
        {
            //Uses books table only still needs things from other tables
            List<BookTrades> borrowsTable = new List<BookTrades>();

            openConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM borrows", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    BookTrades vmBooks = new BookTrades();
                    vmBooks.borrowID = Convert.ToInt32(reader["borrowId"]);
                    vmBooks.StudentID = Convert.ToInt32(reader["studentId"]);
                    vmBooks.TakenDate = Convert.ToDateTime(reader["takenDate"]);
                    vmBooks.BroughtDate = Convert.ToDateTime(reader["broughtDate"]);
                    vmBooks.bookID = Convert.ToInt32(reader["bookId"]);

                    borrowsTable.Add(vmBooks);
                }
            }
            closeConnection();

            return borrowsTable;
        }

        public List<StudentsModel> getStudents()
        {
            //Uses books table only still needs things from other tables
            List<StudentsModel> stu = new List<StudentsModel>();

            openConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM students", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    StudentsModel vmBooks = new StudentsModel();
                    vmBooks.studentId = Convert.ToInt32(reader["studentId"]);
                    vmBooks.name = Convert.ToString(reader["name"]);
                    vmBooks.surname = Convert.ToString(reader["surname"]);
                    vmBooks.Class = Convert.ToInt32(reader["class"]);
                    vmBooks.points = Convert.ToInt32(reader["points"]);

                    stu.Add(vmBooks);
                }
            }
            closeConnection();

            return stu;
        }
    }
}