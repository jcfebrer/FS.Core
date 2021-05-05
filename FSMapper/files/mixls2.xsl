<?xml version="1.0" encoding="utf-16"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" encoding="UTF-8" indent="yes" />
  <xsl:template match="/">
    <DatosServicios>
      <Servicios>
        <CodigoEmpresa></CodigoEmpresa>
        <NombreEmpresa></NombreEmpresa>
        <CodigoServicio></CodigoServicio>
        <NumeroAlbaran></NumeroAlbaran>
        <FechaAlbaran></FechaAlbaran>
        <CodigoArmador></CodigoArmador>
        <NombreArmador></NombreArmador>
        <CodigoLinea>
          <xsl:value-of select="CARGAS/CABECERA_AGRUPACION/CABECERA_ENTREGA/DESTINATARIO" />
        </CodigoLinea>
        <NombreLinea>
          <xsl:value-of select="CARGAS/CABECERA_AGRUPACION/CABECERA_ENTREGA/DIRECCION" />-<xsl:value-of select="CARGAS/CABECERA_AGRUPACION/CABECERA_ENTREGA/POBLACION" /></NombreLinea>
        <CodigoImo></CodigoImo>
        <DescripcionImo xml:space="preserve"> </DescripcionImo>
        <IdentificacionImo xml:space="preserve"> <xsl:value-of select="CARGAS/CABECERA_AGRUPACION/CABECERA_ENTREGA/PROVINCIA" /></IdentificacionImo>
        <CodigoMercancia></CodigoMercancia>
        <DescripcionMercancia></DescripcionMercancia>
        <Peso></Peso>
        <PesoFacturar></PesoFacturar>
        <FechaCarga></FechaCarga>
        <Booking></Booking>
        <CodigoTerminalCarga></CodigoTerminalCarga>
        <NombreTerminalCarga></NombreTerminalCarga>
        <ObservacionesCarga />
        <CodigoTerminalEstadoCarga></CodigoTerminalEstadoCarga>
        <DesEstadoTerminalCarga></DesEstadoTerminalCarga>
        <CodigoTerminalCategoriaCarga></CodigoTerminalCategoriaCarga>
        <DesCategoriaTerminalCarga></DesCategoriaTerminalCarga>
        <PedidoMercancia />
        <CodigoDestino></CodigoDestino>
        <PoblacionDestino></PoblacionDestino>
        <CodigoPais></CodigoPais>
        <DescripcionPais xml:space="preserve"> </DescripcionPais>
        <CodigoTerminalDescarga></CodigoTerminalDescarga>
        <NombreTerminalDescarga></NombreTerminalDescarga>
        <CodigoBarco></CodigoBarco>
        <NombreBarco xml:space="preserve"> </NombreBarco>
        <NumeroBarco />
        <FechaSalida />
        <CodigoPuerto></CodigoPuerto>
        <NombrePuerto xml:space="preserve"> </NombrePuerto>
        <ObservacionesDescarga />
        <CodigoTerminalEstadoDescarga></CodigoTerminalEstadoDescarga>
        <DesEstadoTerminalDescarga></DesEstadoTerminalDescarga>
        <CodTerminalCategoriaDescarga></CodTerminalCategoriaDescarga>
        <DesCategoriaTerminalDescarga></DesCategoriaTerminalDescarga>
        <Temperatura />
        <ConexionFrio></ConexionFrio>
        <ContenedorDañado></ContenedorDañado>
        <CodigoCliente></CodigoCliente>
        <NombreCliente></NombreCliente>
        <CodigoTipoViaje></CodigoTipoViaje>
        <DescripcionTipoViaje></DescripcionTipoViaje>
        <CodigoTipoServicio></CodigoTipoServicio>
        <DescripcionTipoServicio></DescripcionTipoServicio>
        <CodigoAgente></CodigoAgente>
        <NombreAgente></NombreAgente>
        <ObservacionesServicio />
        <ReferenciaServicio></ReferenciaServicio>
        <SobreDimAlto></SobreDimAlto>
        <SobreDimDerecha></SobreDimDerecha>
        <SobreDimIzquierda></SobreDimIzquierda>
        <SobreDimDelante></SobreDimDelante>
        <SobreDimLargo></SobreDimLargo>
        <KilometrosCoste></KilometrosCoste>
        <ImporteCoste></ImporteCoste>
        <PorcentajeIvaCoste></PorcentajeIvaCoste>
        <PrecioCosteTonelada></PrecioCosteTonelada>
        <CosteBaseImponible></CosteBaseImponible>
        <BaseBafCoste></BaseBafCoste>
        <PorcentajeBafCoste></PorcentajeBafCoste>
      </Servicios>
      <Contenedores>
        <CodigoServicio></CodigoServicio>
        <CodigoTipoContenedor></CodigoTipoContenedor>
        <DescripcionTipoContenedor></DescripcionTipoContenedor>
        <NumeroContenedor></NumeroContenedor>
        <Precinto1></Precinto1>
        <Precinto2 />
      </Contenedores>
      <Fabricas>
        <CodigoServicio></CodigoServicio>
        <CodigoFabrica></CodigoFabrica>
        <RazonSocial></RazonSocial>
        <Direccion></Direccion>
        <Poblacion></Poblacion>
        <Cif />
      </Fabricas>
    </DatosServicios>
  </xsl:template>
</xsl:stylesheet>