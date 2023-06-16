using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Datos;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            
            return Ok(VillaStore.villaList);
        
        }

        [HttpGet("id")]//endpoint que devuelve un objeto que coincida con el parámetro id que se le especifique
        public ActionResult<VillaDto> GetVilla(int id)
        {

            if(id == 0) 
            { 
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if(villa == null)
            {
                return NotFound();//regresa un estado 404
            }

            return Ok(villa);

        }
    }
}
