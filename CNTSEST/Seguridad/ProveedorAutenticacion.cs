using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CNTS.Models;
using CNTS.Utilidades;

namespace CNTS.Seguridad
{
    public class ProveedorAutenticacion : MembershipProvider
    {
        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var db = new CNTSEntities())
            {
                var user = db.c_establecimiento.FirstOrDefault(o => o.codigo_establecimiento == username);
                return user == null ? null : new UsuarioMembership(user);
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            using (var db = new CNTSEntities())
            {
                var pass = SeguridadUtilidades.SHA256Encripta(password);
                if (db.c_establecimiento.Any(o => o.password == pass && o.codigo_establecimiento == username))
                {
                    var user = db.c_establecimiento.Where(u => u.codigo_establecimiento == username).First();
                    var UltimoAcceso = user.fe_ultimo_acceso ?? DateTime.Now;
                    HttpContext.Current.Application["UltimoAcceso"] = UltimoAcceso.ToString();
                    //modificar fecha de ultimo ingreso cuando un usuario entre al sistema
                    user.fe_ultimo_acceso = DateTime.Now;
                    db.SaveChanges();

                    //si el usuario esta bloqueado, retornar false
                    if (user.id_estatus_establecimiento == 4) return false;

                    //Verificar si la (fecha actual - la fecha del ultimo acceso) > BloqueoNoIngreso
                    double segundosBNI = Utilidades.Utilidades.SegundosBloqueoNoIngreso();
                    if(segundosBNI > -1)
                    {
                        bool bloqueo = -((DateTime)user.fe_ultimo_acceso).Subtract(UltimoAcceso).TotalSeconds + segundosBNI <= 0;
                        //en caso de que el tiempo de desconexion sea mayor al tiempo de bloqueo por no ingreso
                        //El usuario pasara a estar inactivo y se mostrara un mensaje que le informará que debe comunicarse
                        //Con el administrador
                        if (bloqueo)
                        {
                            user.esta_activo = false;
                            user.id_estatus_establecimiento = 4;
                            db.SaveChanges();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }
    }
}