using System;
using System.Collections.Generic;
using ControlAgricola.Objetos;
using ControlAgricola.Datos;

namespace ControlAgricola.Logica
{
    //--Proceso de calendarización: calcula en qué fecha le toca cada aplicación a cada lote
    internal class LogicaCalendario
    {
        /*
         * ¿Cómo se calcula? (igual que en el Excel "Proyección de Aplicaciones")
         *
         *   Fecha programada = Fecha de siembra del lote + Días después de siembra
         *
         * Para el cultivo seleccionado:
         *   1. Se buscan los lotes ACTIVOS que tienen sembrado ese cultivo.
         *   2. Se buscan las líneas de detalle de los paquetes ACTIVOS de ese cultivo
         *      (cada línea dice: qué aplicación y cuántos días después de siembra).
         *   3. Por cada lote y por cada línea se crea un objeto Calendario con su fecha.
         */

        //--Calcula la lista de aplicaciones programadas de un cultivo (todavía NO la guarda).
        //--Devuelve null si hubo un error leyendo la base de datos
        public static List<Calendario> Calcular(int idCultivo)
        {
            List<Lote> lotes = DatosLotes.ListarActivosPorCultivo(idCultivo);
            List<DetallePaquete> detalles = DatosPaquetes.ListarDetallesActivosPorCultivo(idCultivo);

            if (lotes == null || detalles == null)
            {
                return null;
            }

            List<Calendario> lista = new List<Calendario>();

            foreach (Lote lote in lotes)
            {
                foreach (DetallePaquete detalle in detalles)
                {
                    Calendario cal = new Calendario();
                    cal.idLote = lote.id;
                    cal.idPaquete = detalle.idPaquete;
                    cal.idAplicacion = detalle.idAplicacion;
                    cal.fecha = lote.fechaSiembra.AddDays(detalle.diasSiembra);

                    lista.Add(cal);
                }
            }

            return lista;
        }

        //--Guarda en la base de datos el calendario calculado de un cultivo.
        //--Primero borra el calendario anterior de ese cultivo para no duplicar.
        //--Devuelve la cantidad de aplicaciones guardadas, o -1 si hubo error
        public static int Guardar(int idCultivo, List<Calendario> lista)
        {
            if (!DatosCalendario.BorrarPorCultivo(idCultivo))
            {
                return -1;
            }

            int guardados = 0;

            foreach (Calendario cal in lista)
            {
                if (DatosCalendario.Guardar(cal))
                {
                    guardados++;
                }
            }

            return guardados;
        }
    }
}
