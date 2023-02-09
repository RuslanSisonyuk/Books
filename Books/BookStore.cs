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
        List<Book> Get_all_books(string filename)
        {
            
            string json_string = File.ReadAllText(filename+".json");
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(json_string);
            return books;
        }


        static void Main(string[] args)
        {
               //read the books info from json? file
               
        }
    }
}
