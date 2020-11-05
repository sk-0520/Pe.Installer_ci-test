using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pe.Installer
{
    public static class Constants
    {
        #region proeprty

        /// <summary>
        /// アップデートに用いる情報の記載されたファイルの URI。
        /// </summary>
        public static Uri UpdateFileUri { get; } =
#if DEBUG
             new Uri("https://bitbucket.org/sk_0520/pe-ci_test/downloads/update.json")
#else
             new Uri("https://bitbucket.org/sk_0520/pe/downloads/update.json")
#endif
        ;

        #endregion
    }
}
