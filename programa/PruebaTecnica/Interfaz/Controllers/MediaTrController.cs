using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Interfaz.Controllers
{
    public class MediaTrController : Controller
    {
        private IMediator mediador;
        protected IMediator _mediador => mediador ?? (mediador = HttpContext.RequestServices.GetService<IMediator>());
    }
}
