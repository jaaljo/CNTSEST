using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;
using CNTS.Seguridad;
using CNTS.ViewModels;

namespace CNTS.Utilidades
{
    public static class Utilidades
    {
        static private CNTSEntities db = new CNTSEntities();

        public static void ProtectConnectionString()
        {
            ToggleConnectionStringProtection
        //For Windows
        //(System.Windows.Forms.Application.ExecutablePath, true);
        //For Web
        (null, true);
        }

        public static void UnprotectConnectionString()
        {
            ToggleConnectionStringProtection
        //For Windows
        //(System.Windows.Forms.Application.ExecutablePath, false);
        //For Web
        (null, false);
        }

        private static void ToggleConnectionStringProtection
                (string pathName, bool protect)
        {
            // Define the Dpapi provider name.
            string strProvider = "DataProtectionConfigurationProvider";
            // string strProvider = "RSAProtectedConfigurationProvider";

            System.Configuration.Configuration oConfiguration = null;
            System.Configuration.ConnectionStringsSection oSection = null;

            try
            {
                // Open the configuration file and retrieve 
                // the connectionStrings section.

                // For Web!
                oConfiguration = System.Web.Configuration.
                                  WebConfigurationManager.OpenWebConfiguration("~");

                // For Windows!
                // Takes the executable file name without the config extension.
                //oConfiguration = System.Configuration.ConfigurationManager.
                //                                OpenExeConfiguration(pathName);

                if (oConfiguration != null)
                {
                    bool blnChanged = false;

                    oSection = oConfiguration.GetSection("connectionStrings") as
                System.Configuration.ConnectionStringsSection;

                    if (oSection != null)
                    {
                        if ((!(oSection.ElementInformation.IsLocked)) &&
                (!(oSection.SectionInformation.IsLocked)))
                        {
                            if (protect)
                            {
                                if (!(oSection.SectionInformation.IsProtected))
                                {
                                    blnChanged = true;

                                    // Encrypt the section.
                                    oSection.SectionInformation.ProtectSection
                                (strProvider);
                                    oConfiguration.AppSettings.Settings["Cypher"].Value = "true";
                                }
                            }
                            else
                            {
                                if (oSection.SectionInformation.IsProtected)
                                {
                                    blnChanged = true;

                                    // Remove encryption.
                                    oSection.SectionInformation.UnprotectSection();
                                    System.Configuration.ConnectionStringSettings connString = new ConnectionStringSettings();
                                    if (0 < oConfiguration.ConnectionStrings.ConnectionStrings.Count)
                                    {
                                        connString =
                                            oConfiguration.ConnectionStrings.ConnectionStrings["CNTSEntities"];
                                    }
                                    string con = connString.ConnectionString;
                                    int inicio = con.IndexOf("\"");
                                    int fin = con.IndexOf("\"", inicio + 1);
                                    string conectionString = con.Substring(inicio + 1, fin - inicio - 1);

                                }
                            }
                        }

                        if (blnChanged)
                        {
                            // Indicates whether the associated configuration section 
                            // will be saved even if it has not been modified.
                            oSection.SectionInformation.ForceSave = true;

                            // Save the current configuration.
                            oConfiguration.Save();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }


        public static string GetConnectionString()
        {
            string conectionString = HttpContext.Current.Application["ConnectionString"] == null ? null : HttpContext.Current.Application["ConnectionString"].ToString();
            if (conectionString != null) return conectionString;

            string path = HttpContext.Current.Server.MapPath("~/ccc.cfg");

            System.IO.FileStream sr = new
                    System.IO.FileStream( path, FileMode.Open, FileAccess.Read, FileShare.None);

            byte[] bytes = new byte[sr.Length];

            sr.Read(bytes, 0, (int)sr.Length);
            sr.Close();

            int I = 0;
            int II = 0;
            string data1 = "";
            string data2 = "";
            string data3 = "";
            string data4 = "";
            string data5 = "";

            byte[] bdata1;
            byte[] bdata2;
            byte[] bdata3;
            byte[] bdata4;
            byte[] bdata5;



            string ax1 = "data source";
            string ax2 = "initial catalog";
            string ax3 = "persist security info";
            string ax4 = "user id";
            string ax5 = "password";

            string cad1 = "";
            string cad2 = "";

            string cadena = System.Text.Encoding.Default.GetString(bytes);

            //obtener los datos cifrados de la cadena de conexion
            I = cadena.IndexOf(ax1);
            II = cadena.IndexOf(";"+ax2, I);
            cad1 = cadena.Substring(0, I);

            data1 = cadena.Substring(I + ax1.Length + 1, II - (I + ax1.Length + 1));

            I = cadena.IndexOf(ax2);
            II = cadena.IndexOf(";"+ax3, I);
            data2 = cadena.Substring(I + ax2.Length + 1, II - (I + ax2.Length + 1));

            I = cadena.IndexOf(ax3);
            II = cadena.IndexOf(";"+ax4, I);
            data3 = cadena.Substring(I + ax3.Length + 1, II - (I + ax3.Length + 1));

            I = cadena.IndexOf(ax4);
            II = cadena.IndexOf(";"+ax5, I);
            data4 = cadena.Substring(I + ax4.Length + 1, II - (I + ax4.Length + 1));

            I = cadena.IndexOf(ax5);
            II = cadena.IndexOf(";multipleactiveresultsets", I);
            data5 = cadena.Substring(I + ax5.Length + 1, II - (I + ax5.Length + 1));

            I = II + 1;
            II = cadena.Length - I;
            cad2 = cadena.Substring(I, II);

            //Desencriptar datos
            using (TripleDESCryptoServiceProvider myTripleDES = new TripleDESCryptoServiceProvider())
            {
                byte[] Key = { 115, 96, 94, 217, 148, 212, 105, 222, 20, 6, 167, 52, 243, 3, 153, 144, 123, 183, 121, 25, 217, 65, 132, 161 };
                byte[] IV = { 0, 87, 122, 7, 46, 77, 41, 94 };

                bdata1 = denormalize(data1);
                bdata2 = denormalize(data2);
                bdata3 = denormalize(data3);
                bdata4 = denormalize(data4);
                bdata5 = denormalize(data5);

                data1 = DecryptStringFromBytes(bdata1, Key, IV);
                data2 = DecryptStringFromBytes(bdata2, Key, IV);
                data3 = DecryptStringFromBytes(bdata3, Key, IV);
                data4 = DecryptStringFromBytes(bdata4, Key, IV);
                data5 = DecryptStringFromBytes(bdata5, Key, IV);
            }

            string cc = cad1 + ax1 + "=" + data1 + ";" + ax2 + "=" + data2 + ";" + ax3 + "=" + data3 + ";" + ax4 + "=" + data4 + ";" + ax5 + "=" + data5 + ";" + cad2;

            int inicio = cc.IndexOf("&quot;");
            int fin = cc.IndexOf("&quot;", inicio + 6);
            conectionString = cc.Substring(inicio + 6, fin - inicio - 6);
            HttpContext.Current.Application["ConnectionString"] = conectionString;
            return conectionString;
        }

        private static byte[] denormalize(string encMess)
        {
            string[] chars = encMess.Split(new Char[] { ',' });
            byte[] bytes = new byte[chars.Count()];

            for (int i = 0; i < chars.Count(); i++)
            {
                bytes[i] = (byte)Int16.Parse(chars[i]);
            }

            return bytes;
        }

        public static double SegundosTiempoCaducidad()
        {

            //separar la fecha en dia/mes/anio  y horas/minutos/segundos
            string[] FECHAYHORA = GetSecurityProp("TiempoCaducidad", "30/00/00 00:00:00").Split(new Char[] { ' ' });
            bool aplica = FECHAYHORA.Length == 2;

            int dias = 0;
            int meses = 0;
            int anios = 0;
            int horas = 0;
            int minutos = 0;
            int segundos = 0;

            double total_segundos = 0;

            if (aplica)
            {
                string[] DDMMAA = FECHAYHORA[0].Split(new Char[] { '/' });
                string[] HORA = FECHAYHORA[1].Split(new Char[] { ':' });
                //si ambas cadenas tienen 3 elementos [{dias,meses,anios}{horas,minutos,segundos}]
                if (DDMMAA.Length == 3 && HORA.Length == 3)
                {
                    dias = Int32.Parse(DDMMAA[0]);
                    /*meses = Int32.Parse(DDMMAA[1]);
                    anios = Int32.Parse(DDMMAA[2]);
                    horas = Int32.Parse(HORA[0]);
                    minutos = Int32.Parse(HORA[1]);
                    segundos = Int32.Parse(HORA[2]);*/

                    total_segundos = segundos
                        + (minutos * 60)
                        + (horas * 3600)
                        + (dias * 86400)
                        + (meses * 2592000)
                        + (anios * 31536000);

                    return total_segundos;

                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public static double SegundosBloqueoNoIngreso()
        {

            //separar la fecha en dia/mes/anio  y horas/minutos/segundos
            string[] FECHAYHORA = GetSecurityProp("BloqueoNoIngreso","30/00/00 00:00:00").Split(new Char[] { ' ' });
            bool aplica = FECHAYHORA.Length == 2;

            int dias = 0;
            int meses = 0;
            int anios = 0;
            int horas = 0;
            int minutos = 0;
            int segundos = 0;

            double total_segundos = 0;

            if (aplica)
            {
                string[] DDMMAA = FECHAYHORA[0].Split(new Char[] { '/' });
                string[] HORA = FECHAYHORA[1].Split(new Char[] { ':' });
                //si ambas cadenas tienen 3 elementos [{dias,meses,anios}{horas,minutos,segundos}]
                if (DDMMAA.Length == 3 && HORA.Length == 3)
                {
                    dias = Int32.Parse(DDMMAA[0]);
                    /*meses = Int32.Parse(DDMMAA[1]);
                    anios = Int32.Parse(DDMMAA[2]);
                    horas = Int32.Parse(HORA[0]);
                    minutos = Int32.Parse(HORA[1]);
                    segundos = Int32.Parse(HORA[2]);*/

                    total_segundos = segundos
                        + (minutos * 60)
                        + (horas * 3600)
                        + (dias * 86400)
                        + (meses * 2592000)
                        + (anios * 31536000);

                    return total_segundos;

                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an TripleDESCryptoServiceProvider object
            // with the specified key and IV.
            using (TripleDESCryptoServiceProvider tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = Key;
                tdsAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        public static string GetSecurityProp(string nombre,string defaultValue = "")
        {
            /*
            //Abrir web.config
            System.Configuration.Configuration wcf =
               System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Configuration.KeyValueConfigurationElement customSetting;
            string aux;

            try
            {
                customSetting = wcf.AppSettings.Settings[nombre];
                aux = customSetting.Value.ToString();
            }
            catch
            {
                aux = " ";
            }
            return aux;*/

            string aux;
            c_parametro parametro;

            try
            {
                parametro = db.c_parametro.Where(p => p.nb_parametro == nombre).First();
                aux = parametro.valor_parametro;
            }
            catch
            {
                parametro = new c_parametro();
                parametro.nb_parametro = nombre;
                parametro.valor_parametro = defaultValue;
                db.c_parametro.Add(parametro);
                db.SaveChanges();

                return defaultValue;
            }

            return aux;
        }

        //Intenta devolver un valor entero obtenido desde una cadena, en caso de error devuelve 0
        public static int GetIntSecurityProp(string nombre, string defaultValue = "")
        {
            string value = GetSecurityProp(nombre,defaultValue);
            int result;
            return Int32.TryParse(value, out result) ? result : 0;
        }

        //Intenta devolver un valor booleano obtenido desde una cadena, en caso de error devuelve false
        public static bool GetBoolSecurityProp(string nombre, string defaultValue = "")
        {
            string value = GetSecurityProp(nombre,defaultValue);
            return value.ToLower() == "true" ? true : false;
        }

        public static bool SetSecurityProp(string nombre,string value)
        {
            /*//Abrir web.config
            System.Configuration.Configuration wcf =
               System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Configuration.KeyValueConfigurationElement customSetting;

            try
            {
                customSetting = wcf.AppSettings.Settings[nombre];
                customSetting.Value = value;
                wcf.Save();
                return true;
            }
            catch
            {
                return false;
            }*/
            try
            {
                c_parametro parametro = db.c_parametro.Where(p => p.nb_parametro == nombre).First();
                parametro.valor_parametro = value;
                db.SaveChanges();
                return true;
            }
            catch
            {
                c_parametro parametro = new c_parametro();
                parametro.nb_parametro = nombre;
                parametro.valor_parametro = value;
                db.c_parametro.Add(parametro);
                db.SaveChanges();
                return false;
            }
        }
		
		public static DirectionViewModel getDirection(string direction)
        {
            string[] splitString = direction.Split(new Char[] { '/' });
            DirectionViewModel dir = new DirectionViewModel();
            dir.Controller = splitString[0];
            dir.Action = splitString[1];
            dir.Id = splitString[2];
            return dir;
        }
		
        public static IList<SelectListItem> TiposCampo()
        {
            IList<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Value = "t",Text = "Texto" });
            lista.Add(new SelectListItem { Value = "a", Text = "Área de Texto" });
            lista.Add(new SelectListItem { Value = "n", Text = "Numérico" });

            return lista;
        }

        public static IList<SelectListItem> ColoresMetaCampos()
        {
            IList<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem { Value = "0", Text = "Ninguno" });
            lista.Add(new SelectListItem { Value = "AliceBlue", Text = "Azul Celeste" });
            lista.Add(new SelectListItem { Value = "AntiqueWhite", Text = "Blanco Antiguo" });
            lista.Add(new SelectListItem { Value = "Aqua", Text = "Agua" });
            lista.Add(new SelectListItem { Value = "Aquamarine", Text = "Agua Marina" });
            lista.Add(new SelectListItem { Value = "Beige", Text = "Beige" });
            lista.Add(new SelectListItem { Value = "Black", Text = "Negro" });
            lista.Add(new SelectListItem { Value = "BlanchedAlmond", Text = "Almendra" });
            lista.Add(new SelectListItem { Value = "Blue", Text = "Azul" });
            lista.Add(new SelectListItem { Value = "BlueViolet", Text = "Violeta" });
            lista.Add(new SelectListItem { Value = "Brown", Text = "Café" });
            lista.Add(new SelectListItem { Value = "CadetBlue", Text = "Azul cadete" });
            lista.Add(new SelectListItem { Value = "Chocolate", Text = "Chocolate" });
            lista.Add(new SelectListItem { Value = "Coral", Text = "Coral" });
            lista.Add(new SelectListItem { Value = "CornflowerBlue", Text = "Azul Anciano" });
            lista.Add(new SelectListItem { Value = "Crimson", Text = "Carmesí" });
            lista.Add(new SelectListItem { Value = "Cyan", Text = "Cian" });
            lista.Add(new SelectListItem { Value = "DarkBlue", Text = "Azul Oscuro" });
            lista.Add(new SelectListItem { Value = "DarkCyan", Text = "Cian Oscuro" });
            lista.Add(new SelectListItem { Value = "DarkGoldenRod", Text = "Dorado Oscuro" });
            lista.Add(new SelectListItem { Value = "DarkGray", Text = "Gris Oscuro" });
            lista.Add(new SelectListItem { Value = "DarkGreen", Text = "Verde Oscuro" });
            lista.Add(new SelectListItem { Value = "DarkMagenta", Text = "Violeta Oscuro" });
            lista.Add(new SelectListItem { Value = "DarkOrange ", Text = "Naranja Oscuro" });
            lista.Add(new SelectListItem { Value = "DarkRed ", Text = "Rojo Oscuro" });
            lista.Add(new SelectListItem { Value = "DeepSkyBlue ", Text = "Azul cielo profundo" });
            lista.Add(new SelectListItem { Value = "Gray", Text = "Gris" });
            lista.Add(new SelectListItem { Value = "Gold ", Text = "Amarillo" });
            lista.Add(new SelectListItem { Value = "Green ", Text = "Verde" });
            lista.Add(new SelectListItem { Value = "GreenYellow ", Text = "Verde Limón" });
            lista.Add(new SelectListItem { Value = "Red", Text = "Rojo" });
            lista.Add(new SelectListItem { Value = "Salmon", Text = "Salmón" });
            lista.Add(new SelectListItem { Value = "White", Text = "Blanco" });

            
            return lista;
        }

        public static string ToUnicode(string input)
        {
            byte[] unibyte = Encoding.Unicode.GetBytes(input);
            string uniString = string.Empty;
            foreach (byte b in unibyte)
            {
                uniString += string.Format("{0}{1}", @"\u", b.ToString("X"));
            }
            return uniString;
        }


        public static string getLastConnection()
        {
            string lc;
            try
            {
                lc = HttpContext.Current.Application["UltimoAcceso"].ToString();
            }
            catch
            {
                lc = "--/--/----";
            }
            return lc;
        }

        public static void disposeRParam(string rCode,int id_sp)
        { 
            int val = int.Parse(rCode.Split(new char[] { '-' })[1]);
            string nb = id_sp.ToString() + "R";

            try
            {
                var parametro = db.c_parametro.Where(c => c.nb_parametro == nb && c.valor_parametro == val.ToString()).First();
                db.c_parametro.Remove(parametro);
                db.SaveChanges();
            }
            catch
            {

            }
            return;
        }

        public static void disposeCParam(string cCode, int id_sp)
        {
            int val = int.Parse(cCode.Split(new char[] { '-' })[1]);
            string nb = id_sp.ToString() + "C";

            try
            {
                var parametro = db.c_parametro.Where(c => c.nb_parametro == nb && c.valor_parametro == val.ToString()).First();
                db.c_parametro.Remove(parametro);
                db.SaveChanges();
            }
            catch
            {

            }
            return;
        }

        public static string GetDateFormat()
        {
            string lang = HttpContext.Current.Request.UserLanguages[0];
            if (lang.ToLower().Contains("en"))
            {
                return "MM/DD/YYYY";
            }
            else
            {
                return "DD/MM/YYYY";
            }
        }

        public static void EnviaCorreo(string email, string asunto, string contenido)
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = "mail.string.com.mx",
                Port = 587,
                Credentials = new NetworkCredential("notificaciones@string.com.mx", "S0luc10n3$"), EnableSsl = false
            };

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("notificaciones@string.com.mx", "Registro Nacional de Bancos de Sangre CNTS");
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Subject = asunto;
                mailMessage.Body = contenido;
                mailMessage.IsBodyHtml = true;
                smtp.Send(mailMessage);
            }
            catch
            {

            }
        }
    }
}