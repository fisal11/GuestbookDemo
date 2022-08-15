using AutoMapper;
using GB.DataAccess.DataContext;
using GB.DataAccess.Entities;
using GB.Services.Model;
using GB.Services.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Guestbook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private UnitOfWork _unitOfWork;
        public AdminController(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _mapper = mapper;
        }

        [HttpGet("GetMessage")]
        public IActionResult GetMessage()
        {

            var data = _unitOfWork.MessageRepo.Get();
            if (data == null)
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


        [HttpPost("ReplyOnMessage")]
        public IActionResult ReplyOnMessage([FromBody] string message)
        {
            try
            {
                var Meessage = new Message();
                var data = _unitOfWork.MessageRepo.Get().Where(a =>a.UserId == Meessage.UserId);
                    Meessage.Meessage = message;
                    Meessage.MeessageDate = DateTime.Now;
                    _context.Add(Meessage);
                    _context.SaveChanges();
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Ok();
        }


        [HttpPut("UpdateMessage/{id}")]
        public IActionResult UpdateReply(int id, [FromBody] MessageModel messageModel)
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
        public IActionResult DeleteReply([FromRoute] int id)
        {
            var deleteItme = _unitOfWork.MessageRepo.GetById(id);
            _context.Remove(deleteItme);
            _context.SaveChangesAsync();
            return Ok();
        }



    }
}
