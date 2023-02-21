using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Books
{
    class Utils
    {
        static public void Print_exception(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.ReadLine();
            System.Environment.Exit(1);
        }
        static public List<Dictionary<string, string>> Deserialize_json(string filename)
        {
            string json_string = File.ReadAllText(@"Contents\" + filename + ".json");
            return JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json_string);
        }
    }
    class BookStore
    {
        static public List<Book> Create_book_list(string filename)
        {
            List<Book> books = new List<Book>();
            try
            {
                var json_list = Utils.Deserialize_json(filename);
                foreach (var dict in json_list)
                {
                    Book book = new Book(dict["name"], Int32.Parse(dict["pages"]), dict["author"], dict["publisher"], dict["content"]);
                    books.Add(book);
                }
            }
            catch (FileNotFoundException)
            {
                Utils.Print_exception("File not found");
            }
            catch (FormatException ex)
            {
                Utils.Print_exception(ex.Message);
            }
            return books;
        }
        static public void Return_to_menu(List<Book> books)
        {
            Console.WriteLine("Press esc to return to main screen");
            while (!(Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Escape))
            { }
            Book_menu(books);
        }
        static public void Exception_page(Exception exception, List<Book> books)
        {
            Console.Clear();
            Console.WriteLine("Caught an exception: " + exception);
            Return_to_menu(books);
        }
        static public void Read_book_from_list(List<Book> books, int book_id)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(books[book_id].Contents);
            Console.WriteLine("-------------------------------------------");
            Return_to_menu(books);
        }
        static public void Modify_book_from_list(List<Book> books, int book_id, int option)
        {
            string input = Console.ReadLine();
            try
            {
                switch (option)
                {
                    case 1: books[book_id].Name = input; break;
                    case 2: books[book_id].Publisher = input; break;
                    case 3: books[book_id].Author = input; break;
                    case 4: books[book_id].Pages = Int32.Parse(input); break;
                }
            }
            catch(FormatException ex)
            {
                Exception_page(ex, books);
            }
            Return_to_menu(books);
        }
        static public void Select_book_to_modify(List<Book> books, int book_id)
        {
            Console.WriteLine("Select part to modify: ");
            Console.WriteLine("1-Name  2-Publisher  3-Author  4-Number of Pages");
            int option = new int();
            try
            {
                option = Int32.Parse(Console.ReadLine());
            }
            catch(FormatException ex)
            {
                Exception_page(ex, books);
                return;
            }
            Modify_book_from_list(books, book_id, option);
        }
        static public void Delete_book_from_list(List<Book> books, int book_id)
        {
            books.RemoveAt(book_id);
            Return_to_menu(books);
        }
        static public void Route_to_option(List<Book> books, int book_id, int option)
        {
            Console.Clear();
            switch(option)
            {
                case 1: Read_book_from_list(books, book_id); break;
                case 2: Select_book_to_modify(books, book_id); break;
                case 3: Delete_book_from_list(books, book_id); break;
                default: break;
            }
        }
        static public bool Is_int_in_range(int input, int range_min, int range_max)
        {
            if (input > range_max || input < range_min)
            {
                return false;
            }
            return true;
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

                if (book_id > books.Count || book_id < 1) { 
                    Book_menu(books);
                    return;
                }

                Console.WriteLine("Select:   1-Read book  |  2-Change its information  |  3-Delete book");

                int option = Int32.Parse(Console.ReadLine());

                if (option > 3 || option < 0) { 
                    Book_menu(books); 
                    return; 
                }

                Route_to_option(books, book_id-1, option);
            }
            catch(FormatException exception)
            {
                Exception_page(exception, books);
            }
        }

        static void Main(string[] args)
        {
            List<Book> books = Create_book_list("books");
            Book_menu(books);
        }
    }
}
