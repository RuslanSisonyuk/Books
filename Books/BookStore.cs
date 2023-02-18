using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Books
{
    class Utils
    {
        static private void Print_exception(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.ReadLine();
            System.Environment.Exit(1);
        }
        static public List<Dictionary<string, string>> Deserialize_books_json(string filename)
        {
            string json_string = File.ReadAllText(@"Contents\" + filename + ".json");
            return JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json_string);
        }
        static public List<Book> Get_all_books(string filename)
        {
            List<Book> books = new List<Book>();
            try
            {
                var json_list = Deserialize_books_json(filename);
                foreach (var dict in json_list)
                {
                    Book book = new Book(dict["name"], Int32.Parse(dict["pages"]), dict["author"], dict["publisher"], dict["content"]);
                    books.Add(book);
                }
            }
            catch (FileNotFoundException)
            {
                Print_exception("File not found");
            }
            catch(Exception ex)
            {
                Print_exception(ex.Message);
            }
            return books;
        }
    }
    class BookStore
    {
        static public void Return_to_menu(List<Book> books)
        {
            Console.WriteLine("Press esc to return to main screen");
            while (!(Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape))
            { }
            Book_menu(books);
        }
        static public void exception_page(Exception exception, List<Book> books)
        {
            Console.Clear();
            Console.WriteLine("Caught an exception: " + exception);
            Return_to_menu(books);
        }
        static public void Read_book(List<Book> books, int book_id)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(books[book_id].Contents);
            Console.WriteLine("-------------------------------------------");
            Return_to_menu(books);
        }
        static public void Modify_book(List<Book> books, int book_id)
        {
            Return_to_menu(books);
        }
        static public void Delete_book(List<Book> books, int book_id)
        {
            Return_to_menu(books);
        }
        static public void Route_to_option(List<Book> books, int book_id, int option)
        {
            Console.Clear();
            switch(option)
            {
                case 1: Read_book(books, book_id); break;
                case 2: Modify_book(books, book_id); break;
                case 3: Delete_book(books, book_id); break;
                default: break;
            }
        }
        static public void Book_menu(List<Book> books)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------");
            int i = 1;
            foreach (var book in books) 
            { 
                Console.WriteLine(i + " | " + book.Print_book_info()); i++; 
            }
            try
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Select the book by its number");
                int book_id = Int32.Parse(Console.ReadLine());
                if (book_id > books.Count || book_id < 1) { Book_menu(books); return; }
                Console.WriteLine("Select:   1-Read book  |  2-Change its information  |  3-Delete book");
                int option = Int32.Parse(Console.ReadLine());
                if (option > 3 || option < 0) { Book_menu(books); return; }
                Route_to_option(books, book_id-1, option);
            }
            catch(FormatException exception)
            {
                exception_page(exception, books);
            }
        }

        static void Main(string[] args)
        {
            List<Book> books = Utils.Get_all_books("books");
            Book_menu(books);
        }
    }
}
