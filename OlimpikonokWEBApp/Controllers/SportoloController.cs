using Microsoft.EntityFrameworkCore;
using OlimpikonokWEBApp.DTOs;
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
                    Sportolo uj = new Sportolo() { 
                        Id = 0,
                        Nev = "Hiba az adatbázis elérésekor! "+ex.Message
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
                        Nev = "Hiba az adatbázis elérésekor! " + ex.Message
                    };
                    return hiba;
                }
            }
        }

        public SportoloDTO GetSportoloDTOById(int id)
        {
            using (var context = new OlimpikonokContext())
            {
                try
                {
                    Sportolo sportolo = context.Sportolos.FirstOrDefault(s => s.Id == id);
                    SportoloDTO sportoloDTO = new SportoloDTO()
                    {
                        Id = sportolo.Id,
                        Nev = sportolo.Nev,
                        Kep = sportolo.Kep
                    };
                    return sportoloDTO;
                }
                catch (Exception ex)
                {
                    SportoloDTO hiba = new SportoloDTO()
                    {
                        Id = 0,
                        Nev = "Hiba az adatbázis elérésekor! " + ex.Message
                    };
                    return hiba;
                }
            }
        }

        public string PutSportolo(Sportolo modositSportolo)
        {
            using (var context = new OlimpikonokContext())
            {
                try
                {
                    if (modositSportolo != null)
                    { //kaptam módosítandó adatokat
                        var letezo = context.Sportolos.FirstOrDefault(s => s.Id == modositSportolo.Id);
                        if (context.Sportolos.Contains(modositSportolo))
                        {
                            letezo.Nev = modositSportolo.Nev;
                            letezo.Neme = modositSportolo.Neme;
                            letezo.Ermek = modositSportolo.Ermek;                            
                            letezo.SzulDatum = modositSportolo.SzulDatum;
                            //letezo.IndexKep = modositSportolo.IndexKep;
                            //letezo.Kep = modositSportolo.Kep;
                            //context.Sportolos.Update(modositSportolo);
                            context.SaveChanges();//ennek hatására frissülnek az adatbázis adatai
                            return "Sikeresen módosítottuk az adatokat";
                        }
                        else
                        {
                            return $"Nincs ilyen sportoló! \n{modositSportolo.Id}";
                        }
                    }
                    else
                    { //nem kaptam semmilyen adatot
                        return "Üres objektumot kaptam, nem lehet módosítani!!!";
                    }
                }
                catch (Exception ex)
                {
                    return $"Nem sikerült a módosítás\n{ex.Message}";
                }
            }
        }
    }
}
