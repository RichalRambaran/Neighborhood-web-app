using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Buurtapp.ViewModels
{
    public class GepagineerdeList<T> : List<T>
    {
        public int Pagina { get; private set; }

        public int PaginaAantal { get; private set; }
        public GepagineerdeList(List<T> lijstDeel, int totaalAantal, int pagina, int perPagina)
        {
            Pagina = pagina;
            PaginaAantal = (int)Math.Ceiling(totaalAantal / (double)perPagina);
            this.AddRange(lijstDeel);
        }
        public bool HeeftVorige() { return Pagina > 0; }
        public bool HeeftVolgende() { return Pagina < PaginaAantal - 1; }

        public static async Task<GepagineerdeList<T>> CreateAsync(IQueryable<T> lijst, int pagina, int perPagina)
        {
            return new GepagineerdeList<T>(
                await lijst.Skip(pagina * perPagina).Take(perPagina).ToListAsync(),
                await lijst.CountAsync(),
                pagina,
                perPagina);
        }
    }
}
