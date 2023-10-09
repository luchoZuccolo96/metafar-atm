# metafar-atm
Acceso a swagger desde: https://localhost:7068/swagger/index.html

## Tarjetas de acceso
- tarjeta: 4540730046267543 - pin: 1234
- tarjeta: 4540730046267472 - pin: 5678
- tarjeta: 4540730046260000 - pin: 0000 (sin datos - para poder probar el bloqueo del pin).

## Diagramas de Entidad Relación
<table>
  <tr>
    <th> Auth </th>
    <th> Tarjeta </th>
    <th> Historial </th>
  </tr>
  <tr>
    <td> CardNumber (PK) </td>
    <td> CardNumber (PK) </td>
    <td> Id (PK) </td>
  </tr>
  <tr>
    <td> Pin </td>
    <td> NombreUsuario </td>
    <td> CardNumber (SK) </td>
  </tr>
  <tr>
    <td> Attempts </td>
    <td> NumeroCuenta </td>
    <td> MontoRetirado </td>
  </tr>
    <tr>
    <td> Blocked </td>
    <td> SaldoActual </td>
    <td> SaldoRestante </td>
  </tr>
  <tr>
    <td>  </td>
    <td> UltimaExtraccion </td>
    <td> Fecha </td>
  </tr>
</table>
