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
    public partial class frmHabitacion : System.Web.UI.Page
    {
        IEnumerable<Models.Habitacion> habitaciones = new ObservableCollection<Models.Habitacion>();
        HabitacionManager habitacionManager = new HabitacionManager();

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
                    Models.Habitacion habitacionIngresada = new Models.Habitacion();
                    Models.Habitacion habitacion = new Models.Habitacion()
                    {
                        HOT_CODIGO = Convert.ToInt32(txtHOT_CODIGO.Text),
                        HAB_NUMERO = Convert.ToInt32(txtHAB_NUMERO.Text),
                        HAB_CAPACIDAD = Convert.ToInt32(txtHAB_CAPACIDAD.Text),
                        HAB_TIPO = txtTipo.Text,
                        HAB_DESCRIPCION = txt_HAB_DESCRIPCION.Text,
                        HAB_ESTADO = txtEstado.Text,
                        HAB_PRECIO = Convert.ToDecimal(txtPrecio.Text)
                    };

                    habitacionIngresada = await habitacionManager.Ingresar(habitacion, Session["TokenUsuario"].ToString());

                    if (habitacionIngresada != null)
                    {
                        lblResultado.Text = "Habitacion ingresado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear habitacion";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar la habitacion. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                habitaciones = await habitacionManager.ObtenerHabitaciones(Session["TokenUsuario"].ToString());
                gvHabitaciones.DataSource = habitaciones.ToList();
                gvHabitaciones.DataBind();
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
            if (string.IsNullOrEmpty(txtHOT_CODIGO.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo de Hotel";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtHAB_NUMERO.Text))
            {
                lblResultado.Text = "Debe ingresar el numero de Habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtHAB_CAPACIDAD.Text))
            {
                lblResultado.Text = "Debe ingresar la capacidad de habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtTipo.Text))
            {
                lblResultado.Text = "Debe ingresar el tipo de habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txt_HAB_DESCRIPCION.Text))
            {
                lblResultado.Text = "Debe ingresar la descripcion de habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtEstado.Text))
            {
                lblResultado.Text = "Debe ingresar el estado de habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                lblResultado.Text = "Debe ingresar el precio de habitacion";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void gvHabitaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo Habitacion";
                e.Row.Cells[1].Text = "Codigo Hotel";
                e.Row.Cells[2].Text = "Numero Habitacion";
                e.Row.Cells[3].Text = "Capacidad Habitacion";
                e.Row.Cells[4].Text = "Tipo";
                e.Row.Cells[5].Text = "Descripcion";
                e.Row.Cells[6].Text = "Estado";
                e.Row.Cells[7].Text = "Precio";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Habitacion habitacionModificada = new Models.Habitacion();
                Models.Habitacion habitacion = new Models.Habitacion()
                {
                    HAB_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    HOT_CODIGO = Convert.ToInt32(txtHOT_CODIGO.Text),
                    HAB_NUMERO = Convert.ToInt32(txtHAB_NUMERO.Text),
                    HAB_CAPACIDAD = Convert.ToInt32(txtHAB_CAPACIDAD.Text),
                    HAB_TIPO = txtTipo.Text,
                    HAB_DESCRIPCION = txt_HAB_DESCRIPCION.Text,
                    HAB_ESTADO = txtEstado.Text,
                    HAB_PRECIO = Convert.ToDecimal(txtPrecio.Text)
                };

                habitacionModificada = await habitacionManager.Actualizar(habitacion, Session["TokenUsuario"].ToString());

                if (habitacionModificada != null)
                {
                    lblResultado.Text = "Habitacion actualizada correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar habitacion";
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
                codigoEliminado = await habitacionManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Habitacion eliminado con exito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar la habitacion";
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