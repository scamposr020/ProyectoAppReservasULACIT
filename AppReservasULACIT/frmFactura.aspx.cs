using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasULACIT.Models;
using AppReservasULACIT.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace AppReservasULACIT
{
    public partial class frmFactura : System.Web.UI.Page
    {
        IEnumerable<Models.Factura> facturas = new ObservableCollection<Models.Factura>();
        FacturaManager facturaManager = new FacturaManager();

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
                    Models.Factura facturaIngresada = new Models.Factura();
                    Models.Factura factura = new Models.Factura()
                    {
                        FACT_FEC_RENT = clFechRenta.SelectedDate,
                        FACT_FEC_DEVOLU = clFechDevo.SelectedDate,
                        FACT_MONTO_TOT = Convert.ToInt32(txtMontoTotal.Text),
                        EMP_CODIGO = Convert.ToInt32(txtCodigoEmple.Text),
                        SUC_CODIGO = Convert.ToInt32(txtCodigoSuc.Text),
                        USU_CODIGO = Convert.ToInt32(txtCodigoUsua.Text),
                        ORD_CODIGO = Convert.ToInt32(txtCodigoOrd.Text)
                    };

                   facturaIngresada = await facturaManager.Ingresar(factura, Session["TokenUsuario"].ToString());

                    if (facturaIngresada != null)
                    {
                        lblResultado.Text = "Factura ingresada correctamente";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Error al crear factura";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Hubo un error al ingresar la factura. Detalle: " + ex.Message;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;


            }
        }

        async private void InicializarControles()
        {
            try
            {
                facturas = await facturaManager.ObtenerFacturas(Session["TokenUsuario"].ToString());
                gvFacturas.DataSource = facturas.ToList();
                gvFacturas.DataBind();
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
            if (string.IsNullOrEmpty(txtMontoTotal.Text))
            {
                lblResultado.Text = "Debe ingresar el monto total";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCodigoEmple.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del empleado";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCodigoSuc.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del sucursal";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtCodigoUsua.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo del usuario";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtCodigoOrd.Text))
            {
                lblResultado.Text = "Debe ingresar el codigo de la órden";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                return false;
            }
            return true;
        }

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Código";
                e.Row.Cells[1].Text = "Fecha de renta";
                e.Row.Cells[2].Text = "Fecha de devolución";
                e.Row.Cells[3].Text = "Fecha monto total";
                e.Row.Cells[4].Text = "Código Empleado";
                e.Row.Cells[5].Text = "Código Sucursal";
                e.Row.Cells[6].Text = "Código Usuario";
                e.Row.Cells[7].Text = "Código Orden";
            }
        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar() && (!string.IsNullOrEmpty(txtCodigo.Text)))
            {
                Models.Factura facturaModificado = new Models.Factura();
                Models.Factura factura = new Models.Factura()
                {
                    FACT_CODIGO = Convert.ToInt32(txtCodigo.Text),
                    FACT_FEC_DEVOLU = clFechDevo.SelectedDate,
                    FACT_FEC_RENT = clFechRenta.SelectedDate,
                    FACT_MONTO_TOT = Convert.ToInt32(txtMontoTotal.Text),
                    EMP_CODIGO = Convert.ToInt32(txtCodigoEmple.Text),
                    SUC_CODIGO = Convert.ToInt32(txtCodigoSuc.Text),
                    USU_CODIGO = Convert.ToInt32(txtCodigoUsua.Text),
                    ORD_CODIGO = Convert.ToInt32(txtCodigoOrd.Text)
                };

                facturaModificado = await facturaManager.Actualizar(factura, Session["TokenUsuario"].ToString());

                if (facturaModificado != null)
                {
                    lblResultado.Text = "Hotel actualizado correctamente";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblResultado.Text = "Error al actualizar hotel";
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
                codigoEliminado = await facturaManager.Eliminar(txtCodigo.Text, Session["TokenUsuario"].ToString());
                if (!string.IsNullOrEmpty(codigoEliminado))
                {
                    InicializarControles();
                    lblResultado.Text = "Factura eliminada con éxito";
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Hubo un error al eliminar la factura";
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