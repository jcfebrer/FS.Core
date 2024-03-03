#region

using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using FSLibraryCore;
using FSException;

#endregion

namespace FSFormControlsCore
{
    public class PageSetup
    {
        public static PageSettings PageSettings { get; set; } = new PageSettings();

        public static void Setup()
        {
            try
            {
                var psDlg = new PageSetupDialog();

                psDlg.PageSettings = PageSettings;
                psDlg.ShowDialog();

                PageSettings = psDlg.PageSettings;
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil("An error occurred.", ex);
            }
        }
    }
}