using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Books
{
    class BookStore
    {
        //menu
        //

        //list books
        //access info about the book
        //read book
        //sort books:
        //-by name
        //-by category
        static public List<Dictionary<string, string>> Deserialize_books_json(string filename)
        {
            string json_string = File.ReadAllText(@"Contents\"+filename+".json");
            return JsonSerializer.Deserialize<List<Dictionary<string,string>>>(json_string);
        }
        static public List<Book> Get_all_books(string filename)
        {
            var json_list = Deserialize_books_json(filename);
            List<Book> books = new List<Book>();
            foreach(var dict in json_list)
            {
                Book book = new Book(Int32.Parse(dict["pages"]),dict["author"],dict["publisher"],dict["content"]);
                books.Add(book);
            }
            return books;
        }

         
        static void Main(string[] args)
        {
            List<Book> new_list = Get_all_books("books");
            Console.WriteLine(new_list[0].get_author());
        }
    }
}
