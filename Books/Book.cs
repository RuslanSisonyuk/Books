using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Books
{
    class Book
    {
        protected int number_of_pages;
        protected string name;
        protected string author;
        protected string contents;
        protected string publisher;
        
        public Book()
        {
            this.Pages = 0;
            this.name = "none";
            this.author = "none";
            this.publisher = "none";
            this.contents = "none";
        }
        public Book(string name, int pages, string auth, string publisher, string contents)
        {
            this.Name = name;
            this.Pages = pages;
            this.Author = auth;
            this.Publisher = publisher;
            this.Contents = contents;
        }
        public string Name
        {
            get { return name; }
            set
            {
                string r = @"^\w+\s*\w*\s*\w*$";
                name = Regex.IsMatch(value, r) ? value : "error";
            }
        }
        public int Pages
        {
            get { return number_of_pages; }
            //give an error when this happens?
            set { number_of_pages = value < 0 ? 0 : value; }
        }
        public string Author 
        {
            get { return author; }
            set {
                string r = @"^\w+\.*\s+\w*$";
                author = Regex.IsMatch(value,r) ? value : "error";
            }
        }
        public string Contents
        {
            get { return contents; }
            set{ contents = value.Length > 1000 ? "Given text is too long" : value; }
        }
        public string Publisher
        {
            get { return publisher; }
            set {
                string r = @"^\w+\s*\w*$";
                publisher = Regex.IsMatch(value, r) ? value : "error";
            }
        }

        public string Print_book_info()
        {
            return name + " | " + author + " | " + publisher + " | " + number_of_pages;
        }
    }

    class Comic : Book
    {
        protected int panels_per_page;
        public Comic(string name, int panels, int pages, string auth, string contents, string publisher) : base(name, pages,auth,contents,publisher)
        {
            this.Panels_per_page = panels;
        }
        private int Panels_per_page
        {
            get { return panels_per_page; }
            set {
                if (panels_per_page > 10) panels_per_page = 10;
                else if (panels_per_page <= 0) panels_per_page = 1;
            }
        }
    }

    class Newspaper : Book
    {
        public Newspaper(string name, int pages, string auth, string contents, string publisher) : base(name, pages, auth, contents, publisher) 
        { }
        public void Use_as_intended()
        {
            string[] str = System.IO.File.ReadAllLines("/Contents/quotes.txt");
            List<string> quotes = new List<string>(str);
            var random = new Random();
            int quote_id = random.Next(quotes.Count);
            Console.WriteLine(quotes[quote_id]);
        }
    }
}
