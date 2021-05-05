using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FSForum
{
    public class Variables
    {
        /// <summary>
        ///     Variables de usuario
        /// </summary>
        public static VariablesForum Forum
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Session != null)
                    {
                        if (HttpContext.Current.Session["Forum"] == null)
                        {
                            VariablesForum varUsr = new VariablesForum();
                            HttpContext.Current.Session["Forum"] = varUsr;
                        }

                        return ((VariablesForum)HttpContext.Current.Session["Forum"]);
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            set { HttpContext.Current.Session["Forum"] = value; }
        }
    }
}
