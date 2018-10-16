﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using CNTS.Models;
using CNTS.Seguridad;

namespace CNTS.Utilidades
{
    public class SeguridadUtilidades
    {
        public static String GetSha1(String texto)
        {
            var sha = SHA1.Create();
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] datos;
            StringBuilder builder = new StringBuilder();
            datos = sha.ComputeHash(encoding.GetBytes(texto));
            for (int i = 0; i < datos.Length; i++)
            {
                builder.AppendFormat("{0:x2}", datos[i]);
            }

            return builder.ToString();
        }

        public static string SHA256Encripta(string input)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }

        //public string[] ObtenerFunciones()
        //{
        //    CNTSEntities db = new CNTSEntities();
        //    IdentityPersonalizado Ident = (IdentityPersonalizado)HttpContext.Current.User.Identity;
        //    if (Ident == null)
        //    {
        //        return null;
        //    }

        //    int id_usuario = Ident.Id_establecimiento;

        //    //Encontrar los roles que tiene el usuario
        //    string sql = "select id_rol from c_rol_usuario where id_usuario = " + id_usuario;
        //    var roles = db.Database.SqlQuery<int>(sql).ToArray();

        //    //Encontrar cada funcion que tienen los roles
        //    int[] funciones = new int[] { };
        //    IEnumerable<int> union = funciones;
        //    foreach (int rol in roles)
        //    {
        //        sql= "select id_funcion from c_funcion_rol where id_rol = " + rol;
        //        var fns = db.Database.SqlQuery<int>(sql).ToArray();
        //        union = union.Union(fns);
        //    }

        //    //Encontrar el nombre a partir de cada id 

        //    funciones = union.ToArray();
        //    int nfunciones = funciones.Length;
        //    string[] resultado = new string[nfunciones];
        //    for (int i = 0; i < nfunciones; i++)
        //    {
        //        c_funcion c_funcion = db.c_funcion.Find(funciones[i]);
        //        resultado[i] = c_funcion.cl_funcion;
        //    }
        //    return resultado;
        //}

        //public string[] ObtenerFunciones(int id)
        //{
        //    CNTSEntities db = new CNTSEntities();

        //    int id_usuario = id;

        //    //Encontrar los roles que tiene el usuario
        //    string sql = "select id_rol from c_rol_usuario where id_usuario = " + id_usuario;
        //    var roles = db.Database.SqlQuery<int>(sql).ToArray();

        //    //Encontrar cada funcion que tienen los roles
        //    int[] funciones = new int[] { };
        //    IEnumerable<int> union = funciones;
        //    foreach (int rol in roles)
        //    {
        //        sql = "select id_funcion from c_funcion_rol where id_rol = " + rol;
        //        var fns = db.Database.SqlQuery<int>(sql).ToArray();
        //        union = union.Union(fns);
        //    }

        //    //Encontrar el nombre a partir de cada id 

        //    funciones = union.ToArray();
        //    int nfunciones = funciones.Length;
        //    string[] resultado = new string[nfunciones];
        //    for (int i = 0; i < nfunciones; i++)
        //    {
        //        c_funcion c_funcion = db.c_funcion.Find(funciones[i]);
        //        resultado[i] = c_funcion.cl_funcion;
        //    }
        //    return resultado;
        //}

        public int IdFuncion(string _funcion)
        {
            CNTSEntities db = new CNTSEntities();
            c_funcion funcion = db.c_funcion.Where(f => f.cl_funcion == _funcion).First();
            return funcion.id_funcion;
        }
    }
}