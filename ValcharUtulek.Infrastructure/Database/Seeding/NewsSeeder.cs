using ValcharUtulek.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ValcharUtulek.Infrastructure.Database.Seeding
{
    public class NewsSeeder
    {
        public List<News> GetNews()
        {
            return new List<News>
            {
                new News { Title = "Oslava 1 výročí od založení", Content = "Místní zvířecí útulek včera s dojetím oslavil své první " +
                "výročí, během kterého se za uplynulý rok podařilo najít nový domov pro desítky opuštěných psů a koček." +
                " Oslavy se zúčastnila široká veřejnost i adoptivní rodiny, které přišly poděkovat za své čtyřnohé parťáky a " +
                "finančně podpořit další fungování zařízení. Tento úspěšný rok je pro nás obrovskou motivací a důkazem, že společnými " +
                "silami dokážeme měnit zvířecí osudy k lepšímu.", DateAdded = DateOnly.FromDateTime(DateTime.UtcNow), AuthorId = 1, Photo = "celebrationNews.jpg" },
                new News { Title = "Nové vybavéní pro zvířátka", Content = "Útulek se nyní pyšní novými vyhřívanými pelíšky a moderními " +
                "prolézačkami, které jsme mohli pořídit díky štědrým dárcům. Zvířata si tak při čekání na nový domov užívají mnohem většího " +
                "komfortu a zábavy.", DateAdded = DateOnly.FromDateTime(DateTime.UtcNow), AuthorId = 2, Photo = "newEquipment.jpg" },
                new News { Title = "Přivítejte nového zaměstnance", Content = "S radostí vítáme v našem týmu novou ošetřovatelku, " +
                "která k nám přichází s bohatými zkušenostmi a obrovským srdcem pro zvířata. Díky její pomoci se budeme moci každému " +
                "chlupáčovi věnovat ještě individuálněji a zajistit mu tu nejlepší možnou péči.", DateAdded = DateOnly.FromDateTime(DateTime.UtcNow), AuthorId = 1, Photo = "newEmployee.jpg" }
            };
        }
    }
}
