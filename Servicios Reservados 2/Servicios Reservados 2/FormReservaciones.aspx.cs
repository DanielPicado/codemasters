﻿using Servicios_Reservados_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
namespace Servicios_Reservados_2
{
    public partial class FormReservaciones : System.Web.UI.Page
    {
        private static ControladoraReservaciones controladora = new ControladoraReservaciones();
        public static String[] ids;

        private static Boolean seConsulto = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cargarDatos();  



            }


            // ponerModo();
        }
        /**
        * Requiere: N/A.
        * Efectua: rellena los campos de la interfaz con los datos de las reservaciones.
        * retorna: N/A
        */
        void cargarDatos()
        {
            llenarComboboxReservaciones();
            llenarGridReservaciones();
            
        }
        void llenarComboboxReservaciones()
        {
            DataTable anfitriones = controladora.solicitarAnfitriones();
            DataTable estacion = controladora.solicitarEstaciones();
            cbxEstacion.Items.Clear();
            cbxAnfitriona.Items.Clear();
            cbxEstacion.Items.Add("Seleccionar");
            cbxAnfitriona.Items.Add("Seleccionar");
            if (anfitriones.Rows.Count > 0)
            {
                foreach (DataRow fila in anfitriones.Rows)
                {
                    cbxAnfitriona.Items.Add(fila[0].ToString());
                }
            }
            if (estacion.Rows.Count > 0)
            {
                foreach (DataRow fila2 in estacion.Rows)
                {
                    cbxEstacion.Items.Add(fila2[0].ToString());
                }
            }
        }
        void llenarGridReservaciones()
        {
            DataTable tabla = crearTablaReservaciones();

            try
            {

                Object[] datos = new Object[6];
                DataTable reservaciones = controladora.solicitarTodasReservaciones();// se consultan todos
                ids = new String[reservaciones.Rows.Count]; //crear el vector para ids en el grid

                int i = 0;
                if (reservaciones.Rows.Count > 0)
                {
                    foreach (DataRow fila in reservaciones.Rows)
                    {
                        ids[i] = fila[0].ToString();// guardar el id para su posterior consulta
                        datos[0] = fila[1].ToString();//obtener los datos a mostrar
                        datos[1] = fila[2].ToString();
                        datos[2] = fila[3].ToString();
                        datos[3] = fila[4].ToString();
                        datos[4] = fila[5].ToString();
                        datos[5] = fila[6].ToString();
                        tabla.Rows.Add(datos);// cargar en la tabla los datos de cada proveedor
                        i++;
                    }
                }


                GridViewReservaciones.DataBind();
                //Debug.WriteLine("hola");
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo cargar las reservaciones");
            }


        }

        /**
       * Requiere: n/a
       * Efectua: Crea la DataTable para desplegar.
       * retorna:  un dato del tipo DataTable con la estructura para consultar.
       */
        protected DataTable crearTablaReservaciones()//consultar
        {
            DataTable tabla = new DataTable();
            DataColumn columna;

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Anfitriona";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Estación";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Número";
            tabla.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Solicitante";
            tabla.Columns.Add(columna);
            
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.DateTime");
            columna.ColumnName = "Entra";
            tabla.Columns.Add(columna);
            
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.DateTime");
            columna.ColumnName = "Sale";
            tabla.Columns.Add(columna);
            GridViewReservaciones.DataSource = tabla;
            GridViewReservaciones.DataBind();

            return tabla;
        }
        /**
        * Requiere: haber cargado el grid
        * Efectua: cambia la reservacion selecionada.
        * retorna:  nada.
        */
        protected void seleccionarReservacion(object sender, EventArgs e)
        {
            controladora.seleccionarReservacion(ids[GridViewReservaciones.SelectedIndex]);
        }
         
        protected void clickBuscar(object sender, EventArgs e)
        {
            String anfitriona = "vacio";
            String estacion = "vacio";
            String solicitante = "vacio";
            Debug.WriteLine(cbxEstacion.SelectedIndex);

            if (cbxAnfitriona.SelectedIndex != 0)
        {
                anfitriona = cbxAnfitriona.Value.ToString();
          }
          if (cbxEstacion.SelectedIndex != 0)
          {
                Debug.WriteLine("entre al metodo de la estacion" + estacion);
                estacion = cbxEstacion.Value.ToString();
          }
            if (txtSolicitante.Value != null)
            {
              solicitante = txtSolicitante.Value.ToString();
          }
            if (anfitriona.CompareTo("vacio") != 1 || estacion.CompareTo("vacio") != 1 || solicitante.CompareTo("vacio") != 1)
          {
              DataTable tabla = crearTablaReservaciones();
                Debug.WriteLine("entre al metodo " + estacion);
              try
              {

                  Object[] datos = new Object[6];
                  DataTable reservaciones = controladora.consultarReservaciones(anfitriona, estacion, solicitante);// se consultan todos
                  ids = new String[reservaciones.Rows.Count]; //crear el vector para ids en el grid
                    Debug.WriteLine("CANTIDAD " + reservaciones.Rows.Count);
                  int i = 0;
                  if (reservaciones.Rows.Count > 0)
                  {
                      foreach (DataRow fila in reservaciones.Rows)
                      {
                          ids[i] = fila[0].ToString();// guardar el id para su posterior consulta
                          datos[0] = fila[1].ToString();//obtener los datos a mostrar
                          datos[1] = fila[2].ToString();
                          datos[2] = fila[3].ToString();
                          datos[3] = fila[4].ToString();
                          datos[4] = fila[5].ToString();
                          datos[5] = fila[6].ToString();
                            Debug.WriteLine("dato " + datos[0]);
                          tabla.Rows.Add(datos);// cargar en la tabla los datos de cada proveedor
                          i++;
                      }
                  }

                    //GridViewReservaciones.DataSource = tabla;
                  GridViewReservaciones.DataBind();

              }
              catch (Exception s)
              {
                  Debug.WriteLine("No se pudo cargar las reservaciones");
              }
          }
            else
            {
              llenarGridReservaciones();
          }
        }

    }
}