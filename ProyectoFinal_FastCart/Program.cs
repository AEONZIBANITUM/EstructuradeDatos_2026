using ProyectoFinal_FastCart.Services;
using ProyectoFinal_FastCart.Structures;

//
// ============================================================
// FASTCART BACKEND CORE
// PUNTO DE ENTRADA ÚNICO - FASE 4
// ============================================================
//

AuditoriaService auditoria =
    new AuditoriaService();

InventarioLista inventario =
    new InventarioLista(
        auditoria);

ColaDespacho cola =
    new ColaDespacho();

PilaDevoluciones pila =
    new PilaDevoluciones();

MenuMaestro menu =
    new MenuMaestro(
        auditoria,
        inventario,
        cola,
        pila);

menu.Ejecutar();