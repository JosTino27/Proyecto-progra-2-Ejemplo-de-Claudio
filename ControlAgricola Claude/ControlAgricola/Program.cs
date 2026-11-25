using System;
using System.Windows.Forms;
using ControlAgricola.Presentacion;

namespace ControlAgricola
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// La aplicación inicia en la ventana de Login.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
