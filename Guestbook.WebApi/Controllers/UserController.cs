using AutoMapper;
using GB.DataAccess.DataContext;
using GB.DataAccess.Entities;
using GB.Services.Model;
using GB.Services.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Guestbook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private UnitOfWork _unitOfWork;
        public UserController(IMapper mapper, ApplicationDbContext context) 
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _mapper = mapper;
         }
        [HttpGet("GetMessage")]
        public IActionResult GetMessage()
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = _unitOfWork.MessageRepo.Get().Where(a => a.UserId == userId).ToList();
            if (data == null )
            {
                return BadRequest();
            }
            return Ok(data);
        }
      
        [HttpGet("GetMessagebyId/{id}")]
        public IActionResult GetMessagebyId([FromRoute] int id)
        {

            var data = _unitOfWork.MessageRepo.GetById(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpPost("AddMessage")]
        public IActionResult AddMessage([FromBody] MessageModel messageModel)
        {

            var data = _mapper.Map<Message>(messageModel);
            if (ModelState.IsValid)
            {
                try
                {
                    data.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    data.MeessageDate = DateTime.Now;
                    data.Meessage = messageModel.Meessage;
                    _unitOfWork.MessageRepo.Add(data);
                    _unitOfWork.Save();
                    return Ok();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            if (data == null)
            {
                return BadRequest();
            }
          
            return Ok(messageModel);

        }
        
        
        [HttpPut("UpdateMessage/{id}")]
        public IActionResult UpdateMessage(int id, [FromBody] MessageModel messageModel)
        {
            var data = _mapper.Map<Message>(messageModel);
            if (id != data.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                data.Meessage = messageModel.Meessage;
                data.MeessageDate = DateTime.Now;
                _unitOfWork.MessageRepo.Edit(data);
                return Ok();
            }
            return Ok(messageModel);
        }

        
        [HttpDelete("DeleteMessage/{id}")]
        public IActionResult DeleteMessage([FromRoute] int id)
        {
            var deleteItme = _unitOfWork.MessageRepo.GetById(id);
            _context.Remove(deleteItme);
            _context.SaveChangesAsync();
            return Ok();
        }

        
        [HttpPost("ReplyOnMessage")]
        public IActionResult ReplyOnMessage([FromBody] string message)
        {
            try
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var check = _context.Messages.Where(a => a.UserId == UserId).ToList();
                if (check.Count < 1)
                {
                    var Meessage = new Message();
                    Meessage.UserId = UserId;
                    Meessage.Meessage = message;
                    Meessage.MeessageDate = DateTime.Now;
                    _context.Add(Meessage);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Ok();
        }
  
    }
}
