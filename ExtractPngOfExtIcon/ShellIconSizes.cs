using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionIconExtractor
{
    public enum SHIL_IconSizes
    {
        SHIL_LARGE = 0x0,
        SHIL_SMALL = 0x1,
        SHIL_EXTRALARGE = 0x2,
        SHIL_SYSSMALL = 0x3,
        SHIL_JUMBO = 0x4,
        SHIL_LAST = 0x4,
    }

    /// <summary>
    /// More user friend version of <see cref="SHIL_IconSizes"/>, note this does not contain the "last" option, which would point to Jumbo.
    /// </summary>
    public enum IconSizes
    {
        /// <summary>
        /// SHIL_LARGE, 32x32 
        /// </summary>
        Large = 0x0,
        /// <summary>
        /// SHIL_SMALL, 16x16
        /// </summary>
        Small = 0x1,
        /// <summary>
        /// SHIL_EXTRALARGE, 48x48
        /// </summary>
        ExtraLarge = 0x2,
        /// <summary>
        /// SHIL_SYSSMALL meaning system small, same size as small
        /// </summary>
        SystemSmall = 0x3,
        /// <summary>
        /// SHIL_JUMBO, 256x256
        /// </summary>
        Jumbo = 0x4,
    }
}
