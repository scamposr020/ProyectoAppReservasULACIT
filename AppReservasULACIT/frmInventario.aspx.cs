﻿using AppReservasULACIT.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservasULACIT
{
    public partial class frmInventario : System.Web.UI.Page
    {
        IEnumerable<Models.Inventario> inventrios = new ObservableCollection<Models.Inventario>();
        InventarioManager inventarioManager = new InventarioManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInsertar())
                {
                    Models.Inventario inventarioIngresado = new Models.Inventario();
                    Models.Inventario inventario = new Models.Inventario()
                    {
                        INV_CANT_TOTAL = Convert.ToInt32(txtCantTotal.Text),
                        INV_CANT_DISPONIBLE = Convert.ToInt32(txtCantDisponible.Text),
                        INV_CANT_RENTADOS = Convert.ToInt32(txtCantRentados.Text),
                        SUC_CODIGO = Convert.ToInt32(txtSucursal.Text),
                        VEH_CODIGO = Convert.ToInt32(txtCodVehiculo.Text)


                    };

                    inventarioIngresado = await inventarioManager.Ingresar(inventario, Session["TokenUsuario"].ToString());

                    if (inventarioIngresado != null)
                    {
                        lblResultado.Text = "Inventario ingresado correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear inventario";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar el inventario. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                inventrios = await inventarioManager.ObtenerInventarios(Session["TokenUsuario"].ToString());
                gvInventarios.DataSource = inventrios.ToList();
                gvInventarios.DataBind();
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

            if (string.IsNullOrEmpty(txtCantTotal.Text))
            {
                lblResultado.Text = "Debe ingresar la cantidad total";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCantDisponible.Text))
            {
                lblResultado.Text = "Debe ingresar la cantidad disponible";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCantRentados.Text))
            {
                lblResultado.Text = "Debe ingresar la cantidad de rentados";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtSucursal.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo de la sucursal";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCodVehiculo.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del vehiculo";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void gvInventarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Codigo";
                e.Row.Cells[1].Text = "Cantidad Total";
                e.Row.Cells[2].Text = "Cantiadad Disponible";
                e.Row.Cells[3].Text = "Cantidad Rentados";
                e.Row.Cells[4].Text = "Sucursal";
                e.Row.Cells[5].Text = "Codigo del Vehiculo";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Inventario inventarioModificado = new Models.Inventario();
                Models.Inventario inventario = new Models.Inventario()
                {
                    INV_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    INV_CANT_TOTAL = Convert.ToInt32(txtCantTotal.Text),
                    INV_CANT_DISPONIBLE = Convert.ToInt32(txtCantDisponible.Text),
                    INV_CANT_RENTADOS = Convert.ToInt32(txtCantRentados.Text),
                    SUC_CODIGO = Convert.ToInt32(txtSucursal.Text),
                    VEH_CODIGO = Convert.ToInt32(txtCodVehiculo.Text)
                };

                inventarioModificado = await inventarioManager.Actualizar(inventario, Session["TokenUsuario"].ToString());

                if (inventarioModificado != null)
                {
                    lblResultado.Text = "Invetario actualizado correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar inventario";
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
                codigoEliminado = await inventarioManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Inventario eliminado con exito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar el inventario";
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