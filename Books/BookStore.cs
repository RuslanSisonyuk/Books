using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Books
{
    class Utils
    {
        static public List<Dictionary<string, string>> Deserialize_books_json(string filename)
        {
            string json_string = File.ReadAllText(@"Contents\" + filename + ".json");
            return JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json_string);
        }
        static public List<Book> Get_all_books(string filename)
        {
            var json_list = Deserialize_books_json(filename);
            List<Book> books = new List<Book>();
            foreach (var dict in json_list)
            {
                Book book = new Book(dict["name"], Int32.Parse(dict["pages"]), dict["author"], dict["publisher"], dict["content"]);
                books.Add(book);
            }
            return books;
        }

        
    }
    class BookStore
    {
        //menu
        //read book
        //sort books:
        //-by name
        //-by category

        static public void exception_page(int exception, List<Book> books)
        {
            Console.Clear();
            Console.WriteLine("Caught an exception: " + exception);
            Console.WriteLine("Press 1 to return to main screen");
            while (!(Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape))
            {}
            Book_menu(books);   
        }
        static public void Route_to_option(List<Book> books, int book_id, int option)
        {

        }
        static public void Book_menu(List<Book> books)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------");
            int i = 1;
            foreach (var book in books)
            {
                Console.WriteLine(i + " | " + book.Print_book_info());
                i++;
            }
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Select the book by its number");
            var selected_book = Console.ReadLine();
            try
            {
                int sb = Int32.Parse(selected_book);
            }
            catch
            {

            }
            Console.WriteLine("Select:   1-Read book  |  2-Change its information  |  3-Delete book");
            var option = Console.ReadLine();
            Route_to_option(books, Int32.Parse(selected_book), Int32.Parse(option));
        }

        static void Main(string[] args)
        {
            List<Book> books = Utils.Get_all_books("books");
            exception_page(1, books);
            //Book_menu(books);
        }
    }
}
