using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;

namespace CNTS.Utilidades
{
    public static class DropDown
    {
        static private CNTSEntities db = new CNTSEntities();

        #region Auxiliar
        public static List<SelectListItem> Muestra()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            for(int i = 0; i < 10; i++)
            {
                lista.Add(
                    new SelectListItem
                    {
                        Value = (i+1).ToString(),
                        Text = "Element" + string.Format("{0:00}",i+1)
                    }
                );
            }
            return lista;
        }
        #endregion

    }
}