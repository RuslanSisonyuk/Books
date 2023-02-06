using System;
using System.Collections.Generic;
using System.Text;

namespace Books
{
    class Book
    {
        protected int number_of_pages;
        protected string author;
        protected string contents;
        protected string publisher;
        
        public Book(int pages, string auth, string contents, string publisher)
        {
            this.number_of_pages = pages;
            this.author = auth;
            this.publisher = publisher;
            this.contents = contents;
        }

        private int Pages
        {
            get { return number_of_pages; }
            //give an error when this happens?
            set { number_of_pages = number_of_pages < 0 ? 0 : number_of_pages; }
        }

        private string Author 
        {
            get { return author; }
            set { }
        }
    }

    class Comic : Book
    {
        protected int panels_per_page;

        public Comic(int panels, int pages, string auth, string contents, string publisher) : base(pages,auth,contents,publisher)
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
        public Newspaper(int pages, string auth, string contents, string publisher) : base(pages, auth, contents, publisher) 
        { }

        //rework to get the quotes from a file
        public void Use_as_intended()
        {
            List<string> quotes = new List<string>();
            quotes.Add("GET THIS YOU PESKY FLY!");
            quotes.Add("IT'S NO USE!");
            quotes.Add("Crap, I'm out of paper...");
            quotes.Add("*Reads about brain transplant* \n `Where did you get that brain...  The brain store?! ... \n " +
                "*The room falls in complete fucking silence while you, feeling everybody's judging gazes, reconsider your life choises*");
            quotes.Add("You've chosen your weapon well.");
            var random = new Random();
            int quote_id = random.Next(quotes.Count);
            Console.WriteLine(quotes[quote_id]);
        }
    }

}
