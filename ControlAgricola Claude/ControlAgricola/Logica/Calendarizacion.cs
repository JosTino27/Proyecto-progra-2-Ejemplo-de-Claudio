using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlAgricola.Datos;
using ControlAgricola.Objetos;

namespace ControlAgricola.Logica
{
    // Proceso para calcular el calendario de aplicaciones de un cultivo
    // fecha de la aplicación = fecha de siembra del lote + días después de siembra
    internal class Calendarizacion
    {
        // Revisa que el cultivo tenga lotes y paquetes activos, devuelve el error o ""
        public static string Validar(int idCultivo)
        {
            string error = "";

            List<Lote> lotes = DatosLotes.ListarActivosPorCultivo(idCultivo);
            if (lotes.Count == 0)
            {
                error = "El cultivo no tiene lotes activos. ";
            }

            List<PaqueteDetalle> detalles = DatosPaquetes.ListarDetallesActivosPorCultivo(idCultivo);
            if (detalles.Count == 0)
            {
                error = error + "El cultivo no tiene paquetes de aplicaciones activos.";
            }

            return error;
        }

        // Borra el calendario anterior del cultivo y lo vuelve a calcular
        // Devuelve cuántas aplicaciones se programaron
        public static int Calcular(int idCultivo)
        {
            List<Lote> lotes = DatosLotes.ListarActivosPorCultivo(idCultivo);
            List<PaqueteDetalle> detalles = DatosPaquetes.ListarDetallesActivosPorCultivo(idCultivo);

            DatosCalendario.BorrarPorCultivo(idCultivo);

            int id = DatosCalendario.SiguienteId();
            int cantidad = 0;

            // Por cada lote se programan todas las aplicaciones de los paquetes
            foreach (Lote lote in lotes)
            {
                foreach (PaqueteDetalle detalle in detalles)
                {
                    Calendario cal = new Calendario();
                    cal.id = id;
                    cal.idLote = lote.id;
                    cal.idPaquete = detalle.idPaquete;
                    cal.idAplicacion = detalle.idAplicacion;
                    cal.diasDespuesSiembra = detalle.diasDespuesSiembra;
                    cal.fecha = lote.fechaSiembra.AddDays(detalle.diasDespuesSiembra);

                    if (DatosCalendario.Guardar(cal))
                    {
                        id++;
                        cantidad++;
                    }
                }
            }

            return cantidad;
        }
    }
}
