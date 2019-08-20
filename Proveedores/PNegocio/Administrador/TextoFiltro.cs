using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNegocio.Administrador
{
    public class TextoFiltro
    {
        

        public static string textoTablaFiltro() {
         return "<table class='filtro'>" +
                "<tr>" +
                "<td>Filtrar...</td>" +
                "<td><input id='searchTerm' type='text'></td>" + // "<td><input id='searchTerm' type='text' onkeyup='doSearch()' /></td>" +
                "</tr>" +
                "</table>";
        }

        public static string textoTablaFiltroSort()
        {
            return "<table class='filtro'>" +
                   "<tr>" +
                   "<td>Filtrar...</td>" +
                   "<td><input id='searchTermSort' type='text'/></td>" + // "<td><input id='searchTerm' type='text' onkeyup='doSearchSorter()' /></td>" +
                   "</tr>" +
                   "</table>";
        }

        public static string textoTablaFiltroSortMostrarNoticias()
        {
            return "<table class='filtro'>" +
                   "<tr>" +
                   "<td>Filtrar...</td>" +
                   "<td><input id='searchTermNoticia' type='text'/></td>" + //  "<td><input id='searchTermNoticia' type='text' onkeyup='doSearchSorterMostrarNoticia()' /></td>" +
                   "</tr>" +
                   "</table>";
        }
    }
}
