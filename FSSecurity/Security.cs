using System.Security.Principal;
using System.IO;
using System.Security.AccessControl;

namespace FSSecurity
{
    /// <summary>
    /// Clase con funciones para el manejo de la seguridad en ficheros y directorios.
    /// </summary>
    public class Security
    {
        public static bool HasAccessFolder(string folderPath, FileSystemRights right)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            return HasAccess(directoryInfo, right);
        }

        public static bool HasAccessFile(string filePath, FileSystemRights right)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return HasAccess(fileInfo, right);
        }

        public static bool HasAccess(DirectoryInfo directory, FileSystemRights right)
        {
            // Get the collection of authorization rules that apply to the directory.
            AuthorizationRuleCollection acl = directory.GetAccessControl()
                .GetAccessRules(true, true, typeof(SecurityIdentifier));
            return HasFileOrDirectoryAccess(right, acl);
        }

        public static bool HasAccess(FileInfo file, FileSystemRights right)
        {
            // Get the collection of authorization rules that apply to the file.
            AuthorizationRuleCollection acl = file.GetAccessControl()
                .GetAccessRules(true, true, typeof(SecurityIdentifier));
            return HasFileOrDirectoryAccess(right, acl);
        }

        /// <summary>
        /// Comprueba si se dispone o no permiso en un fichero o directorio.
        /// </summary>
        /// <param name="right">Permiso a comprobar</param>
        /// <param name="acl">Lista de reglas de autorización</param>
        /// <returns></returns>
        private static bool HasFileOrDirectoryAccess(FileSystemRights right, AuthorizationRuleCollection acl)
        {
            WindowsIdentity _currentUser = WindowsIdentity.GetCurrent();
            WindowsPrincipal _currentPrincipal = new WindowsPrincipal(_currentUser);
            bool allow = false;
            bool inheritedAllow = false;
            bool inheritedDeny = false;

            for (int i = 0; i < acl.Count; i++)
            {
                FileSystemAccessRule currentRule = (FileSystemAccessRule)acl[i];

                if (_currentUser.User.Equals(currentRule.IdentityReference) ||
                    _currentPrincipal.IsInRole((SecurityIdentifier)currentRule.IdentityReference))
                {

                    if (currentRule.AccessControlType.Equals(AccessControlType.Deny))
                    {
                        if ((currentRule.FileSystemRights & right) == right)
                        {
                            if (currentRule.IsInherited)
                                inheritedDeny = true;
                            else
                                return false;
                        }
                    }
                    else if (currentRule.AccessControlType.Equals(AccessControlType.Allow))
                    {
                        if ((currentRule.FileSystemRights & right) == right)
                        {
                            if (currentRule.IsInherited)
                                inheritedAllow = true;
                            else
                                allow = true;
                        }
                    }
                }
            }

            if (allow)
                return true;

            return inheritedAllow && !inheritedDeny;
        }

        /// <summary>
        /// Devuelve true o false dependiendo de si la aplicación se esta ejecutando con permisos de administrador.
        /// </summary>
        /// <returns></returns>
        public static bool IsAdmin()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return isElevated;
        }

        /// <summary>
        /// Devuelve el nombre de usuario conectado.
        /// </summary>
        /// <returns></returns>
        public static string Name()
        {
            string name;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                name = identity.Name;
            }

            return name;
        }

        //public static bool HasFolderPermissions(string folderPath)
        //{
        //    WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
        //    var domainAndUser = currentUser.Name;
        //    DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
        //    DirectorySecurity dirAC = dirInfo.GetAccessControl(AccessControlSections.All);
        //    AuthorizationRuleCollection rules = dirAC.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier)); //typeof(System.Security.Principal.NTAccount)

        //    foreach (AuthorizationRule rule in rules)
        //    {
        //        if (currentUser.Groups.Contains(rule.IdentityReference))
        //        {
        //            if ((((FileSystemAccessRule)rule).FileSystemRights & FileSystemRights.WriteData) > 0)
        //            {
        //                if (((FileSystemAccessRule)rule).AccessControlType == AccessControlType.Allow)
        //                    return true;
        //            }
        //        }

        //        if (rule.IdentityReference.Value.Equals(domainAndUser, StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            if ((((FileSystemAccessRule)rule).FileSystemRights & FileSystemRights.WriteData) > 0)
        //            {
        //                if (((FileSystemAccessRule)rule).AccessControlType == AccessControlType.Allow)
        //                    return true;
        //            }
        //        }
        //    }

        //    return false;
        //}
    }
}
