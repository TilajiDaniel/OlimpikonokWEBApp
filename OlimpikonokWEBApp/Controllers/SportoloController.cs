using AspNetCore;
using Microsoft.EntityFrameworkCore;
using OlimpikonokWEBApp.Models;

namespace OlimpikonokWEBApp.Controllers
{
    public class SportoloController
    {
        public List<Sportolo> GetSportolok()
        {
            using (var context = new OlimpikonokContext())
            {
                try
                {
                    List<Sportolo> sportolok = context.Sportolos.Include(s => s.Sportag).ToList();
                    return sportolok;
                }
                catch (Exception ex)
                {
                    List<Sportolo> hiba = new List<Sportolo>();
                    Sportolo uj = new Sportolo()
                    {
                        Id = 0,
                        Nev = "Hiba az adatbázis elérésekor: " + ex.Message
                    };
                    return hiba;
                }
            }
        }
        public Sportolo GetSportoloById(int id)
        {
            using (var context = new OlimpikonokContext())
            {
                try
                {
                    Sportolo sportolo = context.Sportolos.FirstOrDefault(s => s.Id == id);
                    return sportolo;
                }
                catch (Exception ex)
                {
                    Sportolo hiba = new Sportolo()
                    {
                        Id = 0,
                        Nev = "Hiba az adatbázis elérésekor: " + ex.Message
                    };
                    return hiba;
                }
            }
        }
        public string PostSportolo(Sportolo modositSportolo)
        {
            using (var context = new OlimpikonokContext())
            {
                try
                {
                    if (modositSportolo != null)
                    {
                        Sportolo letezo = context.Sportolos.FirstOrDefault(s => s.Id == modositSportolo.Id);
                        if (letezo != null)
                        {
                            context.Sportolos.Update(modositSportolo);
                            return "Sikeresen módosítottuk az adatokat";
                        }
                        else
                        {
                            return "Nincs ilyen sporoló!";
                        }
                    }
                    else
                    {
                        return "Üres objektumot kaptam, nem lehet módosítani!!!";
                    }
                }
                catch (Exception ex)
                {
                    return $"Nem sikerult a modositas{ex.Message}";
                }

            }
        }
    }
}
