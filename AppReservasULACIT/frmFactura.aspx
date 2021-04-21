<%@ Page  Async="true" Title="Mantenimiento Factura" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmFactura.aspx.cs" Inherits="AppReservasULACIT.frmFactura" UnobtrusiveValidationMode="None"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            width: 156px;
        }
        .auto-style2 {
            width: 156px;
            height: 31px;
        }
        .auto-style3 {
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Container">
        <br/>
 <h1><asp:Label ID="Label1" runat="server" Text="Mantenimiento de Factura"></asp:Label></h1>
        <asp:GridView Width="100%" ID="gvFacturas" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvFacturas_RowDataBound">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <table style="width: 100%;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label2" runat="server" Text="Codigo"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodigo" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>

              <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label5" runat="server" Text="Fecha de renta"></asp:Label>
                </td>
                <td>
                    <asp:Calendar ID="clFechRenta" runat="server"></asp:Calendar>
                </td>
                <td>&nbsp;</td>
            </tr>

              <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label6" runat="server" Text="Fecha de devolución"></asp:Label>
                </td>
               
                <td>
                    <asp:Calendar ID="clFechDevo" runat="server"></asp:Calendar>
                  </td>
            </tr>

           

            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label3" runat="server" Text="Monto total"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMontoTotal" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                 <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label7" runat="server" Text="Detalle Factura"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDetalle" runat="server" ></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label4" runat="server" Text="Código Empleado"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoEmple" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Código Sucursal</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtCodigoSuc" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style1">Código Usuario</td>
                <td>
                    <asp:TextBox ID="txtCodigoUsua" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Código Orden</td>
                <td>
                    <asp:TextBox ID="txtCodigoOrd" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn bg-success" OnClick="btnModificar_Click" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click"/>
        <br />
        <asp:Label ID="lblResultado" runat="server" Text="Resultado" Visible="false"></asp:Label>

   </div>
</asp:Content>
