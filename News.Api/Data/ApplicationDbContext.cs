using Microsoft.EntityFrameworkCore;
using News.Api.Models;
using System;

namespace News.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Konstruktor klasy, w któym występuje pole z o nazwie "options", dzięki temu zyskujemy do większej liczby metod jakie oferuje
        /*
         Jakie oferuje ten framework, nie jest wymogiem koniecznym aby generować konstruktor z polem "DbConetextOptions", bez tego klasa też bęzie działała. Natomiast dzięki temu zyskamy dostępm do większej liczby klas i rozszeszy to funkcjonalność naszego systemu.
        
         */
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        /*
         Poniższa linijka kodu jest odpowiedzialna za dodawanie rekordów do bazy danych. Po odwołaniu się do właściwości "Messages" zyskujemy dostęp do metod, które odpowiadają za dodawanie rekorów do bazy.
        
         */
        public DbSet<Message> Messages { get; set; }


        /*
         Za pomocą tej metody możemy konfigurować naszą bazę danych i na przykład tak jak w tym przypadku
        dodawać nowe wpisy do bazy tuż po uruchomieniu programu.
        Jak widać poniżej metoda ta zawiera inną metodą "SeedSampleData ()", która odpowiada, za tworzenie wpisów w bazie.
        Jest to możliwe dzięki Klasie ModelBuilder, która zawiera takie metody jak między innymi "Entity <>" oraz "HasData ()".
        Działa to tak, że w tej pierwszej metodzie (Entity <>) wpisujemy naszą klasę modelową czyli w tym przypadku Message i wywołujemy kolejną metode (HasData())
        i wprowadzamy w niej przykłądowe dane, jakie chcemy aby się znalazły w naszej bazie.
        Robi się to po to aby z góry zobaczyć jak ogólnie system działa na przykładowych danych. Wpisy można także wprawadzać ręcznie z poziomy przeglądarki za pomocą form edycyjnych, które są umieszczone w kodzie Html.
        Między innymi takie formatki znajdują się w komponencie "CreatePost.razor", który odpowiada za dodawanie wpisów do bazy ręcznie.
         */
        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedSampleData(builder);
        }

        /*
         Osadzenie przykładowych danych w bazie.
        Ilość danych może być większa lub mniejsza ilość w zależnośći od potrzeb. Na potrzeby tego projektu zostały wykorzystane 6 wpisów.
        Jak widać Klasa "Message" posiada cztery właściwości (Id, Description, Title, DataDodania) do któych są poprzypisywane dane.
         */
        private void SeedSampleData (ModelBuilder builder)
        {
            builder.Entity<Message>()
                .HasData(new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "This is a list of online newspaper archives and some magazines and journals, including both free and pay wall blocked digital archives. Most are scanned from microfilm into pdf, gif or similar graphic formats and many of the graphic archives have been indexed into searchable text dders.",
                    Title = "Wiadomości – program informacyjny",
                    DataDodania = DateTime.Now.ToShortDateString()
                });

            builder.Entity<Message>()
                .HasData(new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings f  in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.",
                    Title = "This is a list of online newspaper archives",
                    DataDodania = DateTime.Now.ToShortDateString()
                });

            builder.Entity<Message>()
                .HasData(new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = " s interested in the teachings from the field. Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field. Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field. Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field. ",
                    Title = "Archives and some magazines and journals",
                    DataDodania = DateTime.Now.ToShortDateString()
                });

            builder.Entity<Message>()
                .HasData(new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "Provost committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.",
                    Title = "Including both free and pay wall blocked digital",
                    DataDodania = DateTime.Now.ToShortDateString()
                });

            builder.Entity<Message>()
                .HasData(new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "Provost  m that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.",
                    Title = " Most are scanned from microfilm into pdf",
                    DataDodania = DateTime.Now.ToShortDateString()
                });

            builder.Entity<Message>()
                .HasData(new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = "Though  ture.Though the degree will not enable students to take the state bar exam and practice law, it will pave the way to law-adjoining careers or prepare students for a juris doctor in the future.Though the degree will not enable students to take the state bar exam and practice law, it will pave the way to law-adjoining careers or prepare students for a juris doctor in the future.",
                    Title = "Searchable text databases utilizing",
                    DataDodania = DateTime.Now.ToShortDateString()
                });
        }

    }
}
