using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using SQLitePCL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private DataContext _context;

        public ChannelsController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException( nameof(context) );
        }
        public IActionResult Get()
        {
            var channels = _context.Channels.ToList();

            return Ok( channels );
        }

        [HttpGet("{id}")]
        public IActionResult Get( Guid id )
        {
            var channels = _context.Channels.FirstOrDefault( x => x.id == id );
            return Ok( channels );
        }

        
    }
}