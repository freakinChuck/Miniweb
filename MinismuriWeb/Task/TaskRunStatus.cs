using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinismuriWeb
{
    /// <summary>
    /// der Status des TaskRuns
    /// </summary>
    public enum TaskRunStatus
    {
        /// <summary>
        /// erfolgreich
        /// </summary>
        Successful,
        /// <summary>
        /// blockiert
        /// </summary>
        Suspended,
        /// <summary>
        /// fehlgeschlagen
        /// </summary>
        Failed,
    }
}
