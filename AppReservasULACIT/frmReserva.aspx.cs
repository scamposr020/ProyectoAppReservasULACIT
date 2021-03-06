using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Models;
using AppReservasULACIT.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AppReservasULACIT
{
    public partial class frmReserva : System.Web.UI.Page
    {
        IEnumerable<Models.Reserva> reservas = new ObservableCollection<Models.Reserva>();
        ReservaManager reservarManager = new ReservaManager();

        async protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Models.Reserva reservaIngresada = new Models.Reserva();
                    Models.Reserva reserva = new Models.Reserva()
                    {
                        USU_CODIGO = Convert.ToInt32(txtUSU_CODIGO.Text),
                        HAB_CODIGO = Convert.ToInt32(txtHAB_CODIGO.Text),
                        RES_FECHA_INGRESO = clRES_FECHA_INGRESO.SelectedDate,
                        RES_FECHA_SALIDA = clRES_FECHA_SALIDA.SelectedDate
                    };

                    reservaIngresada = await reservarManager.Ingresar(reserva, Session["TokenUsuario"].ToString());

                    if (reservaIngresada != null)
                    {
                        lblResultado.Text = "Reserva ingresada correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear reserva";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar la reserva. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                reservas = await reservarManager.ObtenerReservas(Session["TokenUsuario"].ToString());
                gvReservas.DataSource = reservas.ToList();
                gvReservas.DataBind();
            }
            catch (Exception e)
            {
                lblResultado.Text = "Hubo un error al inicializar controles. Detalle: " + e.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;

            }
        }

        private bool ValidarInsertar()
        {
            if (string.IsNullOrEmpty(txtUSU_CODIGO.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo de usuario";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtHAB_CODIGO.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo de Habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (clRES_FECHA_INGRESO.SelectedDate > clRES_FECHA_SALIDA.SelectedDate)
            {
                lblResultado.Text = "Error. Fecha de ingreso es mayor a la Fecha de salida";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void gvReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo Reserva";
                e.Row.Cells[1].Text = "Codigo Usuario";
                e.Row.Cells[2].Text = "Codigo Habitacion";
                e.Row.Cells[3].Text = "Fecha de Ingreso";
                e.Row.Cells[4].Text = "Fecha de Salida";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Reserva reservaModificada = new Models.Reserva();
                Models.Reserva reserva = new Models.Reserva()
                {
                    RES_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    USU_CODIGO = Convert.ToInt32(txtUSU_CODIGO.Text),
                    HAB_CODIGO = Convert.ToInt32(txtHAB_CODIGO.Text),
                    RES_FECHA_INGRESO = clRES_FECHA_INGRESO.SelectedDate,
                    RES_FECHA_SALIDA = clRES_FECHA_SALIDA.SelectedDate
                };

                reservaModificada = await reservarManager.Actualizar(reserva, Session["TokenUsuario"].ToString());

                if (reservaModificada != null)
                {
                    lblResultado.Text = "Reserva actualizada correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar Reserva";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            else
            {
                lblResultado.Text = "Debe ingresar todos los datos";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }


        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                string codigoEliminado = string.Empty;
                codigoEliminado = await reservarManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Reserva eliminada con exito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar la reserva";
                    lblResultado.ForeColor = Color.Maroon;
                    lblResultado.Visible = true;
                }
            }
            else
            {
                lblResultado.Text = "Debe ingresar el código";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
            }

        }
    }
}