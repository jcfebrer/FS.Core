using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FSSecurity
{
    public class Permission
    {
        /// <summary>
        /// Tests the permission.
        /// </summary>
        /// <param name="Permission">The permission.</param>
        /// <returns></returns>
        public static string TestPermission(CodeAccessPermission Permission)
        {
            string sGranted = null;
            try
            {
                Permission.Demand();
                sGranted = "Yes" + "<br />";
            }
            catch (SecurityException ex)
            {
                sGranted = ex.Message + " - No" + "<br />";
            }

            return sGranted;
        }

        /// <summary>
        /// Permissionses this instance.
        /// </summary>
        /// <returns></returns>
        public static string Permissions()
        {
            var sb = new StringBuilder("");
            sb.Append("UIPermission: " + TestPermission(new UIPermission(PermissionState.Unrestricted)));
            sb.Append("EnvironmentPermission: " +
                      TestPermission(new EnvironmentPermission(PermissionState.Unrestricted)));
            sb.Append("FileDialogPermission: " +
                      TestPermission(new FileDialogPermission(PermissionState.Unrestricted)));
            sb.Append("FileIOPermission: " + TestPermission(new FileIOPermission(PermissionState.Unrestricted)));
            //sb.Append("OdbcPermission: " + TestPermission(new OdbcPermission(PermissionState.Unrestricted)));
            //sb.Append("OleDbPermission: " + TestPermission(new OleDbPermission(PermissionState.Unrestricted)));
            //sb.Append("SqlClientPermission: " + TestPermission(new SqlClientPermission(PermissionState.Unrestricted)));
            //sb.Append("PrintingPermission: " + TestPermission(new PrintingPermission(PermissionState.Unrestricted)));
            sb.Append("RegistryPermission: " + TestPermission(new RegistryPermission(PermissionState.Unrestricted)));
            sb.Append("SecurityPermission: " + TestPermission(new SecurityPermission(PermissionState.Unrestricted)));

            //sb.Append(new FSDatabase.BdUtils().Test());
            return sb.ToString();
        }


    }
}
