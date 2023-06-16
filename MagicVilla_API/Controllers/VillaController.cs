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

        [HttpGet("id:int", Name ="GetVilla")]//endpoint que devuelve un objeto que coincida con el parámetro id que se le especifique
        
        //para evitar que los codigos de estados aparezcan como no documentado
        [ProducesResponseType(StatusCodes.Status200OK)]//documenta el estado 200
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//documenta el estado 400
        [ProducesResponseType(StatusCodes.Status404NotFound)]//documenta el estado 404
        public ActionResult<VillaDto> GetVilla(int id)
        {

            if(id == 0) 
            { 
                return BadRequest();//regresa un bad request 400
            }
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if(villa == null)
            {
                return NotFound();//regresa un estado 404
            }

            return Ok(villa);//regresa un estado 200

        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto) 
        { 

            if(! ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            //validar que no se dupliquen dos registros
            if(VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe!");
                                          //nombre validacion //mensaje que se muestra
                return BadRequest(ModelState);
            }

            
            if(villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);

            return CreatedAtRoute("GetVilla", new {id=villaDto.Id}, villaDto);
            //se llama la ruta GetVilla, se le emvía como parámetro el Id y se especifica el modelo villaDto 
        }


        [HttpDelete("{id:int}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault( v => v.Id == id );
            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.villaList.Remove(villa);

            return NoContent();//siempre se debe retornar NoContent() cuando se trabaja con delete
        }

    }
}
