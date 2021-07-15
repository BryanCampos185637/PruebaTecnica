using LogicaAccesoDatos.LogicaEstudiante;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LogicaAccesoDatos.LogicaSexo;
using System.Collections.Generic;
using Modelo;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Interfaz.Controllers
{
    public class EstudianteController : MediaTrController
    {
        public async Task<IActionResult> Index()
        {
            return View( await _mediador.Send(new ListarEstudiantes.Ejecuta()));
        }
        public async Task<IActionResult> Guardar()
        {
            return View(await ObtenerListaSexo(new GuardarEstudiante.Ejecuta()));
        }
        private async Task<GuardarEstudiante.Ejecuta>ObtenerListaSexo(GuardarEstudiante.Ejecuta ejecuta)
        {
            ejecuta.ListaSexo = await _mediador.Send(new ObtenerSexos.Ejecuta());
            return ejecuta;
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(GuardarEstudiante.Ejecuta ejecuta)
        {
            if (!ModelState.IsValid)
            {
                return View(await ObtenerListaSexo(ejecuta));
            }
            else
            {
                #region obtenemos lo que esta en la cookie
                var cookieString = HttpContext.Session.GetString("detalleEstudiante");
                if (cookieString != null)
                {
                    //quitaremos el serializado json
                    ejecuta.ListaDetalle = JsonConvert.DeserializeObject<List<Detalle>>(cookieString);
                }
                #endregion
                string rpt = await _mediador.Send(ejecuta);
                if (rpt == "ok")
                {
                    TempData["mensaje"] = "Se guardo correctamente";
                    //removemos la cookie 
                    HttpContext.Session.Remove("detalleEstudiante");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["mensaje"] = rpt;
                    return View(await ObtenerListaSexo(ejecuta));
                }
            }
        }
        [HttpGet]
        public JsonResult mostrarDetalles()
        {
            List<Detalle> lst = new List<Detalle>();
            #region verificacion para saber si la cookie ya existe
            var cookieString = HttpContext.Session.GetString("detalleEstudiante");
            if (cookieString != null)
            {
                //quitaremos el serializado json
                lst = JsonConvert.DeserializeObject<List<Detalle>>(cookieString);
            }
            #endregion
            return Json(lst);
        }

        /// <summary>
        /// accion para crear una cookie que contenga la lista de detalles 
        /// </summary>
        /// <param name="objDetalle"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AgregarDetalleTemporal(Detalle objDetalle)
        {
            List<Detalle> lst = new List<Detalle>();//variable donde guardaremos la lista
            try
            {
                #region verificacion para saber si la cookie ya existe
                var cookieString = HttpContext.Session.GetString("detalleEstudiante");
                if (cookieString != null)
                {
                    //quitaremos el serializado json
                    lst = JsonConvert.DeserializeObject<List<Detalle>>(cookieString);
                }
                #endregion

                #region ahora agregaremos a la lista lo que venga de la vista
                lst.Add(objDetalle);


                #region serializamos de nuevo y guardamos en la cookie
                cookieString = JsonConvert.SerializeObject(lst);//serializa
                HttpContext.Session.SetString("detalleEstudiante", cookieString);//almacenamos en la cookie
                #endregion

                #endregion


                //si todo esta bien retornamos un ok
                return Json("ok");
            }
            catch (System.Exception)
            {
                //si algo sale mal retornamos un error
                return Json("error");
            }
        }
        [HttpGet]
        public async Task<JsonResult> MostrarDetalle(int id)
        {
            var lst = await _mediador.Send(new ObtenerDetalles.Ejecuta { idEstudiante = id });
            return Json(lst);
        }
    }
}
