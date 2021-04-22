<%@ Page Async="true" Title="Mantenimiento de Reserva" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReserva.aspx.cs" Inherits="AppReservasULACIT.frmReserva" %>
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
        #overlay {
        width: auto;
        height: auto;
        position: static;
        background-image:none;
        }
        #textOverlay {
            position: static;
            font-size: 10px;
            color: #333;
            transform: translate(0%);
            -ms-transform: translate(0%);
        }
        #Label2 {
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Container">
        <br/>
 <h1><asp:Label ID="Label1" runat="server" Text="Mantenimiento de Reserva"></asp:Label></h1>
        <asp:GridView Width="100%" ID="gvReservas" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvReservas_RowDataBound">
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
                    <asp:Label ID="Label2" runat="server" Text="Codigo de Reserva"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodigo" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label3" runat="server" Text="Codigo de Usuario"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUSU_CODIGO" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label4" runat="server" Text="Codigo de Habitacion"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHAB_CODIGO" runat="server" TextMode="Number"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label5" runat="server" Text="Fecha de Ingreso"></asp:Label>
                </td>
                <td>
                    <asp:Calendar ID="clRES_FECHA_INGRESO" runat="server"></asp:Calendar>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Tipo" runat="server" Text="Fecha de salida"></asp:Label>
                </td>
                <td>
                    <asp:Calendar ID="clRES_FECHA_SALIDA" runat="server"></asp:Calendar>
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
