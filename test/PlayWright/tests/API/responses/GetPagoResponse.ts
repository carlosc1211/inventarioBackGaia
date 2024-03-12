import { test, expect } from '@playwright/test';
export class GetArticuloResponse {
  static expectedStructure =
      {
        "codigoArticuloId": {
          "value": expect.any(String)
        },
        "codigoProveedor": expect.any(String),
        "activo": expect.any(Boolean),
        "tipoArticulo": expect.any(String),
        "bloqueado": expect.any(Boolean),
        "fechaCreacion": expect.any(String),
        "fechaModificacion": expect.any(String),
        "codigoAlmacen": expect.any(String),
        "unidadesCaja": expect.any(Number),
        "unidadesRetractil": expect.any(Number),
        "servirRetractil": expect.any(Boolean),
        "categorizacion": {
          "seccion": expect.any(String),
          "categoria": expect.any(String),
          "subcategoria": expect.any(String),
          "segmento": expect.any(String)
        },
        "marca": {
          "nombre": expect.any(String),
          "descripcion": expect.any(String)
        },
        "coleccion": {
          "nombre": expect.any(String),
          "descripcion": expect.any(String)
        },
        "dimensiones": {
          "tamanyo": expect.any(Number),
          "valorUdMedida": expect.any(Number),
          "udMedidaRef": expect.any(String)
        },
        "caracteristicasWeb": {
          "color": {
            "color": expect.any(String)
          },
          "peso": {
            "cantidad": expect.any(Number),
            "unidadMedida": expect.any(String)
          }
        },
        "eanList": [
          {
            "codigo": expect.any(String),
            "fechaModificacion": expect.any(String),
            "estaActivo": expect.any(Boolean),
            "tipoEan": expect.any(Number),
            "codigoArticuloId": {
              "value": expect.any(String)
            }
          }
        ],
        "configuracionRegiones": [
          {
            "region": {
              "codigo": expect.any(String),
              "descripcion": expect.any(String),
              "canales": [
                {
                  "tipo": expect.any(Number)
                }
              ]
            },
            "vat": {
              "tipoVat": expect.any(Number),
              "descripcion": expect.any(String)
            },
            "porcentajeVat": expect.any(Number),
            "precio": {
              "pvr": expect.any(Number),
              "pvp": expect.any(Number),
              "precioFlash": expect.any(Number),
              "precioOferta": expect.any(Number),
              "mostrarPvr": expect.any(Boolean),
              "moneda": expect.any(String),
              "ultimaActualizacionPrecio": expect.any(String)
            },
            "estaEnWeb": expect.any(Boolean),
            "nombreWeb": expect.any(String),
            "disponibilidad": {
              "limiteDiarioVenta": expect.any(Number),
              "limiteDiarioPorCompra": expect.any(Number)
            },
            "estadoWeb": expect.any(Number)
          },
          {
            "region": {
              "codigo": expect.any(String),
              "descripcion": expect.any(String),
              "canales": [
                {
                  "tipo": expect.any(Number)
                }
              ]
            },
            "vat": {
              "tipoVat": expect.any(Number),
              "descripcion": expect.any(String)
            },
            "porcentajeVat": expect.any(Number),
            "precio": {
              "pvr": expect.any(Number),
              "pvp": expect.any(Number),
              "precioFlash": expect.any(Number),
              "precioOferta": expect.any(Number),
              "mostrarPvr": expect.any(Boolean),
              "moneda": expect.any(String),
              "ultimaActualizacionPrecio": expect.any(String)
            },
            "estaEnWeb": expect.any(Boolean),
            "nombreWeb": expect.any(String),
            "disponibilidad": {
              "limiteDiarioVenta": expect.any(Number),
              "limiteDiarioPorCompra": expect.any(Number)
            },
            "estadoWeb": expect.any(Number)
          }
        ],
        "codigoArticuloPadre": null,
        "articuloPadre": null,
        "articulosHijos": [],
        "planificadoresPrecios": []        
      }
    ;
}